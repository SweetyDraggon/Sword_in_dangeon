using Prime31;
using System;

public class GPGTurnBasedParticipant
{
	public GPGPlayerInfo player;

	public string participantId;

	public bool isAutoMatchedPlayer;

	public int statusInt;

	public GPGTurnBasedParticipantStatus status
	{
		get
		{
			return (GPGTurnBasedParticipantStatus)((int)Enum.ToObject(typeof(GPGTurnBasedParticipantStatus), this.statusInt));
		}
	}

	public string statusString
	{
		get
		{
			return this.status.ToString();
		}
	}

	public override string ToString()
	{
		return JsonFormatter.prettyPrint(Json.encode(this));
	}
}
