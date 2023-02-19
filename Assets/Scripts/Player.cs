using System;
using UnityEngine;
using System.Collections;

public class Player : Entity
{

	public bool leftPressed;

	public bool rightPressed;

	public bool attackPressed;

	public bool jumpPressed;

	public bool xStillPressed;

	public bool cStillPressed;

	public bool keysDisabled;

	public WeaponHand weapon;

	public bool canSwing;

	public float swingDelay;

	public float swingDelayMax;

	public bool swinging;

	public int swingVel;

	public float jumpAccelerationTimer;

	public int jumpAccelerationTimerMax;

	public bool jumpReleased;

	public bool inControl;

	public bool warping;

	public float warpTimer;

	public bool warpedIn;

	public int xp;

	public int xpLevel;

	public int idleFrame;

	public int jumpFrame;

	public int fallFrame;

	public int runFrame;

	public int attackFrame;

	public bool death_screen_shown;

	public float jumpHeldTimer;

	public float jumpHeldTimerMax;


    public void cancelAttack()
	{
		this.attackPressed = false;
		this.cStillPressed = false;
		this.frame = this.idleFrame;
		base.gotoAndStop(this.frame);
		this.swinging = false;
		this.canSwing = true;
		this.weapon.cancelSwing();
	}

	public void updateStats()
	{
		this.health = Main.playerStats.currentHealth;
		this.maxHealth = Main.playerStats.maxHealth;
	}
	public override void reset()
	{
		this.currentAnimationName = "player";
		if (this.weapon == null)
		{
			this.weapon = base.transform.Find("Weapon").GetComponent<WeaponHand>();
		}
		if (this.currentSprite)
		{
			this.currentSprite.gameObject.SetActive(true);
		}
		if (this.weapon && this.weapon.currentSprite)
		{
			this.weapon.currentSprite.gameObject.SetActive(true);
		}
		this.death_screen_shown = false;
		this.jumpHeldTimerMax = 12f;
		this.xVel = 0f;
		this.yVel = 0f;
		this.realW = 28;
		this.realH = 32;
		this.maxVel = 7;
		this.swingVel = 1;
		this.acceleration = 2;
		this.gravity = 1;
		this.jumpPower = 12;
		this.state = 1;
		this.xStillPressed = false;
		this.cStillPressed = false;
		this.airborne = true;
		this.alive = true;
		this.alpha = 1f;
		this.onSlope = false;
		this.swinging = false;
		this.inControl = false;
		this.warping = true;
		this.warpedIn = false;
		this.warpTimer = 0f;
		this.scaleX = 0f;
		this.scaleY = 0f;
		this.jumpAccelerationTimer = 0f;
		this.jumpAccelerationTimerMax = 4;
		this.jumpReleased = true;
		this.canSwing = true;
		this.hit = false;
		this.hitTimer = 0f;
		this.hitState = 0;
		this.immune = false;
		this.type = Main.playerStats.equippedArmor + 1;
		this.sortFrames();
		this.frame = this.idleFrame;
		this.xp = Main.playerStats.playerXp;
		this.health = Main.playerStats.currentHealth;
		this.maxHealth = Main.playerStats.maxHealth;
		this.swingDelay = 0f;
		int num = Convert.ToInt32(Main.itemStats.weaponStats[Main.playerStats.equippedWeapon - 1, 6]);
		if (num == 1)
		{
			this.swingDelayMax = 10f;
		}
		else if (num == 2)
		{
			this.swingDelayMax = 9f;
		}
		else if (num == 3)
		{
			this.swingDelayMax = 8f;
		}
		else if (num == 4)
		{
			this.swingDelayMax = 7f;
		}
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
		this.keysDisabled = false;
		this.collisionRect = new Rectangle((int)this.x - this.realW / 2, (int)this.y - this.realH / 2, this.realW, this.realH);
	}

