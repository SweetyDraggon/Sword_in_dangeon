using System;
using UnityEngine;

public class Coin : Entity
{
	public bool removed;

	public int coinValue;

	public float pickupDelay;

	public int highFrame;

	public int lowFrame;

	public int numBounces;

	public int maxBounces;

	public int bounceHeight;

	public void initWithType(int t)
	{
		this.type = t;
		this.reset();
	}

	public override void reset()
	{
		this.realW = 16;
		this.realH = 32;
		this.xVel = (float)UnityEngine.Random.Range(-4, 4);
		this.yVel = (float)UnityEngine.Random.Range(8, 14);
		this.maxVel = 8;
		this.gravity = 1;
		this.state = 1;
		this.frame = 1;
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.numBounces = 0;
		this.maxBounces = 5;
		this.bounceHeight = 10;
		this.airborne = true;
		this.removed = false;
		this.isActive = true;
		this.pickupDelay = 0f;
		if (this.type == 2)
		{
			this.coinValue = 2;
			this.lowFrame = 13;
			this.highFrame = 24;
		}
		else if (this.type == 3)
		{
			this.coinValue = 3;
			this.lowFrame = 25;
			this.highFrame = 36;
		}
		else
		{
			this.coinValue = 1;
			this.lowFrame = 1;
			this.highFrame = 12;
		}
		this.frame = this.lowFrame;
		base.gotoAndStop(this.frame - 1);
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		base.onWallCollideLeft -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideLeft += new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight += new Entity.OnCollideEvent(this.onWallCollision);
		base.onCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onCollideDown += new Entity.OnCollideEvent(this.bounce);
		base.onPlatformCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onPlatformCollideDown += new Entity.OnCollideEvent(this.bounce);
		base.onSlopeCollide -= new Entity.OnCollideEvent(this.stop);
		base.onSlopeCollide += new Entity.OnCollideEvent(this.stop);
	}

	private void Destroy()
	{
		base.onWallCollideLeft -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onPlatformCollideDown -= new Entity.OnCollideEvent(this.bounce);
		base.onSlopeCollide -= new Entity.OnCollideEvent(this.stop);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && base.isActive)
		{
			this.pickupDelay += dt / 30f;
			this.handleBouncing();
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
			this.die();
		}
	}

	public void handleBouncing()
	{
		if (this.numBounces >= this.maxBounces)
		{
			this.xVel = 0f;
		}
	}

	public new virtual void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.frame++;
		if (this.frame > this.highFrame)
		{
			this.frame = this.lowFrame;
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
		Game.Instance.player.addMoney(this.coinValue);
		AudioManager.Instance.PlaySound("coin", base.gameObject);
		if (this.type == 1)
		{
			Game.Instance.questHandler.trackItem(QuestTracking.COPPER_COINS_FOUND);
		}
		else if (this.type == 2)
		{
			Game.Instance.questHandler.trackItem(QuestTracking.SILVER_COINS_FOUND);
		}
		else if (this.type == 3)
		{
			Game.Instance.questHandler.trackItem(QuestTracking.GOLD_COINS_FOUND);
		}
		else if (this.type == 4)
		{
			Game.Instance.questHandler.trackItem(QuestTracking.TREASURE_PICKED_UP);
		}
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
		this.numBounces++;
		this.yVel = (float)((int)Mathf.Floor((float)(this.bounceHeight / this.numBounces)));
		this.handleBouncing();
	}

	public void stop(Entity entity)
	{
		this.numBounces = this.maxBounces;
		this.handleBouncing();
	}
}
