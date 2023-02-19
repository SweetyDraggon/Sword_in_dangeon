using System;


public class PlayerStats 
{
	public int[] savedStats;

	public int damage;

	public int money;

	public int[] weapons;

	public int[] armors;

	public int[] potions;

	public int[] rings;

	public int[] amulets;

	public int[] defeatedBosses;

	public int equippedWeapon;

	public int equippedArmor;

	public int equippedRing;

	public int equippedAmulet;

	public int levelReached;

	public int playerLevel;

	public int playerXp;

	public int[] xpLevels;

	public int nextXpLevel;

	public int enemiesSlain;

	public int levelsCleared;

	public int questsCompleted;

	public int[] questStatus;

	public int maxDamage;

	public int maxHealth;

	public int currentHealth;

	public int baseStrength;

	public int baseStamina;

	public int baseDexterity;

	public int baseCrit;

	public int strength;

	public int stamina;

	public int dexterity;

	public int armor;

	public int crit;

	public PlayerStats()
	{
		this.levelReached = 1;
		this.damage = 1;
		this.money = 0;
		this.savedStats = new int[]
		{
			1,
			0,
			1,
			0,
			0,
			0,
			1,
			0,
			100,
			5,
			12,
			5
		};
		int[] expr_3B = new int[20];
		expr_3B[0] = 1;
		this.weapons = expr_3B;
		this.armors = new int[10];
		this.potions = new int[10];
		this.rings = new int[20];
		this.amulets = new int[20];
		this.defeatedBosses = new int[5];
		this.equippedWeapon = 1;
		this.equippedArmor = 0;
		this.equippedRing = 0;
		this.equippedAmulet = 0;
		this.playerLevel = 1;
		this.playerXp = 0;
		this.xpLevels = new int[]
		{
			0,
			100,
			300,
			600,
			1000,
			1500,
			2100,
			2800,
			3600,
			4500,
			5500,
			6600
		};
		this.nextXpLevel = 100;
		this.baseStrength = 5;
		this.baseStamina = 12;
		this.baseDexterity = 5;
		this.enemiesSlain = 0;
		this.levelsCleared = 0;
		this.questsCompleted = 0;
		this.questStatus = new int[75];
	}
	

	public void setStats()
	{
		this.levelReached = this.savedStats[0];
		this.money = this.savedStats[1];
		this.equippedWeapon = this.savedStats[2];
		this.equippedArmor = this.savedStats[3];
		this.equippedRing = this.savedStats[4];
		this.equippedAmulet = this.savedStats[5];
		this.playerLevel = this.savedStats[6];
		this.playerXp = this.savedStats[7];
		this.nextXpLevel = this.savedStats[8];
		this.baseStrength = this.savedStats[9];
		this.baseStamina = this.savedStats[10];
		this.baseDexterity = this.savedStats[11];
		this.strength = 0;
		this.stamina = 0;
		this.dexterity = 0;
		this.crit = 0;
		this.armor = 0;
		this.maxDamage = 0;
		this.updatePlayerStats();
		this.currentHealth = this.maxHealth;
		this.enemiesSlain = 0;
		this.levelsCleared = 0;
		this.questsCompleted = 0;
	}

	public void updateSavedStats()
	{
		this.savedStats[0] = this.levelReached;
		this.savedStats[1] = this.money;
		this.savedStats[2] = this.equippedWeapon;
		this.savedStats[3] = this.equippedArmor;
		this.savedStats[4] = this.equippedRing;
		this.savedStats[5] = this.equippedAmulet;
		this.savedStats[6] = this.playerLevel;
		this.savedStats[7] = this.playerXp;
		this.savedStats[8] = this.nextXpLevel;
		this.savedStats[9] = this.baseStrength;
		this.savedStats[10] = this.baseStamina;
		this.savedStats[11] = this.baseDexterity;
	}

