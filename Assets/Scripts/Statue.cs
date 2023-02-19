using System;
using UnityEngine;

public class Statue : BreakableObject
{
	public override void reset()
	{
		base.reset();
		this.frame = UnityEngine.Random.Range(0, 3) + 1;
		this.realH = 48;
		this.metal = true;
		this.currentAnimationName = "statue";
		this.rebuildAnimationClip();
	}

	public override void die()
	{
		base.die();
		Game.Instance.questHandler.trackItem(QuestTracking.STATUES_BROKEN);
	}

	public override void updateRect()
	{
		if (this.collisionRect != null)
		{
			this.collisionRect.x = (int)this.x - this.realW / 2;
			this.collisionRect.y = (int)this.y - this.realH / 2 - 6;
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
	}
}
