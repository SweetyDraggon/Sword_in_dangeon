using System;
using System.Collections.Generic;
using UnityEngine;
using CoreGame;
public class Map : MonoBehaviour
{
	public GameObject background;

	public List<tk2dSprite> backgroundSprites;

	public int debugForceLevel;

	public bool bossBattle;

	public bool secretAreaShown;

	public List<tk2dSpriteCollection> tilesets;

	public List<GameObject> enemies;

	public List<GameObject> hazards;

	public List<GameObject> breakables;

	public List<GameObject> pickups;

	public List<GameObject> secretGameObjects;

	public GameObject enemiesGO;

	public GameObject hazardsGO;

	public GameObject breakablesGO;

	public GameObject pickupsGO;

	public GameObject textMeshPrefab;

	public bool debugEnabled;

	public tk2dTileMap tilemap;

	private GameObject debugGO;

	public bool objectsLayerHidden;

	public int mapW;

	public int mapH;

	public int mapTileW;

	public int mapTileH;

	public int tileW = 32;

	public int tileH = 32;

	public int currentTiles;

	public int currentLevel;

	public int dungeonLevel;

	public int savedDungeonLevel;

	public int playerStartX;

	public int playerStartY;

	public int spawnPosition;

	public int keyPosition;

	private int lastTiles;

