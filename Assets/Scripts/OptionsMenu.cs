using System;
using UnityEngine;

public class OptionsMenu : ShopWindow
{
	public GameObject musicNotches;

	public GameObject sfxNotches;

	public ControlScales controlScales;

	public int state = 1;

	public int musicVolume = 10;

	public int sfxVolume = 10;

	public float originalMusicVolume;

	public float originalSFXVolume;

	public void Awake()
	{
		this.musicVolume = (int)Mathf.Floor(AudioManager.Instance.musicVolume * 10f);
		this.sfxVolume = (int)Mathf.Floor(AudioManager.Instance.sfxVolume * 10f);
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
		base.onSlideInStart += new CustomWindowEvent(this.activate);
		this.updateView();
		this.handleResolutions();
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
	}

	public void ThisDestroy()
	{
		WindowManager.Instance.RemoveMenu(this);
	}
	public void activate()
	{
		this.musicVolume = (int)Mathf.Floor(AudioManager.Instance.musicVolume * 10f);
		this.sfxVolume = (int)Mathf.Floor(AudioManager.Instance.sfxVolume * 10f);
		this.originalMusicVolume = AudioManager.Instance.musicVolume;
		this.originalSFXVolume = AudioManager.Instance.sfxVolume;
		this.updateView();
	}

	public override void closeClicked()
	{
		AudioManager.Instance.musicVolume = this.originalMusicVolume;
		AudioManager.Instance.sfxVolume = this.originalSFXVolume;
		this.deactivate();
	}

	public void deactivate()
	{
		WindowManager.Instance.HideMenu(this);
	}

	public void controlsClicked()
	{
	}

	public void saveClicked()
	{
		this.controlScales.applySetting();
		SaveGame.SavePrefs();
		this.deactivate();
	}

	public void musicVolumeDown()
	{
		this.musicVolume--;
		this.musicVolume = Mathf.Min(Mathf.Max(0, this.musicVolume), 10);
		AudioManager.Instance.musicVolume = (float)this.musicVolume / 10f;
		this.updateNotches();
	}

	public void musicVolumeUp()
	{
		this.musicVolume++;
		this.musicVolume = Mathf.Min(Mathf.Max(0, this.musicVolume), 10);
		AudioManager.Instance.musicVolume = (float)this.musicVolume / 10f;
		this.updateNotches();
	}

	public void sfxVolumeDown()
	{
		this.sfxVolume--;
		this.sfxVolume = Mathf.Min(Mathf.Max(0, this.sfxVolume), 10);
		AudioManager.Instance.sfxVolume = (float)this.sfxVolume / 10f;
		AudioManager.Instance.PlaySound("hit");
		this.updateNotches();
	}

	public void sfxVolumeUp()
	{
		this.sfxVolume++;
		this.sfxVolume = Mathf.Min(Mathf.Max(0, this.sfxVolume), 10);
		AudioManager.Instance.sfxVolume = (float)this.sfxVolume / 10f;
		AudioManager.Instance.PlaySound("hit");
		this.updateNotches();
	}

	public void updateView()
	{
		this.updateNotches();
	}

	public void updateNotches()
	{
		foreach (Transform transform in this.musicNotches.transform)
		{
			tk2dSprite component = transform.gameObject.GetComponent<tk2dSprite>();
			if (component != null)
			{
				if (Convert.ToInt32(transform.gameObject.name.Replace("Notch_", string.Empty)) <= this.musicVolume)
				{
					component.SetSprite("options_notch2");
				}
				else
				{
					component.SetSprite("options_notch1");
				}
			}
		}
		foreach (Transform transform2 in this.sfxNotches.transform)
		{
			tk2dSprite component2 = transform2.gameObject.GetComponent<tk2dSprite>();
			if (component2 != null)
			{
				if (Convert.ToInt32(transform2.gameObject.name.Replace("Notch_", string.Empty)) <= this.sfxVolume)
				{
					component2.SetSprite("options_notch2");
				}
				else
				{
					component2.SetSprite("options_notch1");
				}
			}
		}
	}
}
