using System;

public class Worg : Enemy
{
	public override void reset()
	{
		base.reset();
		this.soundOnHurtPlayer = "explosion";
		this.currentAnimationName = "worg";
		this.headNum = 5;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.WORGS;
		this.playerCollisionMultiplier = 1f;
		this.realW = 56;
		this.realH = 32;
		this.damageW = 32;
		this.damageH = 32;
		this.maxVel = 4;
		this.health = 100;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 6;
		this.xp = 15;
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
