using System;
using UnityEngine;

public class Priest : MovieClip
{
	public int state;

	public bool healed;

	public float idleTimer;

	public int healingCost;

	public Rectangle collisionRect;

	public bool textShown;

	public bool alertShown;

	public float canAskForHealing;

	public bool playerNeedsHealing;

	public override void init()
	{
		this.alertShown = false;
		this.textShown = false;
		this.state = 1;
		this.healed = false;
		this.idleTimer = 0f;
		this.canAskForHealing = 0f;
		this.healingCost = 50;
		this.scaleX = -1f;
		this.scaleY = 1f;
		this.alpha = 1f;
		this.playerNeedsHealing = false;
		this.collisionRect = new Rectangle(this.x - 48f, this.y - 32f, 96, 64);
		this.frame = 1;
		base.gotoAndStop(this.frame - 1);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.playerNeedsHealing = (Main.playerStats.currentHealth < Main.playerStats.maxHealth);
			if (this.canAskForHealing > 0f)
			{
				this.canAskForHealing -= dt / 30f;
				if (this.canAskForHealing < 0f)
				{
					this.canAskForHealing = 0f;
				}
			}
			this.handleAnimation(dt);
			this.testPlayerCollision();
		}
	}

	public void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		if (this.state == 1)
		{
			this.frame++;
			if (this.frame > 12)
			{
				this.frame = 1;
			}
		}
		else if (this.state == 2)
		{
			if (this.frame < 13)
			{
				this.frame = 12;
			}
			this.frame++;
			if (this.frame > 16)
			{
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			this.frame = 17;
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= 10f)
			{
				this.idleTimer = 0f;
				int num = (int)this.x + ((this.scaleX != 1f) ? (-21) : 21);
				int num2 = (int)this.y + 41;
				Game.Instance.fxManager.emitFlash(new Vector2((float)num, (float)num2), FXParticleTypes.FLASH_SMALL);
				Game.Instance.camView.screenShake(13f, 0.4f);
				this.state = 4;
				Game.Instance.player.health = (Main.playerStats.currentHealth = Main.playerStats.maxHealth);
				Main.saveGame();
				Game.Instance.hud.updateHealth(true);
			}
		}
		else if (this.state == 4)
		{
			this.frame = 17;
			this.idleTimer += 1f * dt;
			if (this.idleTimer >= 20f)
			{
				this.idleTimer = 0f;
				this.state = 5;
			}
		}
		else if (this.state == 5)
		{
			this.frame++;
			if (this.frame > 21)
			{
				this.frame = 1;
				this.state = 1;
			}
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void cancelledPurchase()
	{
		Game.Instance.paused = false;
		this.alertShown = false;
		this.canAskForHealing = 1f;
	}

	public void confirmedPurchase()
	{
		Game.Instance.paused = false;
		this.alertShown = false;
		this.healed = true;
		base.Invoke("performHealing", 0.7f);
	}

	public void showCoinsScreen()
	{
		base.Invoke("showShop", 0.7f);
	}

	public void showShop()
	{
		Game.Instance.paused = true;
		this.alertShown = false;
		WindowManager.Instance.ShowShopPage(ShopPage.COINS);
	}

	public void performHealing()
	{
		this.state = 2;
		Main.playerStats.money -= this.healingCost;
		AudioManager.Instance.PlaySound("cash_register");
		AudioManager.Instance.PlaySound("healing", base.gameObject);
		AchievementHandler.Instance.SetValue(ACHIEVEMENT.BLESSED, 1);
	}

	public void testPlayerCollision()
	{
		if (this.collisionRect.Intersects(Game.Instance.player.collisionRect) && !this.healed)
		{
			this.textShown = true;
			if (this.playerNeedsHealing)
			{
				Game.Instance.hud.SetTutorialText(Localisation.GetString("Press_B_to_receive_a_healing_from_the_priest_for_$") + this.healingCost.ToString());
			}
			else
			{
				Game.Instance.hud.SetTutorialText(Localisation.GetString("Priest's_healing_not_needed."));
			}
			Game.Instance.hud.FadeTutorialTextIn();
			if (Game.Instance.player.attackPressed && this.playerNeedsHealing)
			{
				Game.Instance.hud.EnableControls(false);
				Game.Instance.player.cancelAttack();
				if (!this.alertShown && this.canAskForHealing <= 0f)
				{
					Game.Instance.paused = true;
					if (Main.playerStats.money >= this.healingCost)
					{
						this.alertShown = true;
						WindowManager.Instance.ShowAlertView(Localisation.GetString( "Confirm_Healing"),Localisation.GetString("Are_you_sure_you_wish_to_purchase_a_healing_for") + " " + this.healingCost +Localisation.GetString("COINS?"), Localisation.GetString("NO"), Localisation.GetString("YES"), base.gameObject, "cancelledPurchase", "confirmedPurchase");
					}
					else
					{
						this.alertShown = true;
                        WindowManager.Instance.ShowAlertView(Localisation.GetString("Not_Enough_Money"),Localisation.GetString("You_do_not_have_enough_to_purchase_this_item."), Localisation.GetString("ok"), string.Empty, base.gameObject, "cancelledPurchase", string.Empty);
					}
				}
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

	public void removeSelf()
	{
	}
}
