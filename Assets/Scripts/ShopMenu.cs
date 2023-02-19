using PlayHaven;
using System;
using TMPro;
using UnityEngine;

public class ShopMenu : ShopWindow
{
	public GameObject maskTop;

	public GameObject maskBottom;

	public tk2dSlicedSprite trim;

	public tk2dTiledSprite trimBackground;

	public GameObject mainPageContainer;

	public GameObject itemPageContainer;

	public GameObject itemRowPrefab;

	public GameObject itemRowIAPPrefab;

	public GameObject itemRowIAPRestorePrefab;

    public GameObject rewardVideoPrefab;

    public GameObject itemMenu;

	public ShopPage currentPage;

	public ShopPage forceCurrentPage;

	//public tk2dTextMesh bannerTitleText;

	public TextMeshPro bannerTitleText; 

	private int lastMoney = -1;

	public tk2dTextMesh moneyLabel;

	public float buttonScrollSpeed = 10f;

	public float scrollHeightCalc = 70f;

	public float minScrollHeight;

	public float maxScrollHeight;

	public float dragTopLocation = 800f;

	public Rectangle dragAreaRect;

	public float screenTension = 5f;

	public float animationSpeed = 0.5f;

	public float scrollRatio = 2f;

	public float velocityCancel = 40f;

	public bool dragging;

	public float velocity;

	public float velocity_min;

	public LeanTweenType animationTweenType = LeanTweenType.easeOutExpo;

	private Vector3 pageStartPos;

	private Vector2 touchFirstPressPos;

	private Vector2 touchLastPressPos;

	private Vector2 touchCurrentSwipe;

	public void Awake()
	{
		GameCore.Instance.shop = this;
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
		base.onSlideInComplete += new CustomWindowEvent(this.slideInComplete);
		Main.loadPrefs();
		this.handleResolutions();
        //if(AdsControl.Instance)
        // AdsControl.Instance.ShowBanner();
    }

	public void OnDestroy()
	{
		GameCore.Instance.shop = null;
		base.onSlideInComplete -= new CustomWindowEvent(this.slideInComplete);
	}

	public void slideInComplete()
	{
		if (GameCore.Instance.playhavenEnabled)
		{
//			PlayHavenManager.instance.ContentRequest("shop_menu");
		}
	}

	public void Start()
	{
		this.dragAreaRect = new Rectangle(-121, -80, 205, 135);
		this.velocityCancel = 40f;
		this.currentPage = ShopPage.MAIN;
		if (this.forceCurrentPage != this.currentPage)
		{
			this.currentPage = this.forceCurrentPage;
		}
		this.switchPage(this.currentPage, true);
	}

	public void Update()
	{
		if (this.buttonsEnabled)
		{
			this.SwipeDetection();
			this.HandleVerticalAcceleration();
		}
		this.updateMoney();
	}

	public void updateMoney()
	{
		if (this.lastMoney == Main.playerStats.money)
		{
			return;
		}
		this.lastMoney = Main.playerStats.money;
		this.moneyLabel.text = Main.playerStats.money.ToString("N0");
	}

	public void switchPage(ShopPage newPage, bool force = false)
	{
		if (this.currentPage == newPage && !force)
		{
			return;
		}
		this.currentPage = newPage;
		if (this.currentPage == ShopPage.MAIN)
		{
			this.mainPageContainer.gameObject.SetActive(true);
			this.itemPageContainer.gameObject.SetActive(false);
			this.bannerTitleText.text = Localisation.GetString( "SHOP");
		}
		else
		{
			this.mainPageContainer.gameObject.SetActive(false);
			this.itemPageContainer.gameObject.SetActive(true);
			this.buildItemMenu();
		}
	}

