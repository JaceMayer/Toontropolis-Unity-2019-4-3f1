using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class triggerCogBuilding : MonoBehaviour
{


	public GameObject playerChar;

	public Transform launchPos;

	public GameObject cogBuilding;

	public AnimationClip buildingIdle;

	public AnimationClip animBuildingDamage;

	public GameObject cogA;

	public GameObject cogB;

	public GameObject cogC;

	public GameObject cogD;

	public GameObject cogE;

	public GameObject bombA;

	public GameObject bombB;

	public GameObject haze;

	public AudioClip bombSound;

	public AudioClip villianMusic;

	public static bool fear = false;

	public bool cogTakerover;

	public triggerCogBuilding()
	{
		this.cogTakerover = false;
	}

	public void Start()
	{
		this.GetComponent<Animation>().wrapMode = (UnityEngine.WrapMode)2;
		this.GetComponent<Animation>().Play(this.buildingIdle.name);
		if (!this.playerChar)
		{
			this.playerChar = GameObject.FindWithTag("Player");
		}
	}

	public IEnumerator OnTriggerEnter(Collider other)
	{
		//return new triggerCogBuilding.OnTriggerEnter(other, this).GetEnumerator();
		yield return false;
	}

	public void Main()
	{
	}
}
