using Prime31;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GPGTurnBasedMultiplayer
{
	private static AndroidJavaObject _plugin;

	static GPGTurnBasedMultiplayer()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
		{
			GPGTurnBasedMultiplayer._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("turnBasedMultiplayerInstance", new object[0]);
		}
	}

	public static void checkForInvitesAndMatches()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("checkForInvitesAndMatches", new object[0]);
		}
	}

	public static void showInbox()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("showInbox", new object[0]);
		}
	}

	public static void showPlayerSelector(int minPlayersToPick, int maxPlayersToPick)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("showPlayerSelector", new object[]
			{
				minPlayersToPick,
				maxPlayersToPick
			});
		}
	}

	public static void createMatchProgrammatically(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("createMatchProgrammatically", new object[]
			{
				minAutoMatchPlayers,
				maxAutoMatchPlayers,
				exclusiveBitmask
			});
		}
	}

	public static void loadAllMatches()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("loadAllMatches", new object[0]);
		}
	}

	public static void takeTurn(string matchId, byte[] matchData, string pendingParticipantId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("takeTurn", new object[]
			{
				matchId,
				matchData,
				pendingParticipantId
			});
		}
	}

	public static void leaveDuringTurn(string matchId, string pendingParticipantId)
	{
		if (pendingParticipantId == null)
		{
			UnityEngine.Debug.LogWarning("leaveDuringTurn called with a null pendingParticipantId which is invalid");
			return;
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("leaveDuringTurn", new object[]
			{
				matchId,
				pendingParticipantId
			});
		}
	}

	public static void leaveOutOfTurn(string matchId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("leaveOutOfTurn", new object[]
			{
				matchId
			});
		}
	}

	public static void finishMatchWithData(string matchId, byte[] matchData, List<GPGTurnBasedParticipantResult> results)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("finishMatchWithData", new object[]
			{
				matchId,
				matchData,
				Json.encode(results)
			});
		}
	}

	public static void finishMatchWithoutData(string matchId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("finishMatchWithoutData", new object[]
			{
				matchId
			});
		}
	}

	public static void dismissMatch(string matchId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("dismissMatch", new object[]
			{
				matchId
			});
		}
	}

	public static void rematch(string matchId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("rematch", new object[]
			{
				matchId
			});
		}
	}

	public static void joinMatchWithInvitation(string invitationId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("joinMatchWithInvitation", new object[]
			{
				invitationId
			});
		}
	}

	public static void declineMatchWithInvitation(string invitationId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			GPGTurnBasedMultiplayer._plugin.Call("declineMatchWithInvitation", new object[]
			{
				invitationId
			});
		}
	}
}
