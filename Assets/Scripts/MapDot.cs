using System;
using UnityEngine;

public class MapDot : MonoBehaviour
{
	public int state;

	public bool isActive;

	public float alpha;

	public tk2dSprite sprite;

	public void Awake()
	{
		this.sprite = base.GetComponent<tk2dSprite>();
		activate();
	}

	public void reset()
	{
		this.state = 1;
		this.alpha = 1f;
		this.isActive = true;
	}

	public void onEnterFrame(float dt)
	{
		if (this.isActive)
		{
			this.handleAnimation(dt);
		}
		this.sprite.color = new Color(1f, 1f, 1f, this.alpha);
	}

	public void handleAnimation(float dt)
	{
		if (this.state == 1)
		{
			this.alpha -= 0.1f;
			if (this.alpha < 0.1f)
			{
				this.state = 2;
			}
		}
		else if (this.state == 2)
		{
			this.alpha += 0.1f;
			if (this.alpha > 0.9f)
			{
				this.state = 1;
			}
		}
	}

	public void activate()
	{
		this.state = 1;
		this.isActive = true;
		this.alpha = 1f;
	}

	public void deactivate()
	{
		this.isActive = false;
		this.alpha = 0f;
	}
}
