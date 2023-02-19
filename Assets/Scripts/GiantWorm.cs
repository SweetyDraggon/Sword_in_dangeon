using System;

public class GiantWorm : Enemy
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "giant_worm";
		this.headNum = 29;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.GIANT_WORMS;
		this.playerCollisionMultiplier = 1f;
		this.realW = 56;
		this.realH = 32;
		this.maxVel = 2;
		this.health = 220;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 15;
		this.xp = 30;
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.state == 1)
		{
			this.frame++;
			if (this.frame > 10)
			{
				this.frame = 1;
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleMovement(float dt)
	{
		if (this.state == 1)
		{
			this.xVel = (float)this.maxVel * this.scaleX;
		}
		else if (this.state == 2 || this.state == 3 || this.state == 4)
		{
			this.xVel = 0f;
		}
		this.handleKnockback(dt);
		base.handleMovement(dt);
	}

	public override void removeSelf()
	{
	}
}
