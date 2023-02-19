using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPopup : MonoBehaviour
{
	public tk2dTextMesh textMesh;

	private bool isAnimating;

	private bool isShown;

	public float timeLeft;

	public List<string> achievements;

	private void Update()
	{
		if (!this.isAnimating && this.isShown)
		{
			this.timeLeft -= Time.deltaTime;
			if (this.timeLeft <= 0f)
			{
				this.timeLeft = 0f;
				this.deactivate();
			}
		}
	}

	public void AddAchievement(string achievement)
	{
		this.achievements.Add(achievement);
		if (!this.isShown && !this.isAnimating)
		{
			this.activate();
		}
	}

	public void activate()
	{
		this.textMesh.text = this.achievements[0];
		this.achievements.RemoveAt(0);
		this.SlideIn(new Vector3(0f, 0f, 0f), new Vector3(0f, 48f, 0f), 0.5f);
	}

	public void deactivate()
	{
		this.SlideOut(new Vector3(0f, 0f, 0f), new Vector3(0f, 48f, 0f), 0.5f);
	}

	public void SlideIn(Vector3 onScreenPosition, Vector3 offScreenPosition, float animationSpeed)
	{
		if (this.isAnimating || this.isShown)
		{
			return;
		}
		this.isAnimating = true;
		base.transform.localPosition = offScreenPosition;
		LeanTween.moveLocal(base.gameObject, onScreenPosition, animationSpeed).setEase(LeanTweenType.easeOutCubic).setOnComplete(new Action(this.slideInComplete));
	}

	public void SlideOut(Vector3 onScreenPosition, Vector3 offScreenPosition, float animationSpeed)
	{
		if (this.isAnimating || !this.isShown)
		{
			return;
		}
		this.isAnimating = true;
		base.transform.localPosition = onScreenPosition;
		LeanTween.moveLocal(base.gameObject, offScreenPosition, animationSpeed).setEase(LeanTweenType.easeInCubic).setOnComplete(new Action(this.slideOutComplete));
	}

	public void slideInComplete()
	{
		this.isShown = true;
		this.isAnimating = false;
		this.timeLeft = 2f;
	}

	public void slideOutComplete()
	{
		this.isShown = false;
		this.isAnimating = false;
		this.timeLeft = 0f;
		if (this.achievements.Count > 0)
		{
			this.activate();
		}
	}
}