	public void updatePlayerStats()
	{
		this.strength = 0;
		this.stamina = 0;
		this.dexterity = 0;
		this.crit = 0;
		if (this.equippedRing > 0)
		{
			if (Main.itemStats.ringStats[this.equippedRing - 1, 2] == "STR")
			{
				this.strength += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 5]);
			}
			else if (Main.itemStats.ringStats[this.equippedRing - 1, 2] == "STA")
			{
				this.stamina += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 5]);
			}
			else if (Main.itemStats.ringStats[this.equippedRing - 1, 2] == "DEX")
			{
				this.dexterity += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 5]);
			}
			if (Main.itemStats.ringStats[this.equippedRing - 1, 3] == "STR")
			{
				this.strength += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 6]);
			}
			else if (Main.itemStats.ringStats[this.equippedRing - 1, 3] == "STA")
			{
				this.stamina += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 6]);
			}
			else if (Main.itemStats.ringStats[this.equippedRing - 1, 3] == "DEX")
			{
				this.dexterity += Convert.ToInt32(Main.itemStats.ringStats[this.equippedRing - 1, 6]);
			}
		}
		if (this.equippedAmulet > 0)
		{
			if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 2] == "STR")
			{
				this.strength += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 5]);
			}
			else if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 2] == "STA")
			{
				this.stamina += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 5]);
			}
			else if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 2] == "DEX")
			{
				this.dexterity += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 5]);
			}
			if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 3] == "STR")
			{
				this.strength += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 6]);
			}
			else if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 3] == "STA")
			{
				this.stamina += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 6]);
			}
			else if (Main.itemStats.amuletStats[this.equippedAmulet - 1, 3] == "DEX")
			{
				this.dexterity += Convert.ToInt32(Main.itemStats.amuletStats[this.equippedAmulet - 1, 6]);
			}
		}
		if (Main.itemStats.weaponStats[this.equippedWeapon - 1, 4] == "STR")
		{
			this.strength += Convert.ToInt32(Main.itemStats.weaponStats[this.equippedWeapon - 1, 7]);
		}
		else if (Main.itemStats.weaponStats[this.equippedWeapon - 1, 4] == "STA")
		{
			this.stamina += Convert.ToInt32(Main.itemStats.weaponStats[this.equippedWeapon - 1, 7]);
		}
		else if (Main.itemStats.weaponStats[this.equippedWeapon - 1, 4] == "DEX")
		{
			this.dexterity += Convert.ToInt32(Main.itemStats.weaponStats[this.equippedWeapon - 1, 7]);
		}
		if (this.equippedArmor > 0)
		{
			this.stamina += Convert.ToInt32(Main.itemStats.armorStats[this.equippedArmor - 1, 5]);
		}
		for (int i = 0; i < this.potions.Length; i++)
		{
			if (this.potions[i] == 1)
			{
				if (Main.itemStats.potionStats[i, 2] == "STR")
				{
					this.strength += Convert.ToInt32(Main.itemStats.potionStats[i, 5]);
				}
				else if (Main.itemStats.potionStats[i, 2] == "STA")
				{
					this.stamina += Convert.ToInt32(Main.itemStats.potionStats[i, 5]);
				}
				else if (Main.itemStats.potionStats[i, 2] == "DEX")
				{
					this.dexterity += Convert.ToInt32(Main.itemStats.potionStats[i, 5]);
				}
			}
		}
		this.strength += this.baseStrength;
		this.stamina += this.baseStamina;
		this.dexterity += this.baseDexterity;
		this.crit = this.dexterity;
		this.maxHealth = this.stamina * 2;
	}

	public void clearCurrentRunData()
	{
		this.enemiesSlain = 0;
		this.levelsCleared = 0;
		this.questsCompleted = 0;
	}

	public void removeSelf()
	{
	}

	public void pollPurchases()
	{
		bool flag = true;
		int[] array = this.weapons;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == 0)
			{
				flag = false;
				break;
			}
		}
		int[] array2 = this.armors;
		for (int j = 0; j < array2.Length; j++)
		{
			if (array2[j] == 0)
			{
				flag = false;
				break;
			}
		}
		int[] array3 = this.potions;
		for (int k = 0; k < array3.Length; k++)
		{
			if (array3[k] == 0)
			{
				flag = false;
				break;
			}
		}
		int[] array4 = this.rings;
		for (int l = 0; l < array4.Length; l++)
		{
			if (array4[l] == 0)
			{
				flag = false;
				break;
			}
		}
		int[] array5 = this.amulets;
		for (int m = 0; m < array5.Length; m++)
		{
			if (array5[m] == 0)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.COLLECTOR, 1);
		}
	}
}
