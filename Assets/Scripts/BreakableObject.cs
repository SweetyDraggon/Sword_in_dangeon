using System;
using UnityEngine;

public class BreakableObject : Entity
{
	public int maxCamRange;

	public bool inRange;

	public bool metal;

	public int coinLow;

	public int coinHigh;

	public override void reset()
	{
		this.realW = 32;
		this.realH = 32;
		this.xVel = 0f;
		this.yVel = 0f;
		this.gravity = 0;
		this.frame = 1;
		this.alive = true;
		this.isActive = true;
		this.hit = false;
		this.maxCamRange = 480;
		this.hitTimer = 0f;
		this.hitMax = 1;
		this.scaleX = -1f;
		this.scaleY = 1f;
		this.health = 3;
		if (Game.Instance.map.dungeonLevel >= 53)
		{
			this.coinLow = 4;
			this.coinHigh = 6;
		}
		else if (Game.Instance.map.dungeonLevel >= 27)
		{
			this.coinLow = 2;
			this.coinHigh = 4;
		}
		else
		{
			this.coinLow = 1;
			this.coinHigh = 2;
		}
		this.metal = false;
		base.gotoAndStop(this.frame - 1);
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.testActiveRange();
			if (this.isActive)
			{
				this.handleAnimation(dt);
				this.updateRect();
			}
		}
	}

	public virtual void testActiveRange()
	{
	}

	public override void handleAnimation(float dt)
	{
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
		this.handleHitAnimation(dt);
	}

	public virtual void die()
	{
		if (!this.alive)
		{
			return;
		}
		this.alpha = 0f;
		this.alive = false;
		this.isActive = false;
		this.createGold();
		Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), Game.Instance.player.scaleX, FXParticleTypes.WOOD, 10);
		AudioManager.Instance.PlaySound("explosion", base.gameObject);
		base.gameObject.SetActive(false);
	}

	public virtual void createGold()
	{
		int num = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 20f) + 1;
		if (num == 1)
		{
			Game.Instance.fxManager.emitHealingPotion(new Vector2(this.x, this.y), Game.Instance.player.scaleX);
		}
		else
		{
			int amt = UnityEngine.Random.Range(this.coinLow, this.coinHigh + 1);
			Game.Instance.fxManager.emitCoins(new Vector2(this.x, this.y), Game.Instance.player.scaleX, amt);
		}
	}

	public override bool takeHit(int damage, bool isCritical = false, bool ignoreText = false)
	{
		if (!this.hit)
		{
			this.health--;
			if (this.health <= 0)
			{
				this.die();
			}
			else
			{
				this.hit = true;
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
}
