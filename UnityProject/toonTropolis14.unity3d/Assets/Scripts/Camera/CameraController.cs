using System.Collections;
using System;
using UnityEngine;

[AddComponentMenu("ControlPrototype/Camera Controller")]
[Serializable]
public class CameraController : MonoBehaviour
{
	public Transform target;

	public Texture reticle;

	public static float distance = 4f;

	private float xSpeed;

	private float ySpeed;

	private int yMinLimit;

	private int yMaxLimit;

	private int zoomMinLimit;

	private int zoomMaxLimit;

	private float x;

	public static float y = 15f * (float)-1;

	private Status status;

	public static string cameraView = "third";

	public static bool mouseMode = true;

	public static bool dofMode = false;

	public static bool cameraFollow = true;

	public CameraController()
	{
		this.xSpeed = 250f;
		this.ySpeed = 120f;
		this.yMinLimit = -20;
		this.yMaxLimit = 80;
		this.zoomMinLimit = 2;
		this.zoomMaxLimit = 6;
		this.x = 0f;
	}

	public void Start()
	{
		this.x = this.transform.eulerAngles.y;
		this.status = (Status)this.GetComponent(typeof(Status));
	}

	public void LateUpdate()
	{
		this.CheckStatus();
		if (CameraController.cameraView == "third")
		{
			if (CameraController.cameraFollow)
			{
				float num = this.target.transform.eulerAngles.y;
				float num2 = this.transform.eulerAngles.y;
				if (Input.GetButton("CameraZoomIn") && CameraController.distance > (float)this.zoomMinLimit)
				{
					CameraController.distance -= (float)1 / (this.xSpeed * 0.02f);
				}
				if (Input.GetButton("CameraZoomOut") && CameraController.distance < (float)this.zoomMaxLimit)
				{
					CameraController.distance += (float)1 / (this.xSpeed * 0.02f);
				}
				if (CameraController.mouseMode)
				{
					CameraController.distance += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * (float)-1 * this.xSpeed * 0.02f * Mathf.Abs(CameraController.distance);
					this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
					CameraController.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
				}
				this.x += Input.GetAxis("CameraX") * this.xSpeed * 0.02f;
				CameraController.y -= Input.GetAxis("CameraY") * this.ySpeed * 0.02f;
				CameraController.y = CameraController.ClampAngle(CameraController.y, (float)this.yMinLimit, (float)this.yMaxLimit);
				Quaternion quaternion;
				Vector3 position;
				if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
				{
					quaternion = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(CameraController.y, num, (float)0), Time.deltaTime * (float)3);
					position = quaternion * new Vector3(0f, 0f, CameraController.distance * (float)-1) + this.target.position;
					this.x = num;
				}
				else
				{
					quaternion = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(CameraController.y, this.x, (float)0), Time.deltaTime * (float)3);
					position = quaternion * new Vector3(0f, 0f, CameraController.distance * (float)-1) + this.target.position;
				}
				this.transform.rotation = quaternion;
				this.transform.position = position;
			}
			else
			{
				Quaternion quaternion = Quaternion.Euler(CameraController.y, this.x, (float)0);
				Vector3 position = quaternion * new Vector3(0f, 0f, CameraController.distance * (float)-1) + this.target.position;
				this.transform.rotation = quaternion;
				this.transform.position = position;
			}
		}
		else
		{
			float num = this.target.transform.eulerAngles.y;
			this.x = num;
			if (CameraController.mouseMode)
			{
				this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
				CameraController.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			}
			Quaternion quaternion = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(CameraController.y, this.x, (float)0), Time.deltaTime * (float)3);
			Vector3 position = quaternion * new Vector3(0f, 0f, CameraController.distance * (float)-1) + this.target.position;
			this.transform.rotation = quaternion;
			this.transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < (float)-360)
		{
			angle += (float)360;
		}
		if (angle > (float)360)
		{
			angle -= (float)360;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public void CheckStatus()
	{
		if (Input.GetKeyDown((KeyCode)9) || Input.GetKeyDown((KeyCode)122))
		{
			if (CameraController.cameraView == "third")
			{
				CameraController.cameraView = "first";
				CameraController.distance = 0f;
			}
			else
			{
				CameraController.cameraView = "third";
				CameraController.distance = 4f;
			}
		}
		if (Input.GetKeyDown((KeyCode)308) || Input.GetKeyDown((KeyCode)99))
		{
			if (CameraController.mouseMode)
			{
				CameraController.mouseMode = false;
			}
			else
			{
				CameraController.mouseMode = true;
			}
		}
		if (Input.GetKeyDown((KeyCode)98))
		{
			if (CameraController.dofMode)
			{
				CameraController.dofMode = false;
			}
			else
			{
				CameraController.dofMode = true;
			}
		}
		if (Input.GetKeyDown((KeyCode)121))
		{
			if (CameraController.cameraFollow)
			{
				CameraController.cameraFollow = false;
			}
			else
			{
				CameraController.cameraFollow = true;
			}
		}
		/*if (CameraController.dofMode && RuntimeServices.ToBool(this.GetComponent(typeof(DepthOfFieldEffect))))
		{
			RuntimeServices.SetProperty(this.GetComponent(typeof(DepthOfFieldEffect)), "enabled", true);
			RuntimeServices.SetProperty(this.GetComponent(typeof(DOFGui)), "enabled", true);
		}
		else
		{
			RuntimeServices.SetProperty(this.GetComponent(typeof(DepthOfFieldEffect)), "enabled", false);
			RuntimeServices.SetProperty(this.GetComponent(typeof(DOFGui)), "enabled", false);
		}*/
	}

	public void Main()
	{
	}
}
