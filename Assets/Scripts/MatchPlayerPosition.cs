using System;
using UnityEngine;

public class MatchPlayerPosition : MonoBehaviour
{
	public Player player;

	private void Update()
	{
		if (Game.Instance)
		{
			if (!this.player)
			{
				this.player = Game.Instance.player;
			}
			if (this.player)
			{
				base.transform.position = this.player.transform.position;
			}
		}
	}
}
