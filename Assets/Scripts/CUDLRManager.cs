using CUDLR;
using System;
using System.Reflection;
using UnityEngine;

public class CUDLRManager : MonoBehaviour
{
	[Command("showHUD", "Show HUD.", true)]
	public static void showHUD(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nshowHUD \"1\"");
			return;
		}
		GameObject.Find("HUD").GetComponent<Camera>().farClipPlane = ((Convert.ToInt32(args[0]) != 1) ? 0.05f : 200f);
	}

	[Command("addXP", "Add xp.", true)]
	public static void addXP(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\naddXP \"num\"");
			return;
		}
		Game.Instance.player.addXp(Convert.ToInt32(args[0]));
	}

	[Command("setMoney", "Set money.", true)]
	public static void setMoney(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nsetMoney \"num\"");
			return;
		}
		Main.playerStats.money = Convert.ToInt32(args[0]);
	}

	[Command("setWeapon", "Set weapon.", true)]
	public static void setWeapon(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nsetWeapon \"num\"");
			return;
		}
		Main.playerStats.equippedWeapon = Convert.ToInt32(args[0]);
		Game.Instance.player.weapon.updateEquipmentArt();
	}

	[Command("setArmor", "Set weapon.", true)]
	public static void setArmor(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nsetWeapon \"num\"");
			return;
		}
		Main.playerStats.equippedArmor = Convert.ToInt32(args[0]);
		Game.Instance.player.updateItems();
	}

	[Command("setTileset", "Set map tileset.", true)]
	public static void setTileset(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nsetTileset \"tilesetnum\"");
			return;
		}
		Game.Instance.map.setTileset(Convert.ToInt32(args[0]));
	}

	[Command("setDungeonLevel", "Set dungeon level.", true)]
	public static void setDungeonLevel(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nsetDungeonLevel \"level_num\"");
			return;
		}
		Game.Instance.map.setDungeonLevel(Convert.ToInt32(args[0]));
	}

	[Command("loadLevel", "Load specific map.", true)]
	public static void loadLevel(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nloadLevel \"level_num\"");
			return;
		}
		Game.Instance.map.loadLevel(Convert.ToInt32(args[0]));
	}

	[Command("saveGame", "Saves the game.", true)]
	public static void saveGame(string[] args)
	{
		Main.saveGame();
	}

	[Command("loadGame", "Loads the game.", true)]
	public static void loadGame(string[] args)
	{
		Main.loadSave();
	}

	[Command("showMenu", "Shows a menu.", true)]
	public static void showMenu(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is\nshowMenu \"MenuName\"");
			return;
		}
		Game.Instance.paused = true;
		WindowManager.Instance.ShowMenu(args[0]);
	}

	[Command("restart", "Restarted the current scene.", true)]
	public static void restartScene(string[] args)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

	[Command("loadScene", "Load a new scene.", true)]
	public static void loadScene(string[] args)
	{
		if (args.Length != 1)
		{
			CUDLR.Console.Log("Error - Usage is loadScene \"SceneName\"");
			return;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(args[0]);
	}

	[Command("quit", "Quits the game.", true)]
	public static void quitScene(string[] args)
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer)
		{
			Application.Quit();
		}
		else
		{
			UnityEngine.Debug.Log("Can't force a quit on iOS!");
		}
	}

	[Command("printEntity", "Displays the properties of the passed entity", true)]
	public static void printEntity(string[] args)
	{
		GameObject gameObject = GameObject.Find(args[0]);
		if (gameObject == null)
		{
			CUDLR.Console.Log("GameObject not found : " + args[0]);
		}
		else
		{
			CUDLR.Console.Log("Game UnityEngine.Object : " + gameObject.name);
			Component[] components = gameObject.GetComponents(typeof(Component));
			for (int i = 0; i < components.Length; i++)
			{
				Component component = components[i];
				CUDLR.Console.Log("      Component : " + component.GetType());
				FieldInfo[] fields = component.GetType().GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					FieldInfo fieldInfo = fields[j];
					CUDLR.Console.Log(string.Concat(new object[]
					{
						"                ",
						fieldInfo.Name,
						" : ",
						fieldInfo.GetValue(component)
					}));
				}
			}
		}
	}

	[Command("setEntity", "Sets the properties of the passed entity", true)]
	public static void setEntity(string[] args)
	{
		if (args.Length != 3)
		{
			CUDLR.Console.Log("Error - Usage is\nsetEntity \"EntityName\" \"Property\" \"Value\"");
			return;
		}
		GameObject gameObject = GameObject.Find(args[0]);
		if (gameObject == null)
		{
			CUDLR.Console.Log("GameObject not found : " + args[0]);
		}
		else
		{
			Component[] components = gameObject.GetComponents(typeof(Component));
			for (int i = 0; i < components.Length; i++)
			{
				Component component = components[i];
				FieldInfo[] fields = component.GetType().GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					FieldInfo fieldInfo = fields[j];
					if (fieldInfo.Name == args[1])
					{
						fieldInfo.SetValue(component, Convert.ChangeType(args[2], fieldInfo.FieldType));
						CUDLR.Console.Log("Setting value: " + fieldInfo.Name + " with: " + args[2]);
					}
				}
			}
		}
	}

	[Command("printPlayer", "Displays the players properties.", true)]
	public static void printPlayer(string[] args)
	{
		string[] args2 = new string[]
		{
			"Player"
		};
		CUDLRManager.printEntity(args2);
	}

	[Command("setPlayer", "Sets the properties of the player character", true)]
	public static void setPlayer(string[] args)
	{
		if (args.Length != 2)
		{
			CUDLR.Console.Log("Error - Usage is setPlayer \"Property\" \"Value\"");
			return;
		}
		CUDLRManager.setEntity(new string[]
		{
			"Player",
			args[0],
			args[1]
		});
	}
}
