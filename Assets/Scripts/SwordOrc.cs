using System;

public class SwordOrc : PatrollingEnemy
{
	public override void reset()
	{
		base.reset();
		this.soundOnHitPlayer = "hit";
		this.soundOnHitPlayerExtra = "heavy_hit";
		this.currentAnimationName = "sword_orc";
		this.headNum = 1;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.ORC_SWORDSMEN;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 75;
		this.weaponH = 48;
		this.maxVel = 2;
		this.health = 60;
		this.moneyLow = 1;
		this.moneyHigh = 2;
		this.damage = 3;
		this.xp = 10;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
	}

	public override void removeSelf()
	{
	}
}
