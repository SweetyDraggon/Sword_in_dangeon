using System;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
	public enum BUTTON_STATE
	{
		UNSELECTED,
		SELECTED,
		DISABLED
	}

	public Camera hudCamera;

	public bool isEnabled = true;

	public tk2dBaseSprite sprite;

	public ControlManager controlManager;

	public string controlInputMapping;

	public ControlButton.BUTTON_STATE buttonState;

	private ControlButton.BUTTON_STATE lastButtonState;

	private Color buttonColorUnselected = new Color(1f, 1f, 1f, 0.25f);

	private Color buttonColorSelected = new Color(1f, 1f, 1f, 0.75f);

	private Color buttonColorDisabled = new Color(1f, 1f, 1f, 0.1f);

	public bool wasPressed;

	private Vector3 initialPosition;

	private Vector2 initialScale;

	private BoxCollider bCollider;

	private void Start()
	{
		this.sprite = base.GetComponent<tk2dBaseSprite>();
		this.bCollider = (GetComponent<Collider>() as BoxCollider);
		this.controlManager.registerVirtualButton(this);
	}

	private void Update()
	{
		if (!this.sprite)
		{
			this.sprite = base.GetComponent<tk2dBaseSprite>();
		}
		this.updateButton();
		this.performTouchDetection();
	}

	private void performTouchDetection()
	{
		bool flag = false;
		if (Input.touches.Length > 0)
		{
			for (int i = 0; i < Input.touches.Length; i++)
			{
				flag = this.performRaycast(Input.touches[i].position);
				if (flag)
				{
					break;
				}
			}
		}
		else
		{
			flag = this.performRaycast(UnityEngine.Input.mousePosition);
		}
		if (Input.GetMouseButton(0) && flag)
		{
			this.ButtonDown();
		}
		else
		{
			this.ButtonUp();
		}
	}

	private bool performRaycast(Vector2 position)
	{
		Ray ray = this.hudCamera.ScreenPointToRay(position);
		RaycastHit raycastHit;
		return Physics.Raycast(ray, out raycastHit, 100f) && raycastHit.collider.gameObject == base.gameObject;
	}

	private void setBoundsByRect(Rect newBounds)
	{
	}

	private void ButtonDown()
	{
		if (!this.isEnabled)
		{
			return;
		}
		this.buttonState = ControlButton.BUTTON_STATE.SELECTED;
		this.wasPressed = true;
		if (this.controlManager != null)
		{
			this.controlManager.virtualButtonDown(this.controlInputMapping);
		}
	}

	private void ButtonUpEdit()
	{
		this.buttonState = ControlButton.BUTTON_STATE.UNSELECTED;
	}

	public void ButtonUp()
	{
		if (!this.isEnabled)
		{
			return;
		}
		this.buttonState = ControlButton.BUTTON_STATE.UNSELECTED;
		if (this.wasPressed && this.controlManager != null)
		{
			this.controlManager.virtualButtonUp(this.controlInputMapping);
		}
		this.wasPressed = false;
	}

	public void setEnabled(bool enabled)
	{
		base.gameObject.GetComponent<Collider>().enabled = enabled;
		this.isEnabled = enabled;
		this.wasPressed = false;
		if (!this.isEnabled)
		{
			this.buttonState = ControlButton.BUTTON_STATE.DISABLED;
		}
		else
		{
			this.buttonState = ControlButton.BUTTON_STATE.UNSELECTED;
		}
	}

	private void updateButton()
	{
		if (this.lastButtonState == this.buttonState)
		{
			return;
		}
		this.lastButtonState = this.buttonState;
		if (this.sprite != null)
		{
			if (this.buttonState == ControlButton.BUTTON_STATE.UNSELECTED)
			{
				this.sprite.color = this.buttonColorUnselected;
			}
			else if (this.buttonState == ControlButton.BUTTON_STATE.SELECTED)
			{
				this.sprite.color = this.buttonColorSelected;
			}
			else if (this.buttonState == ControlButton.BUTTON_STATE.DISABLED)
			{
				this.sprite.color = this.buttonColorDisabled;
			}
		}
	}
}
