using System;

public class DemonDoor : MovieClip
{
	public bool isActive;

	public override void init()
	{
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.isActive = false;
		this.frame = 1;
		base.gotoAndStop(this.frame - 1);
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
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.frame++;
		if (this.frame > 38)
		{
			this.frame = 38;
			this.isActive = false;
			this.alpha = 0f;
			base.gameObject.SetActive(false);
		}
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void removeSelf()
	{
	}
}
