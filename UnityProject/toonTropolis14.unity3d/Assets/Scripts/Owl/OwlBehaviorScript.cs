using System;
using UnityEngine;

[Serializable]
public class OwlBehaviorScript : MonoBehaviour
{
	private bool canAnimate;

	private int choice;

	public AnimationClip animA;

	public AnimationClip animB;

	public AnimationClip animC;

	public AnimationClip animD;

	private bool first_time;

	private float startIdleTime;

	private float wait_for;

	public bool alwaysIdle;

	public float dead_time;

	public OwlBehaviorScript()
	{
		this.canAnimate = true;
		this.alwaysIdle = true;
		this.dead_time = (float)0;
	}

	public void Start()
	{
		this.first_time = true;
		this.canAnimate = true;
		this.wait_for = 10f;
		GetComponent<Animation>().CrossFade(this.animA.name);
		this.startIdleTime = Time.time;
	}

	public void Update()
	{
		if (this.first_time)
		{
			this.first_time = false;
			this.canAnimate = true;
		}
		else
		{
			if (this.canAnimate)
			{
				this.choice = UnityEngine.Random.Range(1, 6);
				int num = this.choice;
				if (num == 1)
				{
					GetComponent<Animation>().CrossFade(this.animA.name);
					this.wait_for = GetComponent<Animation>()[this.animA.name].length;
					this.startIdleTime = Time.time;
				}
				else if (num == 2)
				{
					GetComponent<Animation>().CrossFade(this.animB.name);
					this.wait_for = GetComponent<Animation>()[this.animB.name].clip.length;
					this.startIdleTime = Time.time;
				}
				else if (num == 3)
				{
					GetComponent<Animation>().CrossFade(this.animC.name);
					this.wait_for = GetComponent<Animation>()[this.animC.name].clip.length;
					this.startIdleTime = Time.time;
				}
				else if (num == 4)
				{
					GetComponent<Animation>().CrossFade(this.animD.name);
					this.wait_for = GetComponent<Animation>()[this.animD.name].clip.length;
					this.startIdleTime = Time.time;
				}
				else if (num == 5)
				{
					GetComponent<Animation>().CrossFade(this.animA.name);
					int num2 = UnityEngine.Random.Range(3, 10);
					this.wait_for = (float)num2 * GetComponent<Animation>()[this.animA.name].length;
					this.startIdleTime = Time.time;
				}
				this.wait_for += (float)UnityEngine.Random.Range(3, 11);
			}
			if (Time.time - this.startIdleTime >= this.wait_for)
			{
				this.canAnimate = true;
			}
			else
			{
				this.canAnimate = false;
			}
		}
	}

	public void Main()
	{
	}
}
