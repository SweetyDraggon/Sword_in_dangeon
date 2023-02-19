using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
	public AchievementPopup achievementPopup;

	public QuestPopup questPopup;

	public LevelText levelText;

	public BossHealthBar bossHealth;

	public XPProgress xpProgressBar;

	public tk2dTextMesh healthLabel;

	public tk2dTextMesh moneyLabel;

	public FadeLayer fadeLayer;

	public FadeLayer levelUpFlash;

	public TextMeshPro tutorialText;

	private tk2dTextMesh tutorialTextShadow;

	public int tutorialTextState;

	public float tutorialAlpha;

	public tk2dSprite key;

	private int lastMoney = -1;

	private int lastHealth = -1;

	public List<ControlButton> controls;

	public bool controlsEnabled = true;

	public CustomUIButton mapButton;

	public CustomUIButton pauseButton;

	public GameObject control_iphone;

	public GameObject control_ipad;

	public GameObject control_iphone_2;

	public GameObject control_iphone_0;

	public void Awake()
	{
		this.controlsEnabled = true;
		this.fadeLayer = TransitionScript.Instance.fadeLayer;
		TransitionScript.Instance.updatePosition();
		this.tutorialTextState = 0;
		this.tutorialAlpha = 0f;
		this.tutorialText.gameObject.SetActive(true);
	//	this.tutorialTextShadow = this.tutorialText.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
		this.tutorialText.color = new Color(1f, 1f, 1f, this.tutorialAlpha);
	//	this.tutorialTextShadow.color = new Color(0f, 0f, 0f, this.tutorialAlpha * 0.5f);
		this.updateControllerSize();
	}

	public void Start()
	{
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeOutComplete -= new FadeEvent(this.fadeOutComplete);
		this.fadeLayer.onFadeInComplete += new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeOutComplete += new FadeEvent(this.fadeOutComplete);
	}

	public void OnDestroy()
	{
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeOutComplete -= new FadeEvent(this.fadeOutComplete);
	}

	public void Update()
	{
		this.updateMoney(false);
		this.updateHealth(false);
		this.updateKey();
		this.updateXP();
		this.updateTutorialText(0.0333333351f);
		this.levelText.update(Time.deltaTime);

		if (this.pauseButton != null)
		{
			this.pauseButton.GetComponent<Collider>().enabled = !Game.Instance.paused;
		}
		if (this.mapButton != null)
		{
			this.mapButton.GetComponent<Collider>().enabled = !Game.Instance.paused;
		}
	}

	public void updateControllerSize()
	{
		UnityEngine.Debug.Log("HUD.updateControllerSize");
		if (GameCore.Instance.controlSize == 0)
		{
			this.control_iphone.SetActive(false);
			this.control_iphone_2.SetActive(false);
			this.control_iphone_0.SetActive(true);
		}
		else if (GameCore.Instance.controlSize == 1)
		{
			this.control_iphone_2.SetActive(false);
			this.control_iphone_0.SetActive(false);
			this.control_iphone.SetActive(true);
		}
		else if (GameCore.Instance.controlSize == 2)
		{
			this.control_iphone.SetActive(false);
			this.control_iphone_0.SetActive(false);
			this.control_iphone_2.SetActive(true);
		}
	}

	public void updateXP()
	{
		this.xpProgressBar.level = Main.playerStats.playerLevel;
		this.xpProgressBar.xp = Main.playerStats.playerXp;
		this.xpProgressBar.xpMax = Main.playerStats.nextXpLevel;
	}

	public void updateKey()
	{
		if (Game.Instance.map.dungeonLevel > 0)
		{
			this.key.color = new Color(1f, 1f, 1f, (!Game.Instance.keyFound) ? 0.5f : 1f);
		}
		else
		{
			this.key.color = new Color(1f, 1f, 1f, 0f);
		}
	}

	public void updateMoney(bool force = false)
	{
		if (this.lastMoney == Main.playerStats.money && !force)
		{
			return;
		}
		this.lastMoney = Main.playerStats.money;
		this.moneyLabel.text = Main.playerStats.money.ToString("N0");
	}

	public void updateHealth(bool force = false)
	{
		if (this.lastHealth == Game.Instance.player.health && !force)
		{
			return;
		}
		this.lastHealth = Game.Instance.player.health;
		this.healthLabel.text = Game.Instance.player.health.ToString("N0");
	}

	public void activateQuestPopup(int i)
	{
		this.questPopup.activate(i);
	}

	public void mapPressed()
	{
		Game.Instance.showMapScreen(true);
	}

	//private void OnApplicationFocus(bool hasFocus)
	//{
	//	if(!hasFocus) pausePressed();
	//}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus) pausePressed();
	}

	public void pausePressed()
	{
		if (!Game.Instance.paused)
		{
			Game.Instance.paused = true;
			WindowManager.Instance.ShowMenu("pause_menu");
		}
	}

	public void removeSelf()
	{
	}

    public void SetTutorialText(string text)
    {
        this.tutorialText.text = text;
    }

    public void FadeTutorialTextIn()
	{
		if (this.tutorialTextState == 1)
		{
			return;
		}
		this.tutorialTextState = 1;
		if (this.levelText.isActive)
		{
			this.levelText.fadeOut();
		}
	}

	public void FadeTutorialTextOut()
	{
		if (this.tutorialTextState == 2)
		{
			return;
		}
		this.tutorialTextState = 2;
	}

	public void updateTutorialText(float dt)
	{
		if (this.tutorialTextState == 1)
		{
			this.tutorialAlpha += dt;
			if (this.tutorialAlpha >= 1f)
			{
				this.tutorialTextState = 0;
				this.tutorialAlpha = 1f;
			}
			this.tutorialText.color = new Color(1f, 1f, 1f, this.tutorialAlpha);
//			this.tutorialTextShadow.color = new Color(0f, 0f, 0f, this.tutorialAlpha * 0.5f);
		}
		else if (this.tutorialTextState == 2)
		{
			this.tutorialAlpha += -dt;
			if (this.tutorialAlpha <= 0f)
			{
				this.tutorialTextState = 0;
				this.tutorialAlpha = 0f;
			}
			this.tutorialText.color = new Color(1f, 1f, 1f, this.tutorialAlpha);
			//this.tutorialTextShadow.color = new Color(0f, 0f, 0f, this.tutorialAlpha * 0.5f);
		}
	}

	public void fadeIn()
	{
		if (this.fadeLayer != null)
		{
			this.fadeLayer.gameObject.SetActive(true);
			this.fadeLayer.FadeIn();
		}
	}

	public void fadeOut()
	{
		if (this.fadeLayer != null)
		{
			this.fadeLayer.gameObject.SetActive(true);
			this.fadeLayer.FadeOut();
		}
	}

	public void fadeInComplete()
	{
		Game.Instance.deleteLevel();
	}

	public void fadeOutComplete()
	{
		Game.Instance.swapFadeOut();
	}

	public void EnableControls(bool e)
	{
		if (this.controlsEnabled == e)
		{
			return;
		}
		this.controlsEnabled = e;
		foreach (ControlButton current in this.controls)
		{
			current.setEnabled(e);
		}
		if (!this.controlsEnabled)
		{
			Game.Instance.player.leftPressed = false;
			Game.Instance.player.rightPressed = false;
			Game.Instance.player.jumpPressed = false;
			Game.Instance.player.attackPressed = false;
		}
	}
}
