
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class eyeballScript : MonoBehaviour
{

	public AnimationClip animBlink;

	public float BlinkSpaceTime;

	public bool secondEye;

	public GameObject lookTarget;

	public GameObject aimer;

	public eyeballScript()
	{
		this.BlinkSpaceTime = 5f;
		this.secondEye = false;
	}

	public void Blink()
	{

		GetComponent<Animation>().Play(this.animBlink.name);
		StartCoroutine(BlinkReset());
		//this.StartCoroutine_Auto(this.BlinkReset());
	}

	public IEnumerator BlinkReset()
	{
		//return new eyeballScript.BlinkReset$77(this).GetEnumerator();
		yield return new WaitForSeconds(BlinkSpaceTime);
		Blink();
	}

	public void Update()
	{
	}

	void Start()
	{
		Blink();
	}
}
