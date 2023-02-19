using System;

public class Olaf : MovieClip
{
	public Rectangle collisionRect;

	public bool textShown;

	public bool allowedToActivateShop;

	public override void init()
	{
		this.textShown = false;
		this.allowedToActivateShop = true;
		this.alpha = 1f;
		this.scaleX = -1f;
		this.scaleY = 1f;
		this.collisionRect = new Rectangle(this.x - 48f, this.y - 32f, 96, 64);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.testPlayerCollision();
		}
	}

	public void testPlayerCollision()
	{
		if (this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			this.textShown = true;
			Game.Instance.hud.SetTutorialText(Localisation.GetString("Press_B_to_purchase_goods_from_Olaf's_shop!"));
			Game.Instance.hud.FadeTutorialTextIn();
			if (Game.Instance.player.attackPressed && this.allowedToActivateShop)
			{
				Game.Instance.hud.EnableControls(false);
				Game.Instance.player.cancelAttack();
				this.allowedToActivateShop = false;
				Game.Instance.paused = true;
				WindowManager.Instance.ShowMenu("shop_menu");
				base.Invoke("allowShop", 0.5f);
			}
		}
		else if (this.textShown)
		{
			base.Invoke("FadeOut", 0.3f);
			this.textShown = false;
		}
	}

	public void FadeOut()
	{
		Game.Instance.hud.FadeTutorialTextOut();
	}

	public void allowShop()
	{
		this.allowedToActivateShop = true;
	}

	public void removeSelf()
	{
	}
}
