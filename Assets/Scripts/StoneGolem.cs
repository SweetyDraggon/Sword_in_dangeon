using System;
using UnityEngine;

public class StoneGolem : Boss
{
	public int shotsFired;

	public int attackType;

	public int strikes;

	public void OnDestroy()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
	}

	public override void reset()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
		base.onAttackHitPlayer += new Enemy.EnemyCallback(this.attackHitPlayer);
		base.reset();
		this.healthBar.name.text =Localisation.GetString("STONE_GOLEM");
		this.bossNum = 3;
		this.headNum = 27;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 95;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 110;
		this.hitW = 72;
		this.maxVel = 3;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 4500);
		this.damage = 25;
		this.moneyLow = 25;
		this.moneyHigh = 50;
		this.xp = 400;
		this.enemyType = 1;
		this.scaleX = -1f;
		this.inRange = false;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.shotsFired = 0;
		this.attackType = 1;
		this.strikes = 0;
	}

	public override void removeSelf()
	{
	}

	public void Update()
	{
	}

	public override void handleAi(float dt)
	{
		if (!Game.Instance.player.alive)
		{
			this.state = 2;
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
			this.xVel = this.scaleX * (float)this.maxVel;
			if (this.frame < 21)
			{
				this.frame = 20;
			}
			this.frame++;
			if (this.frame == 33)
			{
				AudioManager.Instance.PlaySound("creature_footstep", base.gameObject);
				Game.Instance.camView.screenShake(4f, 0.3f);
			}
			if (this.frame > 40)
			{
				this.frame = 21;
			}
			this.idleTimer += dt;
			if (this.idleTimer > 200f)
			{
				this.idleTimer = 0f;
				this.state = 3;
				this.frame = 1;
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
			this.idleTimer += dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
			this.xVel = this.scaleX * 2f;
			if (this.frame < 41)
			{
				this.frame = 40;
			}
			this.frame++;
			if (this.frame == 41 || this.frame == 51)
			{
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
			}
			if (this.frame == 43 || this.frame == 53)
			{
				AudioManager.Instance.PlaySound("creature_footstep", base.gameObject);
				Game.Instance.camView.screenShake(12f, 0.4f);
				base.testWeaponCollision();
			}
			if (this.frame > 60)
			{
				this.strikes++;
				if (this.strikes < 10)
				{
					this.frame = 41;
				}
				else
				{
					this.strikes = 0;
					this.frame = 1;
					this.state = 5;
					this.xVel = 0f;
				}
			}
		}
		else if (this.state == 5)
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
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			if (this.frame < 61)
			{
				this.frame = 60;
			}
			this.frame++;
			if (this.frame == 67)
			{
				this.fireShot();
			}
			if (this.frame > 70)
			{
				this.frame = 61;
				this.shotsFired++;
				if (this.shotsFired == 3)
				{
					this.frame = 1;
					this.state = 1;
					this.shotsFired = 0;
				}
			}
		}
		else if (this.state != 7)
		{
			if (this.state != 8)
			{
				if (this.state != 9)
				{
					if (this.state == 10)
					{
					}
				}
			}
		}
		if (this.state < 6 && Game.Instance.player.alive)
		{
			if (Game.Instance.player.x <= this.x - 32f)
			{
				this.scaleX = -1f;
			}
			else if (Game.Instance.player.x >= this.x + 32f)
			{
				this.scaleX = 1f;
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

	public void fireShot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-25) : 25);
		float y = this.y + 60f;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("shoot", base.gameObject);
		Game.Instance.camView.screenShake(2f, 0.2f);
	}

	public override void updateRect()
	{
		base.updateRect();
		this.collisionRect.x = (int)this.x - (int)((float)this.hitW * 0.5f);
		this.collisionRect.y = (int)this.y - this.hitH / 2 + 40;
		this.collisionRect.width = this.hitW;
		this.collisionRect.height = this.hitH;
	}

	public void attackHitPlayer()
	{
		AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
	}
}
