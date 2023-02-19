using System;

public class QuestHandler
{
	public int quest1;

	public int quest2;

	public int quest3;

	public int[] tracker;

	public string[,] quests;

	public QuestHandler()
	{
		string[,] expr_09 = new string[75, 4];
		expr_09[0, 0] = "Slay_5_Blood_worms.";
		expr_09[0, 1] = "10";
		expr_09[0, 2] = "5";
		expr_09[0, 3] = "30";
		expr_09[1, 0] = "Find_a_key.";
		expr_09[1, 1] = "3";
		expr_09[1, 2] = "1";
		expr_09[1, 3] = "30";
		expr_09[2, 0] = "Smash_10_crates.";
		expr_09[2, 1] = "0";
		expr_09[2, 2] = "10";
		expr_09[2, 3] = "30";
		expr_09[3, 0] = "Slay_5_Skeletal_archers.";
		expr_09[3, 1] = "11";
		expr_09[3, 2] = "5";
		expr_09[3, 3] = "40";
		expr_09[4, 0] = "Collect_30_copper_coins.";
		expr_09[4, 1] = "5";
		expr_09[4, 2] = "30";
		expr_09[4, 3] = "40";
		expr_09[5, 0] = "Slay_5_goblins.";
		expr_09[5, 1] = "13";
		expr_09[5, 2] = "5";
		expr_09[5, 3] = "40";
		expr_09[6, 0] = "Smash_15_vases.";
		expr_09[6, 1] = "1";
		expr_09[6, 2] = "15";
		expr_09[6, 3] = "50";
		expr_09[7, 0] = "Slay_5_Orc_swordsmen.";
		expr_09[7, 1] = "12";
		expr_09[7, 2] = "5";
		expr_09[7, 3] = "50";
		expr_09[8, 0] = "Smash_one_chest.";
		expr_09[8, 1] = "2";
		expr_09[8, 2] = "1";
		expr_09[8, 3] = "50";
		expr_09[9, 0] = "Slay_7_Blood_worms.";
		expr_09[9, 1] = "10";
		expr_09[9, 2] = "7";
		expr_09[9, 3] = "75";
		expr_09[10, 0] = "Slay_5_wolves.";
		expr_09[10, 1] = "14";
		expr_09[10, 2] = "5";
		expr_09[10, 3] = "75";
		expr_09[11, 0] = "Collect_50_copper_coins.";
		expr_09[11, 1] = "5";
		expr_09[11, 2] = "50";
		expr_09[11, 3] = "75";
		expr_09[12, 0] = "Smash_10_statues.";
		expr_09[12, 1] = "9";
		expr_09[12, 2] = "10";
		expr_09[12, 3] = "75";
		expr_09[13, 0] = "Find_a_Tome_of_Knowledge.";
		expr_09[13, 1] = "4";
		expr_09[13, 2] = "1";
		expr_09[13, 3] = "75";
		expr_09[14, 0] = "Find_2_keys.";
		expr_09[14, 1] = "3";
		expr_09[14, 2] = "2";
		expr_09[14, 3] = "75";
		expr_09[15, 0] = "Slay_2_Beholders.";
		expr_09[15, 1] = "16";
		expr_09[15, 2] = "2";
		expr_09[15, 3] = "75";
		expr_09[16, 0] = "Slay_5_Executioners.";
		expr_09[16, 1] = "15";
		expr_09[16, 2] = "5";
		expr_09[16, 3] = "100";
		expr_09[17, 0] = "Slay_7_Orc_Swordsmen.";
		expr_09[17, 1] = "12";
		expr_09[17, 2] = "7";
		expr_09[17, 3] = "100";
		expr_09[18, 0] = "Smash_20_crates.";
		expr_09[18, 1] = "0";
		expr_09[18, 2] = "20";
		expr_09[18, 3] = "100";
		expr_09[19, 0] = "Collect_2_treasures.";
		expr_09[19, 1] = "8";
		expr_09[19, 2] = "2";
		expr_09[19, 3] = "100";
		expr_09[20, 0] = "Slay_7_Skeletal archers.";
		expr_09[20, 1] = "11";
		expr_09[20, 2] = "7";
		expr_09[20, 3] = "125";
		expr_09[21, 0] = "Slay_5_Wolfmen.";
		expr_09[21, 1] = "17";
		expr_09[21, 2] = "5";
		expr_09[21, 3] = "125";
		expr_09[22, 0] = "Smash_2_chests.";
		expr_09[22, 1] = "2";
		expr_09[22, 2] = "2";
		expr_09[22, 3] = "125";
		expr_09[23, 0] = "Slay_5_Orc_axe_throwers.";
		expr_09[23, 1] = "18";
		expr_09[23, 2] = "5";
		expr_09[23, 3] = "150";
		expr_09[24, 0] = "Smash_20_vases.";
		expr_09[24, 1] = "1";
		expr_09[24, 2] = "20";
		expr_09[24, 3] = "150";
		expr_09[25, 0] = "Find_3_keys.";
		expr_09[25, 1] = "3";
		expr_09[25, 2] = "3";
		expr_09[25, 3] = "150";
		expr_09[26, 0] = "Slay_5_Wizards.";
		expr_09[26, 1] = "19";
		expr_09[26, 2] = "5";
		expr_09[26, 3] = "150";
		expr_09[27, 0] = "Slay_7_Executioners.";
		expr_09[27, 1] = "15";
		expr_09[27, 2] = "7";
		expr_09[27, 3] = "150";
		expr_09[28, 0] = "Slay_3_Beholders.";
		expr_09[28, 1] = "16";
		expr_09[28, 2] = "3";
		expr_09[28, 3] = "150";
		expr_09[29, 0] = "Collect_4_treasures.";
		expr_09[29, 1] = "8";
		expr_09[29, 2] = "4";
		expr_09[29, 3] = "200";
		expr_09[30, 0] = "Slay_5_Hellhounds.";
		expr_09[30, 1] = "20";
		expr_09[30, 2] = "5";
		expr_09[30, 3] = "250";
		expr_09[31, 0] = "Collect_15_silver_coins.";
		expr_09[31, 1] = "6";
		expr_09[31, 2] = "10";
		expr_09[31, 3] = "250";
		expr_09[32, 0] = "Slay_7_Wolfmen.";
		expr_09[32, 1] = "17";
		expr_09[32, 2] = "7";
		expr_09[32, 3] = "250";
		expr_09[33, 0] = "Find_2_Tomes_of_Knowledge.";
		expr_09[33, 1] = "4";
		expr_09[33, 2] = "2";
		expr_09[33, 3] = "250";
		expr_09[34, 0] = "Smash_5_chests.";
		expr_09[34, 1] = "2";
		expr_09[34, 2] = "5";
		expr_09[34, 3] = "250";
		expr_09[35, 0] = "Slay_5_Doom_Knights.";
		expr_09[35, 1] = "21";
		expr_09[35, 2] = "5";
		expr_09[35, 3] = "300";
		expr_09[36, 0] = "Slay_5_Goblin_Cannoneers.";
		expr_09[36, 1] = "22";
		expr_09[36, 2] = "5";
		expr_09[36, 3] = "300";
		expr_09[37, 0] = "Smash_20_statues.";
		expr_09[37, 1] = "9";
		expr_09[37, 2] = "20";
		expr_09[37, 3] = "300";
		expr_09[38, 0] = "Smash_30_crates.";
		expr_09[38, 1] = "0";
		expr_09[38, 2] = "30";
		expr_09[38, 3] = "300";
		expr_09[39, 0] = "Slay_5_Spearmen.";
		expr_09[39, 1] = "23";
		expr_09[39, 2] = "5";
		expr_09[39, 3] = "300";
		expr_09[40, 0] = "Slay_7_Orc_axe_throwers.";
		expr_09[40, 1] = "18";
		expr_09[40, 2] = "7";
		expr_09[40, 3] = "325";
		expr_09[41, 0] = "Slay_7_Wizards.";
		expr_09[41, 1] = "19";
		expr_09[41, 2] = "7";
		expr_09[41, 3] = "325";
		expr_09[42, 0] = "Slay_5_Wormriders.";
		expr_09[42, 1] = "24";
		expr_09[42, 2] = "5";
		expr_09[42, 3] = "325";
		expr_09[43, 0] = "Collect_4_treasures.";
		expr_09[43, 1] = "8";
		expr_09[43, 2] = "4";
		expr_09[43, 3] = "350";
		expr_09[44, 0] = "Collect_30_silver_coins.";
		expr_09[44, 1] = "6";
		expr_09[44, 2] = "30";
		expr_09[44, 3] = "350";
		expr_09[45, 0] = "Slay_10_Royal_axe_throwers.";
		expr_09[45, 1] = "25";
		expr_09[45, 2] = "10";
		expr_09[45, 3] = "400";
		expr_09[46, 0] = "Find_7_keys.";
		expr_09[46, 1] = "3";
		expr_09[46, 2] = "7";
		expr_09[46, 3] = "400";
		expr_09[47, 0] = "Slay_10_Ogres.";
		expr_09[47, 1] = "26";
		expr_09[47, 2] = "10";
		expr_09[47, 3] = "400";
		expr_09[48, 0] = "Slay_10_Goblin_Cannoneers.";
		expr_09[48, 1] = "22";
		expr_09[48, 2] = "10";
		expr_09[48, 3] = "400";
		expr_09[49, 0] = "Slay_15_Doom_Knights.";
		expr_09[49, 1] = "21";
		expr_09[49, 2] = "15";
		expr_09[49, 3] = "450";
		expr_09[50, 0] = "Smash_50_vases.";
		expr_09[50, 1] = "1";
		expr_09[50, 2] = "50";
		expr_09[50, 3] = "450";
		expr_09[51, 0] = "Slay_10_Grand_Beholders.";
		expr_09[51, 1] = "28";
		expr_09[51, 2] = "10";
		expr_09[51, 3] = "450";
		expr_09[52, 0] = "Find_3_Tomes_of_Knowledge.";
		expr_09[52, 1] = "4";
		expr_09[52, 2] = "3";
		expr_09[52, 3] = "450";
		expr_09[53, 0] = "Slay_10_Wolve-riders.";
		expr_09[53, 1] = "29";
		expr_09[53, 2] = "10";
		expr_09[53, 3] = "450";
		expr_09[54, 0] = "Collect_5_gold_coins.";
		expr_09[54, 1] = "7";
		expr_09[54, 2] = "5";
		expr_09[54, 3] = "450";
		expr_09[55, 0] = "Slay_10_Royal_spearmen.";
		expr_09[55, 1] = "27";
		expr_09[55, 2] = "10";
		expr_09[55, 3] = "450";
		expr_09[56, 0] = "Slay_15_spearmen.";
		expr_09[56, 1] = "23";
		expr_09[56, 2] = "15";
		expr_09[56, 3] = "450";
		expr_09[57, 0] = "Smash_7_chests.";
		expr_09[57, 1] = "2";
		expr_09[57, 2] = "7";
		expr_09[57, 3] = "450";
		expr_09[58, 0] = "Slay_10_Arch_mages.";
		expr_09[58, 1] = "32";
		expr_09[58, 2] = "10";
		expr_09[58, 3] = "500";
		expr_09[59, 0] = "Smash_50_statues.";
		expr_09[59, 1] = "9";
		expr_09[59, 2] = "50";
		expr_09[59, 3] = "500";
		expr_09[60, 0] = "Slay_10_Dragon_guards.";
		expr_09[60, 1] = "30";
		expr_09[60, 2] = "10";
		expr_09[60, 3] = "600";
		expr_09[61, 0] = "Slay_15_Wolve-riders.";
		expr_09[61, 1] = "29";
		expr_09[61, 2] = "15";
		expr_09[61, 3] = "600";
		expr_09[62, 0] = "Slay_15_Ogres.";
		expr_09[62, 1] = "26";
		expr_09[62, 2] = "15";
		expr_09[62, 3] = "650";
		expr_09[63, 0] = "Smash_75_vases.";
		expr_09[63, 1] = "1";
		expr_09[63, 2] = "75";
		expr_09[63, 3] = "650";
		expr_09[64, 0] = "Find_10_keys.";
		expr_09[64, 1] = "3";
		expr_09[64, 2] = "10";
		expr_09[64, 3] = "700";
		expr_09[65, 0] = "Slay_10_Giant_executioners.";
		expr_09[65, 1] = "31";
		expr_09[65, 2] = "10";
		expr_09[65, 3] = "700";
		expr_09[66, 0] = "Collect_10_gold_coins.";
		expr_09[66, 1] = "7";
		expr_09[66, 2] = "10";
		expr_09[66, 3] = "800";
		expr_09[67, 0] = "Slay_15_Arch_mages.";
		expr_09[67, 1] = "32";
		expr_09[67, 2] = "15";
		expr_09[67, 3] = "800";
		expr_09[68, 0] = "Find_5_Tomes_of_Knowledge.";
		expr_09[68, 1] = "4";
		expr_09[68, 2] = "5";
		expr_09[68, 3] = "900";
		expr_09[69, 0] = "Slay_15_Grand_Beholders.";
		expr_09[69, 1] = "28";
		expr_09[69, 2] = "15";
		expr_09[69, 3] = "900";
		expr_09[70, 0] = "Slay_15_Dragon_guards.";
		expr_09[70, 1] = "30";
		expr_09[70, 2] = "15";
		expr_09[70, 3] = "1000";
		expr_09[71, 0] = "Smash_75_crates.";
		expr_09[71, 1] = "0";
		expr_09[71, 2] = "75";
		expr_09[71, 3] = "1000";
		expr_09[72, 0] = "Slay_15_Giant_executioners.";
		expr_09[72, 1] = "31";
		expr_09[72, 2] = "15";
		expr_09[72, 3] = "1200";
		expr_09[73, 0] = "Collect_25_treasures.";
		expr_09[73, 1] = "8";
		expr_09[73, 2] = "25";
		expr_09[73, 3] = "1200";
		expr_09[74, 0] = "Smash_10_chests.";
		expr_09[74, 1] = "2";
		expr_09[74, 2] = "10";
		expr_09[74, 3] = "150";
		this.quests = expr_09;

		this.tracker = new int[33];
		this.quest1 = 0;
		this.quest2 = 0;
		this.quest3 = 0;
		this.checkActiveQuests();
		this.assignQuests();
	}

