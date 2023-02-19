using PlayHaven;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillGroup : MonoBehaviour
{
	private List<KillWhenPlayerHits> killObjects;

//	private PlayHavenContentRequester requestor;

	private void Awake()
	{
		KillWhenPlayerHits[] componentsInChildren = base.GetComponentsInChildren<KillWhenPlayerHits>();
		this.killObjects = componentsInChildren.ToList<KillWhenPlayerHits>();
		foreach (KillWhenPlayerHits current in this.killObjects)
		{
			current.RegisterKillGroup(this);
		}
		//this.requestor = base.GetComponent<PlayHavenContentRequester>();
	}

	public void ReportDeath(KillWhenPlayerHits deadObject)
	{
		if (this.killObjects.Contains(deadObject))
		{
			this.killObjects.Remove(deadObject);
		}
		if (this.killObjects.Count == 0)
		{
			this.CallPlacement();
		}
	}

	private void CallPlacement()
	{
		//if (this.requestor != null)
		{
		//	this.requestor.Request();
		}
		base.SendMessage("AdvanceIt", SendMessageOptions.DontRequireReceiver);
	}
}
