using System;

public static class Main
{
	public static PlayerStats playerStats = new PlayerStats();

	public static ItemStats itemStats = new ItemStats();

	public static void loadPrefs()
	{
		SaveGame.LoadPrefs();
	}

	public static void savePrefs()
	{
		SaveGame.SavePrefs();
	}

	public static void loadSave()
	{
		SaveGame.Load();
		Main.playerStats.setStats();
	}

	public static void saveGame()
	{
		Main.playerStats.updateSavedStats();
		SaveGame.Save(-1);
	}

	public static void clearSave()
	{
		Main.playerStats = new PlayerStats();
		Main.saveGame();
		Main.loadSave();
	}
}