	public void clearQuests()
	{
		for (int i = 0; i < Main.playerStats.questStatus.Length; i++)
		{
			if (Main.playerStats.questStatus[i] == 1)
			{
				Main.playerStats.questStatus[i] = 0;
			}
		}
		for (int j = 0; j < this.tracker.Length; j++)
		{
			if (this.tracker[j] != 0)
			{
				this.tracker[j] = 0;
			}
		}
	}

	public void trackItem(QuestTracking t)
	{
		this.trackItem((int)t);
	}

	public void trackItem(int t)
	{
		if (this.quest1 > 0 && Convert.ToInt32(this.quests[this.quest1 - 1, 1]) == t)
		{
			this.tracker[t]++;
			int num = this.tracker[t];
			int num2 = Convert.ToInt32(this.quests[this.quest1 - 1, 2]);
			if (num >= num2)
			{
				this.tracker[t] = 0;
				Main.playerStats.questStatus[this.quest1 - 1] = 2;
				Game.Instance.hud.activateQuestPopup(this.quest1);
				Main.playerStats.money += Convert.ToInt32(this.quests[this.quest1 - 1, 3]);
				Main.playerStats.questsCompleted++;
				this.quest1 = 0;
				this.assignQuests();
			}
		}
		else if (this.quest2 > 0 && Convert.ToInt32(this.quests[this.quest2 - 1, 1]) == t)
		{
			this.tracker[t]++;
			int num = this.tracker[t];
			int num2 = Convert.ToInt32(this.quests[this.quest2 - 1, 2]);
			if (num >= num2)
			{
				this.tracker[t] = 0;
				Main.playerStats.questStatus[this.quest2 - 1] = 2;
				Game.Instance.hud.activateQuestPopup(this.quest2);
				Main.playerStats.money += Convert.ToInt32(this.quests[this.quest2 - 1, 3]);
				Main.playerStats.questsCompleted++;
				this.quest2 = 0;
				this.assignQuests();
			}
		}
		else if (this.quest3 > 0 && Convert.ToInt32(this.quests[this.quest3 - 1, 1]) == t)
		{
			this.tracker[t]++;
			int num = this.tracker[t];
			int num2 = Convert.ToInt32(this.quests[this.quest3 - 1, 2]);
			if (num >= num2)
			{
				this.tracker[t] = 0;
				Main.playerStats.questStatus[this.quest3 - 1] = 2;
				Game.Instance.hud.activateQuestPopup(this.quest3);
				Main.playerStats.money += Convert.ToInt32(this.quests[this.quest3 - 1, 3]);
				Main.playerStats.questsCompleted++;
				this.quest3 = 0;
				this.assignQuests();
			}
		}
	}

