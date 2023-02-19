using System;
using UnityEngine;

namespace REXEngine
{
	public class FPSCounter : MonoBehaviour
	{
		public tk2dTextMesh textMesh;

		private int m_frameCounter;

		private float m_timeCounter;

		private float m_lastFramerate;

		public float m_refreshTime = 0.5f;

		private void Start()
		{
			if (this.textMesh == null)
			{
				this.textMesh = base.gameObject.GetComponent<tk2dTextMesh>();
			}
		}

		private void Update()
		{
			if (this.m_timeCounter < this.m_refreshTime)
			{
				this.m_timeCounter += Time.deltaTime;
				this.m_frameCounter++;
			}
			else
			{
				this.m_lastFramerate = (float)this.m_frameCounter / this.m_timeCounter;
				this.m_frameCounter = 0;
				this.m_timeCounter = 0f;
				if (this.textMesh)
				{
					this.textMesh.text = "FPS: " + this.m_lastFramerate.ToString("N0");
				}
			}
		}
	}
}
