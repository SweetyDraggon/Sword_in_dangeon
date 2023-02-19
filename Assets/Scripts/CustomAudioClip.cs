using System;
using UnityEngine;

[Serializable]
public class CustomAudioClip
{
	public bool repeat;

	public Vector2 volumeRange = new Vector2(1f, 1f);

	public Vector2 pitchRange = new Vector2(1f, 1f);

	public AudioClip audioClip;

	public int priority = 128;
}
