using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CountlyUtil
{
	public static class DeviceInformation
	{
		public static string UDID;

		public static string OS;

		public static string OSVersion;

		public static string Device;

		public static string Resolution;

		public static string Carrier;

		public static string Locale;
	}

	public static Coroutine RunCoroutine(IEnumerator coroutine)
	{
		if (Countly.Instance == null)
		{
			Countly.Log("Countly game object must be instantiated before its methods can be called.");
			return null;
		}
		return Countly.Instance.StartCoroutine(coroutine);
	}

	public static void DetectDevice()
	{
		CountlyUtil.DeviceInformation.UDID = SystemInfo.deviceUniqueIdentifier;
		CountlyUtil.DeviceInformation.OSVersion = Environment.OSVersion.Version.ToString();
		CountlyUtil.DeviceInformation.Resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
		CountlyUtil.DeviceInformation.Carrier = null;
		CountlyUtil.DeviceInformation.Locale = Application.systemLanguage.ToString();
	}

	public static string GetMetrics(string appVersion)
	{
		string str = "{";
		str = str + "\"_device\":\"" + CountlyUtil.DeviceInformation.Device + "\"";
		str = str + ",\"_os\":\"" + CountlyUtil.DeviceInformation.OS + "\"";
		str = str + ",\"_os_version\":\"" + CountlyUtil.DeviceInformation.OSVersion + "\"";
		str = str + ",\"_carrier\":\"" + CountlyUtil.DeviceInformation.Carrier + "\"";
		str = str + ",\"_resolution\":\"" + CountlyUtil.DeviceInformation.Resolution + "\"";
		str = str + ",\"_local\":\"" + CountlyUtil.DeviceInformation.Locale + "\"";
		str = str + ",\"_app_version\":\"" + appVersion + "\"";
		return str + "}";
	}

	public static string SeriliazeEvents(List<CountlyEvent> events)
	{
		string text = string.Empty;
		bool flag = true;
		text += "[";
		foreach (CountlyEvent current in events)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				text += ",";
			}
			text += "{";
			text = text + "\"key\":\"" + current.Key + "\",";
			text = text + "\"count\":" + current.Count;
			if (current.UsingSum)
			{
				text += ",";
				text = text + "\"sum\":" + current.Sum;
			}
			if (current.UsingSegmentation)
			{
				text += ",";
				text += "\"segmentation\":{";
				bool flag2 = true;
				foreach (string current2 in current.Segmentation.Keys)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						text += ",";
					}
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\"",
						current2,
						"\":\"",
						current.Segmentation[current2],
						"\""
					});
				}
				text += "}";
			}
			text += "}";
		}
		text += "]";
		return text;
	}
}
