using System;
using UnityEngine;

public class Projectile : Entity
{
	public int damage;

	public bool removed;

	public int numFrames;

	public bool destructible;

	public string projectileDieSound;

	public override void reset()
	{
		base.reset();
		base.onCollideRight -= new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideRight += new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideLeft -= new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideLeft += new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideUp -= new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideUp += new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideDown -= new Entity.OnCollideEvent(this.onCollisionDie);
		base.onCollideDown += new Entity.OnCollideEvent(this.onCollisionDie);
		this.currentAnimationName = "arrow";
		this.numFrames = 0;
		this.destructible = false;
		this.removed = false;
		this.alive = true;
		this.damage = 1;
		this.frame = 1;
		this.scaleX = 1f;
		this.maxVel = 5;
		this.type = 1;
		this.rebuildAnimationClip();
	}

	public void onCollisionDie(Entity entity = null)
	{
		this.die();
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.handleMovement(dt);
			this.handleAnimation(dt);
			if (Game.Instance.player.alive)
			{
				this.testPlayerCollision();
			}
			base.applyTransform();
		}
		this.testCleanUp();
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.numFrames > 0)
		{
			this.frame++;
			if (this.frame > this.numFrames)
			{
				this.frame = 1;
			}
		}
		else
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleMovement(float dt)
	{
		this.xVel = (float)this.maxVel * this.scaleX;
		this.yVel = 0f;
		this.testTileCollision(dt);
		this.updateRect();
	}

	public void testCleanUp()
	{
		if (Game.Instance.cleanUp)
		{
			this.removeSelf();
		}
	}

	public void testPlayerCollision()
	{
		if (base.gameObject.active && this.collisionRect != null && Game.Instance.player != null && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			Game.Instance.player.takeHit(this.damage, false, false);
			this.die();
		}
	}

	public virtual void die()
	{
		if (!this.removed && this.alive)
		{
			this.alive = false;
			Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.WOOD, 10);
			if (this.projectileDieSound != string.Empty)
			{
				AudioManager.Instance.PlaySound(this.projectileDieSound, base.gameObject);
			}
			this.removeSelf();
		}
	}

	public override void removeSelf()
	{
		if (!this.removed)
		{
			this.removed = true;
			base.gameObject.SetActive(false);
		}
	}
}
