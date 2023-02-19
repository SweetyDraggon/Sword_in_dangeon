using System;
using UnityEngine;

public class GameCore : MonoBehaviour
{
	public static GameCore instance;

	public ShopMenu shop;

	public int dungeonLevelFromMenu;

	public bool tutorialLevelComplete;

	public bool introCutsceneSeen;

	public bool ownsCoinDoubler;

	public int[] shopPagePositions;

	public int[] mapRandomizer;

	public int levelSelectLastPage;

	public int currentTilesForSet;

	public int currentMusicForSet;

	public bool iCloudEnabled = true;

	public bool playhavenViewShown;

	public bool playhavenEnabled = true;

	public bool IS_IPAD;

	public int controlSize;

	public static GameCore Instance
	{
		get
		{
			return GameCore.instance;
		}
	}

	public void initGameCore()
	{
		GameCore.instance = this;
		Application.targetFrameRate = 30;
		this.playhavenEnabled = true;
		this.dungeonLevelFromMenu = 0;
		this.tutorialLevelComplete = false;
		this.introCutsceneSeen = false;
		this.ownsCoinDoubler = false;
		this.levelSelectLastPage = 1;
		this.currentTilesForSet = 0;
		this.currentMusicForSet = 0;
		//this.iCloudEnabled = iCloud.iCloudAvailable();
		this.controlSize = 1;
		this.playhavenViewShown = false;
		this.IS_IPAD = false;
		this.shopPagePositions = new int[6];
		this.mapRandomizer = new int[80];
		UnityEngine.Debug.Log("IPAD: " + ((!this.IS_IPAD) ? "false" : "true"));
	}

	public void postInit()
	{
		Main.loadSave();
	}

	public void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			SaveGame.LoadDocumentFromCloud();
		}
	}

	public void Update()
	{
		tk2dUIManager.Instance.InputEnabled = !this.playhavenViewShown;
	}
}
