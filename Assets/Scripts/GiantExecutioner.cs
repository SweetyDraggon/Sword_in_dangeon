using System;
using UnityEngine;

public class GiantExecutioner : ChargingEnemy
{
	public void OnDestroy()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
	}

	public override void reset()
	{
		base.onAttackHitPlayer -= new Enemy.EnemyCallback(this.attackHitPlayer);
		base.onAttackHitPlayer += new Enemy.EnemyCallback(this.attackHitPlayer);
		base.reset();
		this.currentAnimationName = "giant_executioner";
		this.headNum = 21;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.GIANT_EXECUTIONERS;
		this.realW = 40;
		this.realH = 32;
		this.weaponW = 160;
		this.weaponH = 16;
		this.collisionRectUsesHitSize = true;
		this.hitW = 48;
		this.hitH = 64;
		this.maxVel = 2;
		this.chargeVel = 2;
		this.health = 280;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 18;
		this.xp = 35;
		this.idleTimer = 0f;
		this.idleMax = 80;
		this.chargeTimer = 0f;
		this.chargeTimerMax = 50f;
	}

	public override void removeSelf()
	{
	}

	public override void handleAi(float dt)
	{
		base.handleAi(dt);
		if (!Game.Instance.player.alive)
		{
			this.state = 1;
		}
	}

	public override void handleAnimation(float dt)
	{
		base.handleAnimation(dt);
		if (this.frame == 29)
		{
			AudioManager.Instance.PlaySound("butcher_spin", base.gameObject);
		}
	}

	public override void testPlayerCollision()
	{
		if (this.state == 2)
		{
			this.playerCollisionMultiplier = 1f;
		}
		else
		{
			this.playerCollisionMultiplier = 0.5f;
		}
		if (base.gameObject.activeInHierarchy && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			Game.Instance.player.takeHit((int)Mathf.Round((float)this.damage * this.playerCollisionMultiplier), false, false);
		}
	}

	public override void updateRect()
	{
		base.updateRect();
		if (this.weaponRect != null)
		{
			this.weaponRect.x = (int)this.x - (int)((float)this.weaponW * 0.5f);
			this.weaponRect.y = (int)this.y - this.weaponH / 2;
			this.weaponRect.width = this.weaponW;
			this.weaponRect.height = this.weaponH;
		}
	}

	public void attackHitPlayer()
	{
		AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
		AudioManager.Instance.PlaySound("orc_hit", base.gameObject);
		Game.Instance.camView.screenShake(4f, 0.6f);
	}
}
