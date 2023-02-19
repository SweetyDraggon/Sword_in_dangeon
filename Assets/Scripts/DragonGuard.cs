using System;

public class DragonGuard : PatrollingEnemy
{
	public int swings;

	public int swingsMax;

	public override void reset()
	{
		base.reset();
		this.soundOnHitPlayer = "hit";
		this.soundOnHitPlayerExtra = "heavy_hit";
		this.currentAnimationName = "dragon_guard";
		this.headNum = 20;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.DRAGONGUARDS;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 100;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 56;
		this.hitW = 32;
		this.maxVel = 2;
		this.swingVel = 4f;
		this.health = 280;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 18;
		this.xp = 35;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
		this.swings = 0;
		this.swingsMax = 3;
	}

	public override void removeSelf()
	{
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
			if (this.frame < 13)
			{
				this.frame = 12;
			}
			this.frame++;
			if (this.frame > 28)
			{
				this.frame = 13;
			}
		}
		else if (this.state == 2)
		{
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		else if (this.state == 3)
		{
			this.frame = 29;
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == 30)
			{
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
				base.testWeaponCollision();
			}
			if (this.frame > 35)
			{
				this.swings++;
				if (this.swings == this.swingsMax)
				{
					this.swings = 0;
					this.frame = 1;
					this.state = 2;
				}
				else
				{
					this.state = 3;
					this.frame = 29;
					this.swingChargeTimer = 15f;
				}
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}
}
