using System;

public class Ogre : PatrollingEnemy
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
		this.currentAnimationName = "ogre";
		this.headNum = 17;
		this.enemyType = 1;
		this.questTrackingNum = QuestTracking.OGRES;
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 125;
		this.weaponH = 48;
		this.collisionRectUsesHitSize = true;
		this.hitH = 32;
		this.hitW = 64;
		this.maxVel = 2;
		this.health = 250;
		this.moneyLow = 4;
		this.moneyHigh = 8;
		this.damage = 25;
		this.xp = 30;
		this.idleTimer = 0f;
		this.idleMax = 20;
		this.swingChargeTimer = 0f;
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
		AudioManager.Instance.PlaySound("heavy_hit", base.gameObject);
		AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
		Game.Instance.camView.screenShake(5f, 0.4f);
	}
}
