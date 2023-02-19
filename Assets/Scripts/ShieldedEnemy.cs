using System;

public class ShieldedEnemy : PatrollingEnemy
{
	public float shieldTimer;

	public float shieldTimerMax;

	public override void reset()
	{
		base.reset();
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
			this.idleTimer += 1f * dt;
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
		else if (this.state == 3)
		{
			this.swingChargeTimer += 1f * dt;
			if (this.swingChargeTimer >= 20f)
			{
				this.swingChargeTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state != 4)
		{
			if (this.state != 5)
			{
				if (this.state == 6)
				{
					if (Game.Instance.player.x <= this.x)
					{
						this.scaleX = -1f;
					}
					else
					{
						this.scaleX = 1f;
					}
					this.shieldTimer += 1f * dt;
					if (this.shieldTimer >= this.shieldTimerMax)
					{
						this.shieldTimer = 0f;
						this.state = 7;
					}
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
		this.disableLedgeTurn = false;
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
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		else if (this.state == 3)
		{
			this.frame = 29;
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == 30)
			{
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
				base.testWeaponCollision();
			}
			if (this.frame > 35)
			{
				this.frame = 36;
				this.state = 5;
			}
		}
		else if (this.state == 5)
		{
			this.frame++;
			if (this.frame == 38)
			{
				this.state = 6;
			}
		}
		else if (this.state == 6)
		{
			this.disableLedgeTurn = true;
			this.frame = 38;
		}
		else if (this.state == 7)
		{
			this.frame++;
			if (this.frame > 40)
			{
				this.frame = 1;
				this.state = 2;
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
		else if (this.state == 2 || this.state == 3 || this.state == 4 || this.state == 5 || this.state == 6 || this.state == 7)
		{
			this.xVel = 0f;
		}
		this.handleKnockback(dt);
		base.handleMovement(dt);
	}

	public override bool takeHit(int damage, bool isCritical = false, bool ignoreText = false)
	{
		if (!this.hit)
		{
			this.knockBack = true;
			this.shieldTimer = 0f;
			if (this.state != 6 || damage >= 1000)
			{
				base.takeHit(damage, isCritical, false);
			}
			else
			{
				AudioManager.Instance.PlaySound("clang", base.gameObject);
			}
		}
		return this.health <= 0;
	}

	public override void handleKnockback(float dt)
	{
		if (this.state == 6)
		{
			return;
		}
		base.handleKnockback(dt);
	}
}
