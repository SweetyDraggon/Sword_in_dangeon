using System;
using UnityEngine;

public class MovieClip : MonoBehaviour
{
	public string currentAnimationName;

	public tk2dSpriteAnimationClip currentAnimationClip;

	public tk2dSpriteAnimator currentAnimation;

	public tk2dSprite currentSprite;

	public float x;

	public float y;

	public float rotation;

	public float alpha;

	public float scaleX;

	public float scaleY;

	public int frame;

	public float nextUpdate;

	public float nextFixedUpdate;

	private void Awake()
	{
		this.currentSprite = base.GetComponent<tk2dSprite>();
		if (base.transform.Find("Sprite"))
		{
			this.currentSprite = base.transform.Find("Sprite").GetComponent<tk2dSprite>();
		}
		if (this.currentSprite)
		{
			this.currentAnimation = this.currentSprite.GetComponent<tk2dSpriteAnimator>();
		}
	}

	public virtual void update(float dt)
	{
		this.onEnterFrame(dt);
		this.applyTransform();
	}

	public virtual void init()
	{
	}

	public virtual void onEnterFrame(float dt)
	{
	}

	public void applyTransform()
	{
		Vector3 position = base.transform.position;
		position.x = Mathf.Floor(this.x);
		position.y = Mathf.Floor(this.y);
		base.transform.position = position;
		base.transform.rotation = Quaternion.Euler(0f, 0f, this.rotation);
		base.transform.localScale = new Vector3(this.scaleX, this.scaleY, 1f);
		if (this.currentSprite != null)
		{
			Color color = this.currentSprite.color;
			color.a = this.alpha;
			this.currentSprite.color = color;
		}
	}

	public void SetPosition(float nX, float nY)
	{
		Vector3 position = base.transform.position;
		position.x = nX;
		position.y = nY;
		base.transform.position = position;
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
	}

	public void SetPosition(int nX, int nY)
	{
		this.SetPosition((float)nX, (float)nY);
	}

	public void SetPosition(Vector3 pos)
	{
		this.SetPosition(pos.x, pos.y);
	}

	public virtual void rebuildAnimationClip()
	{
		if (this.currentAnimation == null)
		{
			return;
		}
		if (this.currentAnimation.GetClipByName(this.currentAnimationName) != null)
		{
			this.currentAnimationClip = this.currentAnimation.GetClipByName(this.currentAnimationName);
			if (this.currentAnimationClip != null)
			{
				this.currentAnimation.Play(this.currentAnimationClip);
				this.currentAnimation.Stop();
				this.currentAnimation.SetFrame(this.frame);
			}
		}
	}

	public void gotoAndStop(int f)
	{
		if (this.currentAnimation == null)
		{
			return;
		}
		this.currentAnimation.SetFrame(f);
		this.currentAnimation.Stop();
	}
}
