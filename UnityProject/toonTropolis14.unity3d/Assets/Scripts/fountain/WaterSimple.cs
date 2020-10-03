using System;
using UnityEngine;

[ExecuteInEditMode]
public class WaterSimple : MonoBehaviour
{
	private void Update()
	{
		if (!GetComponent<Renderer>())
		{
			return;
		}
		Material sharedMaterial = GetComponent<Renderer>().sharedMaterial;
		if (!sharedMaterial)
		{
			return;
		}
		Vector4 vector = new Vector4(1,1,1,1);
		float @float = 1;
		float num = Time.time / 20f;
		Vector4 vector2 = vector * (num * @float);
		Vector4 vector3 = new Vector4(Mathf.Repeat(vector2.x, 1f), Mathf.Repeat(vector2.y, 1f), Mathf.Repeat(vector2.z, 1f), Mathf.Repeat(vector2.w, 1f));
		sharedMaterial.SetVector("_WaveOffset", vector3);
		Vector3 vector4 = new Vector3(1f / @float, 1f / @float, 1f);
		Vector3 vector5 = new Vector3(vector3.x, vector3.y, 0f);
		Matrix4x4 matrix4x = Matrix4x4.TRS(vector5, Quaternion.identity, vector4);
		sharedMaterial.SetMatrix("_WaveMatrix", matrix4x);
		Vector3 vector6 = new Vector3(vector3.z, vector3.w, 0f);
		matrix4x = Matrix4x4.TRS(vector6, Quaternion.identity, vector4 * 0.45f);
		sharedMaterial.SetMatrix("_WaveMatrix2", matrix4x);
	}
}
