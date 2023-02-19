using System;

public class ButcherAxe : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "butcher_axe";
		this.projectileDieSound = "hit";
		this.realW = 24;
		this.realH = 24;
		this.xVel = 0f;
		this.yVel = 16f;
		this.maxVel = 8;
		this.gravity = 1;
		this.damage = 10;
		this.frame = 1;
		this.maxVel = 12;
		this.type = 1;
		this.rebuildAnimationClip();
	}

	public void updateVelocity()
	{
		float num = -this.yVel;
		float x = Game.Instance.player.x;
		float num3;
		float xVel;
		if (x <= this.x)
		{
			float num2 = this.x - x;
			num3 = num2 * (float)(-(float)this.gravity) / (-2f * num);
			xVel = num3 + 2f;
		}
		else
		{
			float num2 = x - this.x;
			num3 = num2 * (float)(-(float)this.gravity) / (-2f * num) * -1f + 1f;
			xVel = num3 - 2f;
		}
		if (this.type == 1)
		{
			this.xVel = num3;
		}
		else
		{
			this.xVel = xVel;
			this.yVel -= 2f;
		}
	}

	public override void handleAnimation(float dt)
	{
		if (this.scaleX == 1f)
		{
			this.rotation -= 25f * dt;
		}
		else
		{
			this.rotation += 25f * dt;
		}
		this.frame = 1;
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleMovement(float dt)
	{
		this.yVel += (float)(-(float)this.gravity) * dt;
		if (this.yVel < -16f)
		{
			this.yVel = -16f;
		}
		this.testTileCollision(dt);
		this.updateRect();
	}
}
