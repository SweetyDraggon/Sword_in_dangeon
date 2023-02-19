using System;
using System.Collections.Generic;
using UnityEngine;

public class InAppPurchases : MonoBehaviour
{
	public List<IAP> productsList;

	private void Awake()
	{
        /*
		GoogleIABManager.purchaseSucceededEvent -= new Action<GooglePurchase>(this.onProductPurchaseSuccessGoogle);
		GoogleIABManager.purchaseSucceededEvent += new Action<GooglePurchase>(this.onProductPurchaseSuccessGoogle);
		GoogleIABManager.purchaseFailedEvent -= new Action<string>(this.onPurchaseFailedEventGoogle);
		GoogleIABManager.purchaseFailedEvent += new Action<string>(this.onPurchaseFailedEventGoogle);
		GoogleIABManager.queryInventoryFailedEvent -= new Action<string>(this.onQueryFailedEvent);
		GoogleIABManager.queryInventoryFailedEvent += new Action<string>(this.onQueryFailedEvent);
		GoogleIABManager.queryInventorySucceededEvent -= new Action<List<GooglePurchase>, List<GoogleSkuInfo>>(this.onQuerySucceededEvent);
		GoogleIABManager.queryInventorySucceededEvent += new Action<List<GooglePurchase>, List<GoogleSkuInfo>>(this.onQuerySucceededEvent);
		this.setupIAP();
		*/      
	}

	private void OnDestroy()
	{
        /*
		InAppPurchaseManager.purchaseSuccessfulEvent -= new Action<string>(this.onProductPurchaseSuccess);
		InAppPurchaseManager.purchaseFailedEvent -= new Action<string>(this.onPurchaseFailedEvent);
		InAppPurchaseManager.purchaseRestoreEvent -= new Action<string>(this.onProductRestoreSuccess);
		InAppPurchaseManager.rewardSuccessEvent -= new Action<string>(this.onRewardSuccess);
		InAppPurchaseManager.rewardFailedEvent -= new Action<string>(this.onRewardFailed);
		*/      
	}

	public void setupIAP()
	{
        /*
		this.productsList = new List<IAP>();
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coindoubler", -1));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins1", 500));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins2", 3750));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins3", 6250));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins4", 12500));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins5", 25000));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins6", 37500));
		this.productsList.Add(new IAP("com.noodlecake.deviousdungeon.coins7", 62500));
		*/      
	}

	private void onProductPurchaseSuccess(string productIdentifier)
	{
		//this.consumeProduct(productIdentifier, false);
	}

	private void onPurchaseFailedEventGoogle(string error)
	{
        /*
		UnityEngine.Debug.LogError("onPurchaseFailedEventGoogle \n" + error);
		if (error.Contains("User canceled."))
		{
			return;
		}
		WindowManager.Instance.ShowAlertView("Error", "We're sorry but there was an error processing the request.", "OK", string.Empty, base.gameObject, string.Empty, "alertConfirmed");
		*/      
	}

	private void onProductPurchaseSuccessGoogle(GooglePurchase purchase)
	{
        /*
		UnityEngine.Debug.Log("onProductPurchaseSuccessGoogle: " + purchase.productId);
		this.consumeProduct(purchase.productId, false);
		GoogleIAB.consumeProduct(purchase.productId);
		*/      
	}

	private void onProductRestoreSuccess(string productIdentifier)
	{
		//this.consumeProduct(productIdentifier, true);
	}

	private void onPurchaseFailedEvent(string productIdentifier)
	{
		//Game.Instance.paused = true;
		//WindowManager.Instance.ShowAlertView("Error", "We're sorry but there was an error processing the request.", "OK", string.Empty, base.gameObject, string.Empty, "alertConfirmed");
	}

	private void consumeProduct(string productIdentifier, bool restored = false)
	{
        /*
		if (this.productsList.Count == 0)
		{
			this.setupIAP();
		}
		foreach (IAP current in this.productsList)
		{
			if (current.productIdentifier == productIdentifier)
			{
				if (productIdentifier == "com.noodlecake.deviousdungeon.coindoubler")
				{
					GameCore.Instance.ownsCoinDoubler = true;
					if (!restored)
					{
						AudioManager.Instance.PlaySound("cash_register");
					}
				}
				else
				{
					Main.playerStats.money += current.coins;
					if (!restored)
					{
						AudioManager.Instance.PlaySound("cash_register");
					}
				}
				Main.saveGame();
				ShopItemRowIAP[] array = UnityEngine.Object.FindObjectsOfType(typeof(ShopItemRowIAP)) as ShopItemRowIAP[];
				ShopItemRowIAP[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					ShopItemRowIAP shopItemRowIAP = array2[i];
					shopItemRowIAP.updateRowInfo();
				}
				if (GameCore.Instance.shop != null)
				{
					if (restored)
					{
						GameCore.Instance.shop.presentRestoredAlert(productIdentifier);
					}
					else
					{
						GameCore.Instance.shop.presentGratitudeAlert(productIdentifier);
					}
				}
			}
		}
		*/
	}

	private void onRewardSuccess(string productAndQuantity)
	{
        /*
		string[] array = productAndQuantity.Split(new char[]
		{
			'_'
		});
		string a = array[0];
		int num = Convert.ToInt32(array[1]);
		if (a == "coins" && num > 0)
		{
			Main.playerStats.money += num;
			AudioManager.Instance.PlaySound("cash_register");
			Main.saveGame();
			MySDK.AlertView("Reward Success", "You have successfully redeemed your reward of " + num + " coins.");
		}
		*/
	}

	private void onRewardFailed(string errorMsg)
	{
		//WindowManager.Instance.ShowAlertView("Reward Error", "There was a problem redeeming your reward. Please try again later or contact Noodlecake Games.", "OK", string.Empty, base.gameObject, string.Empty, "alertConfirmed");
	}

	private void alertConfirmed()
	{
		//Game.Instance.paused = false;
	}

	private void onQueryFailedEvent(string error)
	{
		//UnityEngine.Debug.LogError("onQueryFailedEvent\n" + error);
		//WindowManager.Instance.ShowAlertView("In App Purchases Error", "There was a problem retrieving the In App Purchase information. Please try again later or contact Noodlecake Games.", "OK", string.Empty, base.gameObject, string.Empty, "alertConfirmed");
	}

	private void onQuerySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
        /*
		UnityEngine.Debug.Log("onQuerySucceededEvent");
		foreach (GooglePurchase current in purchases)
		{
			UnityEngine.Debug.Log("purchase:\n" + current.productId + "\n" + current.originalJson);
			if (current.purchaseState == GooglePurchase.GooglePurchaseState.Purchased)
			{
				this.consumeProduct(current.productId, false);
			}
		}
		UnityEngine.Debug.Log(skus);
		*/      
	}
}
