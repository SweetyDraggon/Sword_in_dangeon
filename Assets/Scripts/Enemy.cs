using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : Entity
{
	public delegate void EnemyCallback();

	public string soundOnHitPlayer;

	public string soundOnHitPlayerExtra;

	public string soundOnHurtPlayer;

	public int headNum;

	public int headNumCreature;

	public QuestTracking questTrackingNum;

	public float playerCollisionMultiplier;

	public bool enemyFlesh;

	public bool enemyWearsMetal;

	public bool enemyTurnsAtWalls;

	public bool enemyTurnsAtLedges;

	public Rectangle weaponRect;

	public int weaponW;

	public int weaponH;

	public Rectangle damageRect;

	public int damageW;

	public int damageH;

	public bool collisionRectUsesHitSize;

	public int hitW;

	public int hitH;

	public int chargeVel;

	public int maxCamRange;

	public int damage;

	public int moneyLow;

	public int moneyHigh;

	public int enemyType;

	public bool inRange;

	public float idleTimer;

	public int idleMax;

	public int xp;

	public int xpLevel;

	public bool disableLedgeTurn;



	public event Enemy.EnemyCallback onAttackHitPlayer;

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && this.alive)
		{
			this.testActiveRange();
			if (this.isActive)
			{
				this.handleAi(dt);
				this.handleMovement(dt);
				this.handleAnimation(dt);
				this.testPlayerCollision();
				this.testSpikeCollision();
			}
		}
	}

	public override void reset()
	{
		this.collisionRectUsesHitSize = false;
		this.headNumCreature = -1;
		this.enemyFlesh = true;
		this.questTrackingNum = QuestTracking.NONE;
		this.playerCollisionMultiplier = 0.5f;
		this.realW = 32;
		this.realH = 32;
		this.xVel = 0f;
		this.yVel = 0f;
		this.gravity = 1;
		this.state = 1;
		this.frame = 1;
		this.alive = true;
		this.isActive = false;
		this.hit = false;
		this.maxCamRange = 480;
		this.hitTimer = 0f;
		this.hitMax = 1;
		this.scaleX = -1f;
		this.scaleY = 1f;
		this.inRange = false;
		this.knockBack = false;
		this.knockBackTimer = 0f;
		this.enemyTurnsAtLedges = true;
		this.enemyTurnsAtWalls = true;
		base.gotoAndStop(this.frame - 1);
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		this.damageRect = new Rectangle(this.x, this.y, 0, 0);
		this.weaponRect = new Rectangle(this.x, this.y - (float)(this.weaponH / 2), this.weaponW, this.weaponH);
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
		base.onWallCollideLeft -= new Entity.OnCollideEvent(this.onWallCollision);
		base.onWallCollideRight -= new Entity.OnCollideEvent(this.onWallCollision);
		if (this.enemyTurnsAtWalls)
		{
			base.onWallCollideLeft += new Entity.OnCollideEvent(this.onWallCollision);
			base.onWallCollideRight += new Entity.OnCollideEvent(this.onWallCollision);
		}
		base.onLedgeCollideLeft -= new Entity.OnCollideEvent(this.onLedgeCollision);
		base.onLedgeCollideRight -= new Entity.OnCollideEvent(this.onLedgeCollision);
		if (this.enemyTurnsAtLedges)
		{
			base.onLedgeCollideLeft += new Entity.OnCollideEvent(this.onLedgeCollision);
			base.onLedgeCollideRight += new Entity.OnCollideEvent(this.onLedgeCollision);
		}
	}

	public virtual void testActiveRange()
	{
		if (this.isActive)
		{
			if (Mathf.Abs(Vector2.Distance(Game.Instance.camView.transform.position, base.transform.position)) > (float)this.maxCamRange)
			{
				bool flag = false;
				base.gameObject.active = flag;
				this.isActive = flag;
			}
		}
		else if (!this.isActive && this.alive && Mathf.Abs(Vector2.Distance(Game.Instance.camView.transform.position, base.transform.position)) < (float)this.maxCamRange)
		{
			bool flag = true;
			base.gameObject.active = flag;
			this.isActive = flag;
		}
	}

	public virtual void handleAi(float dt)
	{
	}

	public override void updateRect()
	{
		if (this.collisionRectUsesHitSize)
		{
			this.collisionRect.width = this.hitW;
			this.collisionRect.height = this.hitH;
		}
		else
		{
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
		this.collisionRect.x = (int)this.x - this.realW / 2;
		this.collisionRect.y = (int)this.y - this.realH / 2;
		this.damageRect.x = (int)this.x - this.damageW / 2;
		this.damageRect.y = (int)this.y - this.damageH / 2;
		if (this.weaponRect != null)
		{
			this.weaponRect.x = (int)this.x;
			this.weaponRect.y = (int)this.y - this.weaponH / 2;
			this.weaponRect.width = this.weaponW;
			this.weaponRect.height = this.weaponH;
			if (this.scaleX == 1f)
			{
				this.weaponRect.x = (int)this.x;
			}
			else
			{
				this.weaponRect.x = (int)this.x - this.weaponW;
			}
		}
	}

	public virtual void testPlayerCollision()
	{
		if (base.gameObject.active && this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			if (Game.Instance.player.alive && !Game.Instance.player.hit && this.soundOnHurtPlayer != string.Empty)
			{
				AudioManager.Instance.PlaySound(this.soundOnHurtPlayer, base.gameObject);
			}
			Game.Instance.player.takeHit((int)Mathf.Round((float)this.damage * this.playerCollisionMultiplier), false, false);
		}
	}

	public virtual void testPlayerRange()
	{
		float num;
		if (Game.Instance.player.x <= this.x)
		{
			num = this.x - Game.Instance.player.x;
		}
		else
		{
			num = Game.Instance.player.x - this.x;
		}
		float num2;
		if (Game.Instance.player.y <= this.y)
		{
			num2 = this.y - Game.Instance.player.y;
		}
		else
		{
			num2 = Game.Instance.player.y - this.y;
		}
		if (num < (float)((this.weaponW <= 0) ? 110 : (this.weaponW + 8)) && num2 < 60f)
		{
			this.inRange = true;
		}
		else
		{
			this.inRange = false;
		}
	}

	public void testWeaponCollision()
	{
		if (!Game.Instance.player.alive)
		{
			return;
		}
		if (this.weaponRect.Intersects(Game.Instance.player.collisionRect) && !Game.Instance.player.hit)
		{
			if (this.onAttackHitPlayer != null)
			{
				this.onAttackHitPlayer();
			}
			Game.Instance.player.takeHit((int)Mathf.Round((float)this.damage), false, false);
			Game.Instance.player.hitPlayerSound(base.gameObject, this.soundOnHitPlayer, this.soundOnHitPlayerExtra);
		}
	}

	public virtual void die()
	{
		if (!this.alive)
		{
			return;
		}
		AchievementHandler.Instance.Increment(ACHIEVEMENT.SLAYER, 1);
		Main.playerStats.enemiesSlain++;
		this.alpha = 0f;
		this.alive = false;
		this.isActive = false;
		Game.Instance.fxManager.emitHead(new Vector2(this.x, this.y), this.scaleX, this.headNum);
		if (this.headNumCreature > 0)
		{
			Game.Instance.fxManager.emitHead(new Vector2(this.x, this.y), this.scaleX, this.headNumCreature);
		}
		if (this.enemyFlesh)
		{
			Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.MEAT, 10);
			AudioManager.Instance.PlaySound("blood_splat", base.gameObject);
		}
		else
		{
			Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.BONE, 10);
			AudioManager.Instance.PlaySound("bone_crunch", base.gameObject);
		}
		this.createGold();
		Game.Instance.player.addXp(this.xp);
		if (this.questTrackingNum != QuestTracking.NONE)
		{
			Game.Instance.questHandler.trackItem((int)this.questTrackingNum);
		}
		base.gameObject.SetActive(false);
	}

	public virtual void createGold()
	{
		int amt = UnityEngine.Random.Range(this.moneyLow, this.moneyHigh + 1);
		Game.Instance.fxManager.emitCoins(new Vector2(this.x, this.y), this.scaleX, amt);
	}

	public override bool takeHit(int damage, bool isCritical = false, bool ignoreText = false)
	{
		if (!this.hit)
		{
			if (isCritical)
			{
				damage *= 2;
			}
			this.knockBack = true;
			this.health -= damage;
			if (this.health <= 0)
			{
				this.die();
			}
			else
			{
				this.currentSprite.color = new Color(1f, 0f, 0f, 1f);
				this.hit = true;
				Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, (!this.enemyFlesh) ? FXParticleTypes.BONE : FXParticleTypes.MEAT, 2);
			}
			if (!ignoreText)
			{
				Game.Instance.fxManager.emitDamageText(new Vector2(this.x, this.y), damage, isCritical);
			}
		}
		return this.health <= 0;
	}

	public new void handleHitAnimation(float dt)
	{
		if (this.hit)
		{
			this.hitTimer += dt;
			if (this.hitTimer >= (float)this.hitMax)
			{
				this.currentSprite.color = new Color(1f, 1f, 1f, 1f);
				this.hit = false;
				this.hitTimer = 0f;
			}
		}
	}

	public override void testSpikeCollision()
	{
		if (!this.hit && this.map.GetTileID(this.centerX, this.centerY, 1) == 301)
		{
			this.takeHit(1000, false, true);
		}
	}

	public void onWallCollision(Entity entity = null)
	{
		if (this.scaleX == -1f)
		{
			this.scaleX = 1f;
		}
		else
		{
			this.scaleX = -1f;
		}
	}

	public void onLedgeCollision(Entity entity = null)
	{
		if (this.disableLedgeTurn)
		{
			return;
		}
		if (this.scaleX == -1f)
		{
			this.scaleX = 1f;
		}
		else
		{
			this.scaleX = -1f;
		}
	}
}
