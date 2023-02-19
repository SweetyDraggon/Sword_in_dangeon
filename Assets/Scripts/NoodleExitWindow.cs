using System;
using UnityEngine;

public class NoodleExitWindow : CustomWindow
{
	public TitleScreen title;

	private void Awake()
	{
		base.onSlideInStart += new CustomWindowEvent(this.disableTitleScreenButtons);
		base.onSlideOutComplete += new CustomWindowEvent(this.enableTitleScreenButtons);
	}

	public void yesClicked()
	{
		UnityEngine.Debug.Log("yesClicked!");
		Application.Quit();
	}

	public void noClicked()
	{
		UnityEngine.Debug.Log("noClicked!");
		WindowManager.Instance.HideMenu(this);
	}

	public void disableTitleScreenButtons()
	{
		this.setGamePaused(true);
		this.setTitleScreenButtons(false);
	}

	public void enableTitleScreenButtons()
	{
		this.setGamePaused(false);
		this.setTitleScreenButtons(true);
	}

	private void setTitleScreenButtons(bool enabled)
	{
		UnityEngine.Debug.Log("setTitleScreenButtons(" + enabled + ")");
		TitleScreen titleScreen = UnityEngine.Object.FindObjectOfType(typeof(TitleScreen)) as TitleScreen;
		if (titleScreen != null)
		{
			titleScreen.enableMainButtons(enabled);
		}
		else
		{
			UnityEngine.Debug.LogWarning("NoodleExitWindow could not find TitleScreen");
		}
	}

	private void setGamePaused(bool paused)
	{
		if (Game.Instance != null)
		{
			Game.Instance.paused = paused;
		}
		else
		{
			UnityEngine.Debug.LogWarning("NoodleExitWindow thinks there is no game");
		}
	}
}
