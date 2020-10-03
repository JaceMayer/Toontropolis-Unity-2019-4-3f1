using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GagShop : RandomAnim
{
    public AnimationClip Horn;
	public AnimationClip AnvilDrop;

    public bool IsPlaying(){
		if(GetComponent<Animation>().IsPlaying(animA.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animB.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animC.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animD.name)) return true;
        if(GetComponent<Animation>().IsPlaying(Horn.name)) return true;
		if(GetComponent<Animation>().IsPlaying(AnvilDrop.name)) return true;
		if(Fear != null && GetComponent<Animation>().IsPlaying(Fear.name)) return true;
		foreach (AnimationClip item in SleepAnims)
		{
			if(GetComponent<Animation>().IsPlaying(item.name)) return true;
		}
		return false;
	}

    public void ToggleAnvilDrop(){
        if(GetComponent<Animation>().IsPlaying(AnvilDrop.name)) return;
		GetComponent<Animation>().Play(AnvilDrop.name);
        AudioSource Audio = GetComponent<AudioSource>();
		GameObject gameObject = GameObject.Find("tt_a_ara_ttc_cogPinata");
        new Sequence.Sequence(this, "Anvil-seq", SequenceType.General,
            Intervals.Wait(0.1f), // All this just to wait 0.1 seconds -_-
            Intervals.Func(Audio.Play)
        ).Start();
		gameObject.GetComponent<PartyCog>().TriggerFlat();
	}

    public void ToggleHonk(){
        if(GetComponent<Animation>().IsPlaying(Horn.name)) return;
		GetComponent<Animation>().Play(Horn.name);
        AudioSource Audio = GetComponent<AudioSource>();
		GameObject gameObject = GameObject.Find("tt_a_ara_ttc_cogPinata");
        new Sequence.Sequence(this, "honk-seq", SequenceType.General,
            Intervals.Wait(0.1f), // All this just to wait 0.1 seconds -_-
            Intervals.Func(Audio.Play)
        ).Start();
		gameObject.GetComponent<PartyCog>().TriggerBounce();
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
