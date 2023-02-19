using System;
using UnityEngine;

public class DragonFire : MovieClip
{
	public Dragon dragon;

	public bool isActive;

	public Rectangle collisionRect;

	public int damage;

	public int realW;

	public int realH;

	public override void init()
	{
		this.isActive = false;
		this.alpha = 0f;
		this.frame = 1;
		this.damage = 10;
		this.realW = 320;
		this.realH = 32;
		this.x = 410f;
		this.y = 5f;
		this.scaleX = -1f;
		this.scaleY = 1f;
		this.collisionRect = new Rectangle(this.x - (float)this.realW, this.y - (float)(this.realH / 2), this.realW, this.realH);
	}

	public override void update(float dt)
	{
		this.onEnterFrame(dt);
		base.applyTransform();
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = Mathf.Floor(this.x);
		localPosition.y = Mathf.Floor(this.y);
		base.transform.localPosition = localPosition;
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && this.isActive)
		{
			this.handleAnimation(dt);
			this.updateRect();
			this.testPlayerCollision();
		}
	}

	public void updateRect()
	{
		this.collisionRect.x = (int)Mathf.Floor(this.x) - 176;
		this.collisionRect.y = (int)Mathf.Floor(this.y) + 128;
		this.collisionRect.width = this.realW;
		this.collisionRect.height = this.realH;
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.frame++;
		if (this.frame > 4)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void testPlayerCollision()
	{
		if (!this.dragon.alive)
		{
			return;
		}
		if (this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			Game.Instance.player.takeHit(this.damage, false, false);
		}
	}

	public void activate()
	{
		this.isActive = true;
		this.alpha = 1f;
	}

	public void deactivate()
	{
		this.isActive = false;
		this.alpha = 0f;
	}
}
