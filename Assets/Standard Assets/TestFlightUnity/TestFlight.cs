/*
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TestFlightUnity
{
	public static class TestFlight
	{
		private static AndroidJavaClass tf;

		[DllImport("__Internal")]
		private static extern void TF_TakeOff(string token);

		[DllImport("__Internal")]
		private static extern void TF_PassCheckpoint(string checkpoint);

		[DllImport("__Internal")]
		private static extern void TF_AddCustomEnvironmentInformation(string info, string key);

		[DllImport("__Internal")]
		private static extern void TF_OpenFeedbackView();

		[DllImport("__Internal")]
		private static extern void TF_SendFeedback(string feedback);

		[DllImport("__Internal")]
		private static extern void TF_Log(string msg);

		[DllImport("__Internal")]
		private static extern void TF_Crash();

		public static void TakeOff(string token)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_TakeOff(token);
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						using (AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", new object[0]))
						{
							TestFlight.CallJavaTF("takeOff", new object[]
							{
								androidJavaObject,
								token
							});
						}
					}
				}
			}
		}

		public static void PassCheckpoint(string checkpoint)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_PassCheckpoint(checkpoint);
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				TestFlight.CallJavaTF("passCheckpoint", new object[]
				{
					checkpoint
				});
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: PassCheckpoint is not supported on your platform");
			}
		}

		public static void AddCustomEnvironmentInformation(string info, string key)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_AddCustomEnvironmentInformation(info, key);
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: AddCustomEnvironmentInformation is not supported on your platform");
			}
		}

		public static void OpenFeedbackView()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_OpenFeedbackView();
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: OpenFeedbackView is not supported on your platform");
			}
		}

		public static void SendFeedback(string feedback)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_SendFeedback(feedback);
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: SendFeedback is not supported on your platform");
			}
		}

		public static void Log(string msg)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_Log(msg);
			}
			else if (Application.platform == RuntimePlatform.Android)
			{
				TestFlight.CallJavaTF("log", new object[]
				{
					msg
				});
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: Logging is not supported on your platform");
			}
		}

		public static void Crash()
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				TestFlight.TF_Crash();
			}
			else
			{
				UnityEngine.Debug.LogWarning("TestFlight: Crash is not supported on your platform");
			}
		}

		private static void CallJavaTF(string method, params object[] args)
		{
			if (TestFlight.tf == null)
			{
				TestFlight.tf = new AndroidJavaClass("com.testflightapp.lib.TestFlight");
			}
			TestFlight.tf.CallStatic(method, args);
		}
	}
}
*/