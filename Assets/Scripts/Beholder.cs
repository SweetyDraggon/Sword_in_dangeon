using System;
using UnityEngine;

public class Beholder : Enemy
{
	public int shotsFired;

	public float fireDelay;

	public float fireDelayMax;

	public int bounceState;

	public float bounceDelay;

	public float bounceDistance;

	public float bounceDistanceMax;

	public int blinks;

	public float blinkTimer;

	public float blinkTimerMax;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "beholder";
		this.headNum = 7;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.BEHOLDERS;
		this.realW = 32;
		this.realH = 32;
		this.maxVel = 0;
		this.health = 70;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 3;
		this.xp = 15;
		this.idleTimer = 0f;
		this.idleMax = 80;
		this.bounceState = 1;
		this.bounceDistance = 0f;
		this.bounceDistanceMax = 6f;
		this.bounceDelay = 0f;
		this.blinkTimer = 0f;
		this.blinkTimerMax = 40f;
		this.blinks = 0;
		this.fireDelay = 0f;
		this.fireDelayMax = 10f;
		this.shotsFired = 0;
	}

	public override void removeSelf()
	{
	}

	public override void handleAi(float dt)
	{
		if (this.state == 1)
		{
			if (Game.Instance.player.x < this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
		}
		else if (this.state == 2)
		{
			this.fireDelay += 1f * dt;
			if (this.fireDelay >= this.fireDelayMax)
			{
				this.shoot();
				this.shotsFired++;
				this.fireDelay = 0f;
				if (this.shotsFired >= 3)
				{
					this.shotsFired = 0;
					this.state = 1;
				}
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
			this.blinkTimer += 1f * dt;
			if (this.blinkTimer >= this.blinkTimerMax)
			{
				this.frame++;
				if (this.frame > 8)
				{
					this.blinkTimer = 0f;
					this.frame = 1;
					this.blinks++;
					if (this.blinks == 3)
					{
						this.blinks = 0;
						this.state = 2;
					}
				}
			}
		}
		else if (this.state == 2)
		{
			this.frame = 9;
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleMovement(float dt)
	{
		this.bounceDelay += 1f * dt;
		if (this.bounceDelay >= 2f)
		{
			if (this.bounceState == 1)
			{
				this.yVel = 1f;
				this.bounceDistance += 1f * dt;
				if (this.bounceDistance >= this.bounceDistanceMax)
				{
					this.bounceDistance = 0f;
					this.bounceState = 0;
				}
			}
			else if (this.bounceState == 0)
			{
				this.yVel = -1f;
				this.bounceDistance += 1f * dt;
				if (this.bounceDistance >= this.bounceDistanceMax)
				{
					this.bounceDistance = 0f;
					this.bounceState = 1;
				}
			}
			this.y += this.yVel * dt;
			this.bounceDelay = 0f;
		}
	}

	public virtual void shoot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-6) : 6);
		float y = this.y;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("beholder_shoot", base.gameObject);
	}
}
