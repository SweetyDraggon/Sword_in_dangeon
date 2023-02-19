using Prime31;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GPGTurnBasedManager : AbstractManager
{






















	public static event Action<GPGTurnBasedMatch> matchChangedEvent;

	public static event Action<string> matchFailedEvent;

	public static event Action<GPGTurnBasedMatch> matchEndedEvent;

	public static event Action playerSelectorCanceledEvent;

	public static event Action<bool, string, List<GPGTurnBasedMatch>> loadMatchesCompletedEvent;

	public static event Action<bool, string> takeTurnCompletedEvent;

	public static event Action<bool, string> finishMatchCompletedEvent;

	public static event Action<bool, string> dismissMatchCompletedEvent;

	public static event Action<bool, string> leaveDuringTurnCompletedEvent;

	public static event Action<bool, string> leaveOutOfTurnCompletedEvent;

	public static event Action<GPGTurnBasedInvitation> invitationReceivedEvent;

	static GPGTurnBasedManager()
	{
		AbstractManager.initialize(typeof(GPGTurnBasedManager));
	}

	private void matchChanged(string json)
	{
		if (GPGTurnBasedManager.matchChangedEvent != null)
		{
			GPGTurnBasedManager.matchChangedEvent(Json.decode<GPGTurnBasedMatch>(json, null));
		}
	}

	private void matchFailed(string error)
	{
		GPGTurnBasedManager.matchFailedEvent.fire(error);
	}

	private void matchEnded(string json)
	{
		if (GPGTurnBasedManager.matchEndedEvent != null)
		{
			GPGTurnBasedManager.matchEndedEvent(Json.decode<GPGTurnBasedMatch>(json, null));
		}
	}

	private void playerSelectorCanceled(string empty)
	{
		GPGTurnBasedManager.playerSelectorCanceledEvent.fire();
	}

	private void loadMatchesFailed(string error)
	{
		GPGTurnBasedManager.loadMatchesCompletedEvent(false, error, null);
	}

	private void loadMatchesSucceeded(string json)
	{
		UnityEngine.Debug.Log("WOOOT " + json);
		if (GPGTurnBasedManager.loadMatchesCompletedEvent != null)
		{
			GPGTurnBasedManager.loadMatchesCompletedEvent.fire(true, null, Json.decode<List<GPGTurnBasedMatch>>(json, null));
		}
	}

	private void takeTurnFailed(string error)
	{
		GPGTurnBasedManager.takeTurnCompletedEvent.fire(false, error);
	}

	private void takeTurnSucceeded(string empty)
	{
		GPGTurnBasedManager.takeTurnCompletedEvent.fire(true, null);
	}

	private void finishMatchFailed(string error)
	{
		GPGTurnBasedManager.finishMatchCompletedEvent.fire(false, error);
	}

	private void finishMatchSucceeded(string empty)
	{
		GPGTurnBasedManager.finishMatchCompletedEvent.fire(true, null);
	}

	private void dismissMatchFailed(string error)
	{
		GPGTurnBasedManager.dismissMatchCompletedEvent.fire(false, error);
	}

	private void dismissMatchSucceeded(string empty)
	{
		GPGTurnBasedManager.dismissMatchCompletedEvent.fire(true, null);
	}

	private void leaveDuringTurnFailed(string error)
	{
		GPGTurnBasedManager.leaveDuringTurnCompletedEvent.fire(false, error);
	}

	private void leaveDuringTurnSucceeded(string empty)
	{
		GPGTurnBasedManager.leaveDuringTurnCompletedEvent.fire(true, null);
	}

	private void leaveOutOfTurnFailed(string error)
	{
		GPGTurnBasedManager.leaveOutOfTurnCompletedEvent.fire(false, error);
	}

	private void leaveOutOfTurnSucceeded(string empty)
	{
		GPGTurnBasedManager.leaveOutOfTurnCompletedEvent.fire(true, null);
	}

	private void invitationReceived(string json)
	{
		if (GPGTurnBasedManager.invitationReceivedEvent != null)
		{
			GPGTurnBasedManager.invitationReceivedEvent(Json.decode<GPGTurnBasedInvitation>(json, null));
		}
	}
}