	public void Awake()
	{
		this.tilemap = base.transform.Find("TileMap").GetComponent<tk2dTileMap>();
		this.backgroundSprites = new List<tk2dSprite>();
		Component[] componentsInChildren = base.transform.Find("Background").GetComponentsInChildren<tk2dSprite>();
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			tk2dSprite item = (tk2dSprite)array[i];
			this.backgroundSprites.Add(item);
		}
	}

	public void Start()
	{
		this.debugGO = new GameObject();
		this.debugGO.name = "Debug";
		this.debugGO.transform.parent = base.transform;
	}

	public void reset()
	{
		UnityEngine.Object.Destroy(this.enemiesGO);
		UnityEngine.Object.Destroy(this.hazardsGO);
		UnityEngine.Object.Destroy(this.breakablesGO);
		UnityEngine.Object.Destroy(this.pickupsGO);
		this.secretAreaShown = false;
		if (this.tilemap != null)
		{
			Utils.DestroyImmediateChildren(this.tilemap.renderData);
		}
	}

	public void updateDimensions()
	{
		this.mapW = this.tilemap.width * this.tileW;
		this.mapH = this.tilemap.height * this.tileH;
		this.mapTileW = this.tilemap.width;
		this.mapTileH = this.tilemap.height;
		BoxCollider component = base.GetComponent<BoxCollider>();
		component.size = new Vector3((float)this.mapW, (float)this.mapH, 0f);
		component.center = new Vector3((float)this.mapW * 0.5f, (float)this.mapH * 0.5f, 0f);
	}

	public int invertCoordinate(int y)
	{
		return this.mapH / this.tileH - 1 - y;
	}

	public void Update()
	{
		if (UnityEngine.Input.GetKeyDown("1"))
		{
			this.setTileset(1);
		}
		if (UnityEngine.Input.GetKeyDown("2"))
		{
			this.setTileset(2);
		}
		if (UnityEngine.Input.GetKeyDown("3"))
		{
			this.setTileset(3);
		}
		if (UnityEngine.Input.GetKeyDown("4"))
		{
			this.setTileset(4);
		}
		if (UnityEngine.Input.GetKeyDown("5"))
		{
			this.setTileset(5);
		}
		if (UnityEngine.Input.GetKeyDown("6"))
		{
			this.setTileset(6);
		}
		if (UnityEngine.Input.GetKeyDown("7"))
		{
			this.setTileset(7);
		}
		if (!this.objectsLayerHidden)
		{
			this.tilemap.renderData.transform.Find("Objects").gameObject.SetActive(false);
			this.objectsLayerHidden = !this.tilemap.renderData.transform.Find("Objects").gameObject.active;
		}
	}

	public void setTileset(int tiles)
	{
		this.currentTiles = tiles;
		if (this.currentTiles != this.lastTiles)
		{
			this.lastTiles = this.currentTiles;
			this.updateTileset();
		}
	}

	public void setDungeonLevel(int levelNum)
	{
		this.dungeonLevel = levelNum;
	}

	public void loadLevel(int levelNum)
	{
		this.debugForceLevel = levelNum;
		Game.Instance.loadNewLevel();
	}

	public void update(float dt)
	{
		this.updateArray(this.enemies, dt);
		this.updateArray(this.hazards, dt);
		this.updateArray(this.breakables, dt);
		this.updateArray(this.pickups, dt);
	}

	public void updateArray(List<GameObject> objects, float dt)
	{
		try
		{
			foreach (GameObject current in objects)
			{
				Entity component = current.GetComponent<Entity>();
				if (component)
				{
					component.update(dt);
				}
				else
				{
					MovieClip component2 = current.GetComponent<MovieClip>();
					if (component2)
					{
						component2.update(dt);
					}
				}
			}
		}
		catch
		{
		}
	}

	public void loadMap()
	{
		this.enemies = new List<GameObject>();
		this.enemiesGO = new GameObject("Enemies");
		this.enemiesGO.transform.parent = base.transform;
		this.hazards = new List<GameObject>();
		this.hazardsGO = new GameObject("Hazards");
		this.hazardsGO.transform.parent = base.transform;
		this.breakables = new List<GameObject>();
		this.breakablesGO = new GameObject("Breakables");
		this.breakablesGO.transform.parent = base.transform;
		this.pickups = new List<GameObject>();
		this.pickupsGO = new GameObject("Pickups");
		this.pickupsGO.transform.parent = base.transform;
		this.loadTMX("level_" + this.currentLevel + ".tmx");
		this.updateDimensions();
		Game.Instance.mapScreen.createMap(this.mapTileW, this.mapTileH);
		this.loadHazards();
	}

	public void addEnemy(int type, int nX, int nY)
	{
		string text = string.Empty;
		if (type == 1)
		{
			text = "SkeletalArcher";
		}
		else if (type == 2)
		{
			text = "BloodWorm";
		}
		else if (type == 3)
		{
			text = "Goblin";
		}
		else if (type == 4)
		{
			text = "SwordOrc";
		}
		else if (type == 5)
		{
			text = "Worg";
		}
		else if (type == 6)
		{
			text = "Wolfman";
		}
		else if (type == 7)
		{
			text = "AxeTroll";
		}
		else if (type == 8)
		{
			text = "Wizard";
		}
		else if (type == 9)
		{
			text = "Executioner";
		}
		else if (type == 10)
		{
			text = "WorgRider";
		}
		else if (type == 11)
		{
			text = "Spearman";
		}
		else if (type == 12)
		{
			text = "GoblinCannoneer";
		}
		else if (type == 13)
		{
			text = "WormRider";
		}
		else if (type == 14)
		{
			text = "DoomKnight";
		}
		else if (type == 15)
		{
			text = "Hellhound";
		}
		else if (type == 16)
		{
			text = "RoyalAxeThrower";
		}
		else if (type == 17)
		{
			text = "RoyalSpearman";
		}
		else if (type == 18)
		{
			text = "Ogre";
		}
		else if (type == 19)
		{
			text = "GiantWorm";
		}
		else if (type == 20)
		{
			text = "CannonGiant";
		}
		else if (type == 21)
		{
			text = "DragonGuard";
		}
		else if (type == 22)
		{
			text = "GiantExecutioner";
		}
		else if (type == 23)
		{
			text = "ArchMage";
		}
		else if (type == 24)
		{
			text = "Beholder";
		}
		else if (type == 25)
		{
			text = "GrandBeholder";
		}
		if (text != string.Empty)
		{
			GameObject gameObject = this.spawnGameObjectPrefab(PrefabManager.Instance.FindTileIDByName(text), nX, nY);
			if (gameObject != null)
			{
				gameObject.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
				gameObject.name = text;
				Enemy component = gameObject.GetComponent<Enemy>();
				component.init();
				this.enemies.Add(gameObject);
			}
		}
	}

	public void loadHazards()
	{
		int num = 1;
		int num2 = 5;
		if (this.dungeonLevel < 7)
		{
			num = 1;
			num2 = 4;
		}
		else if (this.dungeonLevel >= 7 && this.dungeonLevel <= 13)
		{
			num = 1;
			num2 = 8;
		}
		else if (this.dungeonLevel >= 13 && this.dungeonLevel <= 19)
		{
			num = 1;
			num2 = 12;
		}
		else if (this.dungeonLevel >= 20 && this.dungeonLevel <= 26)
		{
			num = 5;
			num2 = 12;
		}
		else if (this.dungeonLevel >= 26 && this.dungeonLevel <= 32)
		{
			num = 5;
			num2 = 16;
		}
		else if (this.dungeonLevel >= 33 && this.dungeonLevel <= 39)
		{
			num = 9;
			num2 = 16;
		}
		else if (this.dungeonLevel >= 40 && this.dungeonLevel <= 45)
		{
			num = 9;
			num2 = 20;
		}
		else if (this.dungeonLevel >= 46 && this.dungeonLevel <= 52)
		{
			num = 13;
			num2 = 20;
		}
		else if (this.dungeonLevel >= 53 && this.dungeonLevel <= 58)
		{
			num = 13;
			num2 = 23;
		}
		else if (this.dungeonLevel >= 59 && this.dungeonLevel <= 65)
		{
			num = 17;
			num2 = 23;
		}
		for (int i = 0; i < this.mapH / this.tileH; i++)
		{
			for (int j = 0; j < this.mapW / this.tileW; j++)
			{
				int tileID = this.GetTileID(j, i, 1);
				if (tileID > 0)
				{
					if (tileID >= 288 && tileID <= 292)
					{
						int num3 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 10f) + 1;
						if (tileID == 288)
						{
							int type = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * (float)(1 + num2 - num)) + num;
							this.addEnemy(type, j, i);
						}
						else if (tileID == 289 && num3 > 5)
						{
							int type = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * (float)(1 + num2 - num)) + num;
							this.addEnemy(type, j, i);
						}
						else if (tileID == 290 && num3 > 7)
						{
							int type = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * (float)(1 + num2 - num)) + num;
							this.addEnemy(type, j, i);
						}
						else if (tileID == 291 && this.dungeonLevel > 50)
						{
							int type = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 5f) + 1;
							this.addEnemy(type, j, i);
						}
						else if (tileID == 292 && num3 > 5 && this.dungeonLevel > 50)
						{
							int type = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 5f) + 1;
							this.addEnemy(type, j, i);
						}
					}
					else if (tileID >= 293 && tileID <= 294)
					{
						int num3 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 10f) + 1;
						if (tileID == 293)
						{
							if (num3 > 5)
							{
								int type;
								if (this.dungeonLevel > 26)
								{
									type = 25;
								}
								else
								{
									type = 24;
								}
								this.addEnemy(type, j, i);
							}
						}
						else if (tileID == 294)
						{
						}
					}
					if (tileID == 326 || (tileID == 285 && this.spawnPosition == 1) || (tileID == 286 && this.spawnPosition == 2))
					{
						this.playerStartX = j;
						this.playerStartY = i;
						GameObject gameObject = this.spawnGameObjectPrefab(285, j, i);
						if (gameObject != null)
						{
							gameObject.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
							this.hazards.Add(gameObject);
						}
					}
					else if (tileID == 301)
					{
						GameObject gameObject2 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject2 != null)
						{
							gameObject2.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
							Spikes component = gameObject2.GetComponent<Spikes>();
							component.SetPosition(gameObject2.transform.position);
							component.init();
							this.hazards.Add(gameObject2);
						}
					}
					else if (tileID == 282)
					{
						GameObject gameObject3 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject3 != null)
						{
							gameObject3.transform.position += new Vector3((float)this.tileW * 2f, (float)this.tileH * 2f - 4f, 0f);
							DemonGate component2 = gameObject3.GetComponent<DemonGate>();
							component2.SetPosition(gameObject3.transform.position);
							component2.init();
							this.hazards.Add(gameObject3);
							Game.Instance.mapScreen.setPortalPosition(j, i);
						}
					}
					if ((tileID == 297 && this.keyPosition == 1) || (tileID == 298 && this.keyPosition == 2) || (tileID == 299 && this.keyPosition == 3))
					{
						GameObject gameObject4 = this.spawnGameObjectPrefab(297, j, i);
						if (gameObject4 != null)
						{
							gameObject4.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
							Key component3 = gameObject4.GetComponent<Key>();
							component3.SetPosition(gameObject4.transform.position);
							component3.init();
							this.pickups.Add(gameObject4);
						}
					}
					else if (tileID == 321)
					{
						GameObject gameObject5 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject5 != null)
						{
							gameObject5.transform.position += new Vector3((float)this.tileW * -2f, (float)this.tileH * -13f, 1f);
							this.hazards.Add(gameObject5);
						}
					}
					else if (tileID == 328)
					{
						GameObject gameObject6 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject6 != null)
						{
							gameObject6.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH, 0f);
							Olaf component4 = gameObject6.GetComponent<Olaf>();
							component4.SetPosition(gameObject6.transform.position);
							component4.init();
							this.hazards.Add(gameObject6);
						}
					}
					else if (tileID == 331)
					{
						GameObject gameObject7 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject7 != null)
						{
							gameObject7.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							this.hazards.Add(gameObject7);
						}
					}
					else if (tileID == 332)
					{
						GameObject gameObject8 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject8 != null)
						{
							gameObject8.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
							Priest component5 = gameObject8.GetComponent<Priest>();
							component5.SetPosition(gameObject8.transform.position);
							component5.init();
							this.hazards.Add(gameObject8);
						}
					}
					else if (tileID == 302 || tileID == 303)
					{
						int num3 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 10f) + 1;
						if (num3 > 9 || tileID == 303)
						{
							GameObject gameObject9 = this.spawnGameObjectPrefab(295, j, i);
							if (gameObject9 != null)
							{
								gameObject9.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
								Tome tome = gameObject9.AddComponent<Tome>();
								tome.name = "Tome";
								tome.init();
								this.pickups.Add(gameObject9);
							}
						}
					}
					else if (tileID == 296 || tileID == 300)
					{
						int num3 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 10f) + 1;
						if (num3 > 5 || tileID == 300)
						{
							GameObject gameObject10 = this.spawnGameObjectPrefab(295, j, i);
							if (gameObject10 != null)
							{
								gameObject10.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH - 2f, 0f);
								Chest chest = gameObject10.AddComponent<Chest>();
								chest.init();
								this.breakables.Add(gameObject10);
							}
						}
					}
					else if (tileID == 295)
					{
						int num3 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 10f) + 1;
						if (num3 >= 1 && num3 <= 4)
						{
							GameObject gameObject11 = this.spawnGameObjectPrefab(295, j, i);
							if (gameObject11 != null)
							{
								gameObject11.transform.position += new Vector3((float)this.tileW * 0.5f + 1f, (float)this.tileH - 2f, 0f);
								Container container = gameObject11.AddComponent<Container>();
								container.init();
								this.breakables.Add(gameObject11);
							}
						}
						else if (num3 == 5)
						{
							GameObject gameObject12 = this.spawnGameObjectPrefab(295, j, i);
							if (gameObject12 != null)
							{
								gameObject12.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH - 2f, 0f);
								Candles candles = gameObject12.AddComponent<Candles>();
								candles.init();
								this.breakables.Add(gameObject12);
							}
						}
						else if (num3 == 6)
						{
							GameObject gameObject13 = this.spawnGameObjectPrefab(295, j, i);
							if (gameObject13 != null)
							{
								gameObject13.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH - 2f, 0f);
								Statue statue = gameObject13.AddComponent<Statue>();
								statue.init();
								this.breakables.Add(gameObject13);
							}
						}
					}
					else if (tileID == 333)
					{
						GameObject gameObject14 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject14 != null)
						{
							gameObject14.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							OrcChieftain component6 = gameObject14.GetComponent<OrcChieftain>();
							component6.init();
							this.enemies.Add(gameObject14);
						}
					}
					else if (tileID == 334)
					{
						GameObject gameObject15 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject15 != null)
						{
							gameObject15.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							AncientBeholder component7 = gameObject15.GetComponent<AncientBeholder>();
							component7.init();
							this.enemies.Add(gameObject15);
						}
					}
					else if (tileID == 335)
					{
						GameObject gameObject16 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject16 != null)
						{
							gameObject16.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							Butcher component8 = gameObject16.GetComponent<Butcher>();
							component8.init();
							this.enemies.Add(gameObject16);
						}
					}
					else if (tileID == 336)
					{
						GameObject gameObject17 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject17 != null)
						{
							gameObject17.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							StoneGolem component9 = gameObject17.GetComponent<StoneGolem>();
							component9.init();
							this.enemies.Add(gameObject17);
						}
					}
					else if (tileID == 337)
					{
						GameObject gameObject18 = this.spawnGameObjectPrefab(tileID, j, i);
						if (gameObject18 != null)
						{
							gameObject18.transform.position += new Vector3((float)this.tileW, (float)this.tileH, 0f);
							Dragon component10 = gameObject18.GetComponent<Dragon>();
							component10.init();
							this.enemies.Add(gameObject18);
						}
					}
					else if (tileID >= 341 && tileID <= 348)
					{
						GameObject gameObject19 = this.spawnGameObjectPrefab(341, j, i);
						if (gameObject19 != null)
						{
							gameObject19.transform.position += new Vector3((float)this.tileW * 0.5f, (float)this.tileH * 0.5f, 0f);
							TutorialSign component11 = gameObject19.GetComponent<TutorialSign>();
							component11.initWithType(tileID - 340);
							this.hazards.Add(gameObject19);
						}
					}
				}
			}
		}
		foreach (GameObject current in this.enemies)
		{
			current.transform.parent = this.enemiesGO.transform;
		}
		foreach (GameObject current2 in this.hazards)
		{
			current2.transform.parent = this.hazardsGO.transform;
		}
		foreach (GameObject current3 in this.breakables)
		{
			current3.transform.parent = this.breakablesGO.transform;
		}
		foreach (GameObject current4 in this.pickups)
		{
			current4.transform.parent = this.pickupsGO.transform;
		}
	}

	public GameObject spawnGameObjectPrefab(int tid, int tx, int ty)
	{
		GameObject gameObject = null;
		GameObject gameObject2 = PrefabManager.Instance.FindByTileID(tid);
		if (gameObject2 != null)
		{
			gameObject = (UnityEngine.Object.Instantiate(gameObject2) as GameObject);
			gameObject.transform.position = new Vector3((float)(tx * this.tileW), (float)(ty * this.tileH), 0f);
			gameObject.transform.parent = base.transform;
		}
		return gameObject;
	}

	public int GetTileID(int x, int y, int layer = 0)
	{
		return this.tilemap.GetTileIdAtPosition(new Vector3((float)(x * 32), (float)(y * 32), 0f), layer) + 1;
	}

	public int GetTileID(Vector3 position, int layer = 0)
	{
		return this.GetTileID((int)position.x, (int)position.y, layer);
	}

	public void generateLevel()
	{
		if (GameCore.Instance.dungeonLevelFromMenu == -1)
		{
			this.dungeonLevel = 0;
			GameCore.Instance.dungeonLevelFromMenu = 0;
		}
		else if (GameCore.Instance.dungeonLevelFromMenu > 0)
		{
			if (!GameCore.Instance.tutorialLevelComplete)
			{
				this.dungeonLevel = -2;
			}
			else
			{
				this.dungeonLevel = GameCore.Instance.dungeonLevelFromMenu;
				GameCore.Instance.dungeonLevelFromMenu = 0;
			}
		}
		if (this.dungeonLevel == 0)
		{
			this.currentTiles = 6;
			this.currentLevel = 1;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == -1)
		{
			this.currentTiles = 2;
			this.currentLevel = 2;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == -2)
		{
			this.currentTiles = 1;
			this.currentLevel = 200;
		}
		else if (this.dungeonLevel == 13)
		{
			this.currentTiles = 1;
			this.currentLevel = 101;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == 26)
		{
			this.currentTiles = 4;
			this.currentLevel = 102;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == 39)
		{
			this.currentTiles = 3;
			this.currentLevel = 103;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == 52)
		{
			this.currentTiles = 5;
			this.currentLevel = 104;
			this.generateNewRandomTileset();
		}
		else if (this.dungeonLevel == 65)
		{
			this.currentTiles = 2;
			this.currentLevel = 105;
			this.generateNewRandomTileset();
		}
		else
		{
			int num = 3;
			int num2 = 75;
			if (GameCore.Instance.currentTilesForSet == 0)
			{
				this.generateNewRandomTileset();
			}
			this.currentTiles = GameCore.Instance.currentTilesForSet;
			this.currentLevel = (int)Mathf.Floor(UnityEngine.Random.Range((float)num, (float)num2));
			int[] mapRandomizer = GameCore.Instance.mapRandomizer;
			if (mapRandomizer != null)
			{
				bool flag = true;
				for (int i = num; i <= num2; i++)
				{
					if (mapRandomizer[i] == 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					for (int j = 0; j < mapRandomizer.Length; j++)
					{
						mapRandomizer[j] = 0;
					}
				}
				int num3 = 0;
				bool flag2 = false;
				while (!flag2 && num3 < 1000)
				{
					this.currentLevel = (int)Mathf.Floor(UnityEngine.Random.Range((float)num, (float)num2));
					if (mapRandomizer[this.currentLevel] == 0)
					{
						mapRandomizer[this.currentLevel] = 1;
						flag2 = true;
					}
					else
					{
						num3++;
					}
				}
			}
		}
		if (this.debugForceLevel > 0)
		{
			this.currentTiles = 1;
			this.currentLevel = this.debugForceLevel;
		}
		this.bossBattle = false;
		if (this.currentLevel >= 101 && this.currentLevel <= 105)
		{
			this.bossBattle = true;
		}
		if (!this.bossBattle)
		{
			Game.Instance.hud.bossHealth.gameObject.SetActive(false);
		}
		this.spawnPosition = (int)(Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 2f) + 1f);
		this.keyPosition = (int)(Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 3f) + 1f);
		this.loadMap();
	}

	public void generateNewRandomTileset()
	{
		int currentMusicForSet = GameCore.Instance.currentMusicForSet;
		while (GameCore.Instance.currentMusicForSet == currentMusicForSet)
		{
			GameCore.Instance.currentMusicForSet = UnityEngine.Random.Range(0, 3) + 1;
		}
		int currentTilesForSet = GameCore.Instance.currentTilesForSet;
		while (GameCore.Instance.currentTilesForSet == currentTilesForSet)
		{
			GameCore.Instance.currentTilesForSet = UnityEngine.Random.Range(0, 6) + 1;
			if (GameCore.Instance.currentTilesForSet == 6)
			{
				GameCore.Instance.currentTilesForSet = 7;
			}
		}
	}

	public void showSecretArea(bool s)
	{
		if (this.secretAreaShown == s)
		{
			return;
		}
		this.secretAreaShown = s;
		Color color = new Color(1f, 1f, 1f, (!s) ? 1f : 0.35f);
		foreach (GameObject current in this.secretGameObjects)
		{
			foreach (Transform transform in current.transform)
			{
				Mesh mesh = transform.gameObject.GetComponent<MeshFilter>().mesh;
				Vector3[] vertices = mesh.vertices;
				Color[] array = new Color[vertices.Length];
				for (int i = 0; i < vertices.Length; i++)
				{
					array[i] = color;
				}
				mesh.colors = array;
			}
		}
	}

	public void updateTileset()
	{
		if ((this.currentTiles >= 1 && this.currentTiles <= 5) || this.currentTiles == 7)
		{
			foreach (tk2dSprite current in this.backgroundSprites)
			{
				current.SetSprite("bg" + this.currentTiles);
			}
		}
		Utils.DestroyImmediateChildren(this.tilemap.renderData);
		this.tilemap.Editor__SpriteCollection = this.tilesets[this.currentTiles - 1].spriteCollection;
		this.tilemap.ForceBuild();
		this.secretGameObjects = new List<GameObject>();
		foreach (Transform transform in this.tilemap.renderData.transform)
		{
			if (transform.gameObject.name == "Secret")
			{
				this.secretGameObjects.Add(transform.gameObject);
			}
		}
		this.objectsLayerHidden = false;
		this.secretAreaShown = false;
	}

	public void loadTMX(string filename)
	{
		Utils.DestroyImmediateChildren(this.tilemap.renderData);
		MapImporter.Import(this.tilemap, "Maps/" + filename);
		for (int i = 0; i < this.tilemap.height; i++)
		{
			for (int j = 0; j < this.tilemap.width; j++)
			{
				int num = this.tilemap.GetTile(j, i, 1);
				if (num >= 100)
				{
					num = -1;
				}
				this.tilemap.SetTile(j, i, 2, num);
			}
		}
		this.updateTileset();
	}

	public void debugDraw()
	{
		if (this.debugEnabled)
		{
			this.updateDebugArray(this.enemies);
			this.updateDebugArray(this.hazards);
			this.updateDebugArray(this.breakables);
			this.updateDebugArray(this.pickups);
		}
	}

	public void updateDebugArray(List<GameObject> objects)
	{
		foreach (GameObject current in objects)
		{
			Enemy component = current.GetComponent<Enemy>();
			if (component)
			{
				if (component.alive)
				{
					Debugger.Instance.DrawRect(component.collisionRect, Color.white);
					Debugger.Instance.DrawRect(component.damageRect, Color.green);
					Debugger.Instance.DrawRect(component.weaponRect, Color.red);
				}
			}
			else
			{
				Entity component2 = current.GetComponent<Entity>();
				if (component2 && component2.alive)
				{
					Debugger.Instance.DrawRect(component2.collisionRect);
				}
			}
		}
	}
}
