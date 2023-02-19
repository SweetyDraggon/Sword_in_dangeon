using System;
using UnityEngine;

public class ApplyColorOnAwake : MonoBehaviour
{
	public Color color;

	private void Awake()
	{
		GetComponent<Renderer>().material.color = this.color;
	}
}
