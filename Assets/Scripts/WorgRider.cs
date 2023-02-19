using System;

public class WorgRider : ChargingEnemy
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "worg_rider";
		this.headNum = 1;
		this.headNumCreature = 5;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.WORG_RIDERS;
		this.playerCollisionMultiplier = 1f;
		this.ridingCreature = true;
		this.realW = 48;
		this.realH = 32;
		this.weaponW = 38;
		this.weaponH = 32;
		this.maxVel = 4;
		this.chargeVel = 4;
		this.health = 140;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 9;
		this.xp = 20;
		this.idleTimer = 0f;
		this.idleMax = 80;
		this.chargeTimer = 0f;
		this.chargeTimerMax = 15f;
	}

	public override void removeSelf()
	{
	}
}
