using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlInputMapping
{
	public string name;

	public List<KeyCode> keys;

	public bool joystickDigital = true;

	public bool joystickNegativeOnly;

	public string onUpMessage;

	public string onDownMessage;

	public void checkKeyboardInput(GameObject target)
	{
		bool flag = false;
		foreach (KeyCode current in this.keys)
		{
			if (UnityEngine.Input.GetKey(current))
			{
				flag = true;
			}
		}
		if (target != null && flag)
		{
			this.sendMessageOnDown(target);
		}
		if (flag)
		{
			return;
		}
		flag = true;
		foreach (KeyCode current2 in this.keys)
		{
			if (!Input.GetKey(current2))
			{
				flag = false;
			}
		}
		if (target != null && !flag)
		{
			this.sendMessageOnUp(target);
		}
	}

	public void sendMessageOnDown(GameObject target)
	{
		if (target != null)
		{
			target.SendMessage(this.onDownMessage);
		}
	}

	public void sendMessageOnUp(GameObject target)
	{
		if (target != null)
		{
			target.SendMessage(this.onUpMessage);
		}
	}
}
