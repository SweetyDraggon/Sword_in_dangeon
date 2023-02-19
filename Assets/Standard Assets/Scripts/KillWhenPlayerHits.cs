using System;
using UnityEngine;

public class KillWhenPlayerHits : MonoBehaviour
{
	private KillGroup killGroup;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.killGroup.ReportDeath(this);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void RegisterKillGroup(KillGroup group)
	{
		this.killGroup = group;
	}
}
