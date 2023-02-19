using System;
using UnityEngine;

public class Container : BreakableObject
{
	public override void reset()
	{
		base.reset();
		this.frame = UnityEngine.Random.Range(0, 8);
		this.metal = (this.frame > 4);
		this.currentAnimationName = "container";
		this.rebuildAnimationClip();
	}

	public override void die()
	{
		base.die();
		if (this.frame < 5)
		{
			Game.Instance.questHandler.trackItem(QuestTracking.CRATES_BROKEN);
			AchievementHandler.Instance.Increment(ACHIEVEMENT.CRATE_SMASHER, 1);
		}
		else
		{
			Game.Instance.questHandler.trackItem(QuestTracking.VASES_BROKEN);
		}
	}

	public override void updateRect()
	{
		if (this.collisionRect != null)
		{
			this.collisionRect.x = (int)this.x - this.realW / 2;
			this.collisionRect.y = (int)this.y - this.realH / 2 - 16;
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
	}
}
