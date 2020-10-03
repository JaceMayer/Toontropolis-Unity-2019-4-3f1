using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class PlayerCharacter : MonoBehaviour
{
	private float walkSpeed;

	private float jumpSpeed;

	private float gravity;

	private Vector3 moveDirection;

	private CharacterController charController;

	private bool flagIdle;

	private float nextLoad;

	private float rate;

	private Status status;

	private TopDownClickWalker tdcWalker;

	private bool runMode;

	private bool fpsWalkerMode;

	private bool tdcWalkerMode;

	private bool jumpMode;

	private bool mouseMode;

	public PlayerCharacter()
	{
		this.walkSpeed = 1f;
		this.jumpSpeed = 6f;
		this.gravity = 20f;
		this.moveDirection = Vector3.zero;
		this.flagIdle = false;
		this.nextLoad = (float)0;
		this.rate = (float)6;
		this.runMode = false;
		this.fpsWalkerMode = false;
		this.tdcWalkerMode = false;
		this.jumpMode = false;
		this.mouseMode = false;
	}

	public void Start()
	{
		this.charController = (CharacterController)this.GetComponent(typeof(CharacterController));
		this.GetComponent<Animation>().wrapMode = (UnityEngine.WrapMode)2;
		this.status = (Status)this.GetComponent(typeof(Status));
		this.tdcWalker = (TopDownClickWalker)this.GetComponent(typeof(TopDownClickWalker));
	}

	public void Update()
	{
		this.CheckStaus();
		if (this.charController.isGrounded)
		{
			this.CharAnim();
			if (this.fpsWalkerMode)
			{
				this.StrafeWalk();
			}
			if (this.tdcWalkerMode)
			{
				this.TdcWalk();
			}
			if (this.fpsWalkerMode)
			{
				float y = this.transform.eulerAngles.y + Input.GetAxis("Mouse X") * (float)3;
				Vector3 eulerAngles = this.transform.eulerAngles;
				float num = eulerAngles.y = y;
				Vector3 vector;
				this.transform.eulerAngles = eulerAngles;
				this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), (float)0, Input.GetAxis("Vertical"));
			}
			else
			{
				if (this.mouseMode)
				{
					float y2 = this.transform.eulerAngles.y + (Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") * (float)3);
					Vector3 eulerAngles2 = this.transform.eulerAngles;
					float num2 = eulerAngles2.y = y2;
					Vector3 vector2;
					this.transform.eulerAngles = eulerAngles2;
				}
				else
				{
					float y3 = this.transform.eulerAngles.y + Input.GetAxis("Horizontal");
					Vector3 eulerAngles3 = this.transform.eulerAngles;
					float num3 = eulerAngles3.y = y3;
					Vector3 vector3;
					this.transform.eulerAngles = eulerAngles3;
				}
				this.moveDirection = new Vector3((float)0, (float)0, Input.GetAxis("Vertical"));
			}
			this.moveDirection = this.transform.TransformDirection(this.moveDirection);
		}
		this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
		this.charController.Move(this.moveDirection * (Time.deltaTime * this.walkSpeed));
	}

	public void CharAnim()
	{
		if (Input.GetAxis("Vertical") > 0.1f)
		{
			if (this.runMode)
			{
				this.Run();
			}
			else
			{
				this.Walk();
			}
		}
		else if (Input.GetButton("Jump"))
		{
			this.Jump();
		}
		else if (Input.GetAxis("Vertical") < 0.1f * (float)-1)
		{
			this.Walk();
		}
		else
		{
			this.Idle();
		}
		if (Input.GetAxis("Horizontal") != 0f && Input.GetAxis("Vertical") == 0f)
		{
			this.Walk();
		}
	}

	public void Idle()
	{
		this.GetComponent<Animation>().CrossFade("Idle_11");
		this.walkSpeed = (float)2;
		if (Time.time > this.nextLoad)
		{
			if (this.flagIdle)
			{
				this.flagIdle = false;
			}
			else
			{
				this.flagIdle = true;
			}
			this.nextLoad = Time.time + this.rate;
		}
		if (this.flagIdle)
		{
			this.GetComponent<Animation>().CrossFade("Idle_11");
		}
		/*else
		{
			this.GetComponent<Animation>().CrossFade("IdleTap");
			if (!this.GetComponent<Animation>().IsPlaying("IdleTap"))
			{
				this.flagIdle = true;
			}
		}*/
	}

	public void Walk()
	{
		this.GetComponent<Animation>().CrossFade("Walk_3");
		this.walkSpeed = (float)6;
		if (Input.GetButton("Jump"))
		{
			this.Jump();
		}
	}

	public void Run()
	{
		this.GetComponent<Animation>().CrossFade("Run_1");
		this.walkSpeed = (float)12;
		if (Input.GetButton("Jump"))
		{
			this.Jump();
		}
	}

	public void Jump()
	{
		this.GetComponent<Animation>().CrossFade("Jump_1");
		this.walkSpeed = (float)8;
		//this.moveDirection.y = this.jumpSpeed;
	}

	public void StrafeWalk()
	{
		if (Input.GetAxis("Vertical") != 0f)
		{
			if (this.jumpMode)
			{
				this.Jump();
			}
			else
			{
				this.Walk();
			}
		}
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			this.StrafeRight();
		}
		if (Input.GetAxis("Horizontal") < 0.1f * (float)-1)
		{
			this.StrafeLeft();
		}
	}

	public void StrafeRight()
	{
		this.GetComponent<Animation>().CrossFade("strafeRight");
		this.walkSpeed = (float)1;
	}

	public void StrafeLeft()
	{
		this.GetComponent<Animation>().CrossFade("strafeLeft");
		this.walkSpeed = (float)1;
	}

	public void TdcWalk()
	{
		Vector3 velocity = this.charController.velocity;
		velocity.y = (float)0;
		float magnitude = velocity.magnitude;
		float y = this.charController.velocity.y;
		float magnitude2 = this.charController.velocity.magnitude;
		MonoBehaviour.print(velocity);
		if (magnitude == 0f)
		{
			this.Idle();
		}
		else
		{
			this.Walk();
		}
		this.StrafeWalk();
	}

	public void CheckStaus()
	{
		Debug.Log(tdcWalker);
		if (this.tdcWalkerMode)
		{
			this.tdcWalker.enabled = true;
		}
		else
		{
			this.tdcWalker.enabled = false;
		}
	}

	public void Main()
	{
	}
}
