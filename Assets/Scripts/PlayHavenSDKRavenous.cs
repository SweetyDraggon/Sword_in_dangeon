using PlayHaven;
using System;
using UnityEngine;

[Serializable]
public class PlayHavenSDKRavenous : MonoBehaviour
{
	private void Awake()
	{
        /*
		if (GameCore.Instance.playhavenEnabled)
		{
			PlayHavenManager.instance.OnDidDisplayContent -= new DidDisplayContentHandler(this.OnDidDisplayContent);
			PlayHavenManager.instance.OnDidDisplayContent += new DidDisplayContentHandler(this.OnDidDisplayContent);
			PlayHavenManager.instance.OnDismissContent -= new DismissHandler(this.OnDismissContent);
			PlayHavenManager.instance.OnDismissContent += new DismissHandler(this.OnDismissContent);
			PlayHavenManager.instance.OnRewardGiven -= new RewardTriggerHandler(this.OnPlayHavenRewardGiven);
			PlayHavenManager.instance.OnRewardGiven += new RewardTriggerHandler(this.OnPlayHavenRewardGiven);
		}
		*/
	}

	private void Destroy()
	{
        /*
		if (GameCore.Instance.playhavenEnabled)
		{
			PlayHavenManager.instance.OnDidDisplayContent -= new DidDisplayContentHandler(this.OnDidDisplayContent);
			PlayHavenManager.instance.OnDismissContent -= new DismissHandler(this.OnDismissContent);
		}
		*/
	}

	private void OnDidDisplayContent(int requestId)
	{
		//GameCore.Instance.playhavenViewShown = true;
	}

	private void OnDismissContent(int requestId, DismissType dismissType)
	{
	//GameCore.Instance.playhavenViewShown = false;
	}

	private void OnPlayHavenRewardGiven(int requestId, Reward reward)
	{
	}
}
