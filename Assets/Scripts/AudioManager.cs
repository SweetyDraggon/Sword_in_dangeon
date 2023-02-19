using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;

	public AudioSource sfxSource;

	public AudioSource musicSource;

	public bool sfxEnabled = true;

	public bool musicEnabled = true;

	public float masterGainSFX = 1f;

	public float musicVolume = 0.25f;

	public float sfxVolume = 1f;

	public MusicClip[] musicClips;

	public SFXClip[] sfxClips;

	private string currentMusic = string.Empty;

	private GameObject sfxGameObject;

	private GameObject musicGameObject;

	private float nextPrune = 5f;

	public static AudioManager Instance
	{
		get
		{
			if (AudioManager.instance == null)
			{
				GameObject gameObject = new GameObject();
				AudioManager.instance = gameObject.AddComponent<AudioManager>();
			}
			return AudioManager.instance;
		}
	}

	public void initAudioManager()
	{
		AudioManager.instance = this;
		UnityEngine.Debug.Log("AudioManager Initializing");
		this.sfxGameObject = new GameObject("SFX");
		this.musicGameObject = new GameObject("Music");
		Transform arg_59_0 = this.musicGameObject.transform;
		Transform transform = base.gameObject.transform;
		this.sfxGameObject.transform.parent = transform;
		arg_59_0.parent = transform;
		this.sfxSource = this.sfxGameObject.AddComponent<AudioSource>();
		this.musicSource = this.musicGameObject.AddComponent<AudioSource>();
		this.updateVolume();
	}

	private void Update()
	{
		this.updateVolume();
		this.nextPrune -= Time.deltaTime;
		if (this.nextPrune <= 0f)
		{
			this.nextPrune = 5f;
			this.pruneGameObjects();
		}
	}

	private void updateVolume()
	{
		
		this.musicVolume = Mathf.Max(0f, Mathf.Min(this.musicVolume, 1f));
		this.sfxVolume = Mathf.Max(0f, Mathf.Min(this.sfxVolume, 1f));
		this.musicEnabled = (this.musicVolume > 0f);
		this.sfxEnabled = (this.sfxVolume > 0f);
		this.musicSource.volume = ((!this.musicEnabled) ? 0f : this.musicVolume);
		this.sfxSource.volume = ((!this.sfxEnabled) ? 0f : this.sfxVolume);
		
	}

	private void pruneGameObjects()
	{
		if (this.sfxGameObject)
		{
			foreach (Transform transform in this.sfxGameObject.transform)
			{
				CustomAudioClipProperties component = transform.gameObject.GetComponent<CustomAudioClipProperties>();
				AudioSource audioSource = component.audioSource;
				if (!audioSource.isPlaying && component.destroyOnFinished)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
	}

	public GameObject PlaySound(string soundName)
	{
		return this.PlaySoundDelayed(soundName, 0f, null, true);
	}

	public GameObject PlaySound(string soundName, GameObject attachGameObject)
	{
		return this.PlaySoundDelayed(soundName, 0f, attachGameObject, true);
	}

	public GameObject PlaySound(string soundName, GameObject attachGameObject, bool destroyOnFinish)
	{
		return this.PlaySoundDelayed(soundName, 0f, attachGameObject, destroyOnFinish);
	}

	public GameObject PlaySoundDelayed(string soundName, float delay, GameObject attachGameObject = null, bool destroyOnFinish = true)
	{
		if (this.sfxClips == null)
		{
			return null;
		}
		SFXClip[] array = this.sfxClips;
		for (int i = 0; i < array.Length; i++)
		{
			SFXClip sFXClip = array[i];
			if (sFXClip.name == soundName && sFXClip.audioClip.Length > 0)
			{
				int num = 0;
				if (sFXClip.audioClip.Length > 0)
				{
					num = UnityEngine.Random.Range(0, sFXClip.audioClip.Length);
				}
				GameObject gameObject = new GameObject(sFXClip.name);
				gameObject.transform.parent = this.sfxGameObject.transform;
				AudioSource audioSource = gameObject.AddComponent<AudioSource>();
				audioSource.clip = sFXClip.audioClip[num].audioClip;
				audioSource.loop = sFXClip.audioClip[num].repeat;
				audioSource.priority = sFXClip.audioClip[num].priority;
				CustomAudioClipProperties customAudioClipProperties = gameObject.AddComponent<CustomAudioClipProperties>();
				customAudioClipProperties.clip = sFXClip.audioClip[num];
				customAudioClipProperties.audioSource = audioSource;
				customAudioClipProperties.destroyOnFinished = destroyOnFinish;
				audioSource.minDistance = 50f;
				audioSource.maxDistance = 120f;
				audioSource.spatialBlend = 0.25f;
				audioSource.dopplerLevel = 0f;
				audioSource.volume = UnityEngine.Random.Range(sFXClip.audioClip[num].volumeRange.x, sFXClip.audioClip[num].volumeRange.y) * this.sfxVolume * this.masterGainSFX;
				audioSource.pitch = UnityEngine.Random.Range(sFXClip.audioClip[num].pitchRange.x, sFXClip.audioClip[num].pitchRange.y);
				if (attachGameObject != null)
				{
					DynamicSFX dynamicSFX = gameObject.AddComponent<DynamicSFX>();
					dynamicSFX.attachedGameObject = attachGameObject;
					dynamicSFX.audioSrc = audioSource;
					dynamicSFX.gain = audioSource.volume;
					dynamicSFX.maxGain = sFXClip.audioClip[num].volumeRange.y;
					dynamicSFX.enabled = true;
				}
				if (delay > 0f)
				{
					audioSource.Play();
				}
				else
				{
					audioSource.PlayDelayed(delay);
				}
				return gameObject;
			}
		}
		return null;
	}

	public void PlayMusic(string musicName)
	{
		if (this.currentMusic == musicName)
		{
			return;
		}
		MusicClip[] array = this.musicClips;
		for (int i = 0; i < array.Length; i++)
		{
			MusicClip musicClip = array[i];
			if (musicClip.name == musicName)
			{
				this.currentMusic = musicName;
				this.musicSource.Stop();
				this.musicSource.loop = musicClip.audioClip[0].repeat;
				this.musicSource.volume = ((!this.musicEnabled) ? 0f : UnityEngine.Random.Range(musicClip.audioClip[0].volumeRange.x, musicClip.audioClip[0].volumeRange.y));
				if (musicClip.audioClip.Length > 0)
				{
					if (musicClip.audioClip.Length > 1)
					{
						AudioClip clip = this.musicSource.clip;
						do
						{
							int num = UnityEngine.Random.Range(0, musicClip.audioClip.Length);
							this.musicSource.clip = musicClip.audioClip[num].audioClip;
						}
						while (this.musicSource.clip == clip);
					}
					else
					{
						this.musicSource.clip = musicClip.audioClip[0].audioClip;
					}
					this.musicSource.Play();
				}
				break;
			}
		}
	}

	public bool IsMusicDonePlaying()
	{
		return !this.musicSource.isPlaying && this.musicSource.time == 0f;
	}
}
