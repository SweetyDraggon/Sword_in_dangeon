using System;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{
	public tk2dSpriteAnimationClip currentAnimationClip;

	public tk2dSpriteAnimator currentAnimation;

	public tk2dSprite currentSprite;

	public float x;

	public float y;

	public int rotation;

	public float alpha;

	public float scaleX;

	public float scaleY;

	public int type;

	public bool swinging;

	public int swingState;

	public bool swordInFront;

	public Rectangle weaponRect;

	public bool deadly;

	public int damage;

	public float swingResetTimer;

	private int[] weaponDamage;

	public bool isBluntWeapon;

	public int frame;

	public int idleFrame;

	private int swingOneFrame;

	private int swingTwoFrame;

	private int swingThreeFrame;

	private void Awake()
	{
		this.currentSprite = base.GetComponent<tk2dSprite>();
		this.currentAnimation = this.currentSprite.GetComponent<tk2dSpriteAnimator>();
	}

	private void Start()
	{
		this.x = base.transform.position.x;
		this.y = base.transform.position.y;
		this.scaleX = (this.scaleY = 1f);
		this.type = Main.playerStats.equippedWeapon;
		this.swinging = false;
		this.swingState = 1;
		this.swordInFront = false;
		this.deadly = false;
		this.updateDamage();
		this.swingResetTimer = 0f;
		this.sortFrames();
		this.frame = this.idleFrame;
		this.gotoAndStop(this.frame);
		this.weaponRect = new Rectangle(this.x, this.y - 32f, 64, 64);
	}

	public void updateDamage()
	{
		this.damage = Convert.ToInt32(Main.itemStats.weaponStats[this.type - 1, 5]) + Main.playerStats.strength;
	}

	public void updateWeapon()
	{
		this.type = Main.playerStats.equippedWeapon;
		this.isBluntWeapon = (this.type % 2 == 0);
		this.sortFrames();
		this.frame = this.idleFrame;
		this.gotoAndStop(this.frame);
		this.updateDamage();
	}

	public void gotoAndStop(int frame)
	{
		this.currentAnimation.SetFrame(frame);
	}

	public void sortFrames()
	{
		this.idleFrame = 1;
		this.swingOneFrame = this.idleFrame + 1;
		this.swingTwoFrame = this.idleFrame + 7;
		this.swingThreeFrame = this.idleFrame + 13;
		this.rebuildAnimationClip();
	}

	private void rebuildAnimationClip()
	{
		tk2dSpriteAnimationClip clipByName = this.currentAnimation.GetClipByName("player_weapon");
		this.currentAnimationClip = new tk2dSpriteAnimationClip();
		this.currentAnimationClip.CopyFrom(clipByName);
		string[] array = new string[]
		{
			"sword1",
			"mace1",
			"sword2",
			"mace2",
			"sword3",
			"mace3",
			"sword4",
			"mace4",
			"sword5",
			"mace5",
			"sword6",
			"mace6",
			"sword7",
			"mace7",
			"sword8",
			"mace8",
			"sword9",
			"mace9",
			"sword10",
			"mace10"
		};
		tk2dSpriteAnimationFrame[] frames = this.currentAnimationClip.frames;
		for (int i = 0; i < frames.Length; i++)
		{
			tk2dSpriteAnimationFrame tk2dSpriteAnimationFrame = frames[i];
			int num = Mathf.Min(Mathf.Max(this.type - 1, 0), 20);
			string name = tk2dSpriteAnimationFrame.spriteCollection.spriteDefinitions[tk2dSpriteAnimationFrame.spriteId].name;
			string name2 = name.Replace("mace1", array[num]);
			tk2dSpriteAnimationFrame.spriteId = tk2dSpriteAnimationFrame.spriteCollection.GetSpriteIdByName(name2, tk2dSpriteAnimationFrame.spriteId);
		}
		this.currentAnimation.Play(this.currentAnimationClip);
		this.currentAnimation.Stop();
		this.currentAnimation.SetFrame(0);
	}

	public void handleAnimation(float dt)
	{
		if (this.swinging)
		{
			if (this.swingState == 1)
			{
				this.frame++;
				if (this.frame == this.idleFrame + 3)
				{
					this.deadly = true;
				}
				else
				{
					this.deadly = false;
				}
				if (this.frame > this.swingTwoFrame - 1)
				{
					this.frame = this.idleFrame;
					this.swinging = false;
					this.swingState = 2;
				}
			}
			else if (this.swingState == 2)
			{
				if (this.frame < this.swingTwoFrame)
				{
					this.frame = this.swingTwoFrame - 1;
				}
				this.frame++;
				if (this.frame == this.swingTwoFrame + 2)
				{
					this.deadly = true;
				}
				else
				{
					this.deadly = false;
				}
				if (this.frame > this.swingThreeFrame - 1)
				{
					this.deadly = false;
					this.frame = this.idleFrame;
					this.swinging = false;
					this.swingState = 3;
				}
			}
			else if (this.swingState == 3)
			{
				if (this.frame < this.swingThreeFrame)
				{
					this.frame = this.swingThreeFrame - 1;
				}
				this.frame++;
				if (this.frame == this.swingThreeFrame + 2)
				{
					this.deadly = true;
				}
				else
				{
					this.deadly = false;
				}
				if (this.frame > this.swingThreeFrame + 5)
				{
					this.deadly = false;
					this.frame = this.idleFrame;
					this.swinging = false;
					this.swingState = 1;
				}
			}
			if (this.deadly)
			{
				AudioManager.Instance.PlaySound("whoosh", base.gameObject);
			}
		}
		else
		{
			this.frame = this.idleFrame;
		}
		this.gotoAndStop(this.frame);
		if (this.frame >= this.idleFrame + 3 && this.frame <= this.swingThreeFrame - 1)
		{
			this.swordInFront = true;
		}
		else
		{
			this.swordInFront = false;
		}
		if (this.deadly)
		{
			this.testEnemyCollision();
		}
		if (this.swingState != 1 && !this.swinging)
		{
			this.swingResetTimer += 1f * dt;
			if (this.swingResetTimer >= 20f)
			{
				this.swingState = 1;
			}
		}
		Vector3 position = base.transform.position;
		position.x = Mathf.Floor(this.x);
		position.y = Mathf.Floor(this.y);
		base.transform.position = position;
		if (this.swordInFront)
		{
			this.currentSprite.SortingOrder = 1;
		}
		else
		{
			this.currentSprite.SortingOrder = -1;
		}
	}

	private void testEnemyCollision()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		bool flag7 = false;
		foreach (GameObject current in Game.Instance.map.breakables)
		{
			Entity component = current.GetComponent<Entity>();
			BreakableObject component2 = current.GetComponent<BreakableObject>();
			if (component.alive && this.weaponRect.Intersects(component.collisionRect) && !component.hit)
			{
				flag6 = component.takeHit(this.damage, false, false);
				flag3 = true;
				if (component2 != null && component2.metal)
				{
					flag5 = true;
				}
				float num = component.x;
				float num2 = this.y;
				Game.Instance.fxManager.emitFlash(new Vector2(num, num2), FXParticleTypes.FLASH_HIT);
			}
		}
		foreach (GameObject current2 in Game.Instance.fxManager.projectilePool)
		{
			Projectile component3 = current2.GetComponent<Projectile>();
			if (component3.alive && this.weaponRect.Intersects(component3.collisionRect) && !component3.hit && component3.destructible)
			{
				flag4 = true;
				flag6 = true;
				component3.die();
				AchievementHandler.Instance.SetValue(ACHIEVEMENT.SICK_REFLEXES, 1);
				float num3 = component3.x;
				float num4 = this.y;
				Game.Instance.fxManager.emitFlash(new Vector2(num3, num4), FXParticleTypes.FLASH_HIT);
			}
		}
		foreach (GameObject current3 in Game.Instance.map.enemies)
		{
			Enemy component4 = current3.GetComponent<Enemy>();
			if (component4.alive && this.weaponRect.Intersects(component4.collisionRect) && !component4.hit)
			{
				int num5 = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 1f) * 100f) + 1;
				bool flag8 = num5 <= Main.playerStats.crit;
				flag6 = component4.takeHit(this.damage, flag8, false);
				if (!flag7)
				{
					flag7 = component4.enemyFlesh;
				}
				flag2 = true;
				if (flag8)
				{
					flag = true;
				}
				float num6 = (this.x >= component4.x) ? (component4.x + 10f) : (component4.x - 10f);
				float num7 = this.y;
				Game.Instance.fxManager.emitFlash(new Vector2(num6, num7), FXParticleTypes.FLASH_HIT);
			}
		}
		if (flag3 || flag4 || flag2)
		{
			if (flag2)
			{
				if (!this.isBluntWeapon)
				{
					if (flag)
					{
						AudioManager.Instance.PlaySound("critical_hit", base.gameObject);
					}
					else if (flag7)
					{
						if (UnityEngine.Random.Range(0, 100) >= 10)
						{
							AudioManager.Instance.PlaySound("new_sword_hit", base.gameObject);
						}
						if (UnityEngine.Random.Range(0, 100) >= 75)
						{
							AudioManager.Instance.PlaySound("sword_stab", base.gameObject);
						}
					}
					else
					{
						AudioManager.Instance.PlaySound("bone_crunch_hit", base.gameObject);
					}
					AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
				}
				else if (flag)
				{
					AudioManager.Instance.PlaySound("blunt_critical_hit", base.gameObject);
				}
				else
				{
					AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
				}
			}
			else if (flag3 || flag4)
			{
				if (!this.isBluntWeapon)
				{
					AudioManager.Instance.PlaySound("heavy_hit", base.gameObject);
					AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
					if (flag5)
					{
						AudioManager.Instance.PlaySound("clang", base.gameObject);
					}
				}
				else
				{
					AudioManager.Instance.PlaySound("heavy_hit", base.gameObject);
					AudioManager.Instance.PlaySound("blunt_hit", base.gameObject);
					if (flag5)
					{
						AudioManager.Instance.PlaySound("clang", base.gameObject);
					}
				}
			}
			if (this.isBluntWeapon)
			{
				if (flag)
				{
					Game.Instance.camView.screenShake(13f, 0.4f);
				}
				else if (flag6 && UnityEngine.Random.Range(0, 100) >= 65)
				{
					Game.Instance.camView.screenShake(4f, 0.3f);
				}
				else if (flag4)
				{
					Game.Instance.camView.screenShake(4f, 0.3f);
				}
				else if (UnityEngine.Random.Range(0, 100) >= 85)
				{
					Game.Instance.camView.screenShake(4f, 0.3f);
				}
			}
			else if (flag)
			{
				Game.Instance.camView.screenShake(10f, 0.3f);
			}
			else if (flag6 && UnityEngine.Random.Range(0, 100) >= 75)
			{
				Game.Instance.camView.screenShake(2f, 0.2f);
			}
			else if (flag4)
			{
				Game.Instance.camView.screenShake(2f, 0.2f);
			}
			else if (UnityEngine.Random.Range(0, 100) >= 98)
			{
				Game.Instance.camView.screenShake(2f, 0.2f);
			}
		}
	}

	public void swing()
	{
		this.swinging = true;
		this.swingResetTimer = 0f;
	}

	public void cancelSwing()
	{
		this.swinging = false;
		this.deadly = false;
		this.swingResetTimer = 0f;
		this.frame = this.idleFrame;
		this.gotoAndStop(this.frame);
	}

	private void updateRect()
	{
		if (this.swingState == 3)
		{
			this.weaponRect.width = 64;
			this.weaponRect.height = 64;
		}
		else
		{
			this.weaponRect.width = 64;
			this.weaponRect.height = 40;
		}
		if (this.scaleX == 1f)
		{
			this.weaponRect.x = (int)this.x;
		}
		else
		{
			this.weaponRect.x = (int)this.x - this.weaponRect.width;
		}
		this.weaponRect.y = (int)this.y - this.weaponRect.height / 2;
	}

	public void updatePosition(float X, float Y, float scale, int f, int iFrame)
	{
		this.x = X;
		this.y = Y;
		this.scaleX = scale;
		if (f == iFrame + 6 || f == iFrame + 7 || f == iFrame + 8 || f == iFrame + 9 || f == iFrame + 16 || f == iFrame + 17 || f == iFrame + 22 || f == iFrame + 23)
		{
			this.y += 2f;
		}
		this.updateRect();
	}

	public void removeSelf()
	{
	}

	public void updateEquipmentArt()
	{
		this.type = Main.playerStats.equippedWeapon;
		this.updateWeapon();
		this.rebuildAnimationClip();
	}
}
