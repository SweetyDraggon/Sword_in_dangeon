using PlayHaven;
using System;
using UnityEngine;
using CoreGame;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
	public FadeLayer fadeLayer;

	public tk2dBaseSprite bg1;

	public tk2dBaseSprite bg2;

	public float alpha = 1f;

	public int state = 1;

    //public tk2dSprite  tapToStart;
    public Text tapToStart;

    public CustomUIButton tapToStartButton;

	public CustomUIButton facebookButton;

	public CustomUIButton twitterButton;

	public CustomUIButton settingsButton;

	public Vector2 scrollSpeed = new Vector2(2f, -2f);

	public bool tappedToStart;

	private CustomWindow optionsWindow;
	public bool isSettingOpen = false;

	private void Awake()
	{
		//GameObject go = GameObject.Find("WindowManager");
		//window = go.GetComponent<WindowManager>();
		//Debug.Log(window);
		Application.targetFrameRate = 30;
		this.fadeLayer = TransitionScript.Instance.fadeLayer;
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeInComplete += new FadeEvent(this.fadeInComplete);
		TransitionScript.Instance.updatePosition();
		this.fadeLayer.gameObject.SetActive(true);
        //AdsControl.Instance.ShowBanner();
	}

	private void OnDestroy()
	{
		if (isSettingOpen == true)
			WindowManager.Instance.RemoveMenu(optionsWindow);
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
	}

	private void Start()
	{
		if (!GameCore.Instance.IS_IPAD)
		{
			this.tapToStartButton.transform.localPosition += new Vector3(0f, 7f, 0f);
		}
		this.fadeLayer.FadeOut();
		this.tappedToStart = false;
		AudioManager.Instance.PlayMusic("title");
		if (GameCore.Instance.playhavenEnabled)
		{
//			PlayHavenManager.instance.ContentRequest("main_menu");
		}
	}

	private void Update()
	{
		this.handleTapText();
		Vector3 b = new Vector3(this.scrollSpeed.x * Utils.GetScaledTime(30), this.scrollSpeed.y * Utils.GetScaledTime(30), 0f);
		this.bg1.transform.localPosition += b;
		if (this.bg1.transform.localPosition.y > -640f)
		{
			this.bg2.transform.localPosition = this.bg1.transform.localPosition + new Vector3(0f, 640f, 0f);
		}
		else
		{
			this.bg1.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.bg2.transform.localPosition = this.bg1.transform.localPosition + new Vector3(0f, 640f, 0f);
		}
	}

	public void handleTapText()
	{
		if (this.state == 1)
		{
			this.alpha -= 0.03f * Utils.GetScaledTime(30);
			if (this.alpha < 0.2f)
			{
				this.state = 2;
				this.alpha = 0.2f;
			}
		}
		else if (this.state == 2)
		{
			this.alpha += 0.03f * Utils.GetScaledTime(30);
			if (this.alpha >= 0.96f)
			{
				this.state = 1;
				this.alpha = 0.96f;
			}
		}
		Color color = this.tapToStart.color;
		color.a = this.alpha;
		this.tapToStart.color = color;
	}

	public void settingsClicked()
	{
		isSettingOpen = true;
		this.enableMainButtons(false);
		this.optionsWindow = WindowManager.Instance.ShowMenu("options_menu");
		this.optionsWindow.onSlideOutComplete -= new CustomWindowEvent(this.optionsMenuSlideOut);
		this.optionsWindow.onSlideOutComplete += new CustomWindowEvent(this.optionsMenuSlideOut);
	}

	public void optionsMenuSlideOut()
	{
		isSettingOpen = false;
		this.optionsWindow.onSlideOutComplete -= new CustomWindowEvent(this.optionsMenuSlideOut);
		this.enableMainButtons(true);
	}

//	public void facebookClicked()
//	{
//        //		MySDK.OpenURL("http://www.facebook.com/ravenousgames");
//        Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
//	}

//	public void twitterClicked()
//	{
////		MySDK.OpenURL("http://www.twitter.com/ravenousgames");
//	}

	public void enableMainButtons(bool e)
	{
		this.tapToStartButton.enabled = e;
		this.facebookButton.enabled = e;
		this.twitterButton.enabled = e;
		this.settingsButton.enabled = e;
	}

	public void startGame()
	{
		if (this.fadeLayer.isAnimating)
		{
			return;
		}
		if (this.tappedToStart)
		{
			return;
		}
		this.tappedToStart = true;
		if(isSettingOpen == true)
		 WindowManager.Instance.RemoveMenu(optionsWindow);
		
		
		this.fadeLayer.gameObject.SetActive(true);
		this.fadeLayer.FadeIn();
	}

	public void fadeInComplete()
	{
		if (GameCore.Instance.introCutsceneSeen)
        {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene");
		}
	}
}
