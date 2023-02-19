/*
using Prime31;
using System;
using UnityEngine;

public class ChartboostUIManager : MonoBehaviourGUI
{
	private void OnGUI()
	{
		base.beginColumn();
		if (GUILayout.Button("Init", new GUILayoutOption[0]))
		{
			ChartboostAndroid.init("4f7b433509b6025804000002", "dd2d41b69ac01b80f443f5b6cf06096d457f82bd", true);
		}
		if (GUILayout.Button("Use Activities for Adverts", new GUILayoutOption[0]))
		{
			ChartboostAndroid.setImpressionsUseActivities(true);
		}
		if (GUILayout.Button("Cache Interstitial", new GUILayoutOption[0]))
		{
			ChartboostAndroid.cacheInterstitial(CBLocation.ItemStore);
		}
		if (GUILayout.Button("Check for Cached Interstitial", new GUILayoutOption[0]))
		{
			UnityEngine.Debug.Log("has cached interstitial: " + ChartboostAndroid.hasCachedInterstitial(CBLocation.ItemStore));
		}
		if (GUILayout.Button("Show Interstitial", new GUILayoutOption[0]))
		{
			ChartboostAndroid.showInterstitial(CBLocation.ItemStore);
		}
		if (GUILayout.Button("Cache More Apps", new GUILayoutOption[0]))
		{
			ChartboostAndroid.cacheMoreApps();
		}
		if (GUILayout.Button("Has Cached More Apps", new GUILayoutOption[0]))
		{
			UnityEngine.Debug.Log("has cached more apps: " + ChartboostAndroid.hasCachedMoreApps());
		}
		if (GUILayout.Button("Show More Apps", new GUILayoutOption[0]))
		{
			ChartboostAndroid.showMoreApps();
		}
		base.endColumn();
	}
}
*/