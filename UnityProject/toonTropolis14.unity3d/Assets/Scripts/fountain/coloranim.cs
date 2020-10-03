using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class coloranim : MonoBehaviour
{
	public Color colorStart;

	public Color colorEnd;

	public float duration;

	public coloranim()
	{
		this.colorStart = Color.red;
		this.colorEnd = Color.green;
		this.duration = 0.01f;
	}

	public void Update()
	{
		float num = Mathf.PingPong(Time.time, this.duration) / this.duration;
		this.GetComponent<Renderer>().material.color = Color.Lerp(this.colorStart, this.colorEnd, num);
	}

	public void Main()
	{
	}
}
