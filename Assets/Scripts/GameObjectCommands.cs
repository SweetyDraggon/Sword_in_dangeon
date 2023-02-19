using CUDLR;
using System;
using System.Reflection;
using UnityEngine;

public static class GameObjectCommands
{
	[Command("object list", "lists all the game objects in the scene", true)]
	public static void ListGameObjects()
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		UnityEngine.Object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object @object = array2[i];
			CUDLR.Console.Log(@object.name);
		}
	}

	[Command("object print", "lists properties of the object", true)]
	public static void PrintGameObject(string[] args)
	{
		if (args.Length < 1)
		{
			CUDLR.Console.Log("expected : object print <Object Name>");
			return;
		}
		GameObject gameObject = GameObject.Find(args[0]);
		if (gameObject == null)
		{
			CUDLR.Console.Log("GameObject not found : " + args[0]);
		}
		else
		{
			CUDLR.Console.Log("Game Object : " + gameObject.name);
			Component[] components = gameObject.GetComponents(typeof(Component));
			for (int i = 0; i < components.Length; i++)
			{
				Component component = components[i];
				CUDLR.Console.Log("  Component : " + component.GetType());
				FieldInfo[] fields = component.GetType().GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					FieldInfo fieldInfo = fields[j];
					CUDLR.Console.Log(string.Concat(new object[]
					{
						"    ",
						fieldInfo.Name,
						" : ",
						fieldInfo.GetValue(component)
					}));
				}
			}
		}
	}
}
