using System;
using UnityEngine;

public class ChartboostAndroidEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		ChartboostAndroidManager.didFailToCacheMoreAppsEvent += new Action(this.didFailToLoadMoreAppsEvent);
		ChartboostAndroidManager.didCacheInterstitialEvent += new Action<CBLocation>(this.didCacheInterstitialEvent);
		ChartboostAndroidManager.didCacheMoreAppsEvent += new Action(this.didCacheMoreAppsEvent);
		ChartboostAndroidManager.didFinishInterstitialEvent += new Action<string>(this.didFinishInterstitialEvent);
		ChartboostAndroidManager.didFinishMoreAppsEvent += new Action<string>(this.didFinishMoreAppsEvent);
		ChartboostAndroidManager.didCloseMoreAppsEvent += new Action(this.didCloseMoreAppsEvent);
		ChartboostAndroidManager.didFailToCacheInterstitialEvent += new Action<CBLocation>(this.didFailToLoadInterstitialEvent);
		ChartboostAndroidManager.didShowInterstitialEvent += new Action<string>(this.didShowInterstitialEvent);
		ChartboostAndroidManager.didShowMoreAppsEvent += new Action(this.didShowMoreAppsEvent);
		ChartboostAndroidManager.didFailToLoadUrlEvent += new Action<string>(this.didFailToLoadUrlEvent);
	}

	private void OnDisable()
	{
		ChartboostAndroidManager.didFailToCacheMoreAppsEvent -= new Action(this.didFailToLoadMoreAppsEvent);
		ChartboostAndroidManager.didCacheInterstitialEvent -= new Action<CBLocation>(this.didCacheInterstitialEvent);
		ChartboostAndroidManager.didCacheMoreAppsEvent -= new Action(this.didCacheMoreAppsEvent);
		ChartboostAndroidManager.didFinishInterstitialEvent -= new Action<string>(this.didFinishInterstitialEvent);
		ChartboostAndroidManager.didFinishMoreAppsEvent -= new Action<string>(this.didFinishMoreAppsEvent);
		ChartboostAndroidManager.didCloseMoreAppsEvent -= new Action(this.didCloseMoreAppsEvent);
		ChartboostAndroidManager.didFailToCacheInterstitialEvent -= new Action<CBLocation>(this.didFailToLoadInterstitialEvent);
		ChartboostAndroidManager.didShowInterstitialEvent -= new Action<string>(this.didShowInterstitialEvent);
		ChartboostAndroidManager.didShowMoreAppsEvent -= new Action(this.didShowMoreAppsEvent);
		ChartboostAndroidManager.didFailToLoadUrlEvent -= new Action<string>(this.didFailToLoadUrlEvent);
	}

	private void didFailToLoadMoreAppsEvent()
	{
		UnityEngine.Debug.Log("didFailToLoadMoreAppsEvent");
	}

	private void didCacheInterstitialEvent(CBLocation location)
	{
		UnityEngine.Debug.Log("didCacheInterstitialEvent: " + location);
	}

	private void didCacheMoreAppsEvent()
	{
		UnityEngine.Debug.Log("didCacheMoreAppsEvent");
	}

	private void didFinishInterstitialEvent(string param)
	{
		UnityEngine.Debug.Log("didFinishInterstitialEvent: " + param);
	}

	private void didFinishMoreAppsEvent(string param)
	{
		UnityEngine.Debug.Log("didFinishMoreAppsEvent: " + param);
	}

	private void didCloseMoreAppsEvent()
	{
		UnityEngine.Debug.Log("didCloseMoreAppsEvent");
	}

	private void didFailToLoadInterstitialEvent(CBLocation location)
	{
		UnityEngine.Debug.Log("didFailToLoadInterstitialEvent: " + location);
	}

	private void didShowInterstitialEvent(string location)
	{
		UnityEngine.Debug.Log("didShowInterstitialEvent: " + location);
	}

	private void didShowMoreAppsEvent()
	{
		UnityEngine.Debug.Log("didShowMoreAppsEvent");
	}

	private void didFailToLoadUrlEvent(string url)
	{
		UnityEngine.Debug.Log("didFailToLoadUrlEvent: " + url);
	}
}
