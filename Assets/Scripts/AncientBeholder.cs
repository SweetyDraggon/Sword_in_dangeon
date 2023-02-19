using System;
using UnityEngine;

public class AncientBeholder : Boss
{
	public int bounceState;

	public float bounceDistance;

	public float bounceDistanceMax;

	public float bounceDelay;

	public float blinkTimer;

	public float blinkTimerMax;

	public int blinks;

	public float fireDelay;

	public float fireDelayMax;

	public int shotsFired;

	public int shootState;

	public override void reset()
	{
		base.reset();
		this.healthBar.name.text = Localisation.GetString("ANCIENT_BEHOLDER");
		this.bossNum = 1;
		this.headNum = 24;
		this.realW = 64;
		this.realH = 64;
		this.weaponW = 125;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 64;
		this.hitW = 48;
		this.maxVel = 0;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 2500);
		this.damage = 250;
		this.moneyLow = 25;
		this.moneyHigh = 50;
		this.xp = 500;
		this.enemyType = 1;
		this.scaleX = -1f;
		this.inRange = false;
		this.idleTimer = 0f;
		this.idleMax = 60;
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
		this.shootState = 1;
	}

	public override void removeSelf()
	{
	}

	public override void handleAi(float dt)
	{
		if (!Game.Instance.player.alive)
		{
			this.state = 1;
		}
		if (Game.Instance.player.x < this.x)
		{
			this.scaleX = -1f;
		}
		else
		{
			this.scaleX = 1f;
		}
		if (this.state == 1)
		{
			this.blinkTimer += dt;
			if (this.blinkTimer > 20f)
			{
				this.frame++;
				if (this.frame > 7)
				{
					this.frame = 1;
					this.blinks++;
					this.blinkTimer = 0f;
					if (this.blinks > 2)
					{
						this.state = 2;
						this.frame = 8;
						this.blinks = 0;
					}
				}
			}
		}
		else if (this.state == 2)
		{
			this.frame++;
			if (this.frame == 10)
			{
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 30f)
			{
				this.idleTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
			this.idleTimer += dt;
			if (this.shootState == 1)
			{
				if (this.idleTimer >= 10f)
				{
					this.fireShot();
					this.shotsFired++;
					this.idleTimer = 0f;
					if (this.shotsFired == 12)
					{
						this.shotsFired = 0;
						this.state = 5;
						this.shootState = 2;
					}
				}
			}
			else if (this.shootState == 2 && this.idleTimer >= 20f)
			{
				this.fireVolley();
				this.fireShot();
				this.shotsFired++;
				this.idleTimer = 0f;
				if (this.shotsFired == 4)
				{
					this.shotsFired = 0;
					this.state = 5;
					this.shootState = 1;
				}
			}
		}
		else if (this.state == 5)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 30f)
			{
				this.idleTimer = 0f;
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
				this.state = 1;
			}
		}
	}

	public override void handleMovement(float dt)
	{
		this.bounceDelay += dt;
		if (this.bounceDelay >= 2f)
		{
			if (this.bounceState == 1)
			{
				this.yVel = 1f;
				this.bounceDistance += dt;
				if (this.bounceDistance >= this.bounceDistanceMax)
				{
					this.bounceDistance = 0f;
					this.bounceState = 0;
				}
			}
			else if (this.bounceState == 0)
			{
				this.yVel = -1f;
				this.bounceDistance += dt;
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

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public void fireShot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-6) : 6);
		float y = this.y;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("beholder_shoot", base.gameObject);
		Game.Instance.camView.screenShake(2f, 0.2f);
	}

	public void fireVolley()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-6) : 6);
		float y = this.y;
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 2, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 3, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 3, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 4, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 5, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 6, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 7, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 8, this.damage);
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 9, this.damage);
		AudioManager.Instance.PlaySound("beholder_volley", base.gameObject);
		Game.Instance.camView.screenShake(12f, 0.8f);
	}

	public override void die()
	{
		base.die();
		Game.Instance.spikesDisabled = true;
		Spikes[] array = UnityEngine.Object.FindObjectsOfType(typeof(Spikes)) as Spikes[];
		Spikes[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Spikes spikes = array2[i];
			spikes.alive = false;
			spikes.gameObject.SetActive(false);
		}
	}
}
