using PlayHaven;
using System;
using System.Collections;
using UnityEngine;

public class DeathMenu : CustomWindow
{
	//Cтаты не менять на ТМПРО
	public tk2dTextMesh txtEnemiesSlain;

	public tk2dTextMesh txtLevelsCleared;

	public tk2dTextMesh txtMissionsCompleted;

	public tk2dTextMesh txtCashBonus;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateText);
		base.onSlideInStart += new CustomWindowEvent(this.updateText);
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
		base.onSlideInComplete += new CustomWindowEvent(this.slideInComplete);
        StartCoroutine(ShowAds());
    }

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateText);
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
	}

	public void slideInComplete()
	{
		if (GameCore.Instance.playhavenEnabled)
		{
			//PlayHavenManager.instance.ContentRequest("level_failed");
		}
	}

	public void resurrectClicked()
	{
		WindowManager.Instance.HideMenu(this);
		Game.Instance.endReached = true;
		AchievementHandler.Instance.Increment(ACHIEVEMENT.DETERMINED, 1);
	}

	public void updateText()
	{
		int enemiesSlain = Main.playerStats.enemiesSlain;
		int levelsCleared = Main.playerStats.levelsCleared;
		int questsCompleted = Main.playerStats.questsCompleted;
		this.txtEnemiesSlain.text = enemiesSlain.ToString();
		this.txtLevelsCleared.text = levelsCleared.ToString();
		this.txtMissionsCompleted.text = questsCompleted.ToString();
		this.txtCashBonus.text = "$" + (enemiesSlain * 2 + levelsCleared * 10 + questsCompleted * 10).ToString("N0");
		Main.playerStats.money += enemiesSlain * 2 + levelsCleared * 10 + questsCompleted * 10;
		Main.playerStats.clearCurrentRunData();
	}
    IEnumerator ShowAds()
    {
        yield return new WaitForSeconds(1.0f);
        //AdsControl.Instance.showAds();
    }
}
