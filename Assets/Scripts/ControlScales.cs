using System;
using UnityEngine;

public class ControlScales : MonoBehaviour
{
	public int controlSize;

	public CustomUIButton controlsButton;

	public tk2dTextMesh controlsButtonText;

	private void Start()
	{
		this.controlSize = GameCore.Instance.controlSize;
		this.updateView();
	}

	private void Update()
	{
	}

	public void updateView()
	{
		if (this.controlSize == 0)
		{
			this.controlsButtonText.text = "1";
		}
		else if (this.controlSize == 1)
		{
			this.controlsButtonText.text = "2";
		}
		else
		{
			this.controlsButtonText.text = "3";
		}
	}

	public void applySetting()
	{
		GameCore.Instance.controlSize = this.controlSize;
		HUD hUD = UnityEngine.Object.FindObjectOfType(typeof(HUD)) as HUD;
		if (hUD)
		{
			hUD.updateControllerSize();
		}
	}

	private void bigButtonsClicked()
	{
		UnityEngine.Debug.Log("ControlScales.scale1Clicked");
		this.controlSize = (this.controlSize + 1) % 3;
		this.updateView();
	}
}
