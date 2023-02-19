using System;

public class Spikes : Enemy
{
	public override void reset()
	{
		base.reset();
		this.maxVel = 0;
		this.currentAnimationName = "spikes";
		this.realW = 32;
		this.realH = 32;
	}

	public override void removeSelf()
	{
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && this.alive)
		{
			this.testActiveRange();
			if (base.isActive)
			{
			}
		}
	}
}
