using System;

public class Wolfman : PatrollingEnemy
{
	public override void reset()
	{
		base.reset();
		this.soundOnHitPlayer = "hit";
		this.soundOnHitPlayerExtra = "heavy_hit";
		this.currentAnimationName = "wolfman";
		this.headNum = 8;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.WOLFMEN;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 95;
		this.weaponH = 48;
		this.maxVel = 2;
		this.health = 100;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 6;
		this.xp = 15;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
	}

	public override void removeSelf()
	{
	}
}
