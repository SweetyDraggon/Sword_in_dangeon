using System;
using UnityEngine;
using CoreGame;

public class ShopItemRowIAPRestore : MonoBehaviour
{
	public ShopMenu shop;

	public string itemName;

	public string itemDescription;

	public string itemSKU;

	public tk2dTextMesh textItemName;

	public tk2dTextMesh textDescription;

	private int buttonState = 1;

	public GameObject button;

	public CustomUIButton customButton;

	private void Awake()
	{
		this.customButton = this.button.GetComponent<CustomUIButton>();
		this.updateButton();
		this.updateRowInfo();
	}

	private void Start()
	{
		if (this.customButton.sendMessageTarget == null)
		{
			this.customButton.sendMessageTarget = base.gameObject;
		}
	}

	private void Update()
	{
	}

	public void updateRowInfo()
	{
		this.textItemName.text = this.itemName;
		this.textDescription.text = this.itemDescription;
		this.updateButton();
	}

	public void buyItemClicked()
	{
		if (!PlayGameServices.isSignedIn())
		{
			this.shop.showPurchaseDisabled();
			return;
		}
		if (!Utils.IsInternetAvailable())
		{
			this.shop.showNetworkError();
			return;
		}
		NoodleIAPManager.RestorePurchases();
		this.shop.UpdateRows();
	}

	public void updateButton()
	{
		if (this.buttonState == 2)
		{
			this.customButton.SendMessageOnClickMethodName = "equipButtonClicked";
		}
		else if (this.buttonState == 3)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
		}
		else if (this.buttonState == 4)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
		}
		else
		{
			this.customButton.SendMessageOnClickMethodName = "buyItemClicked";
		}
		this.customButton.updateSprites();
	}

	public void SetButtonState(int s)
	{
		this.buttonState = s;
		this.updateButton();
	}
}
