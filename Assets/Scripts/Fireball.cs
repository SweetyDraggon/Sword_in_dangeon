using System;
using UnityEngine;

public class Fireball : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "fireball";
		this.type = 1;
		this.realW = 14;
		this.realH = 14;
		this.damage = 5;
		this.xVel = 0f;
		this.yVel = 6f;
		this.maxVel = 6;
		this.frame = 1;
		this.numFrames = 8;
		this.ignorePlatformCollisions = true;
		this.rebuildAnimationClip();
	}

	public override void handleMovement(float dt)
	{
		this.xVel = 0f;
		this.yVel += (float)(-(float)this.gravity) * dt;
		if (this.yVel < -16f)
		{
			this.yVel = -16f;
		}
		this.testTileCollision(dt);
		this.updateRect();
	}

	public override void die()
	{
		if (!this.removed)
		{
			Game.Instance.fxManager.emitFlash(new Vector2(this.x, this.y - 10f), FXParticleTypes.EXPLOSION_SMALL);
			AudioManager.Instance.PlaySound("explosion", base.gameObject);
			this.removeSelf();
		}
	}
}
