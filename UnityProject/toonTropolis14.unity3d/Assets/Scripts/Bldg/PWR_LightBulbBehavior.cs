using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PWR_LightBulbBehavior : MonoBehaviour
{

	public bool lightOn;

	public float setIntensity;

	private bool prevLightOn;

	private GameObject lightBulbLight;

	private float lightTimer;

	private float sputterOnTime;

	private bool firstTime;

	public float BulbIntensityBaseValue;

	public float BulbSineCycleIntensity;

	public float BulbIntensityFlickerValue;

	public float BulbSineCycleIntensitySpeed;

	private object sputtering;

	private bool Awake;

	public void Start()
	{
		this.firstTime = true;
		this.lightOn = false;
		this.prevLightOn = false;
		this.sputtering = false;
		this.lightTimer = Time.time;
		this.Awake = true;
	}

	public IEnumerator ToggleSleepWake()
	{
		//return new PWR_LightBulbBehavior.ToggleSleepWake$71(this).GetEnumerator();
		yield return null;
	}

	public void SetLightState(bool on)
	{
		this.lightOn = on;
	}

	public bool GetLightState()
	{
		return this.lightOn;
	}

	public void SputterOn(object cycle)
	{
		this.lightBulbLight = GameObject.Find("lightBulbLight");
		if ((bool)cycle)
		{
			this.BulbSineCycleIntensity += UnityEngine.Random.Range(0.01f, (float)1) * this.BulbSineCycleIntensitySpeed;
			this.setIntensity = this.BulbIntensityBaseValue + (Mathf.Sin(this.BulbSineCycleIntensity * 0.0174532924f) * (this.BulbIntensityFlickerValue / 2f) + this.BulbIntensityFlickerValue / 2f);
		}
		else
		{
			this.setIntensity = (float)1;
		}
		this.lightBulbLight.GetComponent<Light>().intensity = this.setIntensity;
	}

	public void SputterOff()
	{
		this.lightBulbLight = GameObject.Find("lightBulbLight");
		this.lightBulbLight.GetComponent<Light>().intensity = (float)0;
	}

	public void Update()
	{
		if (this.firstTime)
		{
			this.firstTime = false;
			this.SputterOff();
		}
		else if (this.lightOn)
		{
			if (!this.prevLightOn && !(bool)this.sputtering)
			{
				this.sputtering = true;
				this.sputterOnTime = Time.time;
				this.CallAudio();
			}
			if (!this.prevLightOn && (Time.time - this.sputterOnTime) > 1.25f)
			{
				this.SputterOn(true);
			}
			else
			{
				this.lightOn = true;
				this.sputtering = false;
				this.prevLightOn = true;
				this.SputterOn(false);
			}
		}
		else if (this.prevLightOn)
		{
			this.prevLightOn = false;
			this.SputterOff();
		}
	}

	public void CallAudio()
	{
		//this.audio.Play();
	}

	public void Main()
	{
	}
}
