using System;
using UnityEngine;

public class Dragon : Boss
{
	public DragonFire dragonFire;

	public int shotsFired;

	public int attackType;

	public int strikes;

	public bool fireSwapped;

	public int shotState;

	public float fireBallTimer;

	public int fireBallState;

	public override void reset()
	{
		base.reset();
		this.healthBar.name.text = Localisation.GetString("DRAGON");
		this.dragonFire.init();
		this.dragonFire.dragon = this;
		this.bossNum = 4;
		this.headNum = 28;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 95;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 48;
		this.hitW = 215;
		this.maxVel = 2;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 7000);
		this.damage = 30;
		this.moneyLow = 25;
		this.moneyHigh = 50;
		this.xp = 500;
		this.enemyType = 1;
		this.scaleX = -1f;
		this.inRange = false;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.shotsFired = 0;
		this.attackType = 1;
		this.strikes = 0;
		this.fireSwapped = false;
		this.shotState = 1;
		this.fireBallTimer = 0f;
		this.fireBallState = 1;
	}

	public override void removeSelf()
	{
	}

	public override void update(float dt)
	{
		base.update(dt);
		this.dragonFire.update(dt);
	}

	public override void handleAi(float dt)
	{
		if (!Game.Instance.player.alive)
		{
			this.state = 1;
		}
		if (this.state == 1)
		{
			this.xVel = 0f;
			this.frame++;
			if (this.frame > 20)
			{
				this.frame = 1;
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
			if (this.frame < 21)
			{
				this.frame = 20;
			}
			this.frame++;
			if (this.frame > 28)
			{
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 30f)
			{
				this.idleTimer = 0f;
				this.state = 4;
				this.dragonFire.activate();
				Game.Instance.camView.screenShake(8f, 1.7f);
				AudioManager.Instance.PlaySound("dragon_breath", base.gameObject);
			}
		}
		else if (this.state == 4)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 50f)
			{
				this.idleTimer = 0f;
				this.state = 5;
				this.dragonFire.deactivate();
			}
		}
		else if (this.state == 5)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 10f)
			{
				this.idleTimer = 0f;
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			this.frame++;
			if (this.frame > 37)
			{
				this.frame = 1;
				this.state = 7;
			}
		}
		else if (this.state == 7)
		{
			this.xVel = 0f;
			this.frame++;
			if (this.frame > 20)
			{
				this.frame = 1;
			}
			this.idleTimer += dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 8;
			}
		}
		else if (this.state == 8)
		{
			if (this.frame < 38)
			{
				this.frame = 37;
			}
			this.frame++;
			if (this.frame > 45)
			{
				this.state = 9;
			}
		}
		else if (this.state == 9)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 30f)
			{
				this.idleTimer = 0f;
				this.state = 10;
			}
		}
		else if (this.state == 10)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 15f)
			{
				this.idleTimer = 0f;
				this.fireShot();
				this.shotsFired++;
				if (this.shotState == 1)
				{
					this.shotState = 2;
				}
				else
				{
					this.shotState = 1;
				}
				if (this.shotsFired == 12)
				{
					this.shotsFired = 0;
					this.state = 11;
				}
			}
		}
		else if (this.state == 11)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 30f)
			{
				this.idleTimer = 0f;
				this.state = 12;
			}
		}
		else if (this.state == 12)
		{
			this.frame++;
			if (this.frame > 54)
			{
				this.frame = 1;
				this.state = 1;
			}
		}
		this.handleFireBalls(dt);
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
		float x = this.x - 80f;
		float x2 = this.x - 95f;
		float y = this.y + 36f;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, this.damage);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		Game.Instance.fxManager.emitProjectile(new Vector2(x2, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, this.damage);
		Game.Instance.fxManager.emitFlash(new Vector2(x2, y), FXParticleTypes.FLASH_SMALL);
		Game.Instance.camView.screenShake(2f, 0.2f);
		AudioManager.Instance.PlaySound("shoot", base.gameObject);
	}

	public void handleFireBalls(float dt)
	{
		if (this.fireBallState == 1)
		{
			this.fireBallTimer += dt;
			if (this.fireBallTimer >= 100f)
			{
				this.fireBallTimer = 0f;
				int num = UnityEngine.Random.Range(275, 745);
				Game.Instance.fxManager.emitProjectile(new Vector2((float)num, this.y + 500f), ProjectileType.FIREBALL, 1, 1, 0);
				if (this.health < this.maxHealth / 2)
				{
					this.fireBallState = 2;
				}
			}
		}
		else if (this.fireBallState == 2)
		{
			this.fireBallTimer += dt;
			if (this.fireBallTimer >= 50f)
			{
				this.fireBallTimer = 0f;
				int num2 = UnityEngine.Random.Range(275, 745);
				Game.Instance.fxManager.emitProjectile(new Vector2((float)num2, this.y + 500f), ProjectileType.FIREBALL, 1, 1, 0);
			}
		}
	}

	public override void updateRect()
	{
		base.updateRect();
		this.collisionRect.x = (int)this.x - (int)((float)this.hitW * 0.5f);
	}

	public override void die()
	{
		base.die();
		Game.Instance.quitGame = true;
	}
}
