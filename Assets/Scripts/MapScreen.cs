using System;
using System.Collections.Generic;
using UnityEngine;
using CoreGame;
public class MapScreen : CustomWindow
{
	public bool isActive;

	public int mapWidth;

	public int mapHeight;

	public bool portalRevealed;

	public int portalX;

	public int portalY;

	public int portalXPixels;

	public int portalYPixels;

	public GameObject mapDots;

	public MapDot mapDot;

	public MapDot portalDot;

	public GameObject customMapBkg;

	public GameObject customMap;

	public Texture2D customTexture;

	public List<Color> mapTileColors;

	public bool[,] customMapRevealed;

	public int[,] customMapTiles;

	public void Awake()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateView);
		base.onSlideInStart += new CustomWindowEvent(this.updateView);
		this.removeWindowOnHide = false;
		this.configureCustomTexture();
	}

	public void OnDestroy()
	{
		base.onSlideInStart -= new CustomWindowEvent(this.updateView);
	}

	public void closeClicked()
	{
		Game.Instance.showMapScreen(false);
	}

	public void updateView()
	{
		this.mapDots.transform.localPosition = new Vector3((float)(-(float)this.mapWidth) * 2f, (float)(-(float)this.mapHeight) * 2f, this.mapDots.transform.localPosition.z);
		if (this.mapDot != null)
		{
			int num = (int)Mathf.Floor(Game.Instance.player.x / 32f) * 4 + 2;
			int num2 = (int)Mathf.Floor(Game.Instance.player.y / 32f) * 4 + 2;
			this.mapDot.transform.localPosition = new Vector3((float)num, (float)num2, 0f);
			this.mapDot.activate();
		}
		if (this.portalDot != null)
		{
			int num3 = this.portalX * 4 + 8;
			int num4 = this.portalY * 4 + 2;
			this.portalDot.transform.localPosition = new Vector3((float)num3, (float)num4, 0f);
			if (this.portalRevealed)
			{
				this.portalDot.activate();
			}
			else
			{
				this.portalDot.deactivate();
			}
		}
		this.handleCustomDrawing();
	}

	public void update(float dt)
	{
		this.onEnterFrame(dt);
	}

	public void onEnterFrame(float dt)
	{
		this.handleTiles();
		this.handlePortal();
		this.mapDot.onEnterFrame(dt);
		this.portalDot.onEnterFrame(dt);
	}

	public void configureCustomTexture()
	{
		this.mapTileColors = new List<Color>();
		this.mapTileColors.Add(Utils.HexToColor("f1e5d5"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("452b08"));
		this.mapTileColors.Add(Utils.HexToColor("918f88"));
		this.mapTileColors.Add(Utils.HexToColor("6e6450"));
		Texture2D texture2D = new Texture2D(64, 64, TextureFormat.ARGB32, false);
		texture2D.filterMode = FilterMode.Point;
		this.customMapBkg.GetComponent<Renderer>().material.mainTexture = texture2D;
		for (int i = 0; i < texture2D.height; i++)
		{
			for (int j = 0; j < texture2D.width; j++)
			{
				texture2D.SetPixel(j, i, this.mapTileColors[8]);
			}
		}
		texture2D.Apply();
		this.customTexture = new Texture2D(64, 64, TextureFormat.ARGB32, false);
		this.customTexture.filterMode = FilterMode.Point;
		this.customMap.GetComponent<Renderer>().material.mainTexture = this.customTexture;
		this.resetMapTexture();
	}

	public void resetMapTexture()
	{
		for (int i = 0; i < this.customTexture.height; i++)
		{
			for (int j = 0; j < this.customTexture.width; j++)
			{
				Color color = this.mapTileColors[7];
				this.customTexture.SetPixel(j, i, color);
			}
		}
		this.customTexture.Apply();
	}

	public void handleCustomDrawing()
	{
		for (int i = 0; i < this.customTexture.height; i++)
		{
			for (int j = 0; j < this.customTexture.width; j++)
			{
				int index = (!this.customMapRevealed[i, j]) ? 7 : (this.customMapTiles[i, j] - 1);
				this.customTexture.SetPixel(j, i, this.mapTileColors[index]);
			}
		}
		this.customTexture.Apply();
	}

	public void handleTiles()
	{
		int num = (int)Mathf.Floor(Game.Instance.player.x / 32f);
		int num2 = (int)Mathf.Floor(Game.Instance.player.y / 32f);
		for (int i = -2; i <= 2; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				int num3 = num + j;
				int num4 = num2 + i;
				if (num3 >= 0 && num3 <= this.mapWidth && num4 >= 0 && num4 <= this.mapHeight)
				{
					this.customMapRevealed[num4, num3] = true;
				}
			}
		}
	}

	public void handlePortal()
	{
		if (Mathf.Abs(Game.Instance.player.y - (float)this.portalYPixels) < 200f && Mathf.Abs(Game.Instance.player.x - (float)this.portalXPixels) < 300f)
		{
			this.portalRevealed = true;
		}
	}

	public void setPortalPosition(int x, int y)
	{
		this.portalX = x;
		this.portalY = y;
		this.portalXPixels = this.portalX * 32;
		this.portalYPixels = this.portalY * 32;
	}

	public void createMap(int w, int h)
	{
		this.mapWidth = w;
		this.mapHeight = h;
		this.customMap.transform.localScale = new Vector2((float)(w * 4), (float)(h * 4));
		this.customTexture.Resize(w, h);
		this.resetMapTexture();
		this.customMapRevealed = new bool[80, 80];
		this.customMapTiles = new int[80, 80];
		this.portalRevealed = false;
		bool flag = false;
		if (Game.Instance.map.dungeonLevel == 0 || Game.Instance.map.dungeonLevel == -1)
		{
			flag = true;
			this.portalRevealed = true;
		}
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				this.customMapRevealed[i, j] = flag;
				int num = Game.tileInfo.tileData[Game.Instance.map.GetTileID(j, i, 0)];
				this.customMapTiles[i, j] = num;
			}
		}
	}
}
