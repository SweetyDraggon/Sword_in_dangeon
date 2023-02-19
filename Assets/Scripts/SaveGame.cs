using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using CoreGame;

public class SaveGame : MonoBehaviour
{
	private static string saveFilename = "default.sav";

	public static bool encodeFile = true;

	public string loadedCloudData = string.Empty;

	public void Awake()
	{
        /*
		iCloudManager.cloudLoadGameSuccess -= new Action<string>(this.onCloudLoadGameSuccess);
		iCloudManager.cloudLoadGameSuccess += new Action<string>(this.onCloudLoadGameSuccess);
		iCloudManager.cloudKeepLocalSave -= new Action(this.onCloudKeepLocalSave);
		iCloudManager.cloudKeepLocalSave += new Action(this.onCloudKeepLocalSave);
		iCloudManager.cloudKeepRemoteSave -= new Action(this.onCloudKeepRemoteSave);
		iCloudManager.cloudKeepRemoteSave += new Action(this.onCloudKeepRemoteSave);
		iCloudManager.cloudShowConflictAlert -= new Action(this.onCloudShowConflictAlert);
		iCloudManager.cloudShowConflictAlert += new Action(this.onCloudShowConflictAlert);
		iCloudManager.cloudHideConflictAlert -= new Action(this.onCloudHideConflictAlert);
		iCloudManager.cloudHideConflictAlert += new Action(this.onCloudHideConflictAlert);
		SaveGame.LoadDocumentFromCloud();
		*/      
	}

	public void Destroy()
	{
        /*
		iCloudManager.cloudLoadGameSuccess -= new Action<string>(this.onCloudLoadGameSuccess);
		iCloudManager.cloudKeepLocalSave -= new Action(this.onCloudKeepLocalSave);
		iCloudManager.cloudKeepRemoteSave -= new Action(this.onCloudKeepRemoteSave);
		iCloudManager.cloudShowConflictAlert -= new Action(this.onCloudShowConflictAlert);
		iCloudManager.cloudHideConflictAlert -= new Action(this.onCloudHideConflictAlert);
		*/      
	}

	public static string getDocumentsFolder()
	{
		return Application.persistentDataPath + "/";
	}

	public static void SavePrefs()
	{
		PlayerPrefs.SetInt("levelSelectLastPage", GameCore.Instance.levelSelectLastPage);
		PlayerPrefs.SetInt("iCloudEnabled", (!GameCore.Instance.iCloudEnabled) ? 0 : 1);
		PlayerPrefs.SetInt("controlSize", GameCore.Instance.controlSize);
		PlayerPrefs.SetString("shopPagePositions", Utils.convertIntArray(GameCore.Instance.shopPagePositions, ","));
		PlayerPrefs.SetFloat("musicVolume", AudioManager.Instance.musicVolume);
		PlayerPrefs.SetFloat("sfxVolume", AudioManager.Instance.sfxVolume);
		PlayerPrefs.Save();
	}

	public static void LoadPrefs()
	{
		GameCore.Instance.levelSelectLastPage = PlayerPrefs.GetInt("levelSelectLastPage", 1);
		//GameCore.Instance.iCloudEnabled = (PlayerPrefs.GetInt("iCloudEnabled", (!iCloud.iCloudAvailable()) ? 0 : 1) > 0);
		GameCore.Instance.shopPagePositions = Utils.convertStringToIntArray(PlayerPrefs.GetString("shopPagePositions", "0,0,0,0,0,0"));
		GameCore.Instance.controlSize = PlayerPrefs.GetInt("controlSize", 1);
		AudioManager.Instance.musicVolume = Mathf.Min(Mathf.Max(0f, PlayerPrefs.GetFloat("musicVolume", 0.6f)), 1f);
		AudioManager.Instance.sfxVolume = Mathf.Min(Mathf.Max(0f, PlayerPrefs.GetFloat("sfxVolume", 1f)), 1f);
	}

	public static void Save(int timestamp = -1)
	{
		string text = SaveGame.getDocumentsFolder() + SaveGame.saveFilename;
		UnityEngine.Debug.Log("Saving game to: " + text);
		SaveGame.SaveData(text, timestamp);
		if (timestamp == -1)
		{
			SaveGame.SaveDocumentInCloud();
		}
		SaveGame.SavePrefs();
	}

