/*
using System;
using UnityEngine;

public class ChartboostAndroid
{
	private static AndroidJavaObject _plugin;

	static ChartboostAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.ChartboostPlugin"))
		{
			ChartboostAndroid._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
		}
	}

	public static void setImpressionsUseActivities(bool impressionsUseActivities)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("setImpressionsUseActivities", new object[]
		{
			impressionsUseActivities
		});
	}

	public static void init(string appId, string appSignature, bool shouldRequestInterstitialsInFirstSession = true)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("init", new object[]
		{
			appId,
			appSignature,
			shouldRequestInterstitialsInFirstSession
		});
	}

	public static void cacheInterstitial(CBLocation location)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("cacheInterstitial", new object[]
		{
			location.ToString()
		});
	}

	public static bool hasCachedInterstitial(CBLocation location)
	{
		return Application.platform == RuntimePlatform.Android && ChartboostAndroid._plugin.Call<bool>("hasCachedInterstitial", new object[]
		{
			location.ToString()
		});
	}

	public static void showInterstitial(CBLocation location)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("showInterstitial", new object[]
		{
			location.ToString()
		});
	}

	public static void cacheMoreApps()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("cacheMoreApps", new object[0]);
	}

	public static bool hasCachedMoreApps()
	{
		return Application.platform == RuntimePlatform.Android && ChartboostAndroid._plugin.Call<bool>("hasCachedMoreApps", new object[0]);
	}

	public static void showMoreApps()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		ChartboostAndroid._plugin.Call("showMoreApps", new object[0]);
	}
}
*/