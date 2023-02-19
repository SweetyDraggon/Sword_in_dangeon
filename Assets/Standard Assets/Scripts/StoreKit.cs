/*
using MiniJSON;
using System;
using System.Runtime.InteropServices;

public class StoreKit
{
	[DllImport("__Internal")]
	public static extern void _initializeInAppPurchases(string jsonSKUs, string tok);

	public static void InitializeInAppPurchases(string[] skuList, string tok)
	{
		string text = Json.Serialize(skuList);
	}

	[DllImport("__Internal")]
	public static extern void _purchaseProduct(string productIdentifier);

	public static void PurchaseProduct(string productIdentifier)
	{
	}

	[DllImport("__Internal")]
	public static extern void _restorePurchases();

	public static void RestorePurchases()
	{
	}

	[DllImport("__Internal")]
	public static extern bool _canMakePurchases();

	public static bool CanMakePurchases()
	{
		return false;
	}
}
*/