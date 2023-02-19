using System;
using UnityEngine;

public class Butcher : Boss
{
	public float swingVel;

	public float swingChargeTimer;

	public int swings;

	public int swingsMax;

	public int shotsFired;

	public int attackType;

	public void OnDestroy()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
	}

	public override void reset()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
		base.onAttackHitPlayer += new Enemy.EnemyCallback(this.attackHitPlayer);
		base.reset();
		this.healthBar.name.text =Localisation.GetString("BUTCHER");
		this.bossNum = 2;
		this.headNum = 24;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 130;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 64;
		this.hitW = 48;
		this.maxVel = 3;
		this.swingVel = 4f;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 3000);
		this.damage = 20;
		this.moneyLow = 25;
		this.moneyHigh = 50;
		this.xp = 300;
		this.enemyType = 1;
		this.scaleX = -1f;
		this.inRange = false;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.swingChargeTimer = 0f;
		this.swings = 0;
		this.swingsMax = 2;
		this.shotsFired = 0;
		this.attackType = 1;
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
			base.testWeaponCollision();
			this.xVel = (float)this.maxVel * this.scaleX;
			if (this.frame < 21)
			{
				this.frame = 20;
			}
			this.frame++;
			if (this.frame > 28)
			{
				AudioManager.Instance.PlaySound("butcher_spin", base.gameObject);
				this.frame = 21;
			}
			this.idleTimer += dt;
			if (this.idleTimer > 200f)
			{
				this.idleTimer = 0f;
				this.state = 3;
				this.frame = 1;
				this.xVel = 0f;
			}
		}
		else if (this.state == 3)
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
				if (this.attackType == 1)
				{
					this.state = 4;
					this.frame = 29;
					this.attackType = 2;
				}
				else
				{
					this.state = 9;
					this.frame = 42;
					this.attackType = 1;
				}
			}
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == 35)
			{
				this.state = 5;
			}
		}
		else if (this.state == 5)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 15f)
			{
				this.state = 6;
				this.idleTimer = 0f;
			}
		}
		else if (this.state == 6)
		{
			if (Game.Instance.player.x <= this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
			this.idleTimer += dt;
			if (this.idleTimer >= 10f)
			{
				this.fireShot();
				this.idleTimer = 0f;
				this.shotsFired++;
				if (this.shotsFired == 5)
				{
					this.state = 7;
					this.shotsFired = 0;
				}
			}
		}
		else if (this.state == 7)
		{
			this.idleTimer += dt;
			if (this.idleTimer > 15f)
			{
				this.idleTimer = 0f;
				this.state = 8;
			}
		}
		else if (this.state == 8)
		{
			this.frame++;
			if (this.frame > 41)
			{
				this.state = 1;
				this.frame = 1;
			}
		}
		else if (this.state == 9)
		{
			if (Game.Instance.player.x <= this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
			this.idleTimer += dt;
			if (this.idleTimer >= 10f)
			{
				this.idleTimer = 0f;
				this.frame = 43;
				this.state = 10;
				this.shotsFired++;
				this.throwAxe();
			}
		}
		else if (this.state == 10)
		{
			this.idleTimer += dt;
			if (this.idleTimer == 6f)
			{
				this.idleTimer = 0f;
				if (this.shotsFired == 10)
				{
					this.shotsFired = 0;
					this.state = 1;
					this.frame = 1;
				}
				else
				{
					this.state = 9;
					this.frame = 42;
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
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public void throwAxe()
	{
		Game.Instance.fxManager.emitProjectile(new Vector2(this.x, this.y + 16f), ProjectileType.BUTCHER_AXE, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		AudioManager.Instance.PlaySound("whoosh", base.gameObject);
	}

	public void fireShot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-10) : 10);
		float y = this.y;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("shoot", base.gameObject);
		Game.Instance.camView.screenShake(2f, 0.2f);
	}

	public override void updateRect()
	{
		base.updateRect();
		if (this.weaponRect != null)
		{
			this.weaponRect.x = (int)this.x - (int)((float)this.weaponW * 0.5f);
			this.weaponRect.y = (int)this.y - this.weaponH / 2;
			this.weaponRect.width = this.weaponW;
			this.weaponRect.height = this.weaponH;
		}
	}

	public void attackHitPlayer()
	{
		AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
		AudioManager.Instance.PlaySound("orc_hit", base.gameObject);
		Game.Instance.camView.screenShake(8f, 0.6f);
	}
}
