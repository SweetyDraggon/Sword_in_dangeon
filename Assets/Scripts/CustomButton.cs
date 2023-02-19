using System;
using UnityEngine;

public class CustomButton : MonoBehaviour
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

	public CustomButton.BUTTON_STATE buttonState;

	private CustomButton.BUTTON_STATE lastButtonState;

	private bool wasPressed;

	private Vector3 initialPosition;

	private Vector2 initialScale;

	private BoxCollider bCollider;

	public bool firedDownEvent;

	public bool firedUpEvent;

	public GameObject target;

	public string onDownEvent;

	public string onUpEvent;

	public string onClickEvent;

	public string imageUp = string.Empty;

	public string imageDown = string.Empty;

	public bool allowContinuous;

	private void Start()
	{
		if (base.transform.Find("Sprite"))
		{
			this.sprite = base.transform.Find("Sprite").GetComponent<tk2dBaseSprite>();
		}
		if (base.GetComponent<tk2dBaseSprite>())
		{
			this.sprite = base.GetComponent<tk2dBaseSprite>();
		}
		this.bCollider = (GetComponent<Collider>() as BoxCollider);
		if (!this.hudCamera)
		{
			this.hudCamera = Camera.main;
		}
	}

	private void Update()
	{
		if (!this.sprite)
		{
			this.sprite = base.transform.Find("Sprite").GetComponent<tk2dBaseSprite>();
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

	private void ButtonDown()
	{
		if (!this.isEnabled)
		{
			return;
		}
		this.buttonState = CustomButton.BUTTON_STATE.SELECTED;
		if (this.imageDown != string.Empty)
		{
			this.sprite.SetSprite(this.imageDown);
		}
		this.wasPressed = true;
		this.firedUpEvent = false;
		if (!this.firedDownEvent || this.allowContinuous)
		{
			this.firedDownEvent = true;
			this.onButtonDown();
			if (this.target != null && this.onDownEvent != string.Empty)
			{
				this.target.SendMessage(this.onDownEvent);
			}
		}
	}

	private void ButtonUp()
	{
		if (!this.isEnabled)
		{
			return;
		}
		this.buttonState = CustomButton.BUTTON_STATE.UNSELECTED;
		if (this.imageUp != string.Empty)
		{
			this.sprite.SetSprite(this.imageUp);
		}
		this.firedDownEvent = false;
		if (!this.firedUpEvent)
		{
			this.onButtonUp();
			if (this.target != null && this.onUpEvent != string.Empty)
			{
				this.target.SendMessage(this.onUpEvent);
			}
		}
		if (this.wasPressed)
		{
			this.firedUpEvent = true;
			this.onButtonClick();
			if (this.target != null && this.onClickEvent != string.Empty)
			{
				this.target.SendMessage(this.onClickEvent);
			}
		}
		this.wasPressed = false;
	}

	public void setEnabled(bool enabled)
	{
		base.gameObject.GetComponent<Collider>().enabled = enabled;
		this.isEnabled = enabled;
		if (!this.isEnabled)
		{
			this.buttonState = CustomButton.BUTTON_STATE.DISABLED;
		}
		else
		{
			this.buttonState = CustomButton.BUTTON_STATE.UNSELECTED;
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
		}
	}

	public virtual void onButtonDown()
	{
	}

	public virtual void onButtonUp()
	{
	}

	public virtual void onButtonClick()
	{
	}
}
