using System.Collections;
using System;
using UnityEngine;

[AddComponentMenu("ControlPrototype/Top Down Click Walker"), RequireComponent(typeof(CharacterController))]
[Serializable]
public class TopDownClickWalker : MonoBehaviour
{
	public float maxSpeed;

	public float gravity;

	public Transform indicator;

	public float stopDistance;

	public bool instantTurn;

	public float turnSpeed;

	public string navigationMask;

	private int hasTarget;

	private Vector3 targetPoint;

	private Vector3 moveDirection;

	private bool grounded;

	private float startTime;

	private bool canDrag;

	private bool firstClick;

	public float singleClickAllowance;

	public TopDownClickWalker()
	{
		this.maxSpeed = 10f;
		this.gravity = 9.8f;
		this.stopDistance = 1f;
		this.instantTurn = true;
		this.turnSpeed = 10f;
		this.hasTarget = 0;
		this.targetPoint = Vector3.zero;
		this.moveDirection = Vector3.zero;
		this.grounded = false;
		this.startTime = (float)0;
		this.canDrag = false;
		this.firstClick = false;
		this.singleClickAllowance = 0.25f;
	}

	public void Start()
	{
		if (this.indicator)
		{
			this.indicator.gameObject.active = false;
		}
	}

	public void FixedUpdate()
	{
		if (this.hasTarget != 0)
		{
			float num = Vector3.Distance(new Vector3(this.targetPoint.x, this.transform.position.y, this.targetPoint.z), this.transform.position);
			this.moveDirection = new Vector3((float)0, (float)0, this.maxSpeed);
			this.moveDirection = this.transform.TransformDirection(this.moveDirection);
			if (num < this.stopDistance)
			{
				if (this.indicator)
				{
					this.indicator.gameObject.active = false;
				}
				this.hasTarget = 0;
			}
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(this.targetPoint.x, this.transform.position.y, this.targetPoint.z) - this.transform.position);
			if (this.instantTurn)
			{
				this.transform.rotation = quaternion;
			}
			else
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, Time.deltaTime * this.turnSpeed);
			}
		}
		else
		{
			this.moveDirection = new Vector3((float)0, (float)0, (float)0);
		}
		this.moveDirection.y = this.moveDirection.y - this.gravity;
		CharacterController characterController = (CharacterController)this.GetComponent(typeof(CharacterController));
		CollisionFlags collisionFlags = characterController.Move(this.moveDirection * Time.deltaTime);
		//this.grounded = ((collisionFlags & 4) != 0);
	}

	public void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			this.startTime = Time.time;
			this.canDrag = true;
			this.firstClick = true;
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			this.canDrag = false;
		}
		if (this.firstClick || (this.canDrag && Time.time > this.startTime + this.singleClickAllowance))
		{
			this.firstClick = false;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit = default(RaycastHit);
			//if (Physics.Raycast(ray, ref raycastHit) && RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(RuntimeServices.GetProperty(raycastHit.collider, "gameObject"), "layer"), LayerMask.NameToLayer(this.navigationMask)) && (raycastHit.point - this.transform.position).magnitude > this.stopDistance)
			//{
			//	this.setTargetPos(raycastHit.point);
			//}
		}
	}

	public void setTargetPos(Vector3 target)
	{
		this.hasTarget = 1;
		this.targetPoint = target;
		if (this.indicator)
		{
			this.indicator.position = this.targetPoint;
			this.indicator.gameObject.active = true;
		}
		this.targetPoint.y = this.transform.position.y;
	}

	public void Main()
	{
	}
}
