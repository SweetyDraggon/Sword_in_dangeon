using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Achievements
{
	private static Achievements instance;


	private static Action<bool> __f__am_cache2;

	private static Action<string> __f__am_cache3;

	private static Action<string> __f__am_cache4;

	private static Action<string, string> __f__am_cache5;

	private static Action<string, bool> __f__am_cache6;

	private static Action<string, string> __f__am_cache7;

	private static Action<string, bool> __f__am_cache8;

	public static Achievements Instance
	{
		get
		{
			if (Achievements.instance == null)
			{
				Achievements.instance = new Achievements();
				Achievements.instance.Initialize();
			}
			return Achievements.instance;
		}
	}

	public void Initialize()
	{
		UnityEngine.Debug.Log("Achievements Initialize (should only be called once)");
		this.addGPGSHooks();
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
	}

	public bool ValidPlatform()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
	}

	public void Authenticate()
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			//PlayGameServices.authenticate();
		}
	}

	public bool IsUserAuthenticated()
	{
		//return this.ValidPlatform() && PlayGameServices.isSignedIn();
		return false;
	}

	public void ShowAchievementUI()
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		if (this.IsUserAuthenticated())
		{
			//PlayGameServices.showAchievements();
		}
	}

	public void ShowLeaderboardUI()
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		if (this.IsUserAuthenticated())
		{
			Social.ShowLeaderboardUI();
		}
	}

	public void ReportScore(string leaderboardID, long score)
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		if (this.IsUserAuthenticated())
		{
			Social.ReportScore(score, leaderboardID, delegate(bool success)
			{
			});
		}
	}

	public bool ReportAchievementProgress(string achievementID, int numSteps, int maxSteps)
	{
		if (!this.ValidPlatform())
		{
			return false;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		if (!this.IsUserAuthenticated())
		{
			return false;
		}
		if (!this.IsAchievementComplete(achievementID))
		{
			if (numSteps < maxSteps)
			{
				//PlayGameServices.incrementAchievement(achievementID, numSteps);
			}
			else if (numSteps == maxSteps)
			{
				//PlayGameServices.unlockAchievement(achievementID, false);
			}
			else
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"Achievements.ReportAchievementProgress(",
					achievementID,
					", ",
					numSteps,
					", ",
					maxSteps,
					") has invalid parameters (numSteps > maxSteps)"
				}));
			}
			return true;
		}
		return true;
	}

	public void ResetAchievements()
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		
	}

	private void LoadAchievements()
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
		
	}

	public bool IsAchievementComplete(string achievementID)
	{
		if (!this.ValidPlatform())
		{
			return false;
		}
		if (!this.IsUserAuthenticated())
		{
			this.Authenticate();
		}
	
		return false;
	}

	

	private void ResetAchievementsHandler(bool status)
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (status)
		{
			
			this.LoadAchievements();
		}
	}

	private void ProcessAuthentication(bool success)
	{
		if (!this.ValidPlatform())
		{
			return;
		}
		if (success)
		{
			this.LoadAchievements();
		}
	}

	

	private void addGPGSHooks()
	{
		/*
		GPGManager.authenticationFailedEvent += delegate(string obj)
		{
			UnityEngine.Debug.LogError("GPGManager.authenticationFailedEvent(" + obj + ")");
		};
		GPGManager.authenticationSucceededEvent += delegate(string obj)
		{
			UnityEngine.Debug.Log("GPGManager.authenticationSucceededEvent(" + obj + ")");
		};
		GPGManager.incrementAchievementFailedEvent += delegate(string achievementId, string error)
		{
			UnityEngine.Debug.LogError("GPGManager.incrementAchievementFailedEvent(" + achievementId + ")\n" + error);
		};
		GPGManager.incrementAchievementSucceededEvent += delegate(string achievementId, bool isNew)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"GPGManager.incrementAchievementSucceededEvent(",
				achievementId,
				"isNew: ",
				isNew
			}));
		};
		GPGManager.unlockAchievementFailedEvent += delegate(string achievementId, string error)
		{
			UnityEngine.Debug.LogError("GPGManager.unlockAchievementFailedEvent(" + achievementId + ")\n" + error);
		};
		GPGManager.unlockAchievementSucceededEvent += delegate(string achievementId, bool isNew)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"GPGManager.unlockAchievementSucceededEvent(",
				achievementId,
				"isNew: ",
				isNew
			}));
		};
		*/
	}
}
