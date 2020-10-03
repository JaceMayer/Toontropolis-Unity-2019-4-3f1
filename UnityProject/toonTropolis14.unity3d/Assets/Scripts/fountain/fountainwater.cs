using System;
using UnityEngine;

[Serializable]
public class fountainwater : MonoBehaviour
{
	public void Update()
	{
		float num = Mathf.Sin(Time.time * 0.05f);
		float num2 = Mathf.Sin(Time.time * 0.07f);
		this.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(num, num2));
	}

	public void Main()
	{
	}
}
