using System;
using UnityEngine;

public class CannonGiant : Enemy
{
	public int shotsFired;

	public int attackType;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "cannon_giant";
		this.headNum = 30;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.CANNON_GIANTS;
		this.realW = 32;
		this.realH = 32;
		this.collisionRectUsesHitSize = true;
		this.hitH = 64;
		this.hitW = 32;
		this.maxVel = 0;
		this.health = 220;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 15;
		this.xp = 30;
		this.idleTimer = 0f;
		this.idleMax = 60;
	}

	public override void removeSelf()
	{
	}

	public void fireShot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-45) : 45);
		float y = this.y + 8f;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.CANNONBALL, (this.scaleX <= 0f) ? (-1) : 1, this.damage, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("giant_cannoneer", base.gameObject);
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
		if (num < 200f && num2 < 200f)
		{
			Game.Instance.camView.screenShake(8f, 0.4f);
		}
	}

	public override void handleAi(float dt)
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
			this.xVel = 0f;
			this.frame++;
			if (this.frame > 20)
			{
				this.frame = 1;
			}
			if (Game.Instance.player.x <= this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
			this.idleTimer += dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 2;
			}
		}
		else if (this.state == 2)
		{
			this.frame++;
			if (this.frame == 25)
			{
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 15f)
			{
				this.state = 4;
				this.idleTimer = 0f;
			}
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == 26)
			{
				this.fireShot();
				this.shotsFired++;
			}
			if (this.frame > 37)
			{
				if (this.shotsFired == 2)
				{
					this.state = 5;
					this.shotsFired = 0;
					this.frame = 38;
				}
				else
				{
					this.state = 3;
					this.frame = 25;
				}
			}
		}
		else if (this.state == 5)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 15f)
			{
				this.idleTimer = 0f;
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			this.frame++;
			if (this.frame > 42)
			{
				this.state = 1;
				this.frame = 1;
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}
}
