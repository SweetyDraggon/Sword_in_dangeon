using System;
using UnityEngine;

public class Chest : BreakableObject
{
	public int lowAmount;

	public int highAmount;

	public override void reset()
	{
		base.reset();
		this.realW = 32;
		this.realH = 20;
		if (Game.Instance.map.dungeonLevel < 13)
		{
			this.type = 1;
			this.lowAmount = 1;
			this.highAmount = 2;
		}
		else if (Game.Instance.map.dungeonLevel < 26)
		{
			this.type = 2;
			this.lowAmount = 3;
			this.highAmount = 4;
		}
		else if (Game.Instance.map.dungeonLevel < 39)
		{
			this.type = 3;
			this.lowAmount = 5;
			this.highAmount = 6;
		}
		else
		{
			this.type = 4;
			this.lowAmount = 7;
			this.highAmount = 8;
		}
		this.frame = this.type;
		this.currentAnimationName = "chest";
		this.rebuildAnimationClip();
	}

	public override void createGold()
	{
		int amt = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * (float)(1 + this.highAmount - this.lowAmount)) + this.lowAmount;
		Game.Instance.fxManager.emitTreasure(new Vector2(this.x, this.y), Game.Instance.player.scaleX, amt);
	}

	public override void die()
	{
		base.die();
		Game.Instance.questHandler.trackItem(QuestTracking.CHESTS_BROKEN);
	}

	public override void updateRect()
	{
		if (this.collisionRect != null)
		{
			this.collisionRect.x = (int)this.x - this.realW / 2;
			this.collisionRect.y = (int)this.y - this.realH / 2 - 20;
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
	}
}
