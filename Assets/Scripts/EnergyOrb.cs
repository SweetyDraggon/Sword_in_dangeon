using System;
using UnityEngine;

public class EnergyOrb : Projectile
{
	private SimpleTrig simpleTrig;

	public override void reset()
	{
		this.simpleTrig = new SimpleTrig();
		base.reset();
		this.currentAnimationName = "energy_orb";
		this.type = 1;
		this.realW = 16;
		this.realH = 16;
		this.damage = 1;
		this.maxVel = 6;
		this.frame = 1;
		this.numFrames = 4;
		this.ignorePlatformCollisions = true;
		this.rebuildAnimationClip();
	}

	public override void handleMovement(float dt)
	{
		if (this.type == 0)
		{
			this.type = 1;
		}
		if (this.type == 1 && this.xVel == 0f && this.yVel == 0f)
		{
			this.updateVelocity();
		}
		this.testTileCollision(dt);
		this.updateRect();
	}

	public void updateVelocity()
	{
		if (this.type == 1)
		{
			int num = (int)Mathf.Floor(this.x);
			int num2 = (int)Mathf.Floor(this.y);
			int num3 = (int)Mathf.Floor(Game.Instance.player.x);
			int num4 = (int)Mathf.Floor(Game.Instance.player.y);
			this.xVel = this.simpleTrig.getXvel((float)num, (float)num2, (float)num3, (float)num4, (float)this.maxVel);
			this.yVel = this.simpleTrig.getYvel((float)num, (float)num2, (float)num3, (float)num4, (float)this.maxVel);
		}
		else if (this.type == 2)
		{
			this.xVel = 0f;
			this.yVel = -4f;
		}
		else if (this.type == 3)
		{
			this.xVel = 3f;
			this.yVel = -3f;
		}
		else if (this.type == 4)
		{
			this.xVel = 4f;
			this.yVel = 0f;
		}
		else if (this.type == 5)
		{
			this.xVel = 3f;
			this.yVel = 3f;
		}
		else if (this.type == 6)
		{
			this.xVel = 0f;
			this.yVel = 4f;
		}
		else if (this.type == 7)
		{
			this.xVel = -3f;
			this.yVel = 3f;
		}
		else if (this.type == 8)
		{
			this.xVel = -4f;
			this.yVel = 0f;
		}
		else if (this.type == 9)
		{
			this.xVel = -3f;
			this.yVel = -3f;
		}
	}

	public override void die()
	{
		if (!this.removed)
		{
			Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.SPARK, 6);
			this.removeSelf();
		}
	}
}
