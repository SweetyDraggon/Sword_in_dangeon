using Prime31;
using System;
using System.Runtime.CompilerServices;

public class ChartboostAndroidManager : AbstractManager
{




















	public static event Action didCacheMoreAppsEvent;

	public static event Action didFailToCacheMoreAppsEvent;

	public static event Action<CBLocation> didCacheInterstitialEvent;

	public static event Action<CBLocation> didFailToCacheInterstitialEvent;

	public static event Action<string> didFinishInterstitialEvent;

	public static event Action<string> didFinishMoreAppsEvent;

	public static event Action didCloseMoreAppsEvent;

	public static event Action<string> didShowInterstitialEvent;

	public static event Action didShowMoreAppsEvent;

	public static event Action<string> didFailToLoadUrlEvent;

	static ChartboostAndroidManager()
	{
		AbstractManager.initialize(typeof(ChartboostAndroidManager));
	}

	public void didFailToLoadMoreApps(string empty)
	{
		if (ChartboostAndroidManager.didFailToCacheMoreAppsEvent != null)
		{
			ChartboostAndroidManager.didFailToCacheMoreAppsEvent();
		}
	}

	public void didCacheInterstitial(string location)
	{
		if (ChartboostAndroidManager.didCacheInterstitialEvent != null)
		{
			ChartboostAndroidManager.didCacheInterstitialEvent((CBLocation)((int)Enum.Parse(typeof(CBLocation), location)));
		}
	}

	public void didCacheMoreApps(string empty)
	{
		if (ChartboostAndroidManager.didCacheMoreAppsEvent != null)
		{
			ChartboostAndroidManager.didCacheMoreAppsEvent();
		}
	}

	public void didFinishInterstitial(string param)
	{
		if (ChartboostAndroidManager.didFinishInterstitialEvent != null)
		{
			ChartboostAndroidManager.didFinishInterstitialEvent(param);
		}
	}

	public void didFinishMoreApps(string param)
	{
		if (ChartboostAndroidManager.didFinishMoreAppsEvent != null)
		{
			ChartboostAndroidManager.didFinishMoreAppsEvent(param);
		}
	}

	public void didCloseMoreApps(string empty)
	{
		if (ChartboostAndroidManager.didCloseMoreAppsEvent != null)
		{
			ChartboostAndroidManager.didCloseMoreAppsEvent();
		}
	}

	public void didFailToLoadInterstitial(string location)
	{
		if (ChartboostAndroidManager.didFailToCacheInterstitialEvent != null)
		{
			ChartboostAndroidManager.didFailToCacheInterstitialEvent((CBLocation)((int)Enum.Parse(typeof(CBLocation), location)));
		}
	}

	public void didShowInterstitial(string location)
	{
		if (ChartboostAndroidManager.didShowInterstitialEvent != null)
		{
			ChartboostAndroidManager.didShowInterstitialEvent(location);
		}
	}

	public void didShowMoreApps(string empty)
	{
		if (ChartboostAndroidManager.didShowMoreAppsEvent != null)
		{
			ChartboostAndroidManager.didShowMoreAppsEvent();
		}
	}

	public void didFailToLoadUrl(string url)
	{
		if (ChartboostAndroidManager.didFailToLoadUrlEvent != null)
		{
			ChartboostAndroidManager.didFailToLoadUrlEvent(url);
		}
	}
}
