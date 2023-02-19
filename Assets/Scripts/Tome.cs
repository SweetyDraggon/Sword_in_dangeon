using System;
using UnityEngine;

public class Tome : Entity
{
	public override void reset()
	{
		base.reset();
		this.realW = 32;
		this.realH = 32;
		this.xVel = 0f;
		this.yVel = 0f;
		this.gravity = 0;
		this.frame = 1;
		this.alive = true;
		this.isActive = true;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.health = 3;
		this.currentAnimationName = "tome";
		this.rebuildAnimationClip();
		base.gotoAndStop(this.frame - 1);
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && base.isActive)
		{
			this.handleAnimation(dt);
			this.updateRect();
			this.testPlayerCollision();
		}
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 1f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.frame++;
		if (this.frame > 24)
		{
			this.frame = 1;
		}
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void testPlayerCollision()
	{
		if (this.alive && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			int a = (int)Mathf.Floor((float)(Main.playerStats.nextXpLevel / 3));
			Game.Instance.player.addXp(a);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.KNOWLEDGE_POWER, 1);
			Game.Instance.questHandler.trackItem(QuestTracking.TOMES_FOUND);
			this.die();
		}
	}

	public void die()
	{
		if (!this.alive)
		{
			return;
		}
		this.alpha = 0f;
		this.alive = false;
		this.isActive = false;
		AudioManager.Instance.PlaySound("healing", base.gameObject);
		base.gameObject.SetActive(false);
	}
}
