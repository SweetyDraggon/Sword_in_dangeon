using System;
using UnityEngine;

public class WormRider : PatrollingEnemy
{
	public int swings;

	public int swingsMax;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "worm_rider";
		this.headNum = 4;
		this.headNumCreature = 15;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.WORM_RIDERS;
		this.realW = 48;
		this.realH = 32;
		this.weaponW = 80;
		this.weaponH = 16;
		this.maxVel = 2;
		this.chargeVel = 6;
		this.health = 180;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 12;
		this.xp = 25;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swings = 0;
		this.swingsMax = 2;
	}

	public override void removeSelf()
	{
	}

	public override void handleAi(float dt)
	{
		if (this.state == 1)
		{
			this.testPlayerRange();
			if (this.inRange)
			{
				this.state = 2;
			}
		}
		else if (this.state == 2)
		{
			if (Game.Instance.player.x <= this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
			this.testPlayerRange();
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 3;
			}
			if (!this.inRange)
			{
				this.idleTimer = 0f;
				this.state = 1;
			}
		}
		else if (this.state == 3)
		{
			this.swingChargeTimer += 1f * dt;
			if (this.swingChargeTimer >= 20f)
			{
				this.swingChargeTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
		}
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
			if (this.frame > 22)
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
			this.frame = 23;
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == 24)
			{
				base.testWeaponCollision();
				this.shoot();
			}
			if (this.frame > 29)
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

	public void shoot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-32) : 32);
		float y = this.y - 2f;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ACID_BOLT, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		AudioManager.Instance.PlaySound("worm_shoot", base.gameObject);
	}

	public override void testPlayerRange()
	{
		float num;
		if (Game.Instance.player.x <= this.x)
		{
			num = this.x - Game.Instance.player.x;
		}
		else
		{
			num = Game.Instance.player.x - this.x;
		}
		float num2;
		if (Game.Instance.player.y <= this.y)
		{
			num2 = this.y - Game.Instance.player.y;
		}
		else
		{
			num2 = Game.Instance.player.y - this.y;
		}
		if (num < 200f && num2 < 60f)
		{
			this.inRange = true;
		}
		else
		{
			this.inRange = false;
		}
	}
}
