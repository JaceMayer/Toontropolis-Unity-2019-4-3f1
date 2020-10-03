using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class CollisionSoundEffect : MonoBehaviour
{
	public AudioClip audioClip;

	public float volumeModifier;

	public CollisionSoundEffect()
	{
		this.volumeModifier = 1f;
	}

	public void Main()
	{
	}
}
