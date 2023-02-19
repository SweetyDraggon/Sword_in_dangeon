using System;

public class DoomKnight : PatrollingEnemy
{
	public int swings;

	public int swingsMax;

	public override void reset()
	{
		base.reset();
		this.soundOnHitPlayer = "hit";
		this.currentAnimationName = "doom_knight";
		this.headNum = 12;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.DOOM_KNIGHTS;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 85;
		this.weaponH = 48;
		this.maxVel = 2;
		this.swingVel = 4f;
		this.health = 180;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 12;
		this.xp = 25;
		this.idleTimer = 0f;
		this.idleMax = 10;
		this.swingChargeTimer = 0f;
		this.swings = 0;
		this.swingsMax = 3;
		this.enemyWearsMetal = true;
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
				base.testWeaponCollision();
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
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
