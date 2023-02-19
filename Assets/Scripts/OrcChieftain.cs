using System;
using UnityEngine;

public class OrcChieftain : Boss
{
	public float swingVel;

	public float swingChargeTimer;

	public int swings;

	public int swingsMax;

	public void OnDestroy()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
	}

	public override void reset()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
		base.onAttackHitPlayer += new Enemy.EnemyCallback(this.attackHitPlayer);
		base.reset();
		this.healthBar.name.text =Localisation.GetString("ORC_CHIEF");
		this.bossNum = 0;
		this.headNum = 24;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 115;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 64;
		this.hitW = 64;
		this.maxVel = 3;
		this.swingVel = 4f;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 2000);
		this.damage = 10;
		this.moneyLow = 25;
		this.moneyHigh = 50;
		this.enemyType = 1;
		this.scaleX = -1f;
		this.inRange = false;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.swingChargeTimer = 0f;
		this.swings = 0;
		this.swingsMax = 2;
		this.xp = 200;
	}

	public override void removeSelf()
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
			if (this.frame < 21)
			{
				this.frame = 20;
			}
			this.frame++;
			if (this.frame > 36)
			{
				this.frame = 21;
			}
			this.testPlayerRange();
			if (Game.Instance.player.alive)
			{
				if (Game.Instance.player.x <= this.x - 32f)
				{
					this.xVel = (float)(-(float)this.maxVel);
				}
				else if (Game.Instance.player.x >= this.x + 32f)
				{
					this.xVel = (float)this.maxVel;
				}
				if (this.inRange)
				{
					this.xVel = 0f;
					this.state = 3;
				}
			}
			else
			{
				this.xVel = (float)this.maxVel * this.scaleX;
			}
		}
		else if (this.state == 3)
		{
			this.xVel = 0f;
			this.frame = 37;
			this.swingChargeTimer += dt;
			if (this.swingChargeTimer >= 3f)
			{
				this.swingChargeTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
			if (this.scaleX == -1f)
			{
				this.xVel = (float)(-(float)this.maxVel);
			}
			else
			{
				this.xVel = (float)this.maxVel;
			}
			this.frame++;
			if (this.frame == 41)
			{
				AudioManager.Instance.PlaySound("big_whoosh", base.gameObject);
				base.testWeaponCollision();
			}
			if (this.frame > 43)
			{
				this.swings++;
				if (this.swings > this.swingsMax)
				{
					this.state = 5;
					this.frame = 1;
					this.swings = 0;
				}
				else
				{
					this.frame = 37;
					this.state = 3;
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
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			this.xVel = 0f;
			this.frame = 44;
			this.idleTimer += dt;
			if (this.idleTimer == 20f)
			{
				this.throwSpear();
				this.idleTimer = 0f;
				this.state = 7;
				this.frame = 45;
			}
		}
		else if (this.state == 7)
		{
			this.xVel = 0f;
			this.frame = 45;
			this.idleTimer += dt;
			if (this.idleTimer > 20f)
			{
				this.idleTimer = 0f;
				this.frame = 1;
				this.state = 1;
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

	public void throwSpear()
	{
		AudioManager.Instance.PlaySoundDelayed("archer_shoot", UnityEngine.Random.Range(0f, 0.1f), base.gameObject, true);
		Game.Instance.fxManager.emitProjectile(new Vector2(this.x, this.y + 10f), ProjectileType.SPEAR, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
	}

	public void attackHitPlayer()
	{
		AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
		AudioManager.Instance.PlaySound("orc_hit", base.gameObject);
		Game.Instance.camView.screenShake(2f, 0.2f);
	}

	public override void updateRect()
	{
		base.updateRect();
		this.collisionRect.x = (int)this.x - (int)((float)this.hitW * 0.5f);
	}
}
