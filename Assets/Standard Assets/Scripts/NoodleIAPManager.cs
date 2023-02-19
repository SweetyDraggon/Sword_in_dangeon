using System;
using UnityEngine;

public static class NoodleIAPManager
{
	public static string googleToken = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiosaxuMt5TUnBvBZgBZTdIveoJxMcQuXBcnZgJ5GiZhUyxWaxwr2ngRK3Rnzh0i4hsyU7Iix7xlM5XGOBD7K8/nfdsjesnMY4J61b5w2Bb9yzR6Kgl8G/oGmcg3kvxzpGLvnwbZWZQms2nMXEX2gq59P0NziAsFg3bo0MxvgquaqGOCrEY2GYi9b/K/gJI0y1xEMf1aFgoxlHozoQBa+HBKL4IPNwUA9LdfXNkloB+aBvpgXvAjfX9aTwnOrd8g37dSoEp1UP8giD0OQK6uCP1MrzVvu77UYkTACsiJ5EyWj32dzq9BlgPAcWYkQvGnrAFFnCiut5sMMBpGb6cZ44wIDAQAB";

	public static string[] skus;

	public static void Init(string[] productSkus)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			NoodleIAPManager.skus = productSkus;
			GoogleIAB.init(NoodleIAPManager.googleToken);
		}
	}

	public static void RestorePurchases()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GoogleIAB.queryInventory(NoodleIAPManager.skus);
		}
	}

	public static void PurchaseProduct(string productIdentifier)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			UnityEngine.Debug.Log("Purchasing: " + productIdentifier);
			GoogleIAB.purchaseProduct(productIdentifier);
		}
	}
}