	public void checkActiveQuests()
	{
		for (int i = 0; i < 75; i++)
		{
			if (Main.playerStats.questStatus[i] == 1)
			{
				if (this.quest1 == 0)
				{
					this.quest1 = i + 1;
				}
				else if (this.quest2 == 0)
				{
					this.quest2 = i + 1;
				}
				else if (this.quest3 == 0)
				{
					this.quest3 = i + 1;
				}
			}
		}
	}

	public void assignQuests()
	{
		if (this.quest1 == 0)
		{
			for (int i = 0; i < 75; i++)
			{
				if (Main.playerStats.questStatus[i] == 0)
				{
					this.quest1 = i + 1;
					Main.playerStats.questStatus[i] = 1;
					break;
				}
			}
		}
		if (this.quest2 == 0)
		{
			for (int j = 0; j < 75; j++)
			{
				if (Main.playerStats.questStatus[j] == 0)
				{
					this.quest2 = j + 1;
					Main.playerStats.questStatus[j] = 1;
					break;
				}
			}
		}
		if (this.quest3 == 0)
		{
			for (int k = 0; k < 75; k++)
			{
				if (Main.playerStats.questStatus[k] == 0)
				{
					this.quest3 = k + 1;
					Main.playerStats.questStatus[k] = 1;
					break;
				}
			}
		}
	}
}
