using CUDLR;
using System;
using UnityEngine;

public static class GameObjectRoutes
{
	[Route("^/object/list.json$", "(GET|HEAD)", true)]
	public static void ListGameObjects(RequestContext context)
	{
		string text = "[";
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		UnityEngine.Object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			UnityEngine.Object @object = array2[i];
			text += string.Format("\"{0}\", ", @object.name);
		}
		text = text.TrimEnd(new char[]
		{
			',',
			' '
		}) + "]";
		context.Response.WriteString(text, "application/json");
	}
}
