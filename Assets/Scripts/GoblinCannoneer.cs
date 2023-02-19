using System;
using UnityEngine;

public class GoblinCannoneer : Enemy
{
	public int shotsFired;

	public int shotsFiredMax;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "goblin_cannoneer";
		this.headNum = 13;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.GOBLIN_CANNONEERS;
		this.realW = 32;
		this.realH = 32;
		this.maxVel = 0;
		this.health = 140;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 9;
		this.xp = 20;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.shotsFired = 0;
		this.shotsFiredMax = 3;
		this.enemyWearsMetal = true;
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
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 2;
			}
		}
		else if (this.state == 2)
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
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		else if (this.state == 2)
		{
			this.frame++;
			if (this.frame == 15)
			{
				this.shoot();
			}
			if (this.frame > 24)
			{
				this.shotsFired++;
				if (this.shotsFired == this.shotsFiredMax)
				{
					this.shotsFired = 0;
					this.frame = 1;
					this.state = 1;
				}
				else
				{
					this.frame = 1;
					this.state = 1;
					this.idleTimer = (float)(this.idleMax - 1);
				}
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

	public void shoot()
	{
		float x = this.x;
		float y = this.y + 3f;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.CANNONBALL, (this.scaleX <= 0f) ? (-1) : 1, this.damage, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("cannoneer", base.gameObject);
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
		if (num < 200f && num2 < 200f && this.inRange)
		{
			Game.Instance.camView.screenShake(2f, 0.2f);
		}
	}
}
