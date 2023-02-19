using System;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
	}
}
