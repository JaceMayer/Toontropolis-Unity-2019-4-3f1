using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[Serializable]
public class aiCog : MonoBehaviour
{

	public bool enemyActive;

	public int hitpoints;

	public int runSpeed;

	public float rotateSpeed;

	public GameObject cameraMain;

	private CharacterController characterController;

	public int aiState;

	public GameObject playerChar;

	public GameObject grpWayPonints;

	public GameObject[] WayPointArray;

	public int wayPointInt;

	public GameObject activeWaypoint;

	private bool wayPointSwitch;

	public bool wayPointLoop;

	public bool wayPointReverseOrder;

	public float waypointWaitTime;

	public AnimationClip animWalk;

	public AnimationClip animIdle;

	public AnimationClip animIdleBattle;
	Vector3 velocity;

	public aiCog()
	{
		this.enemyActive = true;
		this.hitpoints = 100;
		this.runSpeed = 10;
		this.rotateSpeed = 0.7f;
		this.aiState = 1;
		this.wayPointInt = 0;
		this.wayPointSwitch = true;
		this.wayPointLoop = true;
		this.wayPointReverseOrder = false;
		this.waypointWaitTime = 0.1f;
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		float velocityReduction = Vector3.Dot(velocity, hit.normal);
		//velocity = velocity - velocityReduction * hit.normal;
	}

    public void SetChildrenActive (bool active) {
        SetChildrenActive (gameObject, active);
    }
 
    private void SetChildrenActive (GameObject obj, bool active) {
        // Look at all of the children of the object passed in
        for (int i=0; i < obj.transform.childCount; i++) {
            // Get the child object to modify
            GameObject childObj = obj.transform.GetChild(i).gameObject;
            // Set that object's active state.
            childObj.SetActive (active);
            // Call this function on that object to set its children.
            SetChildrenActive (childObj, active);
        }
    }

	public void Start()
	{
		this.activeWaypoint = this.WayPointArray[0];
		this.characterController = (CharacterController)this.GetComponent(typeof(CharacterController));
		if (!this.playerChar)
		{
			this.playerChar = GameObject.FindWithTag("Player");
		}
		if (!this.cameraMain)
		{
			this.cameraMain = GameObject.FindWithTag("MainCamera");
		}
		SetChildrenActive(true);
		this.animWalk.wrapMode = (WrapMode)2;
		this.animIdle.wrapMode = (WrapMode)2;
		//this.animIdleBattle.wrapMode = (WrapMode)2;
	}

	public void Update()
	{
		if (this.aiState == 2 && this.enemyActive)
		{
			if ((this.gameObject.transform.position - this.activeWaypoint.transform.position).magnitude >= (float)1)
			{
				this.MoveCharacter();
				this.GetComponent<Animation>().Play(this.animWalk.name);
			}
			else
			{
				this.aiState = 1;
			}
		}
		if (this.aiState == 1 && this.enemyActive)
		{
			this.Idle();
		}
	}

	public void Idle()
	{
		if (this.wayPointSwitch)
		{
			this.StartCoroutine(this.NextWaypoint());
		}
	}

	public void MoveCharacter()
	{
		this.transform.LookAt(this.activeWaypoint.transform);
		//Vector3 vector = this.transform.TransformDirection(Vector3.forward * (float)this.runSpeed);
		//this.characterController.SimpleMove(vector);
		//float step =  (float)this.runSpeed * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, this.activeWaypoint.transform.position, step);
		//this.characterController.SimpleMove(vector);
		// RotateTowardsPosition(this.activeWaypoint.transform.position, (float)this.rotateSpeed);

		Vector3 dir = this.activeWaypoint.transform.position - transform.position;
		// ignore any height difference:
		//dir.y = 0;
		// calculate velocity limited to the desired speed:
		velocity = Vector3.ClampMagnitude(dir * runSpeed, runSpeed);
		// move the character including gravity:
		GetComponent<CharacterController>().SimpleMove(velocity);
	}

	public float RotateTowardsPosition(Vector3 targetPos, float rotateSpeed)
	{
		Vector3 vector = this.transform.InverseTransformPoint(targetPos);
		float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		float num2 = rotateSpeed * Time.deltaTime;
		float num3 = Mathf.Clamp(num, num2 * (float)-1, num2);
		this.transform.Rotate((float)0, num3, (float)0);
		return num;
	}

	public IEnumerator NextWaypoint()
	{
		Debug.Log("WAY POINT");
		activeWaypoint = WayPointArray[wayPointInt];
		wayPointInt += 1;
		if(wayPointInt >= WayPointArray.Length) wayPointInt = 0;
		aiState = 2;
		yield return null;
	}

	public void stopEnemy()
	{
		this.enemyActive = false;
	}

	public void startEnemy()
	{
		this.enemyActive = true;
	}

	public void Main()
	{
	}
}
