using System;

public class RoyalSpearman : ShieldedEnemy
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "royal_spearman";
		this.headNum = 18;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.ROYAL_SPEARMEN;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 85;
		this.weaponH = 48;
		this.maxVel = 2;
		this.health = 220;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 15;
		this.xp = 30;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
		this.shieldTimer = 0f;
		this.shieldTimerMax = 60f;
		this.enemyWearsMetal = true;
	}

	public override void sortFrames()
	{
		this.idleFrame = 1;
		this.walkFrame = 21;
		this.chargeSwingFrame = 41;
		this.weaponDamageFrame = 42;
		this.weaponEndFrame = 49;
	}

	public override void removeSelf()
	{
	}
}
