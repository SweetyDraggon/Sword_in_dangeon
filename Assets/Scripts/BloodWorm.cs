using System;

public class BloodWorm : Enemy
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "bloodworm";
		this.headNum = 3;
		this.enemyType = 1;
		this.playerCollisionMultiplier = 1f;
		this.questTrackingNum = QuestTracking.BLOOD_WORMS;
		this.realW = 32;
		this.realH = 24;
		this.maxVel = 2;
		this.health = 60;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 3;
		this.xp = 10;
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

	public override void updateRect()
	{
		if (this.collisionRect != null)
		{
			this.collisionRect.x = (int)this.x - this.realW / 2;
			this.collisionRect.y = (int)this.y - this.realH / 2 - 8;
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
	}
}
