using System;
using UnityEngine;

public enum BlendMode
{
	Copy,
	Multiply,
	MultiplyDouble,
	Add,
	AddSmoooth,
	Blend
}

[AddComponentMenu("")]
public class ImageEffects
{
	private static Material[] m_BlitMaterials = new Material[6];

	public static Material GetBlitMaterial(BlendMode mode)
	{
		if (ImageEffects.m_BlitMaterials[(int)mode] != null)
		{
			return ImageEffects.m_BlitMaterials[(int)mode];
		}
		ImageEffects.m_BlitMaterials[0] = new Material("Shader \"BlitCopy\" {\n\tSubShader { Pass {\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture}\t}}\nFallback Off }");
		ImageEffects.m_BlitMaterials[1] = new Material("Shader \"BlitMultiply\" {\n\tSubShader { Pass {\n\t\tBlend DstColor Zero\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture }\t}}\nFallback Off }");
		ImageEffects.m_BlitMaterials[2] = new Material("Shader \"BlitMultiplyDouble\" {\n\tSubShader { Pass {\n\t\tBlend DstColor SrcColor\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture }\t}}\nFallback Off }");
		ImageEffects.m_BlitMaterials[3] = new Material("Shader \"BlitAdd\" {\n\tSubShader { Pass {\n\t\tBlend One One\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture }\t}}\nFallback Off }");
		ImageEffects.m_BlitMaterials[4] = new Material("Shader \"BlitAddSmooth\" {\n\tSubShader { Pass {\n\t\tBlend OneMinusDstColor One\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture }\t}}\nFallback Off }");
		ImageEffects.m_BlitMaterials[5] = new Material("Shader \"BlitBlend\" {\n\tSubShader { Pass {\n\t\tBlend SrcAlpha OneMinusSrcAlpha\n \t\tZTest Always Cull Off ZWrite Off Fog { Mode Off }\n\t\tSetTexture [__RenderTex] { combine texture }\t}}\nFallback Off }");
		for (int i = 0; i < ImageEffects.m_BlitMaterials.Length; i++)
		{
			ImageEffects.m_BlitMaterials[i].hideFlags = (HideFlags)13;
			ImageEffects.m_BlitMaterials[i].shader.hideFlags = (HideFlags)13;
		}
		return ImageEffects.m_BlitMaterials[(int)mode];
	}

	public static void Blit(RenderTexture source, RenderTexture dest, BlendMode blendMode)
	{
		Rect rect = new Rect(0f, 0f, 1f, 1f);
		Rect arg_3B_1 = rect;
		Rect destRect = new Rect(0f, 0f, 1f, 1f);
		ImageEffects.Blit(source, arg_3B_1, dest, destRect, blendMode);
	}

	public static void Blit(RenderTexture source, RenderTexture dest)
	{
		ImageEffects.Blit(source, dest, BlendMode.Copy);
	}

	public static void Blit(RenderTexture source, Rect sourceRect, RenderTexture dest, Rect destRect, BlendMode blendMode)
	{
		RenderTexture.active = dest;
		source.SetGlobalShaderProperty("__RenderTex");
		GL.PushMatrix();
		GL.LoadOrtho();
		Material blitMaterial = ImageEffects.GetBlitMaterial(blendMode);
		for (int i = 0; i < blitMaterial.passCount; i++)
		{
			blitMaterial.SetPass(i);
			ImageEffects.DrawQuad();
		}
		GL.PopMatrix();
	}

	public static void BlitWithMaterial(Material material, RenderTexture source, RenderTexture destination)
	{
		RenderTexture.active = destination;
		material.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			ImageEffects.DrawQuad();
		}
		GL.PopMatrix();
	}

	public static void RenderDistortion(Material material, RenderTexture source, RenderTexture destination, float angle, Vector2 center, Vector2 radius)
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
		material.SetMatrix("_RotationMatrix", matrix4x);
		string arg_56_1 = "_CenterRadius";
		Vector4 vector = new Vector4(center.x, center.y, radius.x, radius.y);
		material.SetVector(arg_56_1, vector);
		material.SetFloat("_Angle", angle * 0.0174532924f);
		ImageEffects.BlitWithMaterial(material, source, destination);
	}

	public static void DrawQuad()
	{
		GL.Begin(7);
		GL.TexCoord2(0f, 0f);
		GL.Vertex3(0f, 0f, 0.1f);
		GL.TexCoord2(1f, 0f);
		GL.Vertex3(1f, 0f, 0.1f);
		GL.TexCoord2(1f, 1f);
		GL.Vertex3(1f, 1f, 0.1f);
		GL.TexCoord2(0f, 1f);
		GL.Vertex3(0f, 1f, 0.1f);
		GL.End();
	}

	public static void DrawGrid(int xSize, int ySize)
	{
		GL.Begin(7);
		float num = 1f / (float)xSize;
		float num2 = 1f / (float)ySize;
		for (int i = 0; i < xSize; i++)
		{
			for (int j = 0; j < ySize; j++)
			{
				GL.TexCoord2((float)(j + 0) * num, (float)(i + 0) * num2);
				GL.Vertex3((float)(j + 0) * num, (float)(i + 0) * num2, 0.1f);
				GL.TexCoord2((float)(j + 1) * num, (float)(i + 0) * num2);
				GL.Vertex3((float)(j + 1) * num, (float)(i + 0) * num2, 0.1f);
				GL.TexCoord2((float)(j + 1) * num, (float)(i + 1) * num2);
				GL.Vertex3((float)(j + 1) * num, (float)(i + 1) * num2, 0.1f);
				GL.TexCoord2((float)(j + 0) * num, (float)(i + 1) * num2);
				GL.Vertex3((float)(j + 0) * num, (float)(i + 1) * num2, 0.1f);
			}
		}
		GL.End();
	}
}
