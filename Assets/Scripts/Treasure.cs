using System;
using UnityEngine;

public class Treasure : Coin
{
	public override void reset()
	{
		base.reset();
		this.realW = 32;
		this.realH = 32;
		this.collisionRect = new Rectangle(this.x - (float)(this.realW / 2), this.y - (float)(this.realH / 2), this.realW, this.realH);
		this.type = 4;
		this.coinValue = 25;
		this.frame = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 6f) + 1;
		base.gotoAndStop(this.frame - 1);
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}
}
