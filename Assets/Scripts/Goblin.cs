using System;

public class Goblin : Enemy
{
	public float chargeTimer;

	public int chargeTimerMax;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "goblin";
		this.headNum = 4;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.GOBLINS;
		this.weaponW = 38;
		this.weaponH = 32;
		this.maxVel = 3;
		this.chargeVel = 4;
		this.health = 60;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 3;
		this.xp = 10;
		this.idleTimer = 0f;
		this.idleMax = 80;
		this.chargeTimer = 0f;
		this.chargeTimerMax = 8;
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
			this.frame = 29;
		}
		else if (this.state == 3)
		{
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleAi(float dt)
	{
		if (this.state == 1)
		{
			this.testPlayerRange();
			if (this.inRange)
			{
				if (Game.Instance.player.x <= this.x)
				{
					this.scaleX = -1f;
				}
				else
				{
					this.scaleX = 1f;
				}
				this.state = 2;
				AudioManager.Instance.PlaySound("small_attack", base.gameObject);
				AudioManager.Instance.PlaySound("small_whoosh", base.gameObject);
			}
		}
		else if (this.state == 2)
		{
			base.testWeaponCollision();
			this.chargeTimer += 1f * dt;
			if (this.chargeTimer >= (float)this.chargeTimerMax)
			{
				this.chargeTimer = 0f;
				this.state = 3;
			}
		}
		else if (this.state == 3)
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
				if (this.inRange)
				{
					this.state = 2;
				}
				else
				{
					this.state = 1;
				}
			}
		}
	}

	public override void handleMovement(float dt)
	{
		if (this.state == 1)
		{
			this.xVel = (float)this.maxVel * this.scaleX;
		}
		else if (this.state == 2)
		{
			this.xVel = (float)this.chargeVel * this.scaleX;
		}
		else if (this.state == 3)
		{
			this.xVel = 0f;
		}
		this.handleKnockback(dt);
		base.handleMovement(dt);
	}

	public override void removeSelf()
	{
	}
}
