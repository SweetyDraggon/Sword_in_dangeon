using System;

public class PatrollingEnemy : Enemy
{
	public float swingChargeTimer;

	public float swingVel;

	public int idleFrame;

	public int walkFrame;

	public int chargeSwingFrame;

	public int weaponDamageFrame;

	public int weaponEndFrame;

	public override void reset()
	{
		base.reset();
		this.sortFrames();
	}

	public virtual void sortFrames()
	{
		this.idleFrame = 1;
		this.walkFrame = 12;
		this.chargeSwingFrame = 29;
		this.weaponDamageFrame = 30;
		this.weaponEndFrame = 33;
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
			this.testPlayerRange();
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
		else if (this.state == 4)
		{
		}
		if (!Game.Instance.player.alive)
		{
			this.state = 1;
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
			if (this.frame < this.walkFrame + 1)
			{
				this.frame = this.walkFrame;
			}
			this.frame++;
			if (this.frame >= this.chargeSwingFrame)
			{
				this.frame = this.walkFrame + 1;
			}
		}
		else if (this.state == 2)
		{
			this.frame++;
			if (this.frame > this.walkFrame)
			{
				this.frame = this.idleFrame;
			}
		}
		else if (this.state == 3)
		{
			this.frame = this.chargeSwingFrame;
		}
		else if (this.state == 4)
		{
			this.frame++;
			if (this.frame == this.weaponDamageFrame)
			{
				base.testWeaponCollision();
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
			}
			if (this.frame > this.weaponEndFrame)
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
		else if (this.state == 2 || this.state == 3 || (this.state == 4 && this.swingVel == 0f))
		{
			this.xVel = 0f;
		}
		else if (this.state == 4 && this.swingVel != 0f)
		{
			this.xVel = this.swingVel * this.scaleX;
		}
		this.handleKnockback(dt);
		base.handleMovement(dt);
	}
}
