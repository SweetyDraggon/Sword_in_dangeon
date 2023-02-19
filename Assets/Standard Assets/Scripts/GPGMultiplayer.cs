using Prime31;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GPGMultiplayer
{
	private class RealTimeMessageReceivedListener : AndroidJavaProxy
	{
		public RealTimeMessageReceivedListener() : base("com.prime31.IRealTimeMessageReceivedListener")
		{
		}

		public void onMessageReceived(string senderParticipantId, string messageData)
		{
			byte[] message = Convert.FromBase64String(messageData);
			GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId, message);
		}

		public void onRawMessageReceived(AndroidJavaObject senderParticipantId, AndroidJavaObject messageData)
		{
			string senderParticipantId2 = senderParticipantId.Call<string>("toString", new object[0]);
			byte[] message = AndroidJNI.FromByteArray(messageData.GetRawObject());
			GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId2, message);
		}

		public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] args)
		{
			if (methodName == "onRawMessageReceived")
			{
				this.onRawMessageReceived(args[0], args[1]);
				return null;
			}
			return base.Invoke(methodName, args);
		}

		public string toString()
		{
			return "RealTimeMessageReceivedListener class instance from Unity";
		}
	}

	private static AndroidJavaObject _plugin;

	static GPGMultiplayer()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
		{
			GPGMultiplayer._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("realtimeMultiplayerInstance", new object[0]);
			GPGMultiplayer._plugin.Call("setRealtimeMessageListener", new object[]
			{
				new GPGMultiplayer.RealTimeMessageReceivedListener()
			});
		}
	}

	public static void registerDeviceToken(byte[] deviceToken, bool isProductionEnvironment)
	{
	}

	public static void showInvitationInbox()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("showInvitationInbox", new object[0]);
	}

	public static void startQuickMatch(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0L)
	{
		GPGMultiplayer.createRoomProgrammatically(minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask);
	}

	public static void createRoomProgrammatically(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0L)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("createRoomProgrammatically", new object[]
		{
			minAutoMatchPlayers,
			maxAutoMatchPlayers,
			exclusiveBitmask
		});
	}

	public static void showPlayerSelector(int minPlayers, int maxPlayers)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("showPlayerSelector", new object[]
		{
			minPlayers,
			maxPlayers
		});
	}

	public static void joinRoomWithInvitation(string invitationId)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("joinRoomWithInvitation", new object[]
		{
			invitationId
		});
	}

	public static void showWaitingRoom(int minParticipantsToStart)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("showWaitingRoom", new object[]
		{
			minParticipantsToStart
		});
	}

	public static void leaveRoom()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("leaveRoom", new object[0]);
	}

	public static GPGRoom getRoom()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return new GPGRoom();
		}
		string json = GPGMultiplayer._plugin.Call<string>("getRoom", new object[0]);
		return Json.decode<GPGRoom>(json, null);
	}

	public static List<GPGMultiplayerParticipant> getParticipants()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return null;
		}
		string json = GPGMultiplayer._plugin.Call<string>("getParticipants", new object[0]);
		return Json.decode<List<GPGMultiplayerParticipant>>(json, null);
	}

	public static string getCurrentPlayerParticipantId()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return null;
		}
		return GPGMultiplayer._plugin.Call<string>("getCurrentPlayerParticipantId", new object[0]);
	}

	public static void sendReliableRealtimeMessage(string participantId, byte[] message)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("sendReliableRealtimeMessage", new object[]
		{
			participantId,
			message
		});
	}

	public static void sendReliableRealtimeMessageToAll(byte[] message)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("sendReliableRealtimeMessageToAll", new object[]
		{
			message
		});
	}

	public static void sendUnreliableRealtimeMessage(string participantId, byte[] message)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("sendUnreliableRealtimeMessage", new object[]
		{
			participantId,
			message
		});
	}

	public static void sendUnreliableRealtimeMessageToAll(byte[] message)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		GPGMultiplayer._plugin.Call("sendUnreliableRealtimeMessageToAll", new object[]
		{
			message
		});
	}
}
