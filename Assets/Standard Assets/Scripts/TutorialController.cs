using PlayHaven;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
	public enum TutorialPhase
	{
		Intermission,
		Start,
		Advertisement,
		RewardOne,
		RewardTwo,
		MoreGames,
		Done
	}

	public delegate void TutorialPhaseChange(bool isShowing);

//	public PlayHavenContentRequester advertisement;

////	public PlayHavenContentRequester reward1;

//	public PlayHavenContentRequester reward2;

//	public PlayHavenContentRequester crossPromotion;

	private TutorialController.TutorialPhase tutorialPhase = TutorialController.TutorialPhase.Start;

	private Rect guiPanel;

	private GUIStyle style = new GUIStyle();

	private static TutorialController instance;

	private string problemWhileStartingMessage;

	private static bool dontShowOnPlay;

	private Vector2 tutorialScrollPosition = Vector2.zero;

	private bool foundProblemWhileStarting;



	public static event TutorialController.TutorialPhaseChange OnPhaseChange;

	private bool IsShowingTutorial
	{
		get
		{
			return this.tutorialPhase != TutorialController.TutorialPhase.Intermission && this.tutorialPhase != TutorialController.TutorialPhase.Done;
		}
	}

	private void Awake()
	{
		TutorialController.instance = this;
	}

	private void Start()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		this.guiPanel = new Rect(num / 8f, num2 / 8f, num - num / 4f, num2 - num2 / 4f);
		if (TutorialController.OnPhaseChange != null)
		{
			TutorialController.OnPhaseChange(this.IsShowingTutorial);
		}
		//this.foundProblemWhileStarting = this.CheckForProblems();
	}

	private void OnDisable()
	{
		if (TutorialController.OnPhaseChange != null)
		{
			TutorialController.OnPhaseChange(false);
		}
	}

	private void OnGUI()
	{
		if (!this.IsShowingTutorial)
		{
			return;
		}
		GUI.depth = -20;
		switch (this.tutorialPhase)
		{
		case TutorialController.TutorialPhase.Start:
			GUI.Window(1, this.guiPanel, new GUI.WindowFunction(this.TutorialStart), "Welcome to the PlayHaven Tutorial Level!");
			break;
		case TutorialController.TutorialPhase.Advertisement:
			GUI.Window(2, this.guiPanel, new GUI.WindowFunction(this.TutorialAdvertisement), "Advertisement Example");
			break;
		case TutorialController.TutorialPhase.RewardOne:
			GUI.Window(3, this.guiPanel, new GUI.WindowFunction(this.TutorialRewardOne), "Reward One Example");
			break;
		case TutorialController.TutorialPhase.RewardTwo:
			GUI.Window(4, this.guiPanel, new GUI.WindowFunction(this.TutorialRewardTwo), "Reward Two Example");
			break;
		case TutorialController.TutorialPhase.MoreGames:
			GUI.Window(5, this.guiPanel, new GUI.WindowFunction(this.TutorialMoreGames), "More Games Example");
			break;
		}
	}

	public static void AdvanceTutorial(TutorialController.TutorialPhase phase)
	{
		TutorialController x = TutorialController.instance;
		if (x != null)
		{
			TutorialController.instance.PhaseChange(phase);
		}
	}

	private void PhaseChange(TutorialController.TutorialPhase phase)
	{
		if (base.enabled)
		{
			this.tutorialScrollPosition = Vector2.zero;
			this.tutorialPhase = phase;
			if (TutorialController.OnPhaseChange != null)
			{
				TutorialController.OnPhaseChange(this.IsShowingTutorial);
			}
		}
	}

	private void TutorialStart(int i)
	{
		if (this.foundProblemWhileStarting)
		{
			GUI.color = Color.red;
			GUILayout.Label(this.problemWhileStartingMessage, new GUILayoutOption[0]);
			GUI.color = Color.white;
			GUILayout.Label("Please follow the quick-start guide in the Plugins/PlayHaven folder.", new GUILayoutOption[0]);
		}
		else
		{
			this.tutorialScrollPosition = GUILayout.BeginScrollView(this.tutorialScrollPosition, this.style);
			GUILayout.Label("This tutorial will guide you through what's going on here, and how you can implement PlayHaven content in your own games. You can disable this tutorial at anytime by selecting the Tutorial GameObject in the Hierachy and turning off either the GameObject or the TutorialController Component. It is also possible to disable while not playing to do away with it entirely, but if this is your first time checking out PlayHaven then we recommend giving it a go; it's quite short.", new GUILayoutOption[0]);
			GUILayout.Label("In this very simple scene we will demonstrate the PlayHaven plugin for Unity. There is one Advertisement, two Rewards, and One Cross Promotion Widget. You trigger them by passing through the Trigger Colliders. You can easily find these Colliders in the inspector; they're all contained under the 'Example Placements' GameObjects.", new GUILayoutOption[0]);
			GUILayout.Label("While these content units will work while on the device it is important to note that, while you are playing in the editor, only a generic popup will appear.  When you use PlayHaven on a device, however, you will see rich content being displayed whenever a content unit appears. Give it a try!", new GUILayoutOption[0]);
			GUILayout.Label("The Horizontal Axis (default A/D and Left/Right Keys) moves left and right and the Jump key (default is the Space Key) jumps. We've utilized the Input Manager for this, so if you have modified the Horizontal Axis and/or the Jump Axis then you will be using your project specific settings for these Axes.", new GUILayoutOption[0]);
			GUILayout.Label("Finally, there is documentation for the API available in the Plugins/PlayHaven folder. You should copy the zip file somewhere other than your project before unzipping it. We've tried to keep this demo as small as possible so that it won't take too long to download. If you feel that it's missing something then drop us a line at support@playhaven.com", new GUILayoutOption[0]);
			GUILayout.EndScrollView();
			if (GUILayout.Button("Got it!", new GUILayoutOption[]
			{
				GUILayout.Height(100f)
			}))
			{
				this.PhaseChange(TutorialController.TutorialPhase.Intermission);
			}
		}
	}

	private void TutorialAdvertisement(int i)
	{
		this.tutorialScrollPosition = GUILayout.BeginScrollView(this.tutorialScrollPosition, this.style);
		GUILayout.Label("This placement demonstrates an interstitial advertisement.  Advertisements earn you money each time a player sees/follows one. They are the easiest way to monitize your game!", new GUILayoutOption[0]);
		GUILayout.EndScrollView();
		if (GUILayout.Button("Got it!", new GUILayoutOption[]
		{
			GUILayout.Height(100f)
		}))
		{
			this.PhaseChange(TutorialController.TutorialPhase.Intermission);
		}
	}

	private void TutorialRewardOne(int i)
	{
		this.tutorialScrollPosition = GUILayout.BeginScrollView(this.tutorialScrollPosition, this.style);
		GUILayout.Label("This placement demonstrates a reward. Rewards are easy ways to encourage your players to continue playing. You can provide any reward item you want, and because the data that changes is hosted on the PlayHaven server you can change it anytime without having to build, test, release! The PlayHaven server will provide the player with whatever you set.", new GUILayoutOption[0]);
		GUILayout.Label("This example uses a static trigger to reward the player something when they jump through our little 'hoop'. You don't have to make it difficult for your players to obtain rewards, and could even reward them for making it to a certain level, or for just starting the game!", new GUILayoutOption[0]);
		GUILayout.EndScrollView();
		if (GUILayout.Button("Got it!", new GUILayoutOption[]
		{
			GUILayout.Height(100f)
		}))
		{
			this.PhaseChange(TutorialController.TutorialPhase.Intermission);
		}
	}

	private void TutorialRewardTwo(int i)
	{
		this.tutorialScrollPosition = GUILayout.BeginScrollView(this.tutorialScrollPosition, this.style);
		GUILayout.Label("This placement demonstrates another reward, except this time we've wrapped it up as a compound trigger. Triggering placements are very flexible with practically no limit to how they're used.", new GUILayoutOption[0]);
		GUILayout.Label("It is worth noting that some developers prefer to use a single PlayHavenContentRequester for all of their requests  by setting the placement value to a valid placement before calling. If you decide to do this then you will be responsible for counting uses and prefetching. This is because the PlayHavenContentRequester object would count it's own uses and Prefetch whatever the initial placement value is.", new GUILayoutOption[0]);
		GUILayout.Label("While it's possible to use a single PlayHavenContentRequester, we reccomend using a few different ones. You might consider using a unique placement everywhere you're going to be doing prefetching and limiting uses, but have a shared item that you will use for manual requests. At the end of the day, however, the system is flexible enough for you to use it as you want, and not have to conform to any particular standard.", new GUILayoutOption[0]);
		GUILayout.EndScrollView();
		if (GUILayout.Button("Got it!", new GUILayoutOption[]
		{
			GUILayout.Height(100f)
		}))
		{
			this.PhaseChange(TutorialController.TutorialPhase.Intermission);
		}
	}

	private void TutorialMoreGames(int i)
	{
		this.tutorialScrollPosition = GUILayout.BeginScrollView(this.tutorialScrollPosition, this.style);
		GUILayout.Label("This placement demonstrates the Cross Promotion Widget. The Cross Promotion Widget allows your players to find other awesome games to install and play. You can promote your other titles here as well. You could, of course, use it anywhere in your game that you feel like!", new GUILayoutOption[0]);
		GUILayout.Label(string.Empty, new GUILayoutOption[0]);
		GUILayout.EndScrollView();
		if (GUILayout.Button("Got it!", new GUILayoutOption[]
		{
			GUILayout.Height(100f)
		}))
		{
			this.PhaseChange(TutorialController.TutorialPhase.Intermission);
		}
	}
    /*
	private bool CheckForProblems()
	{

		if (PlayHavenManager.Instance)
		{
			if (string.IsNullOrEmpty(PlayHavenManager.Instance.token) || string.IsNullOrEmpty(PlayHavenManager.Instance.secret))
			{
				this.problemWhileStartingMessage = "We have detected that your token or secret has not been filled in!";
			}
			else if (string.IsNullOrEmpty(this.advertisement.placement) || string.IsNullOrEmpty(this.reward1.placement) || string.IsNullOrEmpty(this.reward2.placement) || string.IsNullOrEmpty(this.crossPromotion.placement))
			{
				this.problemWhileStartingMessage = "We have detected that not all of your placements have been assigned on the PlayHavenContentRequester Componenets!";
			}
			else
			{
				this.problemWhileStartingMessage = string.Empty;
			}
		}
		return !string.IsNullOrEmpty(this.problemWhileStartingMessage);
	}
	*/
}
