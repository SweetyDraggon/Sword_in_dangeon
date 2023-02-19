using System;
using UnityEngine;

public class AcidBolt : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "acid_bolt";
		this.realW = 16;
		this.realH = 16;
		this.maxVel = 8;
		this.damage = 10;
		this.frame = 1;
		this.numFrames = 8;
		this.rebuildAnimationClip();
	}

	public override void die()
	{
		if (!this.removed)
		{
			Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.ACID_SPARK, 10);
			this.removeSelf();
		}
	}
}
