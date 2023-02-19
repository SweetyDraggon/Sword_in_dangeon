using System;
using UnityEngine;

public class Game : MonoBehaviour
{
	private static Game instance;

	public HUD hud;

	public CamView camView;

	public bool isQuitting;

	public int state;

	private bool scrolling;

	public QuestHandler questHandler;

	public Map map;

	public MapScreen mapScreen;

	public FXManager fxManager;

	public static int tileW = 32;

	public static int tileH = 32;

	public int screenW = 480;

	public int screenH = 320;

	public static TileInfo tileInfo;

	public Player player;

	private int playerType;

	public int tileStartX;

	public int tileStartY;

	public bool paused;

	public bool endReached;

	public bool quitGame;

	public bool cleanUp;

	public int cleanUpTimer;

	public bool gotoPortal;

	public bool keyFound;

	public bool spikesDisabled;

	public FixedTimer fixedTimer;

	public static Game Instance
	{
		get
		{
			return Game.instance;
		}
	}

	public void Awake()
	{
		Game.instance = this;
		Application.targetFrameRate = 30;
		SaveGame.LoadDocumentFromCloud();
		if (this.camView == null)
		{
			this.camView = Camera.main.GetComponent<CamView>();
		}
		this.fixedTimer = new FixedTimer(30);
		this.questHandler = new QuestHandler();
		this.isQuitting = false;
        //if(AdsControl.Instance)
        //  AdsControl.Instance.HideBanner();
    }
	
	

	public void Start()
	{
		this.state = 1;
		this.scrolling = true;
		this.spikesDisabled = false;
		Game.tileInfo = new TileInfo();
		this.screenW = (int)this.camView.cam.ScreenExtents.width;
		this.screenH = (int)this.camView.cam.ScreenExtents.height;
		this.reset();
		GameCore.Instance.dungeonLevelFromMenu = 0;
	}

	private void Update()
	{
		this.fixedTimer.update();
		while (this.fixedTimer.step())
		{
			float dt = this.fixedTimer.dt * 30f;
			if (!this.paused) // протестировать фокус
			{
				this.player.update(dt);
				this.map.update(dt);
				this.fxManager.update(dt);
				this.camView.update(dt);
			}
			this.mapScreen.update(dt);
			this.fixedTimer.decrement();
			this.handleState();
		}
		tk2dSpriteAnimator.g_Paused = this.paused;
		if (GameCore.Instance.playhavenViewShown)
		{
			Game.Instance.hud.EnableControls(false);
		}
		else
		{
			Game.Instance.hud.EnableControls(!this.paused);
		}
		if (Debugger.Instance != null)
		{
			Debugger.Instance.DrawRect(this.player.collisionRect);
			Debugger.Instance.DrawRect(this.player.weapon.weaponRect, Color.red);
			this.fxManager.debugDraw();
			this.map.debugDraw();
		}
	}

	public void handleState()
	{
		if (this.state == 1)
		{
			if (this.scrolling)
			{
			}
			if (this.endReached)
			{
				this.hud.fadeIn();
				this.player.inControl = false;
				this.state = 2;
			}
		}
		else if (this.state != 2)
		{
			if (this.state == 4)
			{
				this.cleanUp = true;
				this.cleanUpTimer++;
				if (this.cleanUpTimer == 10)
				{
					this.cleanUp = false;
					this.cleanUpTimer = 0;
					this.state = 2;
					if (!this.quitGame && this.map.dungeonLevel != 0)
					{
						if (!this.player.alive)
						{
							this.map.dungeonLevel = 0;
							this.hud.levelText.activate(this.map.dungeonLevel);
							this.player.health = (Main.playerStats.currentHealth = Main.playerStats.maxHealth);
						}
						else
						{
							if (this.map.dungeonLevel != -1)
							{
								if (this.map.dungeonLevel == -2)
								{
									GameCore.Instance.tutorialLevelComplete = true;
									AchievementHandler.Instance.SetValue(ACHIEVEMENT.QUEST_HAS_BEGUN, 1);
									this.map.dungeonLevel = 0;
								}
								this.map.dungeonLevel++;
								if (this.map.dungeonLevel > Main.playerStats.levelReached)
								{
									Main.playerStats.levelReached = this.map.dungeonLevel;
								}
							}
							if (this.map.dungeonLevel > 1 && this.map.dungeonLevel != -1)
							{
								int dungeonLevel = this.map.dungeonLevel;
								bool flag = false;
								int num = 1;
								if (dungeonLevel >= 1 && dungeonLevel <= 13)
								{
									num = 1;
								}
								if (dungeonLevel >= 14 && dungeonLevel <= 26)
								{
									num = 2;
								}
								if (dungeonLevel >= 27 && dungeonLevel <= 39)
								{
									num = 3;
								}
								if (dungeonLevel >= 40 && dungeonLevel <= 52)
								{
									num = 4;
								}
								if (dungeonLevel >= 53 && dungeonLevel <= 65)
								{
									num = 5;
								}
								if ((dungeonLevel - (num - 1) * 13 - 1) % 3 == 0)
								{
									flag = true;
								}
								if (flag)
								{
									this.map.savedDungeonLevel = this.map.dungeonLevel;
									this.map.dungeonLevel = -1;
								}
							}
							else if (this.map.dungeonLevel == -1)
							{
								this.map.dungeonLevel = this.map.savedDungeonLevel;
							}
							this.hud.levelText.activate(this.map.dungeonLevel);
						}
						this.keyFound = false;
						this.spikesDisabled = false;
						Main.saveGame();
						this.loadNewLevel();
					}
					else if (!this.quitGame && this.map.dungeonLevel == 0)
					{
						this.gotoPortal = true;
						this.endGame();
					}
					else
					{
						Main.saveGame();
						this.gotoPortal = false;
						this.endGame();
					}
				}
			}
		}
	}

