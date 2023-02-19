using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
	public ControlManagerPlayer[] players;

	private List<ControlButton> vButtons;

	private void Start()
	{
		if (this.players == null)
		{
			this.players = new ControlManagerPlayer[1];
			this.players[0] = new ControlManagerPlayer();
		}
	}

	private void Awake()
	{
		this.vButtons = new List<ControlButton>();
	}

	private void Update()
	{
		ControlManagerPlayer[] array = this.players;
		for (int i = 0; i < array.Length; i++)
		{
			ControlManagerPlayer controlManagerPlayer = array[i];
			controlManagerPlayer.mappings.pollInput(controlManagerPlayer.messageTarget);
		}
	}

	public void virtualButtonDown(string inputName)
	{
		ControlManagerPlayer[] array = this.players;
		for (int i = 0; i < array.Length; i++)
		{
			ControlManagerPlayer controlManagerPlayer = array[i];
			if (controlManagerPlayer.deviceId == 0)
			{
				controlManagerPlayer.mappings.virtualButtonDown(controlManagerPlayer.messageTarget, inputName);
			}
		}
	}

	public void virtualButtonUp(string inputName)
	{
		ControlManagerPlayer[] array = this.players;
		for (int i = 0; i < array.Length; i++)
		{
			ControlManagerPlayer controlManagerPlayer = array[i];
			if (controlManagerPlayer.deviceId == 0)
			{
				controlManagerPlayer.mappings.virtualButtonUp(controlManagerPlayer.messageTarget, inputName);
			}
		}
	}

	public void registerVirtualButton(ControlButton vButton)
	{
		this.vButtons.Add(vButton);
	}
}
