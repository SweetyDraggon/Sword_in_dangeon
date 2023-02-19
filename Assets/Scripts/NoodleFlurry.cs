using System;
using UnityEngine;

public class NoodleFlurry : MonoBehaviour
{
	private const string DD_FLURRY_KEY = "8BWZC3JX39RWKG76CHPD";

	private static NoodleFlurry _instance;

	public static void init()
	{
		if (NoodleFlurry._instance != null)
		{
			return;
		}
		GameObject gameObject = new GameObject("NoodleFlurry");
		NoodleFlurry._instance = gameObject.AddComponent<NoodleFlurry>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
        /*
		UnityEngine.Debug.Log("***** NOODLE FLURRY START");
		if (Application.platform == RuntimePlatform.Android)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.flurry.android.FlurryAgent");
				androidJavaClass2.CallStatic("onStartSession", new object[]
				{
					@static,
					"8BWZC3JX39RWKG76CHPD"
				});
			}
		}
		*/
	}
}
