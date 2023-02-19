using System;

public class EnergyOrb2 : EnergyOrb
{
	public override void reset()
	{
		base.reset();
		this.currentAnimationName = "energy_orb2";
		this.type = 1;
		this.realW = 16;
		this.realH = 16;
		this.damage = 10;
		this.maxVel = 8;
		this.frame = 1;
		this.numFrames = 4;
		this.ignorePlatformCollisions = true;
		this.rebuildAnimationClip();
	}
}
