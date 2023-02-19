using System;
using UnityEngine;

namespace REXEngine
{
	[ExecuteInEditMode]
	public class ShadowText : MonoBehaviour
	{
		private tk2dTextMesh text;

		private tk2dTextMesh shadowText;

		private void Start()
		{
		}

		private void Update()
		{
			if (!this.text)
			{
				this.text = base.GetComponent<tk2dTextMesh>();
			}
			if (this.text)
			{
				if (!this.shadowText)
				{
					this.shadowText = base.gameObject.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
				}
				if (this.shadowText && (this.shadowText.scale != this.text.scale || this.shadowText.text != this.text.text))
				{
					this.shadowText.scale = this.text.scale;
					this.shadowText.text = this.text.text;
					this.shadowText.Commit();
				}
			}
		}
	}
}
