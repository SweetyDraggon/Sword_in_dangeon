using System;
using UnityEngine;

public class AdvanceTutorial : MonoBehaviour
{
	public TutorialController.TutorialPhase phaseToTrigger;

	private bool hasBeenTriggered;

	private void OnTriggerEnter(Collider other)
	{
		if (!this.hasBeenTriggered && other.tag == "Player")
		{
			TutorialController.AdvanceTutorial(this.phaseToTrigger);
			this.hasBeenTriggered = true;
		}
	}
}
