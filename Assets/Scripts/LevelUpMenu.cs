using PlayHaven;
using System;

public class LevelUpMenu : CustomWindow
{
	public tk2dTextMesh txtStrength;

	public tk2dTextMesh txtStamina;

	public tk2dTextMesh txtDexterity;

	public int state = 1;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
		base.onSlideInStart += new CustomWindowEvent(this.activate);
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
		base.onSlideInComplete += new CustomWindowEvent(this.slideInComplete);
		base.onSlideOutComplete -= new CustomWindowEvent(this.resume);
		base.onSlideOutComplete += new CustomWindowEvent(this.resume);
		this.state = 1;
		this.updateText();
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
		base.onSlideOutComplete -= new CustomWindowEvent(this.resume);
	}

	public void slideInComplete()
	{
		if (GameCore.Instance.playhavenEnabled)
		{
			//PlayHavenManager.instance.ContentRequest("level_up");
		}
	}

	public void activate()
	{
		this.state = 1;
		this.updateText();
	}

	public void deactivate()
	{
		this.state = 2;
		this.updateText();
		WindowManager.Instance.HideMenu(this);
	}

	public void resume()
	{
		Game.Instance.paused = false;
	}

	public void updateText()
	{
		this.txtStrength.text = Main.playerStats.strength.ToString();
		this.txtStamina.text = Main.playerStats.stamina.ToString();
		this.txtDexterity.text = Main.playerStats.dexterity.ToString();
	}

	public void strengthClicked()
	{
		if (this.state == 1)
		{
			Main.playerStats.baseStrength++;
			Main.playerStats.updatePlayerStats();
			Game.Instance.player.weapon.updateDamage();
			this.deactivate();
		}
	}

	public void staminaClicked()
	{
		if (this.state == 1)
		{
			Main.playerStats.baseStamina++;
			Main.playerStats.updatePlayerStats();
			Main.playerStats.currentHealth += 2;
			if (Main.playerStats.currentHealth > Main.playerStats.maxHealth)
			{
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
			}
			Game.Instance.player.health = Main.playerStats.currentHealth;
			this.deactivate();
		}
	}

	public void dexterityClicked()
	{
		if (this.state == 1)
		{
			Main.playerStats.baseDexterity++;
			Main.playerStats.updatePlayerStats();
			this.deactivate();
		}
	}
}
