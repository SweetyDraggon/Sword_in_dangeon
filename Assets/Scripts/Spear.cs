using System;

public class Spear : Projectile
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "spear";
		this.projectileDieSound = "hit";
		this.realW = 64;
		this.realH = 4;
		this.damage = 5;
		this.frame = 1;
		this.maxVel = 14;
		this.rebuildAnimationClip();
	}
}
