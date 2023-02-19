using System;

public class ChargingEnemy : Enemy
{
	public bool ridingCreature;

	public float chargeTimer;

	public float chargeTimerMax;

	public override void handleAi(float dt)
	{
		if (!Game.Instance.player.alive)
		{
			this.state = 1;
		}
		if (this.state == 1)
		{
			this.testPlayerRange();
			if (this.inRange)
			{
				if (Game.Instance.player.alive)
				{
					if (Game.Instance.player.x <= this.x - 32f)
					{
						this.scaleX = -1f;
					}
					else if (Game.Instance.player.x >= this.x + 32f)
					{
						this.scaleX = 1f;
					}
					if (this.ridingCreature)
					{
						AudioManager.Instance.PlaySound("small_whoosh", base.gameObject);
					}
				}
				this.state = 2;
			}
		}
		else if (this.state == 2)
		{
			this.testPlayerRange();
			base.testWeaponCollision();
			this.chargeTimer += 1f * dt;
			if (this.chargeTimer >= this.chargeTimerMax)
			{
				this.chargeTimer = 0f;
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			if (Game.Instance.player.alive)
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

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.ridingCreature)
		{
			if (this.state == 1)
			{
				if (this.frame < 13)
				{
					this.frame = 12;
				}
				this.frame++;
				if (this.frame > 22)
				{
					this.frame = 13;
				}
			}
			else if (this.state == 2)
			{
				this.frame = 23;
			}
			else if (this.state == 3)
			{
				this.frame++;
				if (this.frame > 12)
				{
					this.frame = 1;
				}
			}
		}
		else if (this.state == 1)
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
			if (this.frame < 28)
			{
				this.frame = 28;
			}
			this.frame++;
			if (this.frame > 36)
			{
				this.frame = 29;
			}
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
}
