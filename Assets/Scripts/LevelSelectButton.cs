using System;
using TMPro;

public class LevelSelectButton : CustomUIButton
{
	public LevelSelect levelSelect;

	public int levelNum = 1;

	public int bossNum;

	public bool locked;

	public bool isBossLevel;

	public void SetLevelNum(int lvl)
	{
		this.levelNum = lvl;
		if (base.transform.Find("Label"))
		{
			TextMeshPro component = base.transform.Find("Label").GetComponent<TextMeshPro>();
			if (!this.isBossLevel)
			{
				component.text = Localisation.GetString("LVL")+ " " + this.levelNum;
			}
		}
		base.enabled = !this.locked;
	}

	public void SetBossNum(int boss)
	{
		this.bossNum = boss;
		if (base.transform.Find("Label"))
		{
            TextMeshPro component = base.transform.Find("Label").GetComponent<TextMeshPro>();
            if (this.isBossLevel)
			{
				component.text =Localisation.GetString("BOSS")+ " "  + this.bossNum;
			}
		}
	}

	public void UpdateSprites()
	{
		if (this.isBossLevel)
		{
			if (this.locked)
			{
				this.imageUp = "boss_portal_button3";
				this.imageDown = "boss_portal_button3";
			}
			else
			{
				this.imageUp = "boss_portal_button1";
				this.imageDown = "boss_portal_button2";
			}
		}
		else if (this.locked)
		{
			this.imageUp = "portal_button3";
			this.imageDown = "portal_button3";
		}
		else
		{
			this.imageUp = "portal_button1";
			this.imageDown = "portal_button2";
		}
		this.sprite.SetSprite(this.imageUp);
	}

	public override void onClick()
	{
		base.onClick();
        //FireBaseManager.instance.LogScreen("LEVEL " + this.levelNum);
		this.levelSelect.loadLevel(this.levelNum);
	}
}
