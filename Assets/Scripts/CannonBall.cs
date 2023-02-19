using System;
using UnityEngine;

public class CannonBall : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "cannon_ball";
		this.projectileDieSound = "explosion";
		this.realW = 16;
		this.realH = 16;
		this.damage = 10;
		this.frame = 1;
		this.maxVel = 8;
		this.numFrames = 8;
		this.rebuildAnimationClip();
	}

	public override void die()
	{
		if (!this.removed && this.alive)
		{
			this.alive = false;
			Game.Instance.fxManager.emitFlash(new Vector2(this.x + this.scaleX * 16f, this.y), FXParticleTypes.EXPLOSION_SMALL);
			if (this.projectileDieSound != string.Empty)
			{
				AudioManager.Instance.PlaySound(this.projectileDieSound, base.gameObject);
			}
			this.removeSelf();
		}
	}
}
