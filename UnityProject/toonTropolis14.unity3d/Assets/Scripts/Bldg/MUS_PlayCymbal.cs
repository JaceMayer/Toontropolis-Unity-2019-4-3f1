using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MUS_PlayCymbal : MonoBehaviour
{

	private float LastTime = 0;

	public IEnumerator PlayCymbal(object which)
	{
		//return new MUS_PlayCymbal.PlayCymbal$66(which, this).GetEnumerator();
		yield return false;
	}

	public void CharHitObj(string what){
		if(what == this.gameObject.name){
			Debug.Log("HIT CYMBAL");
			if(Time.time - LastTime < 1) return;
			LastTime = Time.time;
			AudioSource Aud = GetComponent<AudioSource>();
			Aud.Play();
		}
	}

	public void Main()
	{
	}
}
