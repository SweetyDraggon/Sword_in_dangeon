using System;
using UnityEngine;
using CoreGame;
using TMPro;


public class ShopItemRowIAP : MonoBehaviour
{
	public ShopMenu shop;

	public int itemNum;

	public string iconName;

	public string itemName;

	public float itemCost;

	public string itemSKU;

	public string itemDescription;

	public bool alreadyOwned;

	private int buttonState = 1;

	public GameObject button;

	public tk2dSprite icon;

	public tk2dTextMesh textItemName;

	public tk2dTextMesh textItemCost;

	public tk2dTextMesh textDescription;

	public CustomUIButton customButton;
	public TextMeshPro alternativeTextDescription;

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

	
	public void updateRowInfo()
	{
		if (this.itemSKU == "com.ravenousgames.deviousdungeon.coindoubler" || this.itemSKU == "com.noodlecake.deviousdungeon.coindoubler")
		{
			if (GameCore.Instance.ownsCoinDoubler)
			{
				this.SetButtonState(3);
			}
			else
			{
				this.SetButtonState(1);
			}
		}
		this.icon.SetSprite(this.iconName);
		this.textItemName.text = this.itemName;
		this.textItemCost.text = "$" + this.itemCost.ToString();
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
		if (this.itemSKU == "com.ravenousgames.devious.coindoubler" || this.itemSKU == "com.noodlecake.deviousdungeon.coindoubler")
		{
			WindowManager.Instance.ShowAlertView("In-App Purchase", string.Concat(new object[]
			{
				"Purchase the ",
				this.itemName,
				" for $",
				this.itemCost,
				"? Item does NOT enhance IAP coin bundles!"
			}), "NO", "YES", base.gameObject, string.Empty, "confirmedPurchase");
		}
		else
		{
			NoodleIAPManager.PurchaseProduct(this.itemSKU);
		}
	}

	public void confirmedPurchase()
	{
		NoodleIAPManager.PurchaseProduct(this.itemSKU);
	}

	public void updateButton()
	{
		if (this.buttonState == 2)
		{
			this.customButton.SendMessageOnClickMethodName = "equipButtonClicked";
			this.customButton.imageUp = "shop_item_button3";
			this.customButton.imageDown = "shop_item_button4";
		}
		else if (this.buttonState == 3)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
			this.customButton.imageUp = "shop_item_button5";
			this.customButton.imageDown = "shop_item_button5";
		}
		else if (this.buttonState == 4)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
			this.customButton.imageUp = "shop_item_button6";
			this.customButton.imageDown = "shop_item_button6";
		}
		else
		{
			this.customButton.SendMessageOnClickMethodName = "buyItemClicked";
			this.customButton.imageUp = "shop_item_button1";
			this.customButton.imageDown = "shop_item_button2";
		}
		this.customButton.updateSprites();
	}

	public void SetButtonState(int s)
	{
		this.buttonState = s;
		this.updateButton();
	}

   
}
