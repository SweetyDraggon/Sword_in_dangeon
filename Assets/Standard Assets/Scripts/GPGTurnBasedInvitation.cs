using Prime31;
using System;

public class GPGTurnBasedInvitation
{
	public GPGTurnBasedParticipant invitingParticipant;

	public GPGTurnBasedMatch match;

	public override string ToString()
	{
		return JsonFormatter.prettyPrint(Json.encode(this));
	}
}
