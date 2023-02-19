using Prime31;
using System;
using System.Collections.Generic;

public class GPGTurnBasedMatch
{
	private string data;

	public bool canRematch;

	public string matchDescription;

	public string matchId;

	public int matchNumber;

	public int matchVersion;

	public string pendingParticipantId;

	public string localParticipantId;

	public int statusInt;

	public int userMatchStatusInt;

	public int availableAutoMatchSlots;

	public List<GPGTurnBasedParticipant> players;

	public bool hasDataAvailable
	{
		get
		{
			return this.data != null;
		}
	}

	public byte[] matchData
	{
		get
		{
			return (this.data == null) ? null : Convert.FromBase64String(this.data);
		}
	}

	public GPGTurnBasedMatchStatus status
	{
		get
		{
			return (GPGTurnBasedMatchStatus)((int)Enum.ToObject(typeof(GPGTurnBasedMatchStatus), this.statusInt));
		}
	}

	public string statusString
	{
		get
		{
			return this.status.ToString();
		}
	}

	public GPGTurnBasedUserMatchStatus userMatchStatus
	{
		get
		{
			return (GPGTurnBasedUserMatchStatus)((int)Enum.ToObject(typeof(GPGTurnBasedUserMatchStatus), this.userMatchStatusInt));
		}
	}

	public string userMatchStatusString
	{
		get
		{
			return this.userMatchStatus.ToString();
		}
	}

	public bool isLocalPlayersTurn
	{
		get
		{
			return this.userMatchStatus == GPGTurnBasedUserMatchStatus.YourTurn;
		}
	}

	public override string ToString()
	{
		return JsonFormatter.prettyPrint(Json.encode(this));
	}
}
