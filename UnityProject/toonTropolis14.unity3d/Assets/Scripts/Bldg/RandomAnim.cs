
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class RandomAnim : MonoBehaviour
{

	public bool canAnimate;

	public int choice;

	public int timer;

	public AnimationClip animA;

	public AnimationClip animB;

	public AnimationClip animC;

	public AnimationClip animD;

	public AnimationClip Fear = null;
	public AnimationClip Fall = null;
	public AnimationClip[] SleepAnims;
	public bool IsFear = false;
	public bool IsFall = false;
	public bool IsSleeping = false;

	public RandomAnim()
	{
		this.canAnimate = true;
		this.choice = 0;
		this.timer = 0;
	}

	public bool IsPlaying(){
		if(GetComponent<Animation>().IsPlaying(animA.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animB.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animC.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animD.name)) return true;
		if(Fear != null && GetComponent<Animation>().IsPlaying(Fear.name)) return true;
		foreach (AnimationClip item in SleepAnims)
		{
			if(GetComponent<Animation>().IsPlaying(item.name)) return true;
		}
		return false;
	}

	public void ToggleFear(){
		IsFear = Fear != null && !IsFear;
		GetComponent<Animation>().Play(Fear.name);
	}

	public void ToggleSleep(){
		IsSleeping = !IsSleeping;
		if(IsSleeping && SleepAnims.Length != 0) GetComponent<Animation>().Play(SleepAnims[0].name);
		if(!IsSleeping) GetComponent<Animation>().Play(animA.name);
	}

	public void ToggleFall(){
		IsFall = Fall != null && !IsFall;
		GetComponent<Animation>().Play(Fall.name);
	}

	private void Update() {
		if(!IsPlaying() && !IsFear && !IsFall && !IsSleeping){
			int Anim = UnityEngine.Random.Range(0,3);
			Debug.Log("Playing Bldg anim");
			if(Anim == 0) GetComponent<Animation>().Play(animA.name);
			if(Anim == 1) GetComponent<Animation>().Play(animB.name);
			if(Anim == 2) GetComponent<Animation>().Play(animC.name);
			if(Anim == 3) GetComponent<Animation>().Play(animD.name);
		}
		else if(!IsPlaying() && IsFear){
			GetComponent<Animation>().Play(Fear.name);
		}	
		else if(!IsPlaying() && IsSleeping && SleepAnims.Length != 0){
			int Anim = UnityEngine.Random.Range(0, SleepAnims.Length);
			GetComponent<Animation>().Play(SleepAnims[Anim].name);
		}	
	}
}
