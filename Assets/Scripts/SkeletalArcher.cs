using System;
using UnityEngine;

public class SkeletalArcher : Enemy
{
	public bool arrowShot;

	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "skeletal_archer";
		this.headNum = 2;
		this.enemyType = 1;
		this.enemyFlesh = false;
		this.questTrackingNum = QuestTracking.SKELETONS;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 32;
		this.weaponH = 32;
		this.maxVel = 0;
		this.health = 60;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 3;
		this.xp = 10;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.arrowShot = false;
	}

	public override void removeSelf()
	{
	}

	public override void handleAi(float dt)
	{
		if (this.state == 1)
		{
			if (Game.Instance.player.x < this.x)
			{
				this.scaleX = -1f;
			}
			else
			{
				this.scaleX = 1f;
			}
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= (float)this.idleMax)
			{
				this.idleTimer = 0f;
				this.state = 2;
				AudioManager.Instance.PlaySoundDelayed("archer_draw", UnityEngine.Random.Range(0f, 0.1f), base.gameObject, true);
			}
		}
		else if (this.state == 2)
		{
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= 30f)
			{
				this.idleTimer = 0f;
				this.state = 3;
				this.shoot();
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= 20f)
			{
				this.idleTimer = 0f;
				this.state = 1;
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
		if (this.state == 1)
		{
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		else if (this.state == 2)
		{
			this.frame = 13;
		}
		else if (this.state == 3)
		{
			this.frame = 14;
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public void shoot()
	{
		AudioManager.Instance.PlaySoundDelayed("archer_shoot", UnityEngine.Random.Range(0f, 0.1f), base.gameObject, true);
		Game.Instance.fxManager.emitProjectile(new Vector2(this.x, this.y), ProjectileType.ARROW, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
	}
}
