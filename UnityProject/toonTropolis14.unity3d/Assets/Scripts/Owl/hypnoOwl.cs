using System;
using UnityEngine;

[Serializable]
public class hypnoOwl : MonoBehaviour
{
	public void Start()
	{
		GetComponent<Animation>().wrapMode = (WrapMode)2;
		GetComponent<Animation>()["hypnoEyes"].layer = 9;
		GetComponent<Animation>()["hypnoEyes"].blendMode = 0;
		GetComponent<Animation>().CrossFade("hypnoEyes");
	}

	public void Main()
	{
	}
}
