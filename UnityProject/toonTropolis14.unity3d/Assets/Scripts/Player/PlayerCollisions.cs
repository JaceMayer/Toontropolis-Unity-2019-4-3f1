using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("ControlPrototype/Player Collisions")]
[Serializable]
public class PlayerCollisions : MonoBehaviour
{


	private bool pianoEvent;

	private bool powerEvent;

	private float pianoEventTimer;

	private GameObject currentKey;

	public Material night_material;

	public Material day_material;
	public GameObject Camera;

	public static string dayMode = "day";

	public PlayerCollisions()
	{
		this.pianoEvent = false;
		this.powerEvent = false;
		this.pianoEventTimer = 0f;
	}

	public void Update()
	{
	}

	public IEnumerator OnControllerColliderHit(ControllerColliderHit hit)
	{
		Debug.Log("Collider hit: " + hit.gameObject.name);
		yield return false;
		if(hit.gameObject.name == "pasted__polySurface40"){
			PowerEventStart();
			yield break;
		}
		else if(hit.gameObject.name == "HonkHornTrigger"){
			GameObject gameObject = GameObject.Find("tt_a_ara_ttc_gagShop");
			gameObject.GetComponent<GagShop>().ToggleHonk();
			yield break;
		}
		try{
			hit.gameObject.BroadcastMessage("CharHitObj", hit.gameObject.name);
		}
		catch{

		}
		//return new PlayerCollisions.OnControllerColliderHit(hit, this).GetEnumerator();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT TRIGGER: " + other);
		if(other.gameObject.name == "AnvilDropTrigger"){
			GameObject gameObject = GameObject.Find("tt_a_ara_ttc_gagShop");
			gameObject.GetComponent<GagShop>().ToggleAnvilDrop();
			return;
		}
    }

	public void PianoPlay()
	{
		CameraController.distance = 8f;
		CameraController.y = 30f;
		CameraController.cameraFollow = false;
		PlayerController.tdcWalkerMode = true;
		MonoBehaviour.print("start piano event");
	}

	public void PianoStop()
	{
		CameraController.distance = 4f;
		CameraController.y = 15f * (float)-1;
		CameraController.cameraFollow = true;
		PlayerController.tdcWalkerMode = false;
		MonoBehaviour.print("stop piano event");
	}

	public void GagShopEventStart()
	{
	}

	public void PowerEventStart()
	{
		Debug.Log("start power event");
		//this.BuildingAnimStart();
		if (PlayerCollisions.dayMode == "night")
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = Color.white;
			RenderSettings.fogDensity = 0.005f;
			RenderSettings.ambientLight =Color.gray;
			RenderSettings.skybox = this.day_material;
			PlayerCollisions.dayMode = "day";
			//toggleVisibility.dayMode = "day";
			this.ToggleVisibility(false);
			Camera.GetComponent<MusicMgr>().ChangeToDay();
		}
		else
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = Color.blue;
			RenderSettings.fogDensity = 0.005f;
			RenderSettings.ambientLight = Color.blue;
			RenderSettings.skybox = this.night_material;
			PlayerCollisions.dayMode = "night";
			//toggleVisibility.dayMode = "night";
			this.ToggleVisibility(true);
			Camera.GetComponent<MusicMgr>().ChangeToNight();
		}
	}

	public void ToggleVisibility(bool mode)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("nights");
		GameObject gameObject = GameObject.Find("t2_m_ara_ttp_powerBulding_switch");
		int num = 180;
		int i = 0;
		GameObject[] array2 = array;
		int length = array2.Length;
		checked
		{
			while (i < length)
			{
				array2[i].GetComponent<Renderer>().enabled = mode;
				i++;
			}
			gameObject.transform.Rotate((float)0, (float)(0 + num), (float)0);
		}
		/*GameObject[] pwr = GameObject.FindGameObjectsWithTag("power");
		foreach (GameObject GO in pwr)
		{
			GO.SetLightState(mode);
		}*/
		GameObject[] bldgs = GameObject.FindGameObjectsWithTag("Building");
		foreach (GameObject GO in bldgs)
		{
			GO.GetComponent<RandomAnim>().ToggleSleep();
		}
	}

	public void BuildingAnimStart()
	{
		GameObject gameObject = GameObject.Find("sceneLight");
		GameObject gameObject2 = GameObject.Find("PowerBuilding");
		GameObject gameObject3 = GameObject.Find("PetShop");
		GameObject gameObject4 = GameObject.Find("ClothesShop");
		GameObject gameObject5 = GameObject.Find("CogPinata");
		GameObject gameObject6 = GameObject.Find("GagShop");
		GameObject gameObject7 = GameObject.Find("MusicShop");
		if (gameObject2)
		{
			/*PWR_PowerBuildingControl pWR_PowerBuildingControl = (PWR_PowerBuildingControl)gameObject2.GetComponentInChildren(typeof(PWR_PowerBuildingControl));
			bool flag = RuntimeServices.UnboxBoolean(RuntimeServices.GetProperty(gameObject2.GetComponentInChildren(typeof(PWR_PowerBuildingControl)), "Awake"));
			if (!pWR_PowerBuildingControl)
			{
				MonoBehaviour.print("no randomAnimScript");
				flag = pWR_PowerBuildingControl.IsAwake();
			}*/
			gameObject2.BroadcastMessage("ToggleSleepWake");
			gameObject3.BroadcastMessage("ToggleSleepWake");
			gameObject4.BroadcastMessage("ToggleSleepWake");
			gameObject6.BroadcastMessage("ToggleSleepWake");
			gameObject5.BroadcastMessage("ToggleSleepWake");
			gameObject7.BroadcastMessage("ToggleSleepWake");
		}
	}

	public void Main()
	{
	}
}
