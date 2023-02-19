using System;

public class Arrow : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "arrow";
		this.projectileDieSound = "arrow_hit";
		this.realW = 28;
		this.realH = 8;
		this.damage = 5;
		this.frame = 1;
		this.maxVel = 12;
		this.destructible = true;
		this.rebuildAnimationClip();
	}
}