	public static void Load()
	{
		SaveGame.LoadDocumentFromCloud();
		if (!File.Exists(SaveGame.getDocumentsFolder() + SaveGame.saveFilename))
		{
			SaveGame.Save(0);
		}
		string path = SaveGame.getDocumentsFolder() + SaveGame.saveFilename;
		string fileContents = File.ReadAllText(path);
		SaveGame.LoadData(fileContents);
		if (Game.Instance && Game.Instance.player)
		{
			Game.Instance.player.updateItems();
			Main.playerStats.updatePlayerStats();
		}
		SaveGame.LoadPrefs();
	}

	public static void SaveDocumentInCloud()
	{
		//if (!GameCore.Instance.iCloudEnabled)
	//{
	//		return;
	//	}
		//if (iCloud.iCloudAvailable())
		///{
	//	iCloud.copyFileToCloud(SaveGame.saveFilename);
		//}
	}

	public static void LoadDocumentFromCloud()
	{
	//if (!GameCore.Instance.iCloudEnabled)
	//	{
	//		return;
//	}
		//if (iCloud.iCloudAvailable())
		//{
		//	iCloud.downloadFileFromCloud(SaveGame.saveFilename);
		//}
	}

	public static string convertDataToPlainText(string fileContents)
	{
		try
		{
			string s = fileContents;
			byte[] bytes = Convert.FromBase64String(s);
			string @string = Encoding.UTF8.GetString(bytes);
			fileContents = @string;
		}
		catch
		{
		}
		return fileContents;
	}

