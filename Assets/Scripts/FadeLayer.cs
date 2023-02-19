using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FadeLayer : MonoBehaviour
{
	public tk2dSprite sprite;

	public float fadeMax = 1f;

	public float alpha = 1f;

	public bool isAnimating;

	public float fadeSpeed = 0.65f;

	public bool isShown = true;

	public bool isFadingIn;

	public bool isFadingOut;





	public event FadeEvent onFadeInComplete;

	public event FadeEvent onFadeOutComplete;

	private void Awake()
	{
		this.sprite = base.GetComponent<tk2dSprite>();
	}

	public void Update()
	{
		this.updateSprite();
	}

	public void updateSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = base.GetComponent<tk2dSprite>();
		}
		if (this.sprite != null)
		{
			Color color = this.sprite.color;
			color.a = this.alpha;
			this.sprite.color = color;
		}
	}

	private void updateAlpha(float val)
	{
		this.alpha = val;
	}

	public void FadeIn()
	{
		if (this.isAnimating && this.isFadingIn)
		{
			return;
		}
		this.isFadingIn = true;
		this.isAnimating = true;
		LeanTween.cancel(base.gameObject);
		LeanTween.value(base.gameObject, new Action<float>(this.updateAlpha), 0f, this.fadeMax, this.fadeSpeed).setEase(LeanTweenType.easeInOutCubic).setOnComplete(new Action(this.OnCompleteFadeIn));
	}

	public void FadeOut()
	{
		if (this.isAnimating && this.isFadingOut)
		{
			return;
		}
		this.isFadingOut = true;
		this.isAnimating = true;
		LeanTween.cancel(base.gameObject);
		LeanTween.value(base.gameObject, new Action<float>(this.updateAlpha), this.fadeMax, 0f, this.fadeSpeed).setEase(LeanTweenType.easeInOutCubic).setOnComplete(new Action(this.OnCompleteFadeOut));
	}

	private void OnCompleteFadeIn()
	{
		this.isAnimating = false;
		this.isShown = true;
		this.isFadingIn = false;
		this.alpha = this.fadeMax;
		if (this.onFadeInComplete != null)
		{
			this.onFadeInComplete();
		}
	}

	private void OnCompleteFadeOut()
	{
		this.isAnimating = false;
		this.isShown = false;
		this.isFadingOut = false;
		this.alpha = 0f;
		if (this.onFadeOutComplete != null)
		{
			this.onFadeOutComplete();
		}
	}
}
