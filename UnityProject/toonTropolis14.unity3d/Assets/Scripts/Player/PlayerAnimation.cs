using System.Collections;
using System;
using UnityEngine;

[AddComponentMenu("ControlPrototype/Player Animation")]
[Serializable]
public class PlayerAnimation : MonoBehaviour
{
	public AnimationClip animIdle;

	public AnimationClip animIdleTap;

	public AnimationClip animWalk;

	public AnimationClip animRun;

	public AnimationClip animJumpTakeoff;

	public AnimationClip animJumpCycle;

	public AnimationClip animJumpLand;

	private bool flagIdle;

	private float nextLoad;

	private float rate;

	public PlayerAnimation()
	{
		this.flagIdle = false;
		this.nextLoad = (float)0;
		this.rate = (float)6;
	}

	public void Start()
	{
		this.GetComponent<Animation>().wrapMode = (WrapMode)2;
		Debug.Log(this.GetComponent<Animation>()[animWalk.name]);
		this.GetComponent<Animation>()[animWalk.name].layer = -1;
		this.GetComponent<Animation>()[animRun.name].layer = -1;
		this.GetComponent<Animation>()[animIdle.name].layer = -1;
		this.GetComponent<Animation>()[animJumpCycle.name].layer = -1;
		this.GetComponent<Animation>().SyncLayer(-1);
		this.GetComponent<Animation>().Stop();
		this.GetComponent<Animation>().Play(animIdle.name);
	}

	public void Update()
	{
		CharacterController characterController = (CharacterController)this.transform.GetComponent(typeof(CharacterController));
		Vector3 velocity = characterController.velocity;
		velocity.y = (float)0;
		float magnitude = velocity.magnitude;
		if (magnitude > (float)5)
		{
			this.GetComponent<Animation>().CrossFade(animRun.name);
			this.GetComponent<MusicMgr>().Stop("AV_footstep_walkloop");
			this.GetComponent<MusicMgr>().Play("AV_footstep_runloop");
		}
		else if (magnitude > 0.1f)
		{
			this.GetComponent<Animation>().CrossFade(animWalk.name);
			this.GetComponent<MusicMgr>().Stop("AV_footstep_runloop");
			//this.GetComponent<MusicMgr>().Play("AV_footstep_walkloop");
		}
		else if (Input.GetButton("Jump"))
		{
			this.GetComponent<Animation>().CrossFade(animJumpCycle.name);
			this.GetComponent<MusicMgr>().Stop("AV_footstep_walkloop");
			this.GetComponent<MusicMgr>().Stop("AV_footstep_runloop");
		}
		else
		{
			this.GetComponent<Animation>().CrossFade(animIdle.name);
			this.GetComponent<MusicMgr>().Stop("AV_footstep_walkloop");
			this.GetComponent<MusicMgr>().Stop("AV_footstep_runloop");
		}
		if (Input.GetAxis("Horizontal") != 0f && Input.GetAxis("Vertical") == 0f)
		{
			this.GetComponent<Animation>().CrossFade(animWalk.name);
			this.GetComponent<MusicMgr>().Stop("AV_footstep_runloop");
			//this.GetComponent<MusicMgr>().Play("AV_footstep_walkloop");
		}
	}

	public void Main()
	{
	}
}
