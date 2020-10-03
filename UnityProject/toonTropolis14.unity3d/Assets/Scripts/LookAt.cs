using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class LookAt : MonoBehaviour
{
	public Transform target;

	public void Update()
	{
		this.transform.LookAt(this.target);
	}

	public void Main()
	{
	}
}
