using System;

public class CustomUIButton : tk2dUIItem
{
	public tk2dSprite sprite;

	public string imageUp = string.Empty;

	public string imageDown = string.Empty;

	private void Awake()
	{
		this.sprite = base.GetComponent<tk2dSprite>();
		if (this.sprite == null && base.transform.Find("Sprite"))
		{
			this.sprite = base.transform.Find("Sprite").GetComponent<tk2dSprite>();
		}
		base.OnDown += new Action(this.onDown);
		base.OnUp += new Action(this.onUp);
		base.OnClick += new Action(this.onClick);
		base.OnRelease += new Action(this.onRelease);
	}

	public void updateSprites()
	{
		if (this.sprite != null && this.imageUp != string.Empty)
		{
			this.sprite.SetSprite(this.imageUp);
		}
	}

	public virtual void onDown()
	{
		if (this.imageDown.Length > 0)
		{
			this.sprite.SetSprite(this.imageDown);
		}
	}

	public virtual void onUp()
	{
		if (this.imageUp.Length > 0)
		{
			this.sprite.SetSprite(this.imageUp);
		}
	}

	public virtual void onClick()
	{
		AudioManager.Instance.PlaySound("button");
	}

	public virtual void onRelease()
	{
	}
}