	public void buildItemMenu()
	{
		UnityEngine.Object.Destroy(this.itemMenu);
		this.itemMenu = new GameObject("ItemMenu");
		this.itemMenu.transform.parent = this.itemPageContainer.transform;
		this.itemMenu.transform.localPosition = new Vector3(-38f, 38f, -5f);
		string[,] array = Main.itemStats.weaponStats;
		int[] array2 = Main.playerStats.weapons;
		string arg = "shop_icon_weapon";
		int num = -1;
		switch (this.currentPage)
		{
		case ShopPage.WEAPONS:
			array = Main.itemStats.weaponStats;
			array2 = Main.playerStats.weapons;
			num = Main.playerStats.equippedWeapon;
			arg = "shop_icon_weapon";
			this.bannerTitleText.text = Localisation.GetString("WEAPONS");
			break;
		case ShopPage.ARMOR:
			array = Main.itemStats.armorStats;
			array2 = Main.playerStats.armors;
			num = Main.playerStats.equippedArmor;
			arg = "shop_icon_armor";
			this.bannerTitleText.text = Localisation.GetString("ARMOR");
			break;
		case ShopPage.POTIONS:
			array = Main.itemStats.potionStats;
			array2 = Main.playerStats.potions;
			num = -2;
			arg = "shop_icon_potion";
			this.bannerTitleText.text = Localisation.GetString("POTIONS");
			break;
		case ShopPage.RINGS:
			array = Main.itemStats.ringStats;
			array2 = Main.playerStats.rings;
			num = Main.playerStats.equippedRing;
			arg = "shop_icon_ring";
			this.bannerTitleText.text = Localisation.GetString("RINGS");
			break;
		case ShopPage.AMULETS:
			array = Main.itemStats.amuletStats;
			array2 = Main.playerStats.amulets;
			num = Main.playerStats.equippedAmulet;
			arg = "shop_icon_amulet";
			this.bannerTitleText.text = Localisation.GetString("AMULETS");
			break;
		case ShopPage.COINS:
			array = Main.itemStats.iapStats;
			array2 = null;
			num = 0;
			arg = "shop_icon_coins";
			this.bannerTitleText.text = Localisation.GetString("COINS");
			break;
		}
		if (array.Length / 8 == 10)
		{
			this.scrollHeightCalc = 70f;
		}
		else
		{
			this.scrollHeightCalc = 79f;
		}
		this.minScrollHeight = 38f;
		this.maxScrollHeight = this.scrollHeightCalc * (float)(array.Length / 8);
		if (!GameCore.Instance.IS_IPAD)
		{
			this.minScrollHeight = 18f;
			this.maxScrollHeight = this.scrollHeightCalc * (float)(array.Length / 8) + 20f;
		}
		if (this.currentPage == ShopPage.COINS)
		{
            //don't show IAP

            GameObject gameObject = UnityEngine.Object.Instantiate(this.rewardVideoPrefab) as GameObject;
            gameObject.transform.parent = this.itemMenu.transform;
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            this.maxScrollHeight = 10f;

            //Show IAP

            /*
			this.maxScrollHeight = 615f;
			if (!GameCore.Instance.IS_IPAD)
			{
				this.maxScrollHeight = 628f;
			}
			for (int i = 0; i < array.Length / 8; i++)
			{
				if (i == 0)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(this.itemRowIAPRestorePrefab) as GameObject;
					gameObject.name = "Row" + (i + 1);
					gameObject.transform.parent = this.itemMenu.transform;
					gameObject.transform.localPosition = new Vector3(0f, (float)i * -44f, 0f);
					ShopItemRowIAPRestore component = gameObject.GetComponent<ShopItemRowIAPRestore>();
					component.shop = this;
					component.itemName = array[i, 0];
					component.itemDescription = array[i, 2];
					component.itemSKU = array[i, 3];
					component.SetButtonState(1);
					component.updateRowInfo();
				}
				else
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate(this.itemRowIAPPrefab) as GameObject;
					gameObject2.name = "Row" + (i + 1);
					gameObject2.transform.parent = this.itemMenu.transform;
					gameObject2.transform.localPosition = new Vector3(0f, (float)i * -44f, 0f);
					ShopItemRowIAP component2 = gameObject2.GetComponent<ShopItemRowIAP>();
					component2.shop = this;
					component2.itemNum = i + 1;
					component2.alreadyOwned = false;
					component2.iconName = arg + (component2.itemNum - 1);
					component2.itemName = array[i, 0];
					component2.itemCost = ((!(array[i, 1] != string.Empty)) ? 0.99f : ((float)Convert.ToDouble(array[i, 1])));
					component2.itemDescription = array[i, 2];
					component2.itemSKU = array[i, 3];
					component2.SetButtonState(1);
					if (i == 1)
					{
						component2.alreadyOwned = false;
						if (GameCore.Instance.ownsCoinDoubler)
						{
							component2.alreadyOwned = true;
							component2.SetButtonState(3);
						}
					}
					component2.updateRowInfo();
				}
			}
			*/
        }
		else
		{
			for (int j = 0; j < array.Length / 8; j++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this.itemRowPrefab) as GameObject;
				gameObject3.name = "Row" + (j + 1);
				gameObject3.transform.parent = this.itemMenu.transform;
				gameObject3.transform.localPosition = new Vector3(0f, (float)j * -44f, 0f);
				ShopItemRow component3 = gameObject3.GetComponent<ShopItemRow>();
				component3.shop = this;
				component3.itemNum = j + 1;
				component3.locked = (j != 0 && array2[j - 1] < 1);
				component3.alreadyOwned = (array2[j] >= 1);
				component3.iconName = arg + component3.itemNum;
				component3.itemName = Localisation.GetString (array[j, 0]);
				component3.itemCost = ((!(array[j, 1] != string.Empty)) ? 0 : Convert.ToInt32(array[j, 1]));
				component3.statName1 = array[j, 2];
				component3.statName2 = array[j, 3];
				component3.statName3 = array[j, 4];
				component3.statValue1 = ((!(array[j, 5] != string.Empty)) ? 0 : Convert.ToInt32(array[j, 5]));
				component3.statValue2 = ((!(array[j, 6] != string.Empty)) ? 0 : Convert.ToInt32(array[j, 6]));
				component3.statValue3 = ((!(array[j, 7] != string.Empty)) ? 0 : Convert.ToInt32(array[j, 7]));
				if (component3.alreadyOwned)
				{
					if (num == -2)
					{
						component3.SetButtonState(4);
					}
					else if (num == component3.itemNum)
					{
						component3.SetButtonState(3);
					}
					else
					{
						component3.SetButtonState(2);
					}
				}
				else
				{
					component3.SetButtonState(1);
				}
				component3.updateRowInfo();
			}
		}
		Vector3 localPosition = this.itemMenu.transform.localPosition;
		localPosition.y = (float)GameCore.Instance.shopPagePositions[this.currentPage - ShopPage.WEAPONS];
		this.itemMenu.transform.localPosition = localPosition;
		localPosition.y = (float)this.getSafePageScrollPosition();
		this.itemMenu.transform.localPosition = localPosition;
	}

	public override void closeClicked()
	{
		if (this.forceCurrentPage != ShopPage.MAIN)
		{
			Game.Instance.player.updateItems();
			Main.playerStats.updatePlayerStats();
			Game.Instance.player.cancelAttack();
			Game.Instance.paused = false;
			WindowManager.Instance.HideMenu(this);
		}
		else if (this.currentPage == ShopPage.MAIN)
		{
			Game.Instance.player.updateItems();
			Main.playerStats.updatePlayerStats();
			Game.Instance.player.cancelAttack();
			Game.Instance.paused = false;
			WindowManager.Instance.HideMenu(this);
            //if (AdsControl.Instance)
            //    AdsControl.Instance.HideBanner();
        }
		else
		{
			GameCore.Instance.shopPagePositions[this.currentPage - ShopPage.WEAPONS] = this.getSafePageScrollPosition();
			this.switchPage(ShopPage.MAIN, false);
		}
        Main.savePrefs();
	}

	public void UpdateRows()
	{
		string[,] array = Main.itemStats.weaponStats;
		int[] array2 = Main.playerStats.weapons;
		int num = -1;
		switch (this.currentPage)
		{
		case ShopPage.WEAPONS:
			array = Main.itemStats.weaponStats;
			array2 = Main.playerStats.weapons;
			num = Main.playerStats.equippedWeapon;
			break;
		case ShopPage.ARMOR:
			array = Main.itemStats.armorStats;
			array2 = Main.playerStats.armors;
			num = Main.playerStats.equippedArmor;
			break;
		case ShopPage.POTIONS:
			array = Main.itemStats.potionStats;
			array2 = Main.playerStats.potions;
			num = -2;
			break;
		case ShopPage.RINGS:
			array = Main.itemStats.ringStats;
			array2 = Main.playerStats.rings;
			num = Main.playerStats.equippedRing;
			break;
		case ShopPage.AMULETS:
			array = Main.itemStats.amuletStats;
			array2 = Main.playerStats.amulets;
			num = Main.playerStats.equippedAmulet;
			break;
		}
		ShopItemRow[] componentsInChildren = this.itemMenu.GetComponentsInChildren<ShopItemRow>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ShopItemRow shopItemRow = componentsInChildren[i];
			shopItemRow.locked = (i != 0 && array2[i - 1] < 1);
			shopItemRow.alreadyOwned = (array2[i] >= 1);
			if (shopItemRow.alreadyOwned)
			{
				if (num == -2)
				{
					shopItemRow.SetButtonState(4);
				}
				else if (num == shopItemRow.itemNum)
				{
					shopItemRow.SetButtonState(3);
				}
				else
				{
					shopItemRow.SetButtonState(2);
				}
			}
			else
			{
				shopItemRow.SetButtonState(1);
			}
			shopItemRow.updateRowInfo();
		}
	}

	public void weaponsClicked()
	{
		this.switchPage(ShopPage.WEAPONS, false);
	}

	public void armorClicked()
	{
		this.switchPage(ShopPage.ARMOR, false);
	}

	public void potionsClicked()
	{
		this.switchPage(ShopPage.POTIONS, false);
	}

	public void ringsClicked()
	{
		this.switchPage(ShopPage.RINGS, false);
	}

	public void amuletsClicked()
	{
		this.switchPage(ShopPage.AMULETS, false);
	}

	public void coinsClicked()
	{
		this.switchPage(ShopPage.COINS, false);
	}

	public void SwipeDetection()
	{
		if (this.itemMenu == null)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0) && UnityEngine.Input.mousePosition.y <= this.dragTopLocation)
		{
			this.pageStartPos = this.getPagePosition();
			this.touchFirstPressPos = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			this.dragging = true;
			LeanTween.cancel(this.itemMenu);
		}
		if (Input.GetMouseButton(0) && this.dragging)
		{
			Vector2 vector = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			this.touchLastPressPos = vector;
			this.touchCurrentSwipe = new Vector2(vector.x - this.touchFirstPressPos.x, vector.y - this.touchFirstPressPos.y);
			this.dragging = true;
			Vector3 localPosition = this.itemMenu.transform.localPosition;
			localPosition.y = this.pageStartPos.y + this.touchCurrentSwipe.y / this.screenTension * this.scrollRatio;
			localPosition.y = Mathf.Floor(localPosition.y);
			this.itemMenu.transform.localPosition = localPosition;
		}
		if (Input.GetMouseButtonUp(0) && this.dragging)
		{
			Vector2 vector2 = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			this.touchCurrentSwipe = new Vector2(vector2.x - this.touchLastPressPos.x, vector2.y - this.touchLastPressPos.y);
			this.dragging = false;
			if (this.itemMenu.transform.localPosition.y < this.minScrollHeight)
			{
				Vector3 localPosition2 = this.itemMenu.transform.localPosition;
				localPosition2.y = this.minScrollHeight;
				LeanTween.moveLocal(this.itemMenu, localPosition2, this.animationSpeed).setEase(this.animationTweenType).setOnComplete(new Action(this.animateBounceBackComplete));
				this.velocity = 0f;
			}
			else if (this.itemMenu.transform.localPosition.y > this.maxScrollHeight)
			{
				Vector3 localPosition3 = this.itemMenu.transform.localPosition;
				localPosition3.y = this.maxScrollHeight;
				LeanTween.moveLocal(this.itemMenu, localPosition3, this.animationSpeed).setEase(this.animationTweenType).setOnComplete(new Action(this.animateBounceBackComplete));
				this.velocity = 0f;
			}
			else if (vector2.y < this.touchLastPressPos.y)
			{
				this.velocity = Mathf.Sqrt(this.touchCurrentSwipe.x * this.touchCurrentSwipe.x + this.touchCurrentSwipe.y * this.touchCurrentSwipe.y) * -1f;
				this.velocity *= 5f;
				this.velocity_min = -1f;
				if (this.velocity > -this.velocityCancel)
				{
					this.velocity = 0f;
					this.velocity_min = 0f;
				}
			}
			else
			{
				this.velocity = Mathf.Sqrt(this.touchCurrentSwipe.x * this.touchCurrentSwipe.x + this.touchCurrentSwipe.y * this.touchCurrentSwipe.y);
				this.velocity *= 5f;
				this.velocity_min = 1f;
				if (this.velocity < this.velocityCancel)
				{
					this.velocity = 0f;
					this.velocity_min = 0f;
				}
			}
		}
	}

	public void HandleVerticalAcceleration()
	{
		if (this.itemMenu == null)
		{
			return;
		}
		if (!this.dragging && !Mathf.Approximately(this.velocity, 0f))
		{
			Vector3 localPosition = this.itemMenu.transform.localPosition;
			localPosition.y += this.velocity * Time.deltaTime;
			localPosition.y = Mathf.Floor(localPosition.y);
			this.itemMenu.transform.localPosition = localPosition;
			this.velocity *= 0.95f;
			if ((this.velocity_min > 0f && this.velocity <= this.velocity_min) || (this.velocity_min < 0f && this.velocity >= this.velocity_min))
			{
				LeanTween.cancel(this.itemMenu);
				this.velocity = 0f;
			}
			if (this.itemMenu.transform.localPosition.y < this.minScrollHeight)
			{
				localPosition = this.itemMenu.transform.localPosition;
				localPosition.y = this.minScrollHeight;
				this.itemMenu.transform.localPosition = localPosition;
				this.velocity = 0f;
			}
			if (this.itemMenu.transform.localPosition.y > this.maxScrollHeight)
			{
				localPosition = this.itemMenu.transform.localPosition;
				localPosition.y = this.maxScrollHeight;
				this.itemMenu.transform.localPosition = localPosition;
				this.velocity = 0f;
			}
		}
	}

	public void animateBounceBackComplete()
	{
	}

	public Vector3 getPagePosition()
	{
		if (this.itemMenu != null)
		{
			return this.itemMenu.transform.localPosition;
		}
		return Vector3.zero;
	}

	public int getSafePageScrollPosition()
	{
		if ((float)((int)Mathf.Floor(this.itemMenu.transform.localPosition.y)) < this.minScrollHeight)
		{
			return (int)this.minScrollHeight;
		}
		if ((float)((int)Mathf.Floor(this.itemMenu.transform.localPosition.y)) > this.maxScrollHeight)
		{
			return (int)this.maxScrollHeight;
		}
		return (int)Mathf.Floor(this.itemMenu.transform.localPosition.y);
	}

	public override void handleResolutions()
	{
		base.handleResolutions();
		if (!GameCore.Instance.IS_IPAD)
		{
			this.dragTopLocation = 800f;// было 525
			this.maskTop.transform.localPosition = new Vector3(this.maskTop.transform.localPosition.x, 432f, this.maskTop.transform.localPosition.z);
			this.maskBottom.transform.localPosition = new Vector3(this.maskBottom.transform.localPosition.x, 228f, this.maskBottom.transform.localPosition.z);
			this.trim.transform.localPosition = new Vector3(this.trim.transform.localPosition.x, 82f, this.trim.transform.localPosition.z);
			this.trim.dimensions = new Vector2(200f, 110f);
			this.trimBackground.dimensions = new Vector2(198f, 108f);
		}
	}

	public void showNetworkError()
	{
		WindowManager.Instance.ShowAlertView("Network Error", "You do not appear to be connected to the internet right now. Please check your settings and try again.", string.Empty, "OK", base.gameObject, string.Empty, string.Empty);
	}

	public void showPurchaseDisabled()
	{
		WindowManager.Instance.ShowAlertView("Purchase Error", "You do not appear to be able to make in-app purchases. Please check your settings and try again.", string.Empty, "OK", base.gameObject, string.Empty, string.Empty);
	}

	public void presentRestoredAlert(string productIdentifier)
	{
		WindowManager.Instance.ShowAlertView("Restore Purchases", "We have restored your previous non-consumable purchases! Enjoy!", string.Empty, "OK", base.gameObject, string.Empty, string.Empty);
	}

	public void presentGratitudeAlert(string productIdentifier)
	{
		WindowManager.Instance.ShowAlertView("Thank You!", "We thank you for your purchase and we appreciate you for supporting us! Enjoy!", string.Empty, "OK", base.gameObject, string.Empty, string.Empty);
	}
}
