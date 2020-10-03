using System.Collections;
using System;
using UnityEngine;

[AddComponentMenu("ControlPrototype/Player Controller"), RequireComponent(typeof(CharacterController))]
[Serializable]
public class PlayerController : MonoBehaviour
{
	public float forwardSpeed;

	public float backwardSpeed;

	public float turnSpeed;

	public float jumpSpeed;

	public float gravity;

	private Vector3 moveDirection;

	private CharacterController charController;

	private Status status;

	public static TopDownClickWalker tdcWalker;

	public static bool fpsWalkerMode = false;

	public static bool strafeWalkerMode = false;

	public static bool tdcWalkerMode = false;

	public static bool runMode = false;

	public static bool jumpMode = false;

	public PlayerController()
	{
		this.forwardSpeed = 6f;
		this.backwardSpeed = 3f;
		this.turnSpeed = 3f;
		this.jumpSpeed = 8f;
		this.gravity = 20f;
		this.moveDirection = Vector3.zero;
	}

	public void Start()
	{
		this.charController = (CharacterController)this.GetComponent(typeof(CharacterController));
		this.status = (Status)this.GetComponent(typeof(Status));
		PlayerController.tdcWalker = (TopDownClickWalker)this.GetComponent(typeof(TopDownClickWalker));
	}

	public void Update()
	{
		this.CheckStaus();
		if (this.charController.isGrounded)
		{
			if (PlayerController.fpsWalkerMode)
			{
				float y = this.transform.eulerAngles.y + Input.GetAxis("Mouse X") * this.turnSpeed;
				Vector3 eulerAngles = this.transform.eulerAngles;
				float num = eulerAngles.y = y;
				Vector3 vector;
				this.transform.eulerAngles = eulerAngles;
				this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), (float)0, Input.GetAxis("Vertical"));
			}
			else
			{
				if (CameraController.mouseMode)
				{
					float y2 = this.transform.eulerAngles.y + (Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X")) * this.turnSpeed;
					Vector3 eulerAngles2 = this.transform.eulerAngles;
					float num2 = eulerAngles2.y = y2;
					Vector3 vector2;
					this.transform.eulerAngles = eulerAngles2;
				}
				else
				{
					float y3 = this.transform.eulerAngles.y + Input.GetAxis("Horizontal") * this.turnSpeed;
					Vector3 eulerAngles3 = this.transform.eulerAngles;
					float num3 = eulerAngles3.y = y3;
					Vector3 vector3;
					this.transform.eulerAngles = eulerAngles3;
				}
				this.moveDirection = new Vector3((float)0, (float)0, Input.GetAxis("Vertical"));
			}
			this.moveDirection = this.transform.TransformDirection(this.moveDirection);
			this.CharMove();
		}
		this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
		this.charController.Move(this.moveDirection * Time.deltaTime);
	}

	public void CharMove()
	{
		if (Input.GetAxis("Vertical") > (float)0)
		{
			this.moveDirection *= this.forwardSpeed;
		}
		else if (Input.GetAxis("Vertical") < (float)0)
		{
			this.moveDirection *= this.backwardSpeed;
		}
		if (Input.GetButton("Jump"))
		{
			//this.moveDirection.y = this.jumpSpeed;
		}
	}

	public void CheckStaus()
	{
		if (Input.GetKeyDown((KeyCode)306) || Input.GetKeyDown((KeyCode)120))
		{
			if (PlayerController.runMode)
			{
				PlayerController.runMode = false;
			}
			else
			{
				PlayerController.runMode = true;
			}
		}
		if (Input.GetKeyDown((KeyCode)110))
		{
			if (PlayerController.fpsWalkerMode)
			{
				PlayerController.fpsWalkerMode = false;
			}
			else
			{
				PlayerController.fpsWalkerMode = true;
			}
		}
		if (Input.GetKeyDown((KeyCode)116))
		{
			if (PlayerController.tdcWalkerMode)
			{
				PlayerController.tdcWalkerMode = false;
			}
			else
			{
				PlayerController.tdcWalkerMode = true;
			}
		}
		if (Input.GetKeyDown((KeyCode)106))
		{
			if (PlayerController.jumpMode)
			{
				PlayerController.jumpMode = false;
			}
			else
			{
				PlayerController.jumpMode = true;
			}
		}
		if (PlayerController.tdcWalkerMode)
		{
			PlayerController.tdcWalker.enabled = true;
		}
		else
		{
			PlayerController.tdcWalker.enabled = false;
		}
	}

	public void Main()
	{
	}
}
