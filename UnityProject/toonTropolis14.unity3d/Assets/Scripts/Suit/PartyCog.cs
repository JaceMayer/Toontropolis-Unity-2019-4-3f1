using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCog : RandomAnim
{
    public AnimationClip HonkHorn;
    public AnimationClip AnvilDropped;

    public bool IsPlaying(){
		if(GetComponent<Animation>().IsPlaying(animA.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animB.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animC.name)) return true;
		if(GetComponent<Animation>().IsPlaying(animD.name)) return true;
        if(GetComponent<Animation>().IsPlaying(HonkHorn.name)) return true;
		if(GetComponent<Animation>().IsPlaying(AnvilDropped.name)) return true;
		if(Fear != null && GetComponent<Animation>().IsPlaying(Fear.name)) return true;
		foreach (AnimationClip item in SleepAnims)
		{
			if(GetComponent<Animation>().IsPlaying(item.name)) return true;
		}
		return false;
	}

    public void TriggerFlat(){
        if(GetComponent<Animation>().IsPlaying(AnvilDropped.name)) return;
        Debug.Log("PartyCog::Dropping Anvil");
        GetComponent<Animation>().Play(AnvilDropped.name);
    }

    public void TriggerBounce(){
        if(GetComponent<Animation>().IsPlaying(HonkHorn.name)) return;
        Debug.Log("PartyCog::Honk Horn");
        GetComponent<Animation>().Play(HonkHorn.name);
    }

}
