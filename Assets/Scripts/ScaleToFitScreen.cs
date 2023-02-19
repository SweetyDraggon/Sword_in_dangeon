using System;
using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
	private tk2dSprite sprite;

	private tk2dTiledSprite tiledSprite;

	private tk2dSlicedSprite slicedSprite;

	private Vector2 lastScreenSize;

	private void Start()
	{
		this.Update();
	}

	private void Update()
	{
		Vector2 lhs = new Vector2((float)Screen.width, (float)Screen.height);
		if (lhs != this.lastScreenSize)
		{
			this.lastScreenSize = lhs;
			if (this.sprite = base.GetComponent<tk2dSprite>())
			{
				this.sprite.gameObject.transform.localScale = new Vector3(tk2dCamera.Instance.ScreenExtents.width / this.sprite.CurrentSprite.GetUntrimmedBounds().size.x, tk2dCamera.Instance.ScreenExtents.height / this.sprite.CurrentSprite.GetUntrimmedBounds().size.y, 1f);
			}
			else if (this.tiledSprite = base.GetComponent<tk2dTiledSprite>())
			{
				this.tiledSprite.gameObject.transform.localScale = new Vector3(tk2dCamera.Instance.ScreenExtents.width / this.tiledSprite.dimensions.x, tk2dCamera.Instance.ScreenExtents.height / this.tiledSprite.dimensions.y, 1f);
			}
			else if (this.slicedSprite = base.GetComponent<tk2dSlicedSprite>())
			{
				this.slicedSprite.gameObject.transform.localScale = new Vector3(tk2dCamera.Instance.ScreenExtents.width / this.slicedSprite.dimensions.x, tk2dCamera.Instance.ScreenExtents.height / this.slicedSprite.dimensions.y, 1f);
			}
		}
	}
}
