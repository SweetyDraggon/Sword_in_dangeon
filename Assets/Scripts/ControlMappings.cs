using System;
using UnityEngine;

public class ControlMappings : MonoBehaviour
{
	public ControlInputMapping[] inputs;

	private bool isUsingKeyboard = true;

	private bool isUsingJoystick;

	private void Start()
	{
		this.isUsingKeyboard = true;
		this.isUsingJoystick = false;
	}

	public void Update()
	{
	}

	public void pollInput(GameObject target)
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android)
		{
			ControlInputMapping[] array = this.inputs;
			for (int i = 0; i < array.Length; i++)
			{
				ControlInputMapping controlInputMapping = array[i];
				controlInputMapping.checkKeyboardInput(target);
			}
		}
	}

	public void virtualButtonDown(GameObject target, string inputName)
	{
		ControlInputMapping[] array = this.inputs;
		for (int i = 0; i < array.Length; i++)
		{
			ControlInputMapping controlInputMapping = array[i];
			if (controlInputMapping.name == inputName)
			{
				controlInputMapping.sendMessageOnDown(target);
			}
		}
	}

	public void virtualButtonUp(GameObject target, string inputName)
	{
		ControlInputMapping[] array = this.inputs;
		for (int i = 0; i < array.Length; i++)
		{
			ControlInputMapping controlInputMapping = array[i];
			if (controlInputMapping.name == inputName)
			{
				controlInputMapping.sendMessageOnUp(target);
			}
		}
	}
}
