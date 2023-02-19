using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
	private static AchievementHandler instance;

	public Dictionary<ACHIEVEMENT, AchievementTracking> achievementProgress;

	public float nextUpdate;

	public static AchievementHandler Instance
	{
		get
		{
			return AchievementHandler.instance;
		}
	}

	public void initAchievementHandler()
	{
		AchievementHandler.instance = this;
		this.createAchievements();
	}

	public void Update()
	{
		this.nextUpdate -= Time.deltaTime;
		if (this.nextUpdate <= 0f)
		{
			this.nextUpdate = 60f;
			this.pollAchievements(false);
		}
	}

	public void pollAchievements(bool force = false)
	{
		foreach (KeyValuePair<ACHIEVEMENT, AchievementTracking> current in this.achievementProgress)
		{
			current.Value.Poll(false, force);
		}
	}

	public void createAchievements()
	{
		if (this.achievementProgress != null)
		{
			return;
		}
		this.achievementProgress = new Dictionary<ACHIEVEMENT, AchievementTracking>();
		this.achievementProgress.Add(ACHIEVEMENT.QUEST_HAS_BEGUN, new AchievementTracking("CgkIgMmKyJoMEAIQAQ", "QUEST HAS BEGUN", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.LEVEL_UP, new AchievementTracking("CgkIgMmKyJoMEAIQAg", "LEVEL UP!", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.KNOWLEDGE_POWER, new AchievementTracking("CgkIgMmKyJoMEAIQAw", "KNOWLEDGE IS POWER!", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.NOT_LOST, new AchievementTracking("CgkIgMmKyJoMEAIQBA", "I’M NOT LOST!", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.KEY_MASTER, new AchievementTracking("CgkIgMmKyJoMEAIQBQ", "KEY MASTER", 0, 250));
		this.achievementProgress.Add(ACHIEVEMENT.SICK_REFLEXES, new AchievementTracking("CgkIgMmKyJoMEAIQBg", "SICK REFLEXES", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.CRATE_SMASHER, new AchievementTracking("CgkIgMmKyJoMEAIQBw", "CRATE SMASHER!", 0, 1000));
		this.achievementProgress.Add(ACHIEVEMENT.SLAYER, new AchievementTracking("CgkIgMmKyJoMEAIQCA", "SLAYER!", 0, 1000));
		this.achievementProgress.Add(ACHIEVEMENT.DETERMINED, new AchievementTracking("CgkIgMmKyJoMEAIQCQ", "DETERMINED!", 0, 1000));
		this.achievementProgress.Add(ACHIEVEMENT.BATTLE_TESTED, new AchievementTracking("CgkIgMmKyJoMEAIQCg", "BATTLE TESTED!", 0, 5000));
		this.achievementProgress.Add(ACHIEVEMENT.BOUGHT_WEAPON, new AchievementTracking("CgkIgMmKyJoMEAIQCw", "SHARPENED BLADE", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOUGHT_ARMOR, new AchievementTracking("CgkIgMmKyJoMEAIQDA", "ARMORED!", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOUGHT_POTION, new AchievementTracking("CgkIgMmKyJoMEAIQDQ", "MAGIC BREW", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOUGHT_RING, new AchievementTracking("CgkIgMmKyJoMEAIQDg", "TIL DEATH DO US PART", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOUGHT_AMULET, new AchievementTracking("CgkIgMmKyJoMEAIQDw", "KING OF BLING", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.COLLECTOR, new AchievementTracking("CgkIgMmKyJoMEAIQEA", "THE COLLECTOR", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BLESSED, new AchievementTracking("CgkIgMmKyJoMEAIQEQ", "BLESSED", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.SHOPAHOLIC, new AchievementTracking("CgkIgMmKyJoMEAIQEg", "SHOP-A-HOLIC", 0, 5000));
		this.achievementProgress.Add(ACHIEVEMENT.BOSS1, new AchievementTracking("CgkIgMmKyJoMEAIQEw", "HORDE NO MORE", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOSS2, new AchievementTracking("CgkIgMmKyJoMEAIQFA", "EYE OF THE BEHOLDER", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOSS3, new AchievementTracking("CgkIgMmKyJoMEAIQFQ", "SLAUGHTERED", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOSS4, new AchievementTracking("CgkIgMmKyJoMEAIQFg", "PEBBLED", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.BOSS5, new AchievementTracking("CgkIgMmKyJoMEAIQFw", "FUS-RO-DRAGONSLAYER!", 0, 1));
		this.achievementProgress.Add(ACHIEVEMENT.LEGENDARY_HERO, new AchievementTracking("CgkIgMmKyJoMEAIQGA", "LEGENDARY HERO!", 0, 1));
	}

	public void Increment(ACHIEVEMENT achievement, int cnt = 1)
	{
		AchievementTracking achievementTracking = this.achievementProgress[achievement];
		achievementTracking.currentValue += cnt;
		achievementTracking.Poll(true, false);
		this.achievementProgress[achievement] = achievementTracking;
	}

	public void SetValue(ACHIEVEMENT achievement, int val)
	{
		AchievementTracking achievementTracking = this.achievementProgress[achievement];
		if (achievementTracking.currentValue == val)
		{
			return;
		}
		achievementTracking.currentValue = val;
		achievementTracking.Poll(true, false);
		this.achievementProgress[achievement] = achievementTracking;
	}

	public void SetValueFromSave(ACHIEVEMENT achievement, int val)
	{
		AchievementTracking achievementTracking = this.achievementProgress[achievement];
		achievementTracking.currentValue = val;
		if (achievementTracking.currentValue >= achievementTracking.maxValue)
		{
			achievementTracking.completed = true;
		}
		achievementTracking.Poll(true, false);
		this.achievementProgress[achievement] = achievementTracking;
	}

	public void WriteToSaveXML(XmlWriter writer)
	{
		foreach (KeyValuePair<ACHIEVEMENT, AchievementTracking> current in this.achievementProgress)
		{
			AchievementTracking value = current.Value;
			ACHIEVEMENT key = current.Key;
			string text = string.Empty;
			int value2 = 0;
			if (key == ACHIEVEMENT.QUEST_HAS_BEGUN)
			{
				text = "quest_has_begun";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.LEVEL_UP)
			{
				text = "level_up";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.KNOWLEDGE_POWER)
			{
				text = "knowledge_power";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.NOT_LOST)
			{
				text = "not_lost";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.KEY_MASTER)
			{
				text = "key_master";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.SICK_REFLEXES)
			{
				text = "sick_reflexes";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.CRATE_SMASHER)
			{
				text = "crate_smasher";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.SLAYER)
			{
				text = "slayer";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.DETERMINED)
			{
				text = "determined";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BATTLE_TESTED)
			{
				text = "battle_tested";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOUGHT_WEAPON)
			{
				text = "bought_weapon";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOUGHT_ARMOR)
			{
				text = "bought_armor";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOUGHT_POTION)
			{
				text = "bought_potion";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOUGHT_RING)
			{
				text = "bought_ring";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOUGHT_AMULET)
			{
				text = "bought_amulet";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.COLLECTOR)
			{
				text = "collector";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BLESSED)
			{
				text = "blessed";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.SHOPAHOLIC)
			{
				text = "shopaholic";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOSS1)
			{
				text = "boss1";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOSS2)
			{
				text = "boss2";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOSS3)
			{
				text = "boss3";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOSS4)
			{
				text = "boss4";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.BOSS5)
			{
				text = "boss5";
				value2 = value.currentValue;
			}
			if (key == ACHIEVEMENT.LEGENDARY_HERO)
			{
				text = "legendary_hero";
				value2 = value.currentValue;
			}
			if (text != string.Empty)
			{
				writer.WriteStartElement(text);
				writer.WriteValue(value2);
				writer.WriteEndElement();
			}
		}
	}

	public void ReadFromSaveXML(XmlNode achievementsRootNode)
	{
		Dictionary<ACHIEVEMENT, AchievementTracking> dictionary = new Dictionary<ACHIEVEMENT, AchievementTracking>(this.achievementProgress);
		foreach (KeyValuePair<ACHIEVEMENT, AchievementTracking> current in dictionary)
		{
			AchievementTracking value = current.Value;
			ACHIEVEMENT key = current.Key;
			string text = string.Empty;
			if (key == ACHIEVEMENT.QUEST_HAS_BEGUN)
			{
				text = "quest_has_begun";
			}
			if (key == ACHIEVEMENT.LEVEL_UP)
			{
				text = "level_up";
			}
			if (key == ACHIEVEMENT.KNOWLEDGE_POWER)
			{
				text = "knowledge_power";
			}
			if (key == ACHIEVEMENT.NOT_LOST)
			{
				text = "not_lost";
			}
			if (key == ACHIEVEMENT.KEY_MASTER)
			{
				text = "key_master";
			}
			if (key == ACHIEVEMENT.SICK_REFLEXES)
			{
				text = "sick_reflexes";
			}
			if (key == ACHIEVEMENT.CRATE_SMASHER)
			{
				text = "crate_smasher";
			}
			if (key == ACHIEVEMENT.SLAYER)
			{
				text = "slayer";
			}
			if (key == ACHIEVEMENT.DETERMINED)
			{
				text = "determined";
			}
			if (key == ACHIEVEMENT.BATTLE_TESTED)
			{
				text = "battle_tested";
			}
			if (key == ACHIEVEMENT.BOUGHT_WEAPON)
			{
				text = "bought_weapon";
			}
			if (key == ACHIEVEMENT.BOUGHT_ARMOR)
			{
				text = "bought_armor";
			}
			if (key == ACHIEVEMENT.BOUGHT_POTION)
			{
				text = "bought_potion";
			}
			if (key == ACHIEVEMENT.BOUGHT_RING)
			{
				text = "bought_ring";
			}
			if (key == ACHIEVEMENT.BOUGHT_AMULET)
			{
				text = "bought_amulet";
			}
			if (key == ACHIEVEMENT.COLLECTOR)
			{
				text = "collector";
			}
			if (key == ACHIEVEMENT.BLESSED)
			{
				text = "blessed";
			}
			if (key == ACHIEVEMENT.SHOPAHOLIC)
			{
				text = "shopaholic";
			}
			if (key == ACHIEVEMENT.BOSS1)
			{
				text = "boss1";
			}
			if (key == ACHIEVEMENT.BOSS2)
			{
				text = "boss2";
			}
			if (key == ACHIEVEMENT.BOSS3)
			{
				text = "boss3";
			}
			if (key == ACHIEVEMENT.BOSS4)
			{
				text = "boss4";
			}
			if (key == ACHIEVEMENT.BOSS5)
			{
				text = "boss5";
			}
			if (key == ACHIEVEMENT.LEGENDARY_HERO)
			{
				text = "legendary_hero";
			}
			if (text != string.Empty && achievementsRootNode.SelectSingleNode(text) != null)
			{
				this.SetValueFromSave(key, Convert.ToInt32(achievementsRootNode.SelectSingleNode(text).InnerText));
			}
		}
		AchievementHandler.Instance.pollAchievements(true);
	}
}
