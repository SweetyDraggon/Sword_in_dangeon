using System;

public class Spearman : ShieldedEnemy
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
		this.currentAnimationName = "spearman";
		this.headNum = 14;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.SPEARMEN;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 65;
		this.weaponH = 16;
		this.maxVel = 2;
		this.health = 140;
		this.moneyLow = 2;
		this.moneyHigh = 4;
		this.damage = 9;
		this.xp = 20;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
		this.shieldTimer = 0f;
		this.shieldTimerMax = 60f;
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

	public void attackHitPlayer()
	{
		AudioManager.Instance.PlaySound("blood_splat", base.gameObject);
	}
}
