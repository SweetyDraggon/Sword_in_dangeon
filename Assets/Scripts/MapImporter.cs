using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using tk2dRuntime.TileMap;
using UnityEngine;

public class MapImporter
{
	public enum Format
	{
		TMX
	}

	private class LayerProxy
	{
		public string name;

		public uint[] tiles;
	}

	private const string FormatErrorString = "Unsupported format error.\nPlease ensure layer data is stored as xml, base64(zlib) * or base64(uncompressed) in TileD preferences.\n\n* - Preferred format";

	public static int MaxWidth = 1024;

	public static int MaxHeight = 1024;

	public static int MaxLayers = 32;

	private Type zlibType;

	private int width;

	private int height;

	private List<MapImporter.LayerProxy> layers = new List<MapImporter.LayerProxy>();

	private bool staggered;

	private static int ReadIntAttribute(XmlNode node, string attribute)
	{
		return int.Parse(node.Attributes[attribute].Value, NumberFormatInfo.InvariantInfo);
	}

	private static string ReadStringAttribute(XmlNode node, string attribute, string defValue)
	{
		XmlAttribute xmlAttribute = node.Attributes[attribute];
		return (xmlAttribute != null) ? xmlAttribute.Value : defValue;
	}

	public string ImportTMX(string path)
	{
		try
		{
			TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(textAsset.text);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/map");
			this.width = MapImporter.ReadIntAttribute(xmlNode, "width");
			this.height = MapImporter.ReadIntAttribute(xmlNode, "height");
			string a = MapImporter.ReadStringAttribute(xmlNode, "orientation", "orthogonal");
			if (a != "orthogonal" && a != "staggered")
			{
				throw new Exception("ImportTMX only supports orthogonal and staggered tilemaps.\n\n\n");
			}
			this.staggered = (a == "staggered");
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("layer");
			foreach (XmlNode xmlNode2 in xmlNodeList)
			{
				string value = xmlNode2.Attributes["name"].Value;
				int num = MapImporter.ReadIntAttribute(xmlNode2, "width");
				int num2 = MapImporter.ReadIntAttribute(xmlNode2, "height");
				if (num2 != this.height || num != this.width)
				{
					string result = "Layer \"" + value + "\" has invalid dimensions";
					return result;
				}
				XmlNode xmlNode3 = xmlNode2.SelectSingleNode("data");
				string a2 = (xmlNode3.Attributes["encoding"] == null) ? string.Empty : xmlNode3.Attributes["encoding"].Value;
				string a3 = (xmlNode3.Attributes["compression"] == null) ? string.Empty : xmlNode3.Attributes["compression"].Value;
				uint[] array;
				if (a2 == "base64")
				{
					if (a3 == "zlib")
					{
						UnityEngine.Debug.Log("Error!! ZLib not enabled!");
						string result = "Unsupported format error.\nPlease ensure layer data is stored as xml, base64(zlib) * or base64(uncompressed) in TileD preferences.\n\n* - Preferred format";
						return result;
					}
					if (!(a3 == string.Empty))
					{
						string result = "Unsupported format error.\nPlease ensure layer data is stored as xml, base64(zlib) * or base64(uncompressed) in TileD preferences.\n\n* - Preferred format";
						return result;
					}
					array = this.BytesToInts(Convert.FromBase64String(xmlNode3.InnerText));
				}
				else
				{
					if (!(a2 == string.Empty))
					{
						string result = "Unsupported format error.\nPlease ensure layer data is stored as xml, base64(zlib) * or base64(uncompressed) in TileD preferences.\n\n* - Preferred format";
						return result;
					}
					List<uint> list = new List<uint>();
					XmlNodeList xmlNodeList2 = xmlNode3.SelectNodes("tile");
					foreach (XmlNode xmlNode4 in xmlNodeList2)
					{
						list.Add(uint.Parse(xmlNode4.Attributes["gid"].Value, NumberFormatInfo.InvariantInfo));
					}
					array = list.ToArray();
				}
				if (array != null)
				{
					MapImporter.LayerProxy layerProxy = new MapImporter.LayerProxy();
					layerProxy.name = value;
					layerProxy.tiles = array;
					this.layers.Add(layerProxy);
				}
			}
		}
		catch (Exception ex)
		{
			string result = ex.ToString();
			return result;
		}
		return string.Empty;
	}

	private uint[] BytesToInts(byte[] bytes)
	{
		uint[] array = new uint[bytes.Length / 4];
		int i = 0;
		int num = 0;
		while (i < array.Length)
		{
			array[i] = (uint)((int)bytes[num] | (int)bytes[num + 1] << 8 | (int)bytes[num + 2] << 16 | (int)bytes[num + 3] << 24);
			i++;
			num += 4;
		}
		return array;
	}

