using System;
using UnityEngine;

public class QuestPopup : MonoBehaviour
{
	public tk2dSprite icon;

	public tk2dTextMesh textMesh;

	private bool isAnimating;

	private bool isShown;

	public float timeLeft;

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

	public void activate(int i)
	{
		this.icon.SetSprite("quest_icon" + i);
		this.textMesh.text = "$" + Game.Instance.questHandler.quests[i - 1, 3];
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
	}
}
