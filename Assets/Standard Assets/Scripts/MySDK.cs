/*
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MySDK
{
	public static void OpenURL(string desturl)
	{
		Application.OpenURL(desturl);
	}

	[DllImport("__Internal")]
	public static extern void _alertView(string title, string message);

	public static void AlertView(string title, string message)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			UnityEngine.Debug.LogError("Alert! " + title + "\n" + message);
		}
	}
}
*/