	private void PopulateTilemap(tk2dTileMap tileMap)
	{
		int num = (!this.staggered) ? 0 : 1;
		MapImporter.ResizeTileMap(tileMap, this.width + num, this.height, tileMap.partitionSizeX, tileMap.partitionSizeY);
		if (this.staggered)
		{
			tileMap.data.sortMethod = tk2dTileMapData.SortMethod.TopLeft;
			tileMap.data.tileType = tk2dTileMapData.TileType.Isometric;
		}
		foreach (MapImporter.LayerProxy current in this.layers)
		{
			int num2 = MapImporter.FindOrCreateLayer(tileMap, current.name);
			Layer layer = tileMap.Layers[num2];
			int num3 = this.width + num;
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					layer.SetTile(j, i, -1);
				}
			}
			for (int k = 0; k < this.height; k++)
			{
				for (int l = 0; l < this.width; l++)
				{
					uint num4 = current.tiles[k * this.width + l];
					int tile = (int)((num4 & 536870911u) - 1u);
					int num5 = (this.staggered && (!this.staggered || k % 2 != 0)) ? 1 : 0;
					layer.SetTile(l + num5, this.height - 1 - k, tile);
					bool flag = (num4 & 2147483648u) != 0u;
					bool flag2 = (num4 & 1073741824u) != 0u;
					bool flag3 = (num4 & 536870912u) != 0u;
					tk2dTileFlags tk2dTileFlags = tk2dTileFlags.None;
					if (flag3)
					{
						tk2dTileFlags |= (tk2dTileFlags.FlipX | tk2dTileFlags.Rot90);
					}
					if (flag)
					{
						tk2dTileFlags ^= tk2dTileFlags.FlipX;
					}
					if (flag2)
					{
						tk2dTileFlags ^= tk2dTileFlags.FlipY;
					}
					layer.SetTileFlags(l + num5, this.height - 1 - k, tk2dTileFlags);
				}
			}
			layer.Optimize();
		}
	}

	public static bool Import(tk2dTileMap tileMap, string path)
	{
		MapImporter mapImporter = new MapImporter();
		MapImporter.Format format = MapImporter.Format.TMX;
		string empty = string.Empty;
		MapImporter.Format format2 = format;
		if (format2 == MapImporter.Format.TMX)
		{
			if (!mapImporter.CheckZlib())
			{
				return false;
			}
		}
		if (path.Length == 0)
		{
			return false;
		}
		string text = string.Empty;
		format2 = format;
		if (format2 == MapImporter.Format.TMX)
		{
			text = mapImporter.ImportTMX(path);
		}
		if (text.Length != 0)
		{
			UnityEngine.Debug.Log("Tilemap failed to import: " + path + "\n\n" + text);
			return false;
		}
		mapImporter.PopulateTilemap(tileMap);
		return true;
	}

	private bool CheckZlib()
	{
		return true;
	}

	public static void ResizeTileMap(tk2dTileMap tileMap, int width, int height, int partitionSizeX, int partitionSizeY)
	{
		int b = Mathf.Clamp(width, 1, MapImporter.MaxWidth);
		int b2 = Mathf.Clamp(height, 1, MapImporter.MaxHeight);
		Layer[] array = tileMap.Layers;
		for (int i = 0; i < array.Length; i++)
		{
			Layer layer = array[i];
			layer.DestroyGameData(tileMap);
			if (layer.gameObject != null)
			{
				tk2dUtil.DestroyImmediate(layer.gameObject);
				layer.gameObject = null;
			}
		}
		Layer[] array2 = new Layer[tileMap.Layers.Length];
		for (int j = 0; j < tileMap.Layers.Length; j++)
		{
			Layer layer2 = tileMap.Layers[j];
			array2[j] = new Layer(layer2.hash, b, b2, partitionSizeX, partitionSizeY);
			Layer layer3 = array2[j];
			if (!layer2.IsEmpty)
			{
				int num = Mathf.Min(tileMap.height, b2);
				int num2 = Mathf.Min(tileMap.width, b);
				for (int k = 0; k < num; k++)
				{
					for (int l = 0; l < num2; l++)
					{
						layer3.SetRawTile(l, k, layer2.GetRawTile(l, k));
					}
				}
				layer3.Optimize();
			}
		}
		bool flag = tileMap.ColorChannel != null && !tileMap.ColorChannel.IsEmpty;
		ColorChannel colorChannel = new ColorChannel(b, b2, partitionSizeX, partitionSizeY);
		if (flag)
		{
			int num3 = Mathf.Min(tileMap.height, b2) + 1;
			int num4 = Mathf.Min(tileMap.width, b) + 1;
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < num4; n++)
				{
					colorChannel.SetColor(n, m, tileMap.ColorChannel.GetColor(n, m));
				}
			}
			colorChannel.Optimize();
		}
		tileMap.ColorChannel = colorChannel;
		tileMap.Layers = array2;
		tileMap.width = b;
		tileMap.height = b2;
		tileMap.partitionSizeX = partitionSizeX;
		tileMap.partitionSizeY = partitionSizeY;
		tileMap.ForceBuild();
	}

	public static int AddNewLayer(tk2dTileMap tileMap)
	{
		LayerInfo[] array = tileMap.data.Layers;
		bool flag;
		int num;
		do
		{
			flag = false;
			num = UnityEngine.Random.Range(0, 2147483647);
			LayerInfo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				LayerInfo layerInfo = array2[i];
				if (layerInfo.hash == num)
				{
					flag = true;
				}
			}
		}
		while (flag);
		LayerInfo layerInfo2 = new LayerInfo();
		layerInfo2.name = "New Layer";
		layerInfo2.hash = num;
		layerInfo2.z = 0.1f;
		tileMap.data.tileMapLayers.Add(layerInfo2);
		BuilderUtil.InitDataStore(tileMap);
		GameObject gameObject = tk2dUtil.CreateGameObject(layerInfo2.name);
		gameObject.transform.parent = tileMap.renderData.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localRotation = Quaternion.identity;
		tileMap.Layers[tileMap.Layers.Length - 1].gameObject = gameObject;
		return tileMap.data.NumLayers - 1;
	}

	public static int FindOrCreateLayer(tk2dTileMap tileMap, string name)
	{
		int num = 0;
		LayerInfo[] array = tileMap.data.Layers;
		for (int i = 0; i < array.Length; i++)
		{
			LayerInfo layerInfo = array[i];
			if (layerInfo.name == name)
			{
				return num;
			}
			num++;
		}
		num = MapImporter.AddNewLayer(tileMap);
		tileMap.data.Layers[num].name = name;
		return num;
	}
}
