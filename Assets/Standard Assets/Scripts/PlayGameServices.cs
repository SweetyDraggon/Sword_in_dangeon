using Prime31;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameServices
{
	private static AndroidJavaObject _plugin;

	static PlayGameServices()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
		{
			PlayGameServices._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
		}
	}

	public static void setAchievementToastSettings(GPGToastPlacement placement, int offset)
	{
		PlayGameServices.setToastSettings(placement);
	}

	public static void setWelcomeBackToastSettings(GPGToastPlacement placement, int offset)
	{
		PlayGameServices.setToastSettings(placement);
	}

	public static void enableDebugLog(bool shouldEnable)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("enableDebugLog", new object[]
		{
			shouldEnable
		});
	}

	public static void setToastSettings(GPGToastPlacement placement)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("setToastSettings", new object[]
		{
			(int)placement
		});
	}

	public static string getLaunchInvitation()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return null;
		}
		return PlayGameServices._plugin.Call<string>("getLaunchInvitation", new object[0]);
	}

	public static void init(string clientId, bool requestAppStateScope, bool fetchMetadataAfterAuthentication = true, bool pauseUnityWhileShowingFullScreenViews = true)
	{
	}

	public static void attemptSilentAuthentication()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("attemptSilentAuthentication", new object[0]);
	}

	public static void authenticate()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("authenticate", new object[0]);
	}

	public static void signOut()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("signOut", new object[0]);
	}

	public static bool isSignedIn()
	{
		return Application.platform == RuntimePlatform.Android && PlayGameServices._plugin.Call<bool>("isSignedIn", new object[0]);
	}

	public static GPGPlayerInfo getLocalPlayerInfo()
	{
		GPGPlayerInfo result = new GPGPlayerInfo();
		if (Application.platform != RuntimePlatform.Android)
		{
			return result;
		}
		string json = PlayGameServices._plugin.Call<string>("getLocalPlayerInfo", new object[0]);
		return Json.decode<GPGPlayerInfo>(json, null);
	}

	public static void reloadAchievementAndLeaderboardData()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			PlayGameServices._plugin.Call("loadBasicModelData", new object[0]);
		}
	}

	public static void loadProfileImageForUri(string uri)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			PlayGameServices._plugin.Call("loadProfileImageForUri", new object[]
			{
				uri
			});
		}
	}

	public static void showShareDialog(string prefillText = null, string urlToShare = null)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			PlayGameServices._plugin.Call("showShareDialog", new object[]
			{
				prefillText,
				urlToShare
			});
		}
	}

	public static void setStateData(string data, int key)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("setStateData", new object[]
		{
			data,
			key
		});
	}

	public static string stateDataForKey(int key)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return null;
		}
		return PlayGameServices._plugin.Call<string>("stateDataForKey", new object[]
		{
			key
		});
	}

	public static void loadCloudDataForKey(int key, bool useRemoteDataForConflictResolution = true)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("loadCloudDataForKey", new object[]
		{
			key,
			useRemoteDataForConflictResolution
		});
	}

	public static void deleteCloudDataForKey(int key, bool useRemoteDataForConflictResolution = true)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("deleteCloudDataForKey", new object[]
		{
			key,
			useRemoteDataForConflictResolution
		});
	}

	public static void clearCloudDataForKey(int key, bool useRemoteDataForConflictResolution = true)
	{
	}

	public static void updateCloudDataForKey(int key, bool useRemoteDataForConflictResolution = true)
	{
		PlayGameServices.loadCloudDataForKey(key, useRemoteDataForConflictResolution);
	}

	public static void showAchievements()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("showAchievements", new object[0]);
	}

	public static void revealAchievement(string achievementId)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("revealAchievement", new object[]
		{
			achievementId
		});
	}

	public static void unlockAchievement(string achievementId, bool showsCompletionNotification = true)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("unlockAchievement", new object[]
		{
			achievementId,
			showsCompletionNotification
		});
	}

	public static void incrementAchievement(string achievementId, int numSteps)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("incrementAchievement", new object[]
		{
			achievementId,
			numSteps
		});
	}

	public static List<GPGAchievementMetadata> getAllAchievementMetadata()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return new List<GPGAchievementMetadata>();
		}
		string json = PlayGameServices._plugin.Call<string>("getAllAchievementMetadata", new object[0]);
		return Json.decode<List<GPGAchievementMetadata>>(json, null);
	}

	public static void showLeaderboard(string leaderboardId, GPGLeaderboardTimeScope timeScope = GPGLeaderboardTimeScope.AllTime)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("showLeaderboard", new object[]
		{
			leaderboardId
		});
	}

	public static void showLeaderboards()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("showLeaderboards", new object[0]);
	}

	public static void submitScore(string leaderboardId, long score)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("submitScore", new object[]
		{
			leaderboardId,
			score
		});
	}

	public static void loadScoresForLeaderboard(string leaderboardId, GPGLeaderboardTimeScope timeScope, bool isSocial, bool personalWindow)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		PlayGameServices._plugin.Call("loadScoresForLeaderboard", new object[]
		{
			leaderboardId,
			(int)timeScope,
			isSocial,
			personalWindow
		});
	}

	public static List<GPGLeaderboardMetadata> getAllLeaderboardMetadata()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return new List<GPGLeaderboardMetadata>();
		}
		string json = PlayGameServices._plugin.Call<string>("getAllLeaderboardMetadata", new object[0]);
		return Json.decode<List<GPGLeaderboardMetadata>>(json, null);
	}
}