	public void swapFadeOut()
	{
		if (!this.isQuitting)
		{
		}
	}

	public void deleteLevel()
	{
		if (!this.isQuitting)
		{
			Game.Instance.paused = true;
			this.state = 4;
		}
	}

	public void reset()
	{
		this.paused = false;
		this.endReached = false;
		this.quitGame = false;
		this.cleanUp = false;
		this.cleanUpTimer = 0;
		this.gotoPortal = false;
		this.player.reset();
		this.fxManager.reset();
		this.map.reset();
		this.map.generateLevel();
		this.getPlayerPosition();
		this.getTileStartPosition();
		this.player.SetPosition((float)(this.map.playerStartX * this.map.tileW) + (float)this.map.tileW * 0.5f, (float)(this.map.playerStartY * this.map.tileH) + (float)this.map.tileW * 0.5f);
		this.player.updateItems();
		this.player.loadNewLevel();
		this.setInitialMapPosition();
		this.hud.fadeOut();
		if (this.map.dungeonLevel != 16 && this.map.dungeonLevel != 32 && this.map.dungeonLevel != 48 && this.map.dungeonLevel != 64 && this.map.dungeonLevel != 80)
		{
			this.hud.levelText.activate(this.map.dungeonLevel);
		}
		if (this.map.bossBattle)
		{
			AudioManager.Instance.PlayMusic("boss");
		}
		//else if (this.map.dungeonLevel == 0 ) //|| this.map.dungeonLevel == -1)
		//{
		//	//AudioManager.Instance.PlayMusic("kings_court");
		//	AudioManager.Instance.PlayMusic("title");
		//}
		//else if (this.map.dungeonLevel == -1)
		//{
		//	AudioManager.Instance.PlayMusic("caravan");
		//}
		else
		{
			if (GameCore.Instance.currentMusicForSet < 1)
			{
				GameCore.Instance.currentMusicForSet = 1;
			}
			if (GameCore.Instance.currentMusicForSet > 3)
			{
				GameCore.Instance.currentMusicForSet = 3;
			}
			AudioManager.Instance.PlayMusic("game_music" + GameCore.Instance.currentMusicForSet);
		}
	}

	public void loadNewLevel()
	{
		this.reset();
		this.endReached = false;
		this.state = 1;
	}

	public void getTileStartPosition()
	{
	}

	public void setInitialMapPosition()
	{
		this.camView.setInitialPosition();
	}

	public void endGame()
	{
		Main.playerStats.clearCurrentRunData();
		if (this.gotoPortal)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
		}
		else
		{
			this.loadOutro();
		}
	}

	public void loadOutro()
	{
		PlayerPrefs.SetInt("cutscene_outro", 1);
		PlayerPrefs.Save();
		UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene");
	}

	public static float scaledDeltaTime()
	{
		return Time.deltaTime * 15f;
	}

	public void removeEnemy()
	{
	}

	private void removeEnemies()
	{
	}

	private void removeBreakables()
	{
	}

	private void removeWater()
	{
	}

	private void removeHazards()
	{
	}

	private void removePickUps()
	{
	}

	private void removeMap()
	{
	}

	private void removeSecrets()
	{
	}

	private void removeFromActive()
	{
	}

	private void activateEnemy()
	{
	}

	public void removeSelf()
	{
	}

	private void getPlayerPosition()
	{
	}

	public void showMapScreen(bool s)
	{
		if (s && !this.mapScreen.isShown)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.NOT_LOST, 1);
			this.hud.EnableControls(false);
			Game.Instance.paused = true;
			WindowManager.Instance.ShowMenu(this.mapScreen, 0f);
		}
		else if (!s && this.mapScreen.isShown)
		{
			this.hud.EnableControls(true);
			Game.Instance.paused = false;
			WindowManager.Instance.HideMenu(this.mapScreen);
		}
	}
}
