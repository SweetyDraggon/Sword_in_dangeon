using PlayHaven;
using System;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
	public string reward1 = "redskin";

	public string reward2 = "blueskin";

	public GameObject player;

	private void OnPlayHavenRewardGiven(Reward reward)
	{
		UnityEngine.Debug.Log(string.Format("Reward given = {0}", reward));
		if (this.player == null)
		{
			return;
		}
		if (reward.name == this.reward1)
		{
			this.player.GetComponent<Renderer>().material.color = Color.red;
		}
		if (reward.name == this.reward2)
		{
			this.player.GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}
