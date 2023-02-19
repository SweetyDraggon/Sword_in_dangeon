using System;
using UnityEngine;

namespace REXEngine
{
	public class IPAddress : MonoBehaviour
	{
		public tk2dTextMesh textMesh;

		private void Start()
		{
			if (this.textMesh == null)
			{
				this.textMesh = base.gameObject.GetComponent<tk2dTextMesh>();
			}
			if (this.textMesh != null)
			{
				//this.textMesh.text = "IP: " + Network.player.ipAddress;
			}
		}

		private void Update()
		{
		}
	}
}
