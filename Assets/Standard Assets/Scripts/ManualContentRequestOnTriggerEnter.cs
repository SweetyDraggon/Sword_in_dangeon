using PlayHaven;
using System;
using UnityEngine;

public class ManualContentRequestOnTriggerEnter : MonoBehaviour
{
//	private PlayHavenContentRequester requestor;

	private new Light light;

	private void Awake()
	{
		//this.requestor = base.GetComponent<PlayHavenContentRequester>();
		//this.light = base.GetComponentInChildren<Light>();
	}

	private void OnTriggerEnter(Collider other)
	{
        /*
		if (this.requestor != null && other.tag == "Player")
		{
			this.requestor.Request();
			this.light.enabled = !this.requestor.IsExhausted;
		}
		*/
	}
}
