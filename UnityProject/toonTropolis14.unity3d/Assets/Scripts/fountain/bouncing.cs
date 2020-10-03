using System;
using UnityEngine;

[Serializable]
public class bouncing : MonoBehaviour
{
	public float height;

	public float R1;

	public float R2;

	public GameObject joint1;

	public GameObject joint2;

	public GameObject joint3;

	public GameObject joint4;

	private Vector3 joint5;

	private Vector3 joint6;

	private Vector3 joint7;

	private Vector2 P1;

	private Vector2 P2;

	private Vector2 Ptemp;

	public float angle_range;

	private float theta;

	private float phi;

	private float dis1;

	private float dis2;

	private float vy;

	private float vx;

	private float vz;

	private float gravity;

	private bool flag;

	public bouncing()
	{
		this.gravity = 0.01f;
		this.flag = false;
	}

	public void SetPositions()
	{
		this.theta = (float)UnityEngine.Random.Range(0, 360);
		this.phi = UnityEngine.Random.Range((float)0, this.angle_range);
		this.dis1 = UnityEngine.Random.Range(this.R1, this.R2);
		this.dis2 = UnityEngine.Random.Range(this.R1, this.R2);
		this.P1.x = this.dis1 * Mathf.Cos(this.theta);
		this.P1.y = this.dis1 * Mathf.Sin(this.theta);
		this.P2.x = this.dis2 * Mathf.Cos(this.theta + this.phi);
		this.P2.y = this.dis2 * Mathf.Sin(this.theta + this.phi);
	}

	public void Initialize()
	{
		this.vy = Mathf.Sqrt((float)2 * this.height * this.gravity);
		this.vx = (this.P1.x - this.P2.x) / ((float)2 * Mathf.Sqrt((float)2 * this.height / this.gravity));
		this.vz = (this.P1.y - this.P2.y) / ((float)2 * Mathf.Sqrt((float)2 * this.height / this.gravity));
		float x = this.P1.x;
		Vector3 localPosition = this.joint1.transform.localPosition;
		float num = localPosition.x = x;
		Vector3 vector;
		this.joint1.transform.localPosition = localPosition;
		float y = this.P1.y;
		Vector3 localPosition2 = this.joint1.transform.localPosition;
		float num2 = localPosition2.z = y;
		Vector3 vector2;
		this.joint1.transform.localPosition = localPosition2;
	}

	public void Jump()
	{
		float y = this.joint1.transform.localPosition.y + this.vy;
		Vector3 localPosition = this.joint1.transform.localPosition;
		float num = localPosition.y = y;
		Vector3 vector;
		this.joint1.transform.localPosition = localPosition;
		float x = this.joint1.transform.localPosition.x + this.vx;
		Vector3 localPosition2 = this.joint1.transform.localPosition;
		float num2 = localPosition2.x = x;
		Vector3 vector2;
		this.joint1.transform.localPosition = localPosition2;
		float z = this.joint1.transform.localPosition.z + this.vz;
		Vector3 localPosition3 = this.joint1.transform.localPosition;
		float num3 = localPosition3.z = z;
		Vector3 vector3;
		this.joint1.transform.localPosition = localPosition3;
		this.vy -= this.gravity;
		if (this.joint1.transform.localPosition.y < (float)-10)
		{
			this.vy = Mathf.Sqrt((float)2 * this.height * this.gravity);
			this.vy = (float)0;
			this.vx = (float)0;
			this.vz = (float)0;
			int num4 = 0;
			Vector3 localPosition4 = this.joint1.transform.localPosition;
			float num5 = localPosition4.y = (float)num4;
			Vector3 vector4;
			this.joint1.transform.localPosition = localPosition4;
			int num6 = 0;
			Vector3 localPosition5 = this.joint1.transform.localPosition;
			float num7 = localPosition5.x = (float)num6;
			Vector3 vector5;
			this.joint1.transform.localPosition = localPosition5;
			int num8 = 0;
			Vector3 localPosition6 = this.joint1.transform.localPosition;
			float num9 = localPosition6.z = (float)num8;
			Vector3 vector6;
			this.joint1.transform.localPosition = localPosition6;
			this.flag = false;
		}
	}

	public void ResetValues()
	{
		this.SetPositions();
		this.Initialize();
		this.flag = true;
	}

	public void Update()
	{
		if (!this.flag)
		{
			this.ResetValues();
		}
		else
		{
			float x = this.joint5.x;
			Vector3 localPosition = this.joint4.transform.localPosition;
			float num = localPosition.x = x;
			Vector3 vector;
			this.joint4.transform.localPosition = localPosition;
			float y = this.joint5.y;
			Vector3 localPosition2 = this.joint4.transform.localPosition;
			float num2 = localPosition2.y = y;
			Vector3 vector2;
			this.joint4.transform.localPosition = localPosition2;
			float z = this.joint5.z;
			Vector3 localPosition3 = this.joint4.transform.localPosition;
			float num3 = localPosition3.z = z;
			Vector3 vector3;
			this.joint4.transform.localPosition = localPosition3;
			this.joint5.x = this.joint3.transform.localPosition.x;
			this.joint5.y = this.joint3.transform.localPosition.y;
			this.joint5.z = this.joint3.transform.localPosition.z;
			float x2 = this.joint6.x;
			Vector3 localPosition4 = this.joint3.transform.localPosition;
			float num4 = localPosition4.x = x2;
			Vector3 vector4;
			this.joint3.transform.localPosition = localPosition4;
			float y2 = this.joint6.y;
			Vector3 localPosition5 = this.joint3.transform.localPosition;
			float num5 = localPosition5.y = y2;
			Vector3 vector5;
			this.joint3.transform.localPosition = localPosition5;
			float z2 = this.joint6.z;
			Vector3 localPosition6 = this.joint3.transform.localPosition;
			float num6 = localPosition6.z = z2;
			Vector3 vector6;
			this.joint3.transform.localPosition = localPosition6;
			this.joint6.x = this.joint2.transform.localPosition.x;
			this.joint6.y = this.joint2.transform.localPosition.y;
			this.joint6.z = this.joint2.transform.localPosition.z;
			float x3 = this.joint7.x;
			Vector3 localPosition7 = this.joint2.transform.localPosition;
			float num7 = localPosition7.x = x3;
			Vector3 vector7;
			this.joint2.transform.localPosition = localPosition7;
			float y3 = this.joint7.y;
			Vector3 localPosition8 = this.joint2.transform.localPosition;
			float num8 = localPosition8.y = y3;
			Vector3 vector8;
			this.joint2.transform.localPosition = localPosition8;
			float z3 = this.joint7.z;
			Vector3 localPosition9 = this.joint2.transform.localPosition;
			float num9 = localPosition9.z = z3;
			Vector3 vector9;
			this.joint2.transform.localPosition = localPosition9;
			this.joint7.x = this.joint1.transform.localPosition.x;
			this.joint7.y = this.joint1.transform.localPosition.y;
			this.joint7.z = this.joint1.transform.localPosition.z;
			this.Jump();
		}
	}

	public void Main()
	{
	}
}
