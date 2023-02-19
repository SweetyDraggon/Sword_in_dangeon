using System;
using UnityEngine;

public class Debugger : MonoBehaviour
{
	private static Debugger instance;

	public static Debugger Instance
	{
		get
		{
			return Debugger.instance;
		}
	}

	private void Awake()
	{
		Debugger.instance = this;
	}

	public void DrawRect(Rectangle rect, Color color)
	{
		if (rect != null)
		{
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.x, (float)rect.y, 0f), new Vector3((float)(rect.x + rect.width), (float)rect.y, 0f), color);
			UnityEngine.Debug.DrawLine(new Vector3((float)(rect.x + rect.width), (float)rect.y, 0f), new Vector3((float)(rect.x + rect.width), (float)(rect.y + rect.height), 0f), color);
			UnityEngine.Debug.DrawLine(new Vector3((float)(rect.x + rect.width), (float)(rect.y + rect.height), 0f), new Vector3((float)rect.x, (float)(rect.y + rect.height), 0f), color);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.x, (float)(rect.y + rect.height), 0f), new Vector3((float)rect.x, (float)rect.y, 0f), color);
		}
	}

	public void DrawRect(Rectangle rect)
	{
		this.DrawRect(rect, new Color(0f, 1f, 0f, 1f));
	}
}
