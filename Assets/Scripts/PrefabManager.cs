using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
	private static PrefabManager instance;

	public List<PrefabMapping> prefabMappings;

	public static PrefabManager Instance
	{
		get
		{
			return PrefabManager.instance;
		}
	}

	public void Awake()
	{
		PrefabManager.instance = this;
	}

	public int FindTileIDByName(string name)
	{
		foreach (PrefabMapping current in this.prefabMappings)
		{
			if (current.name == name)
			{
				return current.tileID;
			}
		}
		return -1;
	}

	public GameObject FindByName(string name)
	{
		foreach (PrefabMapping current in this.prefabMappings)
		{
			if (current.name == name)
			{
				return current.prefab;
			}
		}
		return null;
	}

	public GameObject FindByTileID(int tileID)
	{
		foreach (PrefabMapping current in this.prefabMappings)
		{
			if (current.tileID == tileID)
			{
				return current.prefab;
			}
		}
		return null;
	}
}
