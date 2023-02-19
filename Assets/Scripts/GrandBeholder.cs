using System;
using UnityEngine;

public class GrandBeholder : Beholder
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "grand_beholder";
		this.headNum = 19;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.GRAND_BEHOLDERS;
		this.realW = 32;
		this.realH = 32;
		this.maxVel = 0;
		this.health = 200;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 9;
		this.xp = 25;
		this.idleTimer = 0f;
		this.idleMax = 60;
		this.bounceState = 1;
		this.bounceDistance = 0f;
		this.bounceDistanceMax = 6f;
		this.bounceDelay = 0f;
		this.blinkTimer = 0f;
		this.blinkTimerMax = 40f;
		this.blinks = 1;
		this.fireDelay = 0f;
		this.fireDelayMax = 8f;
		this.shotsFired = 0;
	}

	public override void removeSelf()
	{
	}

	public override void shoot()
	{
		float x = this.x + (float)((this.scaleX <= 0f) ? (-6) : 6);
		float y = this.y;
		Game.Instance.fxManager.emitProjectile(new Vector2(x, y), ProjectileType.ENERGY_ORB2, (this.scaleX <= 0f) ? (-1) : 1, 1, 0);
		Game.Instance.fxManager.emitFlash(new Vector2(x, y), FXParticleTypes.FLASH_SMALL);
		AudioManager.Instance.PlaySound("beholder_shoot", base.gameObject);
	}
}
