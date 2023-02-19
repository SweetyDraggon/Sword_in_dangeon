using System;
using UnityEngine;

public class HealingPotion : Entity
{
	public bool removed;

	public int numBounces;

	public int hpValue;

	public float pickupDelay;

	public float idleTimer;

	public float idleTimerMax;

	public override void reset()
	{
		this.realW = 32;
		this.realH = 32;
		this.xVel = (float)UnityEngine.Random.Range(-5, 5);
		this.yVel = (float)UnityEngine.Random.Range(8, 16);
		this.maxVel = 8;
		this.gravity = 1;
		this.state = 1;
		this.frame = 1;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.numBounces = 0;
		this.airborne = true;
		this.removed = false;
		this.isActive = true;
		this.hpValue = 5;
		this.pickupDelay = 0f;
		this.frame = 1;
		base.gotoAndStop(this.frame - 1);
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		base.onWallCollideLeft -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideLeft += new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight += new Entity.OnCollideEvent(this.onWallCollision);
		base.onCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onCollideDown += new Entity.OnCollideEvent(this.bounce);
		base.onPlatformCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onPlatformCollideDown += new Entity.OnCollideEvent(this.bounce);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && base.isActive)
		{
			this.pickupDelay += dt;
			this.handleMovement(dt);
			this.handleAnimation(dt);
			this.testPlayerCollision();
		}
	}

	public void testPlayerCollision()
	{
		if (this.pickupDelay < 0.5f)
		{
			return;
		}
		if (this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			Game.Instance.fxManager.emitFlash(new Vector2(this.x, this.y + 8f), FXParticleTypes.FLASH_COIN_SPARK);
			Game.Instance.fxManager.emitCustomText(new Vector2(this.x, this.y), this.hpValue.ToString() + " HP");
			Game.Instance.player.addHealth(this.hpValue);
			AudioManager.Instance.PlaySound("potion", base.gameObject);
			this.die();
		}
	}

	public new void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.idleTimer += dt;
		if (this.idleTimer > 45f)
		{
			this.frame++;
			if (this.frame > 10)
			{
				this.frame = 1;
				this.idleTimer = 0f;
			}
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void die()
	{
		if (!this.isActive)
		{
			return;
		}
		Game.Instance.fxManager.emitFlash(new Vector2(this.x, this.y + 8f), FXParticleTypes.FLASH_COIN_SPARK);
		this.alpha = 0f;
		this.isActive = false;
		base.gameObject.SetActive(false);
	}

	public new void removeSelf()
	{
	}

	public void onWallCollision(Entity entity)
	{
		if (this.scaleX == 1f)
		{
			this.scaleX = -1f;
		}
		else if (this.scaleX == -1f)
		{
			this.scaleX = 1f;
		}
	}

	public void bounce(Entity entity)
	{
		if (this.numBounces >= 3)
		{
			this.xVel = 0f;
			this.yVel = 0f;
			return;
		}
		this.numBounces++;
		this.yVel = (float)((int)Mathf.Floor(8f / (float)this.numBounces));
	}
}
