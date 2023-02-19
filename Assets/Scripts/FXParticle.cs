using System;
using UnityEngine;

public class FXParticle : MovieClip
{
	public FXParticleTypes type;

	public bool removed;

	public float removeTimer;

	public int gravity;

	public float xVel;

	public float yVel;

	public float rotationSpeed;

	public bool stationary;

	public int numFrames;

	public float fadeOutSpeed;

	public float fadeOutCutoff;

	public override void init()
	{
		this.reset();
	}

	public void reset()
	{
		this.removed = false;
		this.removeTimer = 0f;
		this.numFrames = 0;
		this.fadeOutSpeed = 0f;
		this.fadeOutCutoff = 0f;
		this.rotationSpeed = 10f;
		this.scaleX = (this.scaleY = 1f);
		this.gravity = 1;
		this.alpha = 1f;
		if (this.frame == 0)
		{
			this.frame = 1;
		}
		this.xVel = (float)UnityEngine.Random.Range(-6, 6);
		this.yVel = (float)UnityEngine.Random.Range(0, 10);
		this.rotation = (float)UnityEngine.Random.Range(0, 360);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.handleAnimation(dt);
			this.handleMovement(dt);
			if (this.fadeOutSpeed > 0f)
			{
				this.alpha -= this.fadeOutSpeed * dt;
				if (this.alpha < this.fadeOutCutoff)
				{
					this.removeSelf();
				}
			}
			this.removeTimer += 1f * dt;
			if (this.removeTimer >= 100f && !this.removed)
			{
				this.removeSelf();
			}
			this.testOutOfBounds();
		}
		this.testCleanUp();
		base.applyTransform();
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.numFrames > 0)
		{
			this.frame++;
			if (this.frame > this.numFrames)
			{
				this.removeSelf();
			}
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void handleMovement(float dt)
	{
		this.x += this.xVel * dt;
		this.y += this.yVel * dt;
		this.yVel += (float)(-(float)this.gravity) * dt;
		this.rotation += this.rotationSpeed * dt;
	}

	private void testOutOfBounds()
	{
		if (this.y < 10f)
		{
			this.removeSelf();
			return;
		}
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
		if (num > 640f || (num2 > 480f && !this.removed))
		{
			this.removeSelf();
		}
	}

	private void testCleanUp()
	{
	}

	private void removeSelf()
	{
		if (!this.removed)
		{
			this.removed = true;
			base.gameObject.SetActive(false);
		}
	}

	public void makeStationary()
	{
		this.xVel = 0f;
		this.yVel = 0f;
		this.gravity = 0;
		this.rotationSpeed = 0f;
	}

	public void SetType(FXParticleTypes t)
	{
		bool flag = true;
		if (this.type == FXParticleTypes.HEAD)
		{
			flag = false;
		}
		if (this.type != t)
		{
			this.type = t;
			if (this.type == FXParticleTypes.WOOD)
			{
				this.currentAnimationName = "wood_chip";
			}
			else if (this.type == FXParticleTypes.STONE)
			{
				this.currentAnimationName = "stone";
			}
			else if (this.type == FXParticleTypes.ACID_SPARK)
			{
				this.currentAnimationName = "acid_spark";
			}
			else if (this.type == FXParticleTypes.SPARK)
			{
				this.currentAnimationName = "spark";
			}
			else if (this.type == FXParticleTypes.MEAT)
			{
				this.currentAnimationName = "meat";
			}
			else if (this.type == FXParticleTypes.BONE)
			{
				this.currentAnimationName = "bone";
			}
			else if (this.type == FXParticleTypes.HEAD)
			{
				this.currentAnimationName = "head";
			}
			else if (this.type == FXParticleTypes.FLASH_SMALL)
			{
				this.currentAnimationName = "flash_small";
			}
			else if (this.type == FXParticleTypes.FLASH_HIT)
			{
				this.currentAnimationName = "hit_flash";
			}
			else if (this.type == FXParticleTypes.FLASH_COIN_SPARK)
			{
				this.currentAnimationName = "coin_spark";
			}
			else if (this.type == FXParticleTypes.EXPLOSION_SMALL)
			{
				this.currentAnimationName = "explosion_small";
			}
			else if (this.type == FXParticleTypes.FLASH_LEVEL_UP)
			{
				this.currentAnimationName = "level_up_spark";
			}
			this.rebuildAnimationClip();
		}
		if (flag)
		{
			this.frame = UnityEngine.Random.Range(0, 3) + 1;
		}
		base.gotoAndStop(this.frame - 1);
	}
}
