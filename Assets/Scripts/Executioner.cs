using System;
using UnityEngine;

public class Executioner : ChargingEnemy
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
		this.currentAnimationName = "executioner";
		this.headNum = 6;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.EXECUTIONERS;
		this.realW = 40;
		this.realH = 32;
		this.weaponW = 108;
		this.weaponH = 16;
		this.maxVel = 2;
		this.chargeVel = 2;
		this.health = 140;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 9;
		this.xp = 20;
		this.idleTimer = 0f;
		this.idleMax = 80;
		this.chargeTimer = 0f;
		this.chargeTimerMax = 90f;
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
		if (base.gameObject.active && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
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
		Game.Instance.camView.screenShake(2f, 0.4f);
	}
}
