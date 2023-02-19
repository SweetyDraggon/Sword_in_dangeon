using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InAppPurchaseManager : MonoBehaviour
{
	public string token;

	public string[] product_identifiers;

	public string[] android_product_identifiers;

	public static event Action<List<string>> productListReceivedEvent;

	public static event Action<string> purchaseSuccessfulEvent;

	public static event Action<string> purchaseFailedEvent;

	public static event Action<string> purchaseCancelledEvent;

	public static event Action<string> purchaseRestoreEvent;

	public static event Action<string> rewardSuccessEvent;

	public static event Action<string> rewardFailedEvent;

	private void Start()
	{
		//NoodleIAPManager.Init(this.android_product_identifiers);
	}

	private void onPurchaseSuccessfulEvent(string productIdentifier)
	{
		UnityEngine.Debug.Log("STOREKIT: purchaseSuccessfulEvent: " + productIdentifier);
		if (InAppPurchaseManager.purchaseSuccessfulEvent != null)
		{
			InAppPurchaseManager.purchaseSuccessfulEvent(productIdentifier);
		}
	}

	private void onPurchaseFailedEvent(string productIdentifier)
	{
		UnityEngine.Debug.Log("STOREKIT: purchaseFailedEvent: " + productIdentifier);
		if (InAppPurchaseManager.purchaseFailedEvent != null)
		{
			InAppPurchaseManager.purchaseFailedEvent(productIdentifier);
		}
	}

	private void onPurchaseCancelledEvent(string productIdentifier)
	{
		UnityEngine.Debug.Log("STOREKIT: purchaseCancelledEvent: " + productIdentifier);
		if (InAppPurchaseManager.purchaseCancelledEvent != null)
		{
			InAppPurchaseManager.purchaseCancelledEvent(productIdentifier);
		}
	}

	private void onPurchaseRestoreEvent(string productIdentifier)
	{
		UnityEngine.Debug.Log("STOREKIT: purchaseRestoreEvent: " + productIdentifier);
		if (InAppPurchaseManager.purchaseRestoreEvent != null)
		{
			InAppPurchaseManager.purchaseRestoreEvent(productIdentifier);
		}
	}

	private void onRewardEventSuccess(string productAndValue)
	{
		if (InAppPurchaseManager.rewardSuccessEvent != null)
		{
			InAppPurchaseManager.rewardSuccessEvent(productAndValue);
		}
	}

	private void onRewardEventFailed(string error)
	{
		UnityEngine.Debug.Log("onRewardEventFailed: " + error);
		if (InAppPurchaseManager.rewardFailedEvent != null)
		{
			InAppPurchaseManager.rewardFailedEvent(error);
		}
	}
}
