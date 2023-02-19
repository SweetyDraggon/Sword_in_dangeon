using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CustomWindow : MonoBehaviour
{
	public bool isAnimating;

	public bool isShown;

	public bool buttonsEnabled = true;

	public bool removeWindowOnHide = true;

	public float zOrder;









	public event CustomWindowEvent onSlideInStart;

	public event CustomWindowEvent onSlideOutStart;

	public event CustomWindowEvent onSlideInComplete;

	public event CustomWindowEvent onSlideOutComplete;

	public void SetCamera(Camera cam)
	{
		Component[] componentsInChildren = base.GetComponentsInChildren<CustomButton>();
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			CustomButton customButton = (CustomButton)array[i];
			customButton.hudCamera = cam;
		}
	}

	public void EnableButtons(bool e)
	{
		this.buttonsEnabled = e;
		Component[] componentsInChildren = base.GetComponentsInChildren<CustomButton>();
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			CustomButton customButton = (CustomButton)array[i];
			customButton.isEnabled = e;
		}
		componentsInChildren = base.GetComponentsInChildren<CustomUIButton>();
		Component[] array2 = componentsInChildren;
		for (int j = 0; j < array2.Length; j++)
		{
			CustomUIButton customUIButton = (CustomUIButton)array2[j];
			customUIButton.enabled = e;
		}
	}

	public void TweakSortOrder(int zOrder)
	{
		Component[] componentsInChildren = base.GetComponentsInChildren<tk2dBaseSprite>();
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			tk2dBaseSprite tk2dBaseSprite = (tk2dBaseSprite)array[i];
			tk2dBaseSprite.SortingOrder += zOrder * 10;
		}
		Component[] componentsInChildren2 = base.GetComponentsInChildren<tk2dTextMesh>();
		Component[] array2 = componentsInChildren2;
		for (int j = 0; j < array2.Length; j++)
		{
			tk2dTextMesh tk2dTextMesh = (tk2dTextMesh)array2[j];
			tk2dTextMesh.SortingOrder += zOrder * 10;
		}
	}

	public void SlideIn(Vector3 onScreenPosition, Vector3 offScreenPosition, float animationSpeed)
	{
		if (this.isAnimating || this.isShown)
		{
			return;
		}
		this.isAnimating = true;
		this.slideInStarted();
		base.transform.localPosition = offScreenPosition;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		LeanTween.moveLocal(base.gameObject, onScreenPosition, animationSpeed).setEase(LeanTweenType.easeOutCubic).setOnComplete(new Action(this.slideInCompleted));
		AudioManager.Instance.PlaySound("menu_whoosh");
	}

	public void SlideOut(Vector3 onScreenPosition, Vector3 offScreenPosition, float animationSpeed)
	{
		if (this.isAnimating || !this.isShown)
		{
			return;
		}
		this.isAnimating = true;
		this.slideOutStarted();
		base.transform.localPosition = onScreenPosition;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		LeanTween.moveLocal(base.gameObject, offScreenPosition, animationSpeed).setEase(LeanTweenType.easeInCubic).setOnComplete(new Action(this.slideOutCompleted));
		AudioManager.Instance.PlaySound("menu_whoosh");
	}

	public void slideInStarted()
	{
		if (this.onSlideInStart != null)
		{
			this.onSlideInStart();
		}
	}

	public void slideOutStarted()
	{
		if (this.onSlideOutStart != null)
		{
			this.onSlideOutStart();
		}
	}

	public void slideInCompleted()
	{
		this.isAnimating = false;
		this.isShown = true;
		if (this.onSlideInComplete != null)
		{
			this.onSlideInComplete();
		}
	}

	public void slideOutCompleted()
	{
		this.isAnimating = false;
		this.isShown = false;
		if (this.onSlideOutComplete != null)
		{
			this.onSlideOutComplete();
		}
		if (this.removeWindowOnHide)
		{
			WindowManager.Instance.RemoveMenu(this);
		}
	}
}
