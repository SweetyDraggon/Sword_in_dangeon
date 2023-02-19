using System;
using UnityEngine;

public class AxeTroll : PatrollingEnemy
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "axe_troll";
		this.headNum = 9;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.ORC_AXETHROWERS;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 110;
		this.weaponH = 48;
		this.maxVel = 2;
		this.health = 100;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 6;
		this.xp = 15;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
	}

	public override void removeSelf()
	{
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
				this.shoot();
			}
			if (this.frame > 35)
			{
				this.frame = 1;
				this.state = 2;
			}
		}
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public void shoot()
	{
		Game.Instance.fxManager.emitProjectile(new Vector2(this.x, this.y + 10f), ProjectileType.THROWING_AXE, (this.scaleX <= 0f) ? (-1) : 1, 1, this.damage);
		AudioManager.Instance.PlaySound("whoosh", base.gameObject);
	}
}