	public void loadNewLevel()
	{
		this.hit = false;
		this.hitTimer = 0f;
		this.hitState = 0;
		this.immune = false;
		this.xVel = 0f;
		this.yVel = 0f;
		tk2dBaseSprite arg_68_0 = this.weapon.currentSprite;
		Color color = new Color(1f, 1f, 1f, 1f);
		this.currentSprite.color = color;
		arg_68_0.color = color;
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			if (this.alive && this.inControl)
			{
				this.handleMovement(dt);
				this.handleAnimation(dt);
				this.handleWeapon();
				this.testSecretCollision();
				this.testSpikeCollision();
			}
			if (this.warping)
			{
				this.handleWarp(dt);
			}
			if (!this.alive)
			{
				this.handleDeath(dt);
			}
		}
	}

	public void testSecretCollision()
	{
		if ((this.map.GetTileID(this.right, this.centerY, 2) >= 1 && this.map.GetTileID(this.right, this.centerY, 2) <= 32) || (this.map.GetTileID(this.left, this.centerY, 2) >= 1 && this.map.GetTileID(this.left, this.centerY, 2) <= 32))
		{
			this.map.showSecretArea(true);
		}
		else
		{
			this.map.showSecretArea(false);
		}
	}

	public void addMoney(int a)
	{
		Main.playerStats.money += a;
	}

	public void addHealth(int a)
	{
		this.health += a;
		if (this.health > this.maxHealth)
		{
			this.health = this.maxHealth;
		}
	}

	public void addXp(int a)
	{
		Game.Instance.fxManager.emitCustomText(new Vector2(this.x, this.y), a.ToString() + " XP");
		this.xp += a;
		Main.playerStats.playerXp = this.xp;
		if (this.xp >= Main.playerStats.nextXpLevel)
		{
			Main.playerStats.playerLevel++;
			Main.playerStats.nextXpLevel = (int)Mathf.Round((float)Main.playerStats.nextXpLevel * 1.3f);
			Main.playerStats.playerXp = 0;
			this.xp = 0;
			this.health = (Main.playerStats.currentHealth = Main.playerStats.maxHealth);
			AudioManager.Instance.PlaySound("level_up");
			Game.Instance.camView.screenShake(13f, 0.5f);
			AchievementHandler.Instance.SetValue(ACHIEVEMENT.LEVEL_UP, 1);
			base.Invoke("showLevelUpText", 0.1f);
			if (Game.Instance.hud.levelUpFlash != null)
			{
				Game.Instance.hud.levelUpFlash.gameObject.SetActive(true);
				Game.Instance.hud.levelUpFlash.FadeOut();
			}
			Game.Instance.fxManager.emitFlash(new Vector2(this.x, this.y), FXParticleTypes.FLASH_LEVEL_UP);
			base.Invoke("showLevelUpMenu", 0.5f);
		}
	}

	private void showLevelUpText()
	{
		Game.Instance.fxManager.emitCustomText(new Vector2(this.x, this.y), Localisation.GetString( "level_up!"));
	}

	private void showLevelUpMenu()
	{
		Game.Instance.paused = true;
		WindowManager.Instance.ShowMenu("level_up_menu");
	}

	public void updateItems()
	{
		this.sortFrames();
		this.frame = this.idleFrame;
		if (this.weapon == null)
		{
			this.weapon = base.transform.Find("Weapon").GetComponent<WeaponHand>();
		}
		if (this.weapon != null)
		{
			this.weapon.updateWeapon();
		}
		int num = Convert.ToInt32(Main.itemStats.weaponStats[Main.playerStats.equippedWeapon - 1, 6]);
		if (num == 1)
		{
			this.swingDelayMax = 10f;
		}
		else if (num == 2)
		{
			this.swingDelayMax = 9f;
		}
		else if (num == 3)
		{
			this.swingDelayMax = 8f;
		}
		else if (num == 4)
		{
			this.swingDelayMax = 7f;
		}
	}

	private void sortFrames()
	{
		this.type = Main.playerStats.equippedArmor + 1;
		this.idleFrame = 0;
		this.jumpFrame = this.idleFrame + 12;
		this.fallFrame = this.idleFrame + 13;
		this.runFrame = this.idleFrame + 14;
		this.attackFrame = this.idleFrame + 26;
		this.rebuildAnimationClip();
	}

	public override void rebuildAnimationClip()
	{
		tk2dSpriteAnimationClip clipByName = this.currentAnimation.GetClipByName(this.currentAnimationName);
		this.currentAnimationClip = new tk2dSpriteAnimationClip();
		this.currentAnimationClip.CopyFrom(clipByName);
		tk2dSpriteAnimationFrame[] frames = this.currentAnimationClip.frames;
		for (int i = 0; i < frames.Length; i++)
		{
			tk2dSpriteAnimationFrame tk2dSpriteAnimationFrame = frames[i];
			int num = Mathf.Min(Mathf.Max(this.type - 1, 0), 10);
			string name = tk2dSpriteAnimationFrame.spriteCollection.spriteDefinitions[tk2dSpriteAnimationFrame.spriteId].name;
			string name2 = name.Replace("armor00", "armor" + num.ToString("N0").PadLeft(2, '0'));
			tk2dSpriteAnimationFrame.spriteId = tk2dSpriteAnimationFrame.spriteCollection.GetSpriteIdByName(name2, tk2dSpriteAnimationFrame.spriteId);
		}
		this.currentAnimation.Play(this.currentAnimationClip);
		base.gotoAndStop(0);
	}

	public override bool takeHit(int damage, bool isCritical = false, bool ignoreText = false)
	{
		if (!this.hit && this.alive && !this.immune && !this.warping && !Game.Instance.endReached)
		{
			base.takeHit(damage, isCritical, false);
			this.hit = true;
			this.health -= damage;
			Main.playerStats.currentHealth = this.health;
			if (this.health <= 0)
			{
				this.hit = false;
				this.alpha = 0f;
				this.alive = false;
				Main.playerStats.currentHealth = (this.health = 0);
				this.inControl = false;
				int headNum = 23;
				if (this.type > 1)
				{
					headNum = 30 + (this.type - 1);
				}
				Game.Instance.fxManager.emitHead(new Vector2(this.x, this.y), this.scaleX, headNum);
				Game.Instance.fxManager.emitParticles(new Vector2(this.x, this.y), this.scaleX, FXParticleTypes.MEAT, 10);
				AudioManager.Instance.PlaySound("blood_splat");
				AudioManager.Instance.PlaySound("player_die");
				Game.Instance.camView.screenShake(10f, 0.3f);
				this.currentSprite.gameObject.SetActive(false);
				this.weapon.currentSprite.gameObject.SetActive(false);
			}
			else
			{
				AudioManager.Instance.PlaySound("player_hurt");
			}
		}
		return this.health <= 0;
	}

	public void hitPlayerSound(GameObject go, string soundOnHitPlayer = "", string soundOnHitPlayerExtra = "")
	{
		if (soundOnHitPlayer == "hit")
		{
			if (Main.playerStats.equippedArmor == 7)
			{
				soundOnHitPlayer = "bone_crunch_hit";
			}
			else if (Main.playerStats.equippedArmor >= 3)
			{
				soundOnHitPlayer = "hit";
			}
			else
			{
				soundOnHitPlayer = "blunt_hit";
			}
			AudioManager.Instance.PlaySound("sword_stab", go);
		}
		if (soundOnHitPlayer != string.Empty)
		{
			AudioManager.Instance.PlaySound(soundOnHitPlayer, go);
		}
		if (soundOnHitPlayerExtra != string.Empty)
		{
			AudioManager.Instance.PlaySound(soundOnHitPlayerExtra, go);
		}
	}

	public void warp()
	{
		if (this.warping)
		{
			return;
		}
		AudioManager.Instance.PlaySound("portal", base.gameObject);
		this.warping = true;
		this.inControl = false;
	}

	private void handleWarp(float dt)
	{
		if (!this.warpedIn)
		{
			this.warpIn(dt);
		}
		else
		{
			this.warpOut(dt);
		}
	}

	private void handleDeath(float dt)
	{
		this.warpTimer += 1f * dt;
		if (this.warpTimer >= 30f)
		{
			if (this.death_screen_shown)
			{
				return;
			}
			this.death_screen_shown = true;
			base.Invoke("showDeathMenu", 0.2f);
		}
	}

	private void showDeathMenu()
	{
		WindowManager.Instance.ShowMenu("death_menu");
	}

	private void warpIn(float dt)
	{
		this.warpTimer += 1f * dt;
		if (this.warpTimer < 20f)
		{
			this.rotation += 36f * dt;
			this.scaleX += 0.05f * dt;
			this.scaleY += 0.05f * dt;
		}
		else
		{
			this.scaleX = 1f;
			this.scaleY = 1f;
			this.inControl = true;
			this.warpedIn = true;
			this.warping = false;
			this.warpTimer = 0f;
			this.rotation = 0f;
			AchievementHandler.Instance.Increment(ACHIEVEMENT.BATTLE_TESTED, 1);
		}
	}

	private void warpOut(float dt)
	{
		this.warpTimer += 1f * dt;
		if (this.warpTimer < 20f)
		{
			this.rotation += 40f * dt;
			if (this.scaleX > 0f)
			{
				this.scaleX -= 0.05f * dt;
				this.scaleY -= 0.05f * dt;
			}
			else if (this.scaleX < 0f)
			{
				this.scaleX += 0.05f * dt;
				this.scaleY += 0.05f * dt;
			}
		}
		else
		{
			this.alpha = 0f;
			this.scaleX = 0f;
			this.scaleY = 0f;
			this.warping = false;
			Main.playerStats.levelsCleared++;
			Game.Instance.endReached = true;
            StartCoroutine(ShowAds());
		}
	}

    IEnumerator ShowAds()
    {
        yield return new WaitForSeconds(1.0f);
        
        
            PlayerPrefs.SetInt("ShowFirst", 1);
        

    }

    private void handleWeapon()
	{
		this.weapon.updatePosition(this.x, this.y, this.scaleX, this.frame, this.idleFrame);
	}

	public override void handleAnimation(float dt)
	{
		this.nextUpdate += dt;
		if (this.nextUpdate < 0.75f)
		{
			return;
		}
		this.nextUpdate = 0f;
		this.weapon.handleAnimation(dt);
		if (this.xVel == 0f && !this.airborne && !this.swinging)
		{
			this.frame++;
			if (this.frame > this.idleFrame + 11)
			{
				this.frame = this.idleFrame;
			}
		}
		if (this.airborne && !this.swinging)
		{
			if (this.yVel >= 0f)
			{
				this.frame = this.jumpFrame;
			}
			else
			{
				this.frame = this.fallFrame;
			}
		}
		if (!this.airborne && this.xVel != 0f && !this.swinging)
		{
			if (this.frame < this.runFrame)
			{
				this.frame = this.runFrame - 1;
			}
			this.frame++;
			if (this.frame > this.runFrame + 11)
			{
				this.frame = this.runFrame;
			}
		}
		if (this.swinging)
		{
			if (this.frame < this.attackFrame)
			{
				this.frame = this.attackFrame - 1;
			}
			this.frame++;
			if (this.frame > this.attackFrame + 5)
			{
				this.frame = this.idleFrame;
				this.swinging = false;
			}
		}
		if (this.hit)
		{
			this.hitTimer += 1f;
			if (this.hitTimer % 2f != 0f)
			{
				if (this.hitState == 1)
				{
					tk2dBaseSprite arg_20F_0 = this.weapon.currentSprite;
					Color color = new Color(1f, 1f, 1f, 1f);
					this.currentSprite.color = color;
					arg_20F_0.color = color;
					this.hitState = 0;
				}
				else
				{
					tk2dBaseSprite arg_252_0 = this.weapon.currentSprite;
					Color color = new Color(1f, 0f, 0f, 1f);
					this.currentSprite.color = color;
					arg_252_0.color = color;
					this.hitState = 1;
				}
			}
			if (this.hitTimer >= 28f)
			{
				this.hitTimer = 0f;
				this.hit = false;
				this.hitState = 0;
			}
		}
		base.gotoAndStop(this.frame);
	}

	public override void handleMovement(float dt)
	{
		if (this.leftPressed)
		{
			if (!this.cStillPressed)
			{
				this.scaleX = -1f;
			}
			this.xVel = (float)(-(float)this.maxVel);
			if (this.swinging && !this.airborne)
			{
				this.xVel = (float)(-(float)this.swingVel);
			}
		}
		if (this.rightPressed)
		{
			if (!this.cStillPressed)
			{
				this.scaleX = 1f;
			}
			this.xVel = (float)this.maxVel;
			if (this.swinging && !this.airborne)
			{
				this.xVel = (float)this.swingVel;
			}
		}
		if (!this.leftPressed && !this.rightPressed)
		{
			this.xVel = 0f;
		}
		if (this.jumpPressed && !this.xStillPressed && !this.airborne && this.inControl)
		{
			this.jumpHeldTimer = this.jumpHeldTimerMax;
			this.jumpReleased = false;
			this.yVel = (float)this.jumpPower;
			this.xStillPressed = true;
			this.airborne = true;
		}
		else if (this.jumpPressed)
		{
			this.xStillPressed = true;
		}
		if (this.xStillPressed && this.jumpAccelerationTimer < (float)this.jumpAccelerationTimerMax && !this.jumpReleased && this.airborne)
		{
			this.jumpAccelerationTimer += 1f * dt;
			if (this.jumpAccelerationTimer <= 4f)
			{
				this.yVel += 1f * dt;
			}
			else
			{
				this.yVel += 1f * dt;
			}
		}
		if (!this.jumpPressed)
		{
			this.jumpHeldTimer = this.jumpHeldTimerMax;
			this.xStillPressed = false;
			this.jumpReleased = true;
			this.jumpAccelerationTimer = 0f;
			if (this.yVel > 7f)
			{
				this.yVel = 7f;
			}
		}
		if (this.attackPressed && this.canSwing)
		{
			this.cStillPressed = true;
			this.swinging = true;
			this.weapon.swing();
			this.canSwing = false;
		}
		if (!this.attackPressed)
		{
			this.cStillPressed = false;
		}
		if (!this.canSwing)
		{
			this.swingDelay += 1f * dt;
			if (this.swingDelay >= this.swingDelayMax)
			{
				this.swingDelay = 0f;
				this.canSwing = true;
			}
		}
		base.handleMovement(dt);
		if (this.xStillPressed && !this.airborne && (Game.tileInfo.tileData[this.downCenter] == 7 || Game.tileInfo.tileData[this.downLeft] == 7 || Game.tileInfo.tileData[this.downRight] == 7))
		{
			this.jumpHeldTimer -= 1f * dt;
			if (this.jumpHeldTimer <= 0f)
			{
				this.jumpHeldTimer = this.jumpHeldTimerMax;
				this.y -= 4f;
			}
		}
	}

	public override void removeSelf()
	{
	}

	public override void testHorizontalCollision(float dt)
	{
		this.xVelSrc = this.xVel;
		this.getCorners(this.x + this.xVel, this.y);
		if (this.xVel <= 0f)
		{
			if (Game.tileInfo.tileData[this.upLeft] == 2 && this.airborne)
			{
				this.tileX = this.left * Game.Instance.map.tileW + Game.Instance.map.tileW;
				this.x = (float)(this.tileX + this.realW / 2);
				this.xVel = 0f;
			}
			else if ((Game.tileInfo.tileData[this.downCenter] == 5 || Game.tileInfo.tileData[this.downCenter] == 6) && !this.airborne)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				this.yVel = 0f;
				if (Game.tileInfo.tileData[this.downCenter] == 5)
				{
					this.y = (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f;
				}
				else
				{
					this.y = (float)(this.tileY + this.realH / 2) + 32f - ((this.x + this.xVel - (float)this.tileX) / 2f + 16f);
				}
				this.airborne = false;
			}
			else if (Game.tileInfo.tileData[this.downLeft] == 2 && !this.onSlope)
			{
				this.tileX = this.left * Game.Instance.map.tileW + Game.Instance.map.tileW;
				this.x = (float)(this.tileX + this.realW / 2);
				this.xVel = 0f;
			}
			else
			{
				this.onSlope = false;
			}
		}
		else if (this.xVel > 0f)
		{
			if (Game.tileInfo.tileData[this.upRight] == 2 && this.airborne)
			{
				this.tileX = this.right * Game.Instance.map.tileW;
				this.x = (float)(this.tileX - this.realW / 2);
				this.xVel = 0f;
			}
			if ((Game.tileInfo.tileData[this.downCenter] == 3 || Game.tileInfo.tileData[this.downCenter] == 4) && !this.airborne)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				this.yVel = 0f;
				if (Game.tileInfo.tileData[this.downCenter] == 3)
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)this.tileX - this.x - this.xVel) / 2f;
				}
				else
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)this.tileX - this.x - this.xVel) / 2f + 16f;
				}
				this.airborne = false;
			}
			else if (Game.tileInfo.tileData[this.downLeft] == 4 && !this.airborne)
			{
				this.tileX = this.left * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = false;
				this.yVel = 0f;
				this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)this.tileX - this.x - this.xVel) / 2f + 16f;
				this.airborne = false;
			}
			else if ((Game.tileInfo.tileData[this.downCenter] == 5 || Game.tileInfo.tileData[this.downCenter] == 6) && !this.airborne)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				this.yVel = 0f;
				if (Game.tileInfo.tileData[this.downCenter] == 5)
				{
					this.y = (float)(this.tileY + this.realH / 2) + 28f - (this.x + this.xVel - (float)this.tileX) / 2f;
				}
				else
				{
					this.y = (float)(this.tileY + this.realH / 2) + 28f - ((this.x + this.xVel - (float)this.tileX) / 2f + 16f);
				}
				this.airborne = false;
			}
			else if ((Game.tileInfo.tileData[this.downLow] == 5 || Game.tileInfo.tileData[this.downLow] == 6) && !this.airborne)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.below * Game.Instance.map.tileH;
				this.onSlope = true;
				this.yVel = 0f;
				if (Game.tileInfo.tileData[this.downLow] == 5)
				{
					this.y = (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f;
				}
				else
				{
					this.y = (float)(this.tileY + this.realH / 2) + 32f - ((this.x + this.xVel - (float)this.tileX) / 2f + 16f);
				}
				this.airborne = false;
			}
			else if (Game.tileInfo.tileData[this.downRight] == 2 && !this.onSlope)
			{
				this.tileX = this.right * Game.Instance.map.tileW;
				this.x = (float)(this.tileX - this.realW / 2);
				this.xVel = 0f;
			}
			else
			{
				this.onSlope = false;
			}
		}
		this.x += this.xVel * dt;
		this.x = Mathf.Floor(this.x);
	}

	public override void testVerticalCollision(float dt)
	{
		this.yVelSrc = this.yVel;
		this.getCorners(this.x, this.y + this.yVel);
		if (this.yVel > 0f && (Game.tileInfo.tileData[this.upLeft] == 2 || Game.tileInfo.tileData[this.upRight] == 2))
		{
			this.tileY = (this.top - 1) * Game.Instance.map.tileH + Game.Instance.map.tileH;
			this.y = (float)(this.tileY - this.realH / 2 - this.realH / 4);
			this.yVel = 0f;
		}
		if (this.yVel < 0f)
		{
			if (Game.tileInfo.tileData[this.downCenter] == 3 || Game.tileInfo.tileData[this.downCenter] == 4)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				if (Game.tileInfo.tileData[this.downCenter] == 3)
				{
					if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f)
					{
						this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f;
						this.yVel = 0f;
						this.airborne = false;
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f)
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f + 16f;
					this.yVel = 0f;
					this.airborne = false;
				}
			}
			if (Game.tileInfo.tileData[this.center] == 3 || Game.tileInfo.tileData[this.center] == 4)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.centerY * Game.Instance.map.tileH;
				this.onSlope = true;
				if (Game.tileInfo.tileData[this.center] == 3)
				{
					if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f)
					{
						this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f;
						this.yVel = 0f;
						this.airborne = false;
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f)
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f + 16f;
					this.yVel = 0f;
					this.airborne = false;
				}
			}
			else if (Game.tileInfo.tileData[this.downCenter] == 5 || Game.tileInfo.tileData[this.downCenter] == 6)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				if (Game.tileInfo.tileData[this.downCenter] == 5)
				{
					if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f)
					{
						this.y = (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f;
						this.yVel = 0f;
						this.airborne = false;
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f)
				{
					this.y = (float)(this.tileY + this.realH / 2) + 32f - (this.x + this.xVel - (float)this.tileX) / 2f;
					this.yVel = 0f;
					this.airborne = false;
				}
			}
			else if ((Game.tileInfo.tileData[this.downLeft] == 2 || Game.tileInfo.tileData[this.downRight] == 2) && this.yVel != 0f)
			{
				this.tileY = (this.bottom + 1) * Game.Instance.map.tileH;
				this.y = (float)(this.tileY + this.realH / 2);
				this.yVel = 0f;
				this.airborne = false;
			}
			else if ((Game.tileInfo.tileData[this.downLeft] == 7 || Game.tileInfo.tileData[this.downRight] == 7) && this.yVel != 0f && (int)this.y - this.realH / 2 + 1 >= (this.bottom + 1) * Game.Instance.map.tileH && !this.ignorePlatformCollisions)
			{
				this.tileY = (this.bottom + 1) * Game.Instance.map.tileH;
				this.y = (float)(this.tileY + this.realH / 2);
				this.yVel = 0f;
				this.airborne = false;
			}
		}
		this.y += this.yVel * dt;
		this.y = Mathf.Floor(this.y);
	}

	public void onLeftPress()
	{
		this.leftPressed = true;
	}

	public void onLeftRelease()
	{
		this.leftPressed = false;
	}

	public void onRightPress()
	{
		this.rightPressed = true;
	}

	public void onRightRelease()
	{
		this.rightPressed = false;
	}

	public void onJumpPress()
	{
		this.jumpPressed = true;
	}

	public void onJumpRelease()
	{
		this.jumpPressed = false;
	}

	public void onAttackPress()
	{
		this.attackPressed = true;
	}

	public void onAttackRelease()
	{
		this.attackPressed = false;
	}
}
