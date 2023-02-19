using System;
using UnityEngine;

public class Boss : Enemy
{
	public BossHealthBar healthBar;

	public int bossNum;

	public override void reset()
	{
		this.healthBar = Game.Instance.hud.bossHealth;
		base.reset();
		this.realW = 32;
		this.realH = 32;
		this.weaponW = 125;
		this.weaponH = 64;
		this.collisionRectUsesHitSize = true;
		this.hitH = 64;
		this.hitW = 48;
		this.maxVel = 3;
		this.alive = true;
		this.state = 1;
		this.frame = 1;
		this.maxHealth = (this.health = 500);
		this.damage = 10;
		this.moneyLow = 100;
		this.moneyHigh = 100;
		this.enemyType = 1;
		this.scaleX = -1f;
	}

	public override void onEnterFrame(float dt)
	{
		base.onEnterFrame(dt);
		this.updateHealth();
	}

	public void updateHealth()
	{
		this.healthBar.health = this.health;
		this.healthBar.healthMax = this.maxHealth;
		this.healthBar.gameObject.SetActive(true);
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
		base.handleHitAnimation(dt);
		base.gotoAndStop(this.frame - 1);
	}

	public override void die()
	{
		base.die();
		Game.Instance.keyFound = true;
	}

	public override void createGold()
	{
		bool flag = Main.playerStats.defeatedBosses[this.bossNum] > 0;
		Main.playerStats.defeatedBosses[this.bossNum] = 1;
		if (this.bossNum == 0)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOSS1, 1);
		}
		if (this.bossNum == 1)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOSS2, 1);
		}
		if (this.bossNum == 2)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOSS3, 1);
		}
		if (this.bossNum == 3)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOSS4, 1);
		}
		if (this.bossNum == 4)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOSS5, 1);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.LEGENDARY_HERO, 1);
		}
		int amt = UnityEngine.Random.Range(this.moneyLow, this.moneyHigh + 1);
		if (flag)
		{
			amt = this.moneyLow;
		}
		Game.Instance.fxManager.emitCoins(new Vector2(this.x, this.y), this.scaleX, amt);
		if (!flag)
		{
			Game.Instance.fxManager.emitTreasure(new Vector2(this.x, this.y), this.scaleX, 4);
		}
	}
}
