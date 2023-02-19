using System;

public class DoorPuff : MovieClip
{
	public bool isActive;

	public override void init()
	{
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.isActive = false;
		this.alpha = 0f;
		this.frame = 1;
		base.gotoAndStop(this.frame);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused && this.isActive)
		{
			this.handleAnimation(dt);
		}
	}

	public void activate()
	{
		this.isActive = true;
		this.alpha = 0.8f;
		this.frame = 1;
		base.gotoAndStop(this.frame);
	}

	public void deactivate()
	{
		this.isActive = false;
		this.alpha = 0f;
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.alpha -= 0.1f * dt;
		this.frame++;
		if (this.frame > 8)
		{
			this.frame = 1;
			this.deactivate();
		}
		base.gotoAndStop(this.frame);
	}

	public void removeSelf()
	{
	}
}
