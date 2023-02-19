using System;
using UnityEngine;

public class DamageText : MovieClip
{
	public tk2dTextMesh label;

	public tk2dTextMesh labelShadow;

	public float yVel;

	public int damage;

	private void Awake()
	{
		this.label = base.GetComponent<tk2dTextMesh>();
		this.labelShadow = this.label.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
	}

	public override void init()
	{
		this.reset();
	}

	public void reset()
	{
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.yVel = 8f;
	}

	public void setDamage(int d, bool critical = false)
	{
		this.damage = d;
		if (this.damage == 9999)
		{
			int num = (int)Mathf.Floor((float)(Main.playerStats.nextXpLevel / 3));
			this.label.text = "+" + num.ToString() + "xp";
			this.label.color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			this.label.text = this.damage.ToString();
			this.label.color = ((!critical) ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 0f, 0f, 1f));
		}
	}

	public void setText(string text)
	{
		this.label.text = text;
		this.label.color = new Color(1f, 1f, 1f, 1f);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.handleMovement(dt);
			this.handleAnimation(dt);
		}
	}

	public void handleMovement(float dt)
	{
		this.y += this.yVel * dt;
		this.yVel -= dt;
		if (this.yVel < 1f)
		{
			this.yVel = 1f;
		}
		if (this.yVel <= 2f)
		{
			this.alpha -= 0.05f * dt;
			if (this.alpha < 0f)
			{
				this.removeSelf();
			}
		}
	}

	public void handleAnimation(float dt)
	{
		Color color = this.label.color;
		color.a = this.alpha;
		this.label.color = color;
		color = this.labelShadow.color;
		color.a = this.alpha * 0.5f;
		this.labelShadow.color = color;
	}

	public void removeSelf()
	{
		base.gameObject.SetActive(false);
	}
}
