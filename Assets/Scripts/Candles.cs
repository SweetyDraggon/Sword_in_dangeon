using System;

public class Candles : BreakableObject
{
	public override void reset()
	{
		base.reset();
		this.frame = 1;
		this.realH = 64;
		this.metal = true;
		this.currentAnimationName = "candles";
		this.rebuildAnimationClip();
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.frame++;
		if (this.frame > 4)
		{
			this.frame = 1;
		}
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
		base.handleHitAnimation(dt);
	}
}
