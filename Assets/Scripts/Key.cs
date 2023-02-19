using System;
using UnityEngine;

public class Key : MovieClip
{
	public Rectangle collisionRect;

	public int realW;

	public int realH;

	public bool isActive;

	public float idleTimer;

	public override void init()
	{
		this.realW = 32;
		this.realH = 32;
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.isActive = true;
		this.idleTimer = 0f;
		this.frame = 1;
		base.applyTransform();
		this.rebuildAnimationClip();
		base.gotoAndStop(this.frame);
		this.collisionRect = new Rectangle(this.x, this.y + 16f, 32, 16);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && this.isActive)
		{
			this.handleAnimation(dt);
			this.updateCollisionRect();
			this.testPlayerCollision();
		}
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.idleTimer += 1f * dt;
		if (this.idleTimer >= 30f)
		{
			this.frame++;
			if (this.frame > 10)
			{
				this.frame = 1;
				this.idleTimer = 0f;
			}
		}
		base.gotoAndStop(this.frame);
	}

	public void updateCollisionRect()
	{
		this.collisionRect.x = (int)(this.x - 16f);
		this.collisionRect.y = (int)(this.y + 16f);
		this.collisionRect.width = 32;
		this.collisionRect.height = 16;
	}

	public void testPlayerCollision()
	{
		if (base.gameObject.activeInHierarchy && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			AchievementHandler.Instance.Increment(ACHIEVEMENT.KEY_MASTER, 1);
			Game.Instance.keyFound = true;
			this.die();
		}
	}

	public void die()
	{
		this.alpha = 0f;
		this.isActive = false;
		base.gameObject.SetActive(false);
		AudioManager.Instance.PlaySound("key", base.gameObject);
		Game.Instance.fxManager.emitFlash(new Vector2(this.x + 16f, this.y + 16f), FXParticleTypes.FLASH_COIN_SPARK);
		Game.Instance.questHandler.trackItem(QuestTracking.KEYS_FOUND);
	}

	public void removeSelf()
	{
	}
}
