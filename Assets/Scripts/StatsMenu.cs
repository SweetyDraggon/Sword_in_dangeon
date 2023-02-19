using System;
using TMPro;

public class StatsMenu : ShopWindow
{
	public TextMeshPro txtLevel;

	public tk2dTextMesh txtXP;

	public tk2dTextMesh txtXPNext;

	public tk2dTextMesh txtStrength;

	public tk2dTextMesh txtStamina;

	public tk2dTextMesh txtDexterity;

	public tk2dTextMesh txtHitPoints;

	public tk2dTextMesh txtDamage;

	public tk2dTextMesh txtCritical;

	public int state = 1;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
		base.onSlideInStart += new CustomWindowEvent(this.activate);
		this.updateText();
		this.handleResolutions();
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.activate);
	}

	public void activate()
	{
		this.updateText();
	}

	public override void closeClicked()
	{
		this.deactivate();
	}

	public void deactivate()
	{
		WindowManager.Instance.HideMenu(this);
	}

	public void updateText()
	{
		this.txtLevel.text =Localisation.GetString("LVL")+ " " + Main.playerStats.playerLevel.ToString();
		this.txtXP.text = Main.playerStats.playerXp.ToString();
		this.txtXPNext.text = Main.playerStats.nextXpLevel.ToString() + "XP";
		this.txtStrength.text = Main.playerStats.strength.ToString();
		this.txtStamina.text = Main.playerStats.stamina.ToString();
		this.txtDexterity.text = Main.playerStats.dexterity.ToString();
		this.txtHitPoints.text = Main.playerStats.maxHealth.ToString();
		this.txtDamage.text = Game.Instance.player.weapon.damage.ToString();
		this.txtCritical.text = Main.playerStats.dexterity.ToString() + "%";
	}
}
