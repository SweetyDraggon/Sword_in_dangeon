using System;
using UnityEngine;

public class ShopWindow : CustomWindow
{
	public GameObject background;

	public GameObject banner;

	public GameObject closeButton;

	public GameObject money;

	public GameObject railTop;

	public GameObject railBottom;

	public virtual void handleResolutions()
	{
		if (!GameCore.Instance.IS_IPAD)
		{
			if (this.railTop != null)
			{
				this.railTop.SetActive(false);
			}
			if (this.railBottom != null)
			{
				this.railBottom.SetActive(false);
			}
			bool flag = tk2dCamera.Instance.ScreenExtents.width * 4f > 960f;
			if (this.background != null)
			{
				this.background.GetComponent<tk2dSprite>().SetSprite((!flag) ? "shop_bg2" : "shop_bg3");
			}
			if (flag)
			{
				if (this.banner != null)
				{
					this.banner.transform.localPosition += new Vector3(0f, -30f, 0f);
				}
				if (this.closeButton != null)
				{
					this.closeButton.transform.localPosition += new Vector3(30f, -28f, 0f);
				}
				if (this.money != null)
				{
					this.money.transform.localPosition += new Vector3(-30f, -30f, 0f);
				}
			}
			else
			{
				if (this.banner != null)
				{
					this.banner.transform.localPosition += new Vector3(0f, -30f, 0f);
				}
				if (this.closeButton != null)
				{
					this.closeButton.transform.localPosition += new Vector3(-11f, -28f, 0f);
				}
				if (this.money != null)
				{
					this.money.transform.localPosition += new Vector3(15f, -30f, 0f);
				}
			}
		}
	}

	public virtual void closeClicked()
	{
	}
}
