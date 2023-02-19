using System;
using UnityEngine;

public class DynamicSFX : MonoBehaviour
{
	public AudioSource audioSrc;

	public GameObject attachedGameObject;

	public float forcedZ;

	public float gain = 1f;

	public float maxGain = 1f;

	private void Start()
	{
	}

	private void Awake()
	{
		this.UpdateSoundPosition();
	}

	private void Update()
	{
		this.UpdateSoundPosition();
		this.UpdateSoundVolume();
	}

	public void UpdateSoundPosition()
	{
		if (this.attachedGameObject != null)
		{
			Vector3 position = base.gameObject.transform.position;
			position.x = this.attachedGameObject.transform.position.x;
			position.y = this.attachedGameObject.transform.position.y;
			position.z = this.forcedZ;
			base.gameObject.transform.position = position;
		}
		else
		{
			Vector3 position2 = base.gameObject.transform.position;
			position2.z = this.forcedZ;
			base.gameObject.transform.position = position2;
		}
	}

	public void UpdateSoundVolume()
	{
		float num = 0f;
		float num2 = Vector3.Distance(AudioManager.Instance.gameObject.transform.position, base.gameObject.transform.position);
		if (num <= 0f)
		{
			num = tk2dCamera.Instance.ScreenExtents.width * 2f;
		}
		float num3 = this.gain;
		float num4 = Mathf.Max(0f, num3 - num3 / num * num2);
		if (num4 > this.maxGain)
		{
			num4 = this.maxGain;
		}
		this.audioSrc.volume = num4 * AudioManager.Instance.sfxVolume;
	}
}
