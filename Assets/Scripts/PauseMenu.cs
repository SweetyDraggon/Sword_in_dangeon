using PlayHaven;
using System;
using UnityEngine;

public class PauseMenu : CustomWindow
{
	public void Awake()
	{
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
		base.onSlideInComplete += new CustomWindowEvent(this.slideInComplete);
		base.onSlideOutComplete += new CustomWindowEvent(this.slideOutComplete);
        //if (AdsControl.Instance)
        //    AdsControl.Instance.ShowBanner();
    }

	private void slideOutComplete()
	{
		Game.Instance.paused = false;
	}

	public void OnDestroy()
	{
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
	}

	public void slideInComplete()
	{
		if (GameCore.Instance.playhavenEnabled)
		{
//			PlayHavenManager.instance.ContentRequest("pause_menu");
		}
	}

	public void statsClicked()
	{
		WindowManager.Instance.ShowMenu("stats_menu");
	}

	public void questsClicked()
	{
		WindowManager.Instance.ShowMenu("quests_menu");
	}

	public void achievementsClicked()
	{
		PlayGameServices.showAchievements();
	}

	public void optionsClicked()
	{
		WindowManager.Instance.ShowMenu("options_menu");
	}

	public void quitClicked()
	{
		if (Game.Instance.isQuitting)
		{
			return;
		}
		Game.Instance.isQuitting = true;
		TransitionScript.Instance.fadeLayer.FadeIn();
		base.Invoke("quitGame", 1f);
	}

	public void quitGame()
	{
		WindowManager.Instance.HideMenu(this);
		UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
	}

	public void continueClicked()
	{
		WindowManager.Instance.HideMenu(this);
        //if (AdsControl.Instance)
        //    AdsControl.Instance.HideBanner();
    }
}
