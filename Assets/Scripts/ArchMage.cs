using System;
using UnityEngine;

public class ArchMage : Wizard
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "arch_mage";
		this.headNum = 22;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.ARCH_MAGES;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 150;
		this.weaponH = 48;
		this.maxVel = 2;
		this.swingVel = 0f;
		this.health = 280;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 18;
		this.xp = 35;
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
			this.idleTimer += dt;
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
				this.swingChargeTimer += dt;
				if (this.swingChargeTimer >= 20f && this.shotsFired < 2)
				{
					this.shoot();
					this.shotsFired++;
					this.swingChargeTimer = 0f;
				}
				else if (this.swingChargeTimer >= 40f)
				{
					this.swingChargeTimer = 0f;
					this.state = 5;
					this.shotsFired = 0;
				}
			}
			else if (this.state == 5)
			{
			}
		}
	}

	public override void shoot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-21) : 21);
		float y = this.y + 61f;
		if (this.shotsFired == 0)
		{
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 2, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 4, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 6, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 8, this.damage);
		}
		else
		{
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 3, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 5, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 7, this.damage);
			Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 9, this.damage);
		}
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
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
