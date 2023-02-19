using System;

[Serializable]
public class AchievementTracking
{
	public bool completed;

	public string achievementID;

	public string achievementTitle;

	public int currentValue;

	public int maxValue;

	public AchievementTracking(string aid, string title, int curVal, int maxVal)
	{
		this.achievementID = aid;
		this.achievementTitle = title;
		this.currentValue = curVal;
		this.maxValue = maxVal;
		this.completed = (this.currentValue >= maxVal);
	}

	public void Poll(bool submitOnlyOnComplete = false, bool forceReport = false)
	{
		if (!Achievements.Instance.IsUserAuthenticated())
		{
			return;
		}
		if (this.completed && !forceReport)
		{
			return;
		}
		if (this.currentValue >= this.maxValue)
		{
			this.completed = true;
		}
		if (forceReport || !submitOnlyOnComplete || (submitOnlyOnComplete && this.completed))
		{
			if (!forceReport && this.completed && Game.Instance != null && Game.Instance.hud != null)
			{
				Game.Instance.hud.achievementPopup.AddAchievement(this.achievementTitle);
			}
			Achievements.Instance.ReportAchievementProgress(this.achievementID, this.currentValue, this.maxValue);
		}
	}
}