	public static double getTimestampFromData(string data)
	{
		string xml = SaveGame.convertDataToPlainText(data);
		double result = 0.0;
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/savegame/general/timestamp");
			result = Convert.ToDouble(xmlNode.InnerText);
		}
		catch
		{
		}
		return result;
	}

	public static void Backup()
	{
		string path = SaveGame.getDocumentsFolder() + SaveGame.saveFilename;
		string path2 = SaveGame.getDocumentsFolder() + SaveGame.saveFilename.Replace(".sav", "_backup.sav");
		string contents = File.ReadAllText(path);
		File.WriteAllText(path2, contents);
	}

	public static void SaveData(string path, int timestamp = -1)
	{
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
		xmlWriterSettings.Indent = true;
		xmlWriterSettings.IndentChars = "\t";
		string text = string.Empty;
		using (StringWriter stringWriter = new StringWriter())
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("savegame");
				xmlWriter.WriteStartElement("general");
				xmlWriter.WriteStartElement("timestamp");
				xmlWriter.WriteValue((timestamp != -1) ? ((double)timestamp) : Utils.GetTimeSinceEpoch());
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("tutorialComplete");
				xmlWriter.WriteValue((!GameCore.Instance.tutorialLevelComplete) ? 0 : 1);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("introSeen");
				xmlWriter.WriteValue((!GameCore.Instance.introCutsceneSeen) ? 0 : 1);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("ownsCoinDoubler");
				xmlWriter.WriteValue((!GameCore.Instance.ownsCoinDoubler) ? 0 : 1);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("mapRandomizer");
				xmlWriter.WriteValue(Utils.convertIntArray(GameCore.Instance.mapRandomizer, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("achievements");
				AchievementHandler.Instance.WriteToSaveXML(xmlWriter);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("player_stats");
				xmlWriter.WriteStartElement("savedStats");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.savedStats, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("damage");
				xmlWriter.WriteValue(Main.playerStats.damage);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("money");
				xmlWriter.WriteValue(Main.playerStats.money);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("weapons");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.weapons, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("armors");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.armors, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("potions");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.potions, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("rings");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.rings, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("amulets");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.amulets, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("defeatedBosses");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.defeatedBosses, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("equippedWeapon");
				xmlWriter.WriteValue(Main.playerStats.equippedWeapon);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("equippedArmor");
				xmlWriter.WriteValue(Main.playerStats.equippedArmor);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("equippedRing");
				xmlWriter.WriteValue(Main.playerStats.equippedRing);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("equippedAmulet");
				xmlWriter.WriteValue(Main.playerStats.equippedAmulet);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("levelReached");
				xmlWriter.WriteValue(Main.playerStats.levelReached);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("playerLevel");
				xmlWriter.WriteValue(Main.playerStats.playerLevel);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("playerXp");
				xmlWriter.WriteValue(Main.playerStats.playerXp);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("xpLevels");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.xpLevels, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("nextXpLevel");
				xmlWriter.WriteValue(Main.playerStats.nextXpLevel);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("enemiesSlain");
				xmlWriter.WriteValue(Main.playerStats.enemiesSlain);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("levelsCleared");
				xmlWriter.WriteValue(Main.playerStats.levelsCleared);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("questsCompleted");
				xmlWriter.WriteValue(Main.playerStats.questsCompleted);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("maxDamage");
				xmlWriter.WriteValue(Main.playerStats.maxDamage);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("maxHealth");
				xmlWriter.WriteValue(Main.playerStats.maxHealth);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("currentHealth");
				xmlWriter.WriteValue(Main.playerStats.currentHealth);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("baseStrength");
				xmlWriter.WriteValue(Main.playerStats.baseStrength);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("baseStamina");
				xmlWriter.WriteValue(Main.playerStats.baseStamina);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("baseDexterity");
				xmlWriter.WriteValue(Main.playerStats.baseDexterity);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("baseCrit");
				xmlWriter.WriteValue(Main.playerStats.baseCrit);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("strength");
				xmlWriter.WriteValue(Main.playerStats.strength);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("stamina");
				xmlWriter.WriteValue(Main.playerStats.stamina);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("dexterity");
				xmlWriter.WriteValue(Main.playerStats.dexterity);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("armor");
				xmlWriter.WriteValue(Main.playerStats.armor);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("crit");
				xmlWriter.WriteValue(Main.playerStats.crit);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("questStatus");
				xmlWriter.WriteValue(Utils.convertIntArray(Main.playerStats.questStatus, ","));
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
			}
			text = stringWriter.ToString();
		}
		if (SaveGame.encodeFile)
		{
			string s = text;
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			string text2 = Convert.ToBase64String(bytes);
			text = text2;
		}
		File.WriteAllText(path, text);
	}

	public static void LoadData(string fileContents)
	{
		fileContents = SaveGame.convertDataToPlainText(fileContents);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(fileContents);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("/savegame");
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("general");
		GameCore.Instance.tutorialLevelComplete = (Convert.ToInt32(xmlNode2.SelectSingleNode("tutorialComplete").InnerText) > 0);
		GameCore.Instance.introCutsceneSeen = (Convert.ToInt32(xmlNode2.SelectSingleNode("introSeen").InnerText) > 0);
		GameCore.Instance.ownsCoinDoubler = (Convert.ToInt32(xmlNode2.SelectSingleNode("ownsCoinDoubler").InnerText) > 0);
		GameCore.Instance.mapRandomizer = Utils.convertStringToIntArray(xmlNode2.SelectSingleNode("mapRandomizer").InnerText);
		XmlNode achievementsRootNode = xmlNode.SelectSingleNode("achievements");
		AchievementHandler.Instance.ReadFromSaveXML(achievementsRootNode);
		XmlNode xmlNode3 = xmlNode.SelectSingleNode("player_stats");
		Main.playerStats.savedStats = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("savedStats").InnerText);
		Main.playerStats.damage = Convert.ToInt32(xmlNode3.SelectSingleNode("damage").InnerText);
		Main.playerStats.money = Convert.ToInt32(xmlNode3.SelectSingleNode("money").InnerText);
		Main.playerStats.weapons = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("weapons").InnerText);
		Main.playerStats.armors = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("armors").InnerText);
		Main.playerStats.potions = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("potions").InnerText);
		Main.playerStats.rings = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("rings").InnerText);
		Main.playerStats.amulets = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("amulets").InnerText);
		Main.playerStats.defeatedBosses = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("defeatedBosses").InnerText);
		Main.playerStats.equippedWeapon = Convert.ToInt32(xmlNode3.SelectSingleNode("equippedWeapon").InnerText);
		Main.playerStats.equippedArmor = Convert.ToInt32(xmlNode3.SelectSingleNode("equippedArmor").InnerText);
		Main.playerStats.equippedRing = Convert.ToInt32(xmlNode3.SelectSingleNode("equippedRing").InnerText);
		Main.playerStats.equippedAmulet = Convert.ToInt32(xmlNode3.SelectSingleNode("equippedAmulet").InnerText);
		Main.playerStats.levelReached = Convert.ToInt32(xmlNode3.SelectSingleNode("levelReached").InnerText);
		Main.playerStats.playerLevel = Convert.ToInt32(xmlNode3.SelectSingleNode("playerLevel").InnerText);
		Main.playerStats.playerXp = Convert.ToInt32(xmlNode3.SelectSingleNode("playerXp").InnerText);
		Main.playerStats.xpLevels = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("xpLevels").InnerText);
		Main.playerStats.nextXpLevel = Convert.ToInt32(xmlNode3.SelectSingleNode("nextXpLevel").InnerText);
		Main.playerStats.enemiesSlain = Convert.ToInt32(xmlNode3.SelectSingleNode("enemiesSlain").InnerText);
		Main.playerStats.levelsCleared = Convert.ToInt32(xmlNode3.SelectSingleNode("levelsCleared").InnerText);
		Main.playerStats.questsCompleted = Convert.ToInt32(xmlNode3.SelectSingleNode("questsCompleted").InnerText);
		Main.playerStats.maxDamage = Convert.ToInt32(xmlNode3.SelectSingleNode("maxDamage").InnerText);
		Main.playerStats.maxHealth = Convert.ToInt32(xmlNode3.SelectSingleNode("maxHealth").InnerText);
		Main.playerStats.currentHealth = Convert.ToInt32(xmlNode3.SelectSingleNode("currentHealth").InnerText);
		Main.playerStats.baseStrength = Convert.ToInt32(xmlNode3.SelectSingleNode("baseStrength").InnerText);
		Main.playerStats.baseStamina = Convert.ToInt32(xmlNode3.SelectSingleNode("baseStamina").InnerText);
		Main.playerStats.baseDexterity = Convert.ToInt32(xmlNode3.SelectSingleNode("baseDexterity").InnerText);
		Main.playerStats.baseCrit = Convert.ToInt32(xmlNode3.SelectSingleNode("baseCrit").InnerText);
		Main.playerStats.strength = Convert.ToInt32(xmlNode3.SelectSingleNode("strength").InnerText);
		Main.playerStats.stamina = Convert.ToInt32(xmlNode3.SelectSingleNode("stamina").InnerText);
		Main.playerStats.dexterity = Convert.ToInt32(xmlNode3.SelectSingleNode("dexterity").InnerText);
		Main.playerStats.armor = Convert.ToInt32(xmlNode3.SelectSingleNode("armor").InnerText);
		Main.playerStats.crit = Convert.ToInt32(xmlNode3.SelectSingleNode("crit").InnerText);
		Main.playerStats.questStatus = Utils.convertStringToIntArray(xmlNode3.SelectSingleNode("questStatus").InnerText);
	}

	public void onCloudLoadGameSuccess(string data)
	{
		if (this.loadedCloudData == data)
		{
			return;
		}
		this.loadedCloudData = data;
		double timestampFromData = SaveGame.getTimestampFromData(data);
		double timestampFromData2 = SaveGame.getTimestampFromData(File.ReadAllText(SaveGame.getDocumentsFolder() + SaveGame.saveFilename));
		if (timestampFromData > timestampFromData2)
		{
			//iCloud.showConflictResolutionAlert();
		}
	}

	public void onCloudKeepLocalSave()
	{
		SaveGame.Save(-1);
		SaveGame.Load();
	}

	public void onCloudKeepRemoteSave()
	{
		if (this.loadedCloudData.Length > 0)
		{
			string path = SaveGame.getDocumentsFolder() + SaveGame.saveFilename;
			SaveGame.Backup();
			File.WriteAllText(path, this.loadedCloudData);
			SaveGame.Load();
			SaveGame.Save(-1);
		}
	}

	public void onCloudShowConflictAlert()
	{
		if (Game.Instance != null)
		{
			Game.Instance.paused = true;
		}
	}

	public void onCloudHideConflictAlert()
	{
		if (Game.Instance != null)
		{
			Game.Instance.paused = false;
		}
	}
}
