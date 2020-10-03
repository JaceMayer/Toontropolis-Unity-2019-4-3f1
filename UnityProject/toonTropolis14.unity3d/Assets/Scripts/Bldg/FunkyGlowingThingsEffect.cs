using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Funky Glowing Things"), ExecuteInEditMode, RequireComponent(typeof(Camera))]
[Serializable]
public class FunkyGlowingThingsEffect : MonoBehaviour
{
	public int iterations;

	public float blurSpread;

	public Texture colorRamp;

	public Texture blurRamp;

	public Shader compositeShader;

	public Shader renderThingsShader;

	private static string blurMatString = "Shader \"BlurConeTap\" { SubShader { Pass { " + "ZTest Always Cull Off ZWrite Off Fog { Mode Off } " + "\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant alpha} " + "\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous} " + "\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous} " + "\tSetTexture [__RenderTex] {constantColor (0,0,0,0.25) combine texture * constant + previous} " + "} } Fallback off }";

	private Material m_Material;

	private Material m_CompositeMaterial;

	private RenderTexture renderTexture;

	private GameObject shaderCamera;

	public FunkyGlowingThingsEffect()
	{
		this.iterations = 5;
		this.blurSpread = 0.5f;
	}

	private Material GetMaterial()
	{
		if (this.m_Material == null)
		{
			this.m_Material = new Material(FunkyGlowingThingsEffect.blurMatString);
			this.m_Material.hideFlags = (HideFlags)13;
			this.m_Material.shader.hideFlags = (HideFlags)13;
		}
		return this.m_Material;
	}

	private Material GetCompositeMaterial()
	{
		if (this.m_CompositeMaterial == null)
		{
			this.m_CompositeMaterial = new Material(this.compositeShader);
			this.m_CompositeMaterial.hideFlags = (HideFlags)13;
		}
		return this.m_CompositeMaterial;
	}

	public void OnDisable()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material.shader);
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
		UnityEngine.Object.DestroyImmediate(this.m_CompositeMaterial);
		UnityEngine.Object.DestroyImmediate(this.shaderCamera);
		if (this.renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(this.renderTexture);
			this.renderTexture = null;
		}
	}

	public void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			this.enabled = false;
		}
		else if (!this.GetMaterial().shader.isSupported)
		{
			this.enabled = false;
		}
	}

	public void OnPreRender()
	{
		if (this.enabled && this.gameObject.active)
		{
			if (this.renderTexture != null)
			{
				RenderTexture.ReleaseTemporary(this.renderTexture);
				this.renderTexture = null;
			}
			this.renderTexture = checked(RenderTexture.GetTemporary((int)this.GetComponent<Camera>().pixelWidth, (int)this.GetComponent<Camera>().pixelHeight, 16));
			if (!this.shaderCamera)
			{
				this.shaderCamera = new GameObject("ShaderCamera", new Type[]
				{
					typeof(Camera)
				});
				this.shaderCamera.GetComponent<Camera>().enabled = false;
				this.shaderCamera.hideFlags = (HideFlags)13;
			}
			Camera camera = this.shaderCamera.GetComponent<Camera>();
			camera.CopyFrom(this.GetComponent<Camera>());
			camera.backgroundColor = new Color((float)0, (float)0, (float)0, (float)0);
			camera.clearFlags = (CameraClearFlags)2;
			camera.targetTexture = this.renderTexture;
			camera.RenderWithShader(this.renderThingsShader, "RenderType");
		}
	}

	private void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		RenderTexture.active = dest;
		source.SetGlobalShaderProperty("__RenderTex");
		float offsetX = (0.5f + (float)iteration * this.blurSpread) / (float)source.width;
		float offsetY = (0.5f + (float)iteration * this.blurSpread) / (float)source.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		Material material = this.GetMaterial();
		checked
		{
			for (int i = 0; i < material.passCount; i++)
			{
				material.SetPass(i);
				FunkyGlowingThingsEffect.Render4TapQuad(dest, offsetX, offsetY);
			}
			GL.PopMatrix();
		}
	}

	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		RenderTexture.active = dest;
		source.SetGlobalShaderProperty("__RenderTex");
		float offsetX = 1f / (float)source.width;
		float offsetY = 1f / (float)source.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		Material material = this.GetMaterial();
		checked
		{
			for (int i = 0; i < material.passCount; i++)
			{
				material.SetPass(i);
				FunkyGlowingThingsEffect.Render4TapQuad(dest, offsetX, offsetY);
			}
			GL.PopMatrix();
		}
	}

	//TODO
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(this.renderTexture, temporary);
		bool flag = true;
		checked
		{
			for (int i = 0; i < this.iterations; i++)
			{
				if (flag)
				{
					this.FourTapCone(temporary, temporary2, i);
				}
				else
				{
					this.FourTapCone(temporary2, temporary, i);
				}
				flag = !flag;
			}
			Material compositeMaterial = this.GetCompositeMaterial();
			compositeMaterial.SetTexture("_BlurTex", (!flag) ? temporary2 : temporary);
			compositeMaterial.SetTexture("_ColorRamp", this.colorRamp);
			compositeMaterial.SetTexture("_BlurRamp", this.blurRamp);
			ImageEffects.BlitWithMaterial(compositeMaterial, source, destination);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			if (this.renderTexture != null)
			{
				RenderTexture.ReleaseTemporary(this.renderTexture);
				this.renderTexture = null;
			}
		}
	}

	private static void Render4TapQuad(RenderTexture dest, float offsetX, float offsetY)
	{
		GL.Begin(7);
		Vector2 vector = Vector2.zero;
		if (dest != null)
		{
			vector = dest.GetTexelOffset() * 0.75f;
		}
		FunkyGlowingThingsEffect.Set4TexCoords(vector.x, vector.y, offsetX, offsetY);
		GL.Vertex3((float)0, (float)0, 0.1f);
		FunkyGlowingThingsEffect.Set4TexCoords(1f + vector.x, vector.y, offsetX, offsetY);
		GL.Vertex3((float)1, (float)0, 0.1f);
		FunkyGlowingThingsEffect.Set4TexCoords(1f + vector.x, 1f + vector.y, offsetX, offsetY);
		GL.Vertex3((float)1, (float)1, 0.1f);
		FunkyGlowingThingsEffect.Set4TexCoords(vector.x, 1f + vector.y, offsetX, offsetY);
		GL.Vertex3((float)0, (float)1, 0.1f);
		GL.End();
	}

	private static void Set4TexCoords(float x, float y, float offsetX, float offsetY)
	{
		GL.MultiTexCoord2(0, x - offsetX, y - offsetY);
		GL.MultiTexCoord2(1, x + offsetX, y - offsetY);
		GL.MultiTexCoord2(2, x + offsetX, y + offsetY);
		GL.MultiTexCoord2(3, x - offsetX, y + offsetY);
	}

	public void Main()
	{
	}
}
