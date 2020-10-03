using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class Status : MonoBehaviour
{
	public static bool menuMode = false;

	private float x;

	private float y;

	private float width;

	private float height;

	public Status()
	{
		this.x = (float)10;
		this.y = (float)10;
		this.width = (float)120;
		this.height = (float)40;
	}

	public void Update()
	{
		this.CheckMode();
	}

	public void OnGUI()
	{
		if (Status.menuMode)
		{
			GUI.Box(new Rect(this.x + this.width * (float)0, this.y, this.width, this.height), "Tab or [Z]:\n" + CameraController.cameraView);
			GUI.Box(new Rect(this.x + this.width * (float)1, this.y, this.width, this.height), "LeftCtrl or [X]:\n run " + PlayerController.runMode);
			GUI.Box(new Rect(this.x + this.width * (float)2, this.y, this.width, this.height), "LeftAlt or [C]:\n mouse " + CameraController.mouseMode);
			GUI.Box(new Rect(this.x + this.width * (float)4, this.y, this.width * (float)3, this.height * (float)2), "[player control]:  a,d,w,s, arrow key\n [camera control]:  8,2,4,6, page up/down\n [jump]:  space\n [zoom in/out]:  +, -, mouse scroll wheel\n [menu on/off]: escape or M");
			GUI.Box(new Rect(this.x + this.width * (float)0, this.y + this.height, this.width, this.height), string.Empty);
			//GUI.Box(new Rect(this.x + this.width * (float)1, this.y + this.height, this.width, this.height), "[V]:\n" + toggleVisibility.dayMode);
			GUI.Box(new Rect(this.x + this.width * (float)2, this.y + this.height, this.width, this.height), "[B]:\n depth of field " + CameraController.dofMode);
			GUI.Box(new Rect(this.x + this.width * (float)0, this.y + this.height * (float)2, this.width, this.height), "[N]:\n FPS walker " + PlayerController.fpsWalkerMode);
			GUI.Box(new Rect(this.x + this.width * (float)1, this.y + this.height * (float)2, this.width, this.height), "[T]:\n TDC walker" + PlayerController.tdcWalkerMode);
			GUI.Box(new Rect(this.x + this.width * (float)2, this.y + this.height * (float)2, this.width, this.height), "[Y]:\n camera follow" + CameraController.cameraFollow);
		}
	}

	public void CheckMode()
	{
		if (Input.GetKeyDown((KeyCode)27) || Input.GetKeyDown((KeyCode)109))
		{
			if (Status.menuMode)
			{
				Status.menuMode = false;
			}
			else
			{
				Status.menuMode = true;
			}
		}
	}

	public void Main()
	{
	}
}
