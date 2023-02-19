using System;
using UnityEngine;

public class Wizard : PatrollingEnemy
{
	public int shotsFired;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "wizard";
		this.headNum = 10;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.WIZARDS;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 80;
		this.weaponH = 48;
		this.maxVel = 2;
		this.swingVel = 0f;
		this.health = 100;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 6;
		this.xp = 15;
		this.idleTimer = 0f;
		this.idleMax = 40;
		this.swingChargeTimer = 0f;
		this.shotsFired = 0;
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
		else if (this.state != 3)
		{
			if (this.state == 4)
			{
				this.swingChargeTimer += 1f * dt;
				if (this.swingChargeTimer >= 30f && this.shotsFired == 0)
				{
					this.shoot();
					this.shotsFired++;
				}
				else if (this.swingChargeTimer >= 50f)
				{
					this.shotsFired = 0;
					this.swingChargeTimer = 0f;
					this.state = 5;
				}
			}
			else if (this.state == 5)
			{
			}
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
			if (this.frame < 28)
			{
				this.frame = 28;
			}
			this.frame++;
			if (this.frame == 33)
			{
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
			this.frame = 33;
		}
		else if (this.state == 5)
		{
			this.frame++;
			if (this.frame > 37)
			{
				this.frame = 1;
				this.state = 2;
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public virtual void shoot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-21) : 21);
		float y = this.y + 24f;
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 8, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 7, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 6, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 5, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 4, this.damage);
		AudioManager.Instance.PlaySound("shoot", base.gameObject);
		Game.Instance.camView.screenShake(2f, 0.2f);
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
		if (num < 200f && num2 < 100f)
		{
			this.inRange = true;
		}
		else
		{
			this.inRange = false;
		}
	}
}
