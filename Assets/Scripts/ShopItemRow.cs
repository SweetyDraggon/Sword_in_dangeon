using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemRow : MonoBehaviour
{
	public ShopMenu shop;

	public int itemNum;

	public string iconName;

	public string itemName;

	public int itemCost;

	public string statName1;

	public int statValue1;

	public string statName2;

	public int statValue2;

	public string statName3;

	public int statValue3;

	public bool locked;

	public bool alreadyOwned;

	private int buttonState = 1;

	public GameObject button;

	public GameObject divider;

	public tk2dSprite lockedSprite;

	public tk2dSprite icon;

	public tk2dSprite highlight;

	public tk2dSprite color;

	public tk2dTextMesh textItemName;

	public tk2dTextMesh textItemCost;

	public TextMeshPro textStatLabel1;

	public TextMeshPro textStatLabel2;

	public TextMeshPro textStatLabel3;

	public tk2dTextMesh textStatValue1;

	public tk2dTextMesh textStatValue2;

	public tk2dTextMesh textStatValue3;

	public tk2dTextMesh update;

	public CustomUIButton customButton;
	
	public TextMeshPro buttonText;

	public TextMeshPro sold;
	public Player player;

	[SerializeField] private TextMeshPro _itemName;

	private void Awake()
	{
		this.customButton = this.button.GetComponent<CustomUIButton>();
		this.updateButton();
		this.updateRowInfo();
		GameObject go = GameObject.Find("Player");
		player = go.GetComponent<Player>();
	}

	private void Start()
	{
		if (this.customButton.sendMessageTarget == null)
		{
			this.customButton.sendMessageTarget = base.gameObject;
		}

		
	}
	

	public void updateRowInfo()
	{
		if (this.locked)
		{
			this.icon.SetSprite("shop_icon_locked");
			this.lockedSprite.gameObject.SetActive(true);
			this.icon.gameObject.SetActive(true);
			this.highlight.gameObject.SetActive(false);
			this.divider.gameObject.SetActive(false);
			this.button.gameObject.SetActive(false);
			this.textItemName.gameObject.SetActive(false);
			this.textItemCost.gameObject.SetActive(false);
			this.textStatLabel1.gameObject.SetActive(false);
			this.textStatLabel2.gameObject.SetActive(false);
			this.textStatLabel3.gameObject.SetActive(false);
			this.textStatValue1.gameObject.SetActive(false);
			this.textStatValue2.gameObject.SetActive(false);
			this.textStatValue3.gameObject.SetActive(false);
		}
		else
		{
			this.lockedSprite.gameObject.SetActive(false);
			this.icon.gameObject.SetActive(true);
			this.highlight.gameObject.SetActive(this.buttonState == 3);
			this.divider.gameObject.SetActive(true);
			this.button.gameObject.SetActive(true);
			this.textItemName.gameObject.SetActive(true);
			this.textItemCost.gameObject.SetActive(true);
			this.textStatLabel1.gameObject.SetActive(true);
			this.textStatLabel2.gameObject.SetActive(true);
			this.textStatLabel3.gameObject.SetActive(true);
			this.textStatValue1.gameObject.SetActive(true);
			this.textStatValue2.gameObject.SetActive(true);
			this.textStatValue3.gameObject.SetActive(true);
			this.icon.SetSprite(this.iconName);
			//this.textItemName.text = this.itemName;
			_itemName.text = itemName;
			this.textItemCost.text = "$" + this.itemCost.ToString();
			if (this.itemName.Length >= 10)
			{
				this.textItemName.scale = new Vector2(0.95f, 0.95f);
			}
			if (this.itemName.Length >= 13)
			{
				this.textItemName.scale = new Vector2(0.9f, 0.9f);
			}
			if (this.itemName.Length >= 16)
			{
				this.textItemName.scale = new Vector2(0.75f, 0.75f);
			}
			if (this.statName1 == string.Empty)
			{
				this.textStatLabel1.gameObject.SetActive(false);
				this.textStatValue1.gameObject.SetActive(false);
			}
			else
			{
				this.textStatLabel1.gameObject.SetActive(true);
				this.textStatValue1.gameObject.SetActive(true);
				this.textStatLabel1.text = Localisation.GetString(this.statName1);
				this.textStatValue1.text = this.statValue1.ToString("N0");
			}
			if (this.statName2 == string.Empty)
			{
				this.textStatLabel2.gameObject.SetActive(false);
				this.textStatValue2.gameObject.SetActive(false);
			}
			else
			{
				this.textStatLabel2.gameObject.SetActive(true);
				this.textStatValue2.gameObject.SetActive(true);
				this.textStatLabel2.text = Localisation.GetString(this.statName2);
				this.textStatValue2.text = this.statValue2.ToString("N0");
			}
			if (this.statName3 == string.Empty)
			{
				this.textStatLabel3.gameObject.SetActive(false);
				this.textStatValue3.gameObject.SetActive(false);
			}
			else
			{
				this.textStatLabel3.gameObject.SetActive(true);
				this.textStatValue3.gameObject.SetActive(true);
				this.textStatLabel3.text = Localisation.GetString(this.statName3);
				this.textStatValue3.text = this.statValue3.ToString("N0");
			}

			if (this.alreadyOwned)
			{
				switch (this.shop.currentPage)
				{
					case ShopPage.WEAPONS:
						this.buttonText.text = Localisation.GetString("EQUIP");
						if (Main.playerStats.equippedWeapon == this.itemNum)
						{
							this.buttonText.text = Localisation.GetString("EQUIPPED");
						};
						break;
					case ShopPage.ARMOR:
						this.buttonText.text = Localisation.GetString("EQUIP");
						if (Main.playerStats.equippedArmor == this.itemNum)
						{
							this.buttonText.text = Localisation.GetString("EQUIPPED");
							
						};
						break;
					case ShopPage.RINGS:
						this.buttonText.text = Localisation.GetString("EQUIP");
						if (Main.playerStats.equippedRing == this.itemNum)
						{
							this.buttonText.text = Localisation.GetString("EQUIPPED");
						};
						break;
					case ShopPage.AMULETS:
						this.buttonText.text = Localisation.GetString("EQUIP");
						if (Main.playerStats.equippedAmulet == this.itemNum)
						{
							this.buttonText.text = Localisation.GetString("EQUIPPED");
						};
						break;
					case ShopPage.POTIONS:
						//this.buttonText.text = Localisation.GetString("SOLD");
						this.button.SetActive(false);
						this.sold.enabled = true;
						this.sold.text = Localisation.GetString("SOLD");
						break;
				}
			}
			else if (!this.alreadyOwned)
			{
				this.buttonText.text = Localisation.GetString("BUY");
			}
			
			this.updateButton();
		}
	}

	public void showCoinsScreen()
	{
		this.shop.coinsClicked();
	}

	public void buyItemClicked()
	{
		if (this.alreadyOwned)
		{
			return;
		}
		if (Main.playerStats.money >= this.itemCost)
		{
			WindowManager.Instance.ShowAlertView(Localisation.GetString( "CONFIRM_PURCHASE"), string.Concat(new object[]
			{
				Localisation.GetString( "ARE_YOU_SURE_YOU_WISH_TO_PURCHASE"),
				this.itemName + " ",
				Localisation.GetString( "FOR"),
				" "+ this.itemCost + " ",
				Localisation.GetString( "COINS?")
			}), Localisation.GetString( "NO"), Localisation.GetString( "YES"), base.gameObject, string.Empty, "confirmedPurchase" );
		}
		else
		{
			Debug.LogError("Music" + Localisation.GetString("ok"));
			WindowManager.Instance.ShowAlertView(Localisation.GetString("Not_Enough_Money"), Localisation.GetString("You_do_not_have_enough_to_purchase_this_item."), Localisation.GetString("ok"), string.Empty, base.gameObject, "cancelledPurchase", string.Empty);
		}
	}

	public void confirmedPurchase()//ПОЧИНИТЬ ВОТ ЗДЕСЬ присылается сообщение и вызывается этот метод
	{
		if (this.alreadyOwned)
		{
			return;
		}
		this.alreadyOwned = true;
		Main.playerStats.money -= this.itemCost;
		AudioManager.Instance.PlaySound("cash_register");
		AchievementHandler.Instance.Increment(ACHIEVEMENT.SHOPAHOLIC, this.itemCost);
		int num = this.itemNum - 1;
		if (num < 0)
		{
			num = 0;
		}
		switch (this.shop.currentPage)
		{
		case ShopPage.WEAPONS:
			Main.playerStats.weapons[num] = 1;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				Main.playerStats.equippedWeapon = this.itemNum;
			this.SetButtonState(3);
			Game.Instance.player.weapon.updateEquipmentArt();
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOUGHT_WEAPON, 1);
			break;
		case ShopPage.ARMOR:
			Main.playerStats.armors[num] = 1;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				Main.playerStats.equippedArmor = this.itemNum;
			this.SetButtonState(3);
			Game.Instance.player.updateItems();
				
				AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOUGHT_ARMOR, 1);
			break;
		case ShopPage.POTIONS:
			Main.playerStats.potions[num] = 1;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				this.SetButtonState(4);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOUGHT_POTION, 1);
			break;
		case ShopPage.RINGS:
			Main.playerStats.rings[num] = 1;
			Main.playerStats.equippedRing = this.itemNum;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				this.SetButtonState(3);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOUGHT_RING, 1);
			break;
		case ShopPage.AMULETS:
			Main.playerStats.amulets[num] = 1;
			Main.playerStats.equippedAmulet = this.itemNum;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				this.SetButtonState(3);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.BOUGHT_AMULET, 1);
			break;
		}
		Main.playerStats.pollPurchases();
		Main.saveGame();
		this.shop.UpdateRows();
	}

	public void equipButtonClicked()
	{
		switch (this.shop.currentPage)
		{
		case ShopPage.WEAPONS:
			Main.playerStats.equippedWeapon = this.itemNum;
			this.SetButtonState(3);
			Game.Instance.player.weapon.updateEquipmentArt();
			break;
		case ShopPage.ARMOR:
			Main.playerStats.equippedArmor = this.itemNum;
			this.SetButtonState(3);
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				Game.Instance.player.updateItems();
				Debug.Log(Main.playerStats.stamina);
				break;
		case ShopPage.POTIONS:
			this.SetButtonState(4);
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				break;
		case ShopPage.RINGS:
			Main.playerStats.equippedRing = this.itemNum;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				this.SetButtonState(3);
			break;
		case ShopPage.AMULETS:
			Main.playerStats.equippedAmulet = this.itemNum;
				Main.playerStats.updatePlayerStats();
				Main.playerStats.maxHealth = Main.playerStats.stamina * 2;
				Main.playerStats.currentHealth = Main.playerStats.maxHealth;
				player.updateStats();
				this.SetButtonState(3);
			break;
		}
		Main.saveGame();
		this.shop.UpdateRows();
	}

	public void updateButton()
	{
		if (this.buttonState == 2)
		{
			this.customButton.SendMessageOnClickMethodName = "equipButtonClicked";
			this.customButton.imageUp = "shop_item_button3";
			this.customButton.imageDown = "shop_item_button4";
			
		}
		else if (this.buttonState == 3)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
			this.customButton.imageUp = "shop_item_button5";
			this.customButton.imageDown = "shop_item_button5";
		}
		else if (this.buttonState == 4)
		{
			this.customButton.SendMessageOnClickMethodName = string.Empty;
			this.customButton.imageUp = "shop_item_button6";
			this.customButton.imageDown = "shop_item_button6";
		}
		else
		{
			this.customButton.SendMessageOnClickMethodName = "buyItemClicked";
			this.customButton.imageUp = "shop_item_button1";
			this.customButton.imageDown = "shop_item_button2";
		}
		this.customButton.updateSprites();
	}

	public void SetButtonState(int s)
	{
		this.buttonState = s;
		this.updateButton();
	}
}
