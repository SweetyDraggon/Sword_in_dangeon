using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Entity : MovieClip
{
	public delegate void OnCollideEvent(Entity entity = null);

	public bool initialized;

	public Map map;

	protected bool frameSkipped;

	public bool isActive = true;

	public bool ignorePlatformCollisions;

	public int tileX;

	public int tileY;

	protected int top;

	protected int bottom;

	protected int left;

	protected int right;

	protected int centerX;

	protected int centerY;

	protected int below;

	public int upLeft;

	public int upRight;

	public int upCenter;

	public int downLeft;

	public int downRight;

	public int downCenter;

	public int downLow;

	public int center;

	public int farDownLeft;

	public int farDownRight;

	public int leftCenter;

	public int rightCenter;

	public Rectangle collisionRect;

	public bool knockBack;

	public float knockBackTimer;

	public int realW = 32;

	public int realH = 32;

	public int gravity;

	public float xVel;

	public float yVel;

	public float xVelSrc;

	public float yVelSrc;

	public int maxVel;

	public int acceleration;

	public int jumpPower;

	public int state;

	public bool airborne;

	public bool alive;

	public bool onSlope;

	public bool hit;

	public bool immune;

	public float immuneTimer;

	public int immuneTimerMax;

	public int health;

	public int maxHealth;

	public int type;

	public float hitTimer;

	public int hitMax;

	public int hitState;





















	public event Entity.OnCollideEvent onCollideDown;

	public event Entity.OnCollideEvent onCollideUp;

	public event Entity.OnCollideEvent onCollideLeft;

	public event Entity.OnCollideEvent onCollideRight;

	public event Entity.OnCollideEvent onWallCollideLeft;

	public event Entity.OnCollideEvent onWallCollideRight;

	public event Entity.OnCollideEvent onLedgeCollideLeft;

	public event Entity.OnCollideEvent onLedgeCollideRight;

	public event Entity.OnCollideEvent onSlopeCollide;

	public event Entity.OnCollideEvent onPlatformCollideDown;

	private void Start()
	{
		if (!this.initialized)
		{
			this.baseInit();
		}
	}

	private void Awake()
	{
		this.maxVel = 7;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.map = Game.Instance.map;
		this.currentSprite = base.GetComponent<tk2dSprite>();
		if (base.transform.Find("Sprite"))
		{
			this.currentSprite = base.transform.Find("Sprite").GetComponent<tk2dSprite>();
		}
		this.currentAnimation = this.currentSprite.GetComponent<tk2dSpriteAnimator>();
	}

	private void Update()
	{
	}

	public override void update(float dt)
	{
		this.onEnterFrame(dt);
		this.updateRect();
		base.applyTransform();
	}

	public void baseInit()
	{
		this.initialized = true;
		this.maxVel = 7;
		this.acceleration = 2;
		this.gravity = 1;
		this.jumpPower = 12;
		this.state = 1;
		this.airborne = true;
		this.alive = true;
		this.alpha = 1f;
		this.onSlope = false;
		this.hit = false;
		this.hitTimer = 0f;
		this.hitState = 0;
		this.immune = false;
		this.collisionRect = new Rectangle((int)this.x - this.realW / 2, (int)this.y - this.realH / 2, this.realW, this.realH);
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
		this.reset();
		this.rebuildAnimationClip();
	}

	public override void init()
	{
		if (!this.initialized)
		{
			this.baseInit();
		}
		this.reset();
	}

	public virtual void reset()
	{
	}

	public new virtual void onEnterFrame(float dt)
	{
	}

	public new void SetPosition(float nX, float nY)
	{
		Vector3 position = base.transform.position;
		position.x = nX;
		position.y = nY;
		base.transform.position = position;
		this.x = Mathf.Floor(base.transform.position.x);
		this.y = Mathf.Floor(base.transform.position.y);
	}

	public new void SetPosition(int nX, int nY)
	{
		this.SetPosition((float)nX, (float)nY);
	}

	public new virtual void rebuildAnimationClip()
	{
		this.currentAnimationClip = this.currentAnimation.GetClipByName(this.currentAnimationName);
		this.currentAnimation.Play(this.currentAnimationClip);
		this.currentAnimation.Stop();
		this.currentAnimation.SetFrame(this.frame);
	}

	public new void gotoAndStop(int frame)
	{
		this.currentAnimation.SetFrame(frame);
		this.currentAnimation.Stop();
	}

	public virtual void handleAnimation(float dt)
	{
	}

	public virtual void handleMovement(float dt)
	{
		this.yVel += (float)(-(float)this.gravity) * dt;
		if (this.yVel < -14f)
		{
			this.yVel = -14f;
		}
		if (this.yVel < -2f)
		{
			this.airborne = true;
		}
		this.testTileCollision(dt);
	}

	public virtual void updateRect()
	{
		if (this.collisionRect != null)
		{
			this.collisionRect.x = (int)this.x - this.realW / 2;
			this.collisionRect.y = (int)this.y - this.realH / 2;
			this.collisionRect.width = this.realW;
			this.collisionRect.height = this.realH;
		}
	}

	public virtual void testTileCollision(float dt)
	{
		this.testHorizontalCollision(dt);
		this.testVerticalCollision(dt);
	}

	public virtual void testHorizontalCollision(float dt)
	{
		this.xVelSrc = this.xVel;
		this.getCorners(this.x + this.xVel, this.y);
		if (this.xVel < 0f)
		{
			if (Game.tileInfo.tileData[this.upLeft] == 2 && this.airborne)
			{
				this.tileX = this.left * Game.Instance.map.tileW + Game.Instance.map.tileW;
				this.x = (float)(this.tileX + this.realW / 2);
				this.xVel = 0f;
				if (this.onCollideLeft != null)
				{
					this.onCollideLeft(this);
				}
			}
			else if (Game.tileInfo.tileData[this.downLeft] == 2 && !this.onSlope)
			{
				this.tileX = this.left * Game.Instance.map.tileW + Game.Instance.map.tileW;
				this.x = (float)(this.tileX + this.realW / 2);
				this.xVel = 0f;
				if (this.onCollideLeft != null)
				{
					this.onCollideLeft(this);
				}
				if (this.onWallCollideLeft != null)
				{
					this.onWallCollideLeft(this);
				}
			}
			else
			{
				this.onSlope = false;
			}
		}
		if (this.xVel > 0f)
		{
			if (Game.tileInfo.tileData[this.upRight] == 2 && this.airborne)
			{
				this.tileX = this.right * Game.Instance.map.tileW;
				this.x = (float)(this.tileX - this.realW / 2);
				this.xVel = 0f;
				if (this.onCollideRight != null)
				{
					this.onCollideRight(this);
				}
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
				if (this.onCollideRight != null)
				{
					this.onCollideRight(this);
				}
			}
			else if (Game.tileInfo.tileData[this.downLeft] == 4 && !this.airborne)
			{
				this.tileX = this.left * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = false;
				this.yVel = 0f;
				this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)this.tileX - this.x - this.xVel) / 2f + 16f;
				this.airborne = false;
				if (this.onCollideRight != null)
				{
					this.onCollideRight(this);
				}
			}
			else if ((Game.tileInfo.tileData[this.downCenter] == 5 || Game.tileInfo.tileData[this.downCenter] == 6) && !this.airborne)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				this.yVel = 0f;
				if (Game.tileInfo.tileData[this.downCenter] == 5)
				{
					this.y = (float)(this.tileY + this.realH / 2 + Game.Instance.map.tileH) + ((float)((int)this.x) + this.xVel - (float)this.tileX) / 2f * -1f;
				}
				else
				{
					this.y = (float)(this.tileY + this.realH / 2 + Game.Instance.map.tileH) + (((float)((int)this.x) + this.xVel - (float)this.tileX) / 2f + 16f) * -1f;
				}
				this.airborne = false;
				if (this.onCollideRight != null)
				{
					this.onCollideRight(this);
				}
			}
			else if (Game.tileInfo.tileData[this.downRight] == 2 && !this.onSlope)
			{
				this.tileX = this.right * Game.Instance.map.tileW;
				this.x = (float)(this.tileX - this.realW / 2);
				this.xVel = 0f;
				if (this.onCollideRight != null)
				{
					this.onCollideRight(this);
				}
				if (this.onWallCollideRight != null)
				{
					this.onWallCollideRight(this);
				}
			}
			else
			{
				this.onSlope = false;
			}
		}
		this.x += this.xVel * dt;
		this.x = Mathf.Floor(this.x);
	}

	public virtual void testVerticalCollision(float dt)
	{
		this.yVelSrc = this.yVel;
		this.getCorners(this.x, this.y + this.yVel);
		if (this.yVel > 0f && (Game.tileInfo.tileData[this.upLeft] == 2 || Game.tileInfo.tileData[this.upRight] == 2))
		{
			this.tileY = (this.top - 1) * Game.Instance.map.tileH + Game.Instance.map.tileH;
			this.y = (float)(this.tileY - this.realH / 2 - this.realH / 4);
			this.yVel = 0f;
			if (this.onCollideUp != null)
			{
				this.onCollideUp(this);
			}
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
						if (this.onCollideDown != null)
						{
							this.onCollideDown(this);
						}
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f)
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f + 16f;
					this.yVel = 0f;
					this.airborne = false;
					if (this.onCollideDown != null)
					{
						this.onCollideDown(this);
					}
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
						if (this.onCollideDown != null)
						{
							this.onCollideDown(this);
						}
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f)
				{
					this.y = (float)(this.tileY + Game.Instance.map.tileH - this.realH / 2) - ((float)(this.tileX - (int)this.x) - this.xVel) / 2f + 16f;
					this.yVel = 0f;
					this.airborne = false;
					if (this.onCollideDown != null)
					{
						this.onCollideDown(this);
					}
				}
			}
			else if (Game.tileInfo.tileData[this.downCenter] == 5 || Game.tileInfo.tileData[this.downCenter] == 6)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.bottom * Game.Instance.map.tileH;
				this.onSlope = true;
				if (Game.tileInfo.tileData[this.downCenter] == 5)
				{
					if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 32f) * -1f)
					{
						this.y = (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 32f) * -1f;
						this.yVel = 0f;
						this.airborne = false;
						if (this.onCollideDown != null)
						{
							this.onCollideDown(this);
						}
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f) * -1f)
				{
					this.y = (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f) * -1f;
					this.yVel = 0f;
					this.airborne = false;
					if (this.onCollideDown != null)
					{
						this.onCollideDown(this);
					}
				}
			}
			else if (Game.tileInfo.tileData[this.center] == 5 || Game.tileInfo.tileData[this.center] == 6)
			{
				this.tileX = this.centerX * Game.Instance.map.tileW;
				this.tileY = this.centerY * Game.Instance.map.tileH;
				this.onSlope = true;
				if (Game.tileInfo.tileData[this.center] == 5)
				{
					if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 32f) * -1f)
					{
						this.y = (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 32f) * -1f;
						this.yVel = 0f;
						this.airborne = false;
						if (this.onCollideDown != null)
						{
							this.onCollideDown(this);
						}
					}
				}
				else if ((float)((int)this.y) + this.yVel <= (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f) * -1f)
				{
					this.y = (float)(this.tileY + this.realH / 2) - (((float)(this.tileX - (int)this.x) + this.xVel) / 2f + 16f) * -1f;
					this.yVel = 0f;
					this.airborne = false;
					if (this.onCollideDown != null)
					{
						this.onCollideDown(this);
					}
				}
			}
			else if ((Game.tileInfo.tileData[this.downLeft] == 2 || Game.tileInfo.tileData[this.downRight] == 2) && this.yVel != 0f)
			{
				this.tileY = (this.bottom + 1) * Game.Instance.map.tileH;
				this.y = (float)(this.tileY + this.realH / 2);
				this.yVel = 0f;
				this.airborne = false;
				if (this.onCollideDown != null)
				{
					this.onCollideDown(this);
				}
			}
			else if ((Game.tileInfo.tileData[this.downLeft] == 7 || Game.tileInfo.tileData[this.downRight] == 7) && this.yVel != 0f && (int)this.y - this.realH / 2 + 1 >= (this.bottom + 1) * Game.Instance.map.tileH && !this.ignorePlatformCollisions)
			{
				this.tileY = (this.bottom + 1) * Game.Instance.map.tileH;
				this.y = (float)(this.tileY + this.realH / 2);
				this.yVel = 0f;
				this.airborne = false;
				if (this.onPlatformCollideDown != null)
				{
					this.onPlatformCollideDown(this);
				}
			}
		}
		if (this.onSlope && this.onSlopeCollide != null)
		{
			this.onSlopeCollide(this);
		}
		if (this.xVelSrc < 0f)
		{
			if (Game.tileInfo.tileData[this.downLeft] == 1 && (Game.tileInfo.tileData[this.downCenter] == 2 || Game.tileInfo.tileData[this.downCenter] == 7) && this.onLedgeCollideLeft != null)
			{
				this.onLedgeCollideLeft(this);
			}
		}
		else if (this.xVelSrc > 0f && Game.tileInfo.tileData[this.downRight] == 1 && (Game.tileInfo.tileData[this.downCenter] == 2 || Game.tileInfo.tileData[this.downCenter] == 7) && this.onLedgeCollideRight != null)
		{
			this.onLedgeCollideRight(this);
		}
		this.y += this.yVel * dt;
		this.y = Mathf.Floor(this.y);
	}

	public virtual void getCorners(float xPos, float yPos)
	{
		this.left = (int)Mathf.Floor((xPos - (float)(this.realW / 2)) / (float)Game.Instance.map.tileW);
		this.right = (int)Mathf.Floor((xPos + (float)(this.realW / 2) - 1f) / (float)Game.Instance.map.tileW);
		this.centerX = (int)Mathf.Floor(xPos / (float)Game.Instance.map.tileW);
		this.top = (int)Mathf.Floor((yPos + (float)(this.realH / 2)) / (float)Game.Instance.map.tileH);
		this.bottom = (int)Mathf.Floor((yPos - (float)(this.realH / 2)) / (float)Game.Instance.map.tileH);
		this.below = (int)Mathf.Floor((yPos - (float)(this.realH / 2) + 10f) / (float)Game.Instance.map.tileH);
		this.centerY = (int)Mathf.Floor(yPos / (float)Game.Instance.map.tileH);
		if (this.top < 0)
		{
			this.top = 0;
		}
		if (this.left < 0)
		{
			this.left = 0;
		}
		if (this.upLeft < 0)
		{
			this.upLeft = 0;
		}
		if (this.downLeft < 0)
		{
			this.downLeft = 0;
		}
		this.upLeft = this.map.GetTileID(this.left, this.top, 0);
		this.upRight = this.map.GetTileID(this.right, this.top, 0);
		this.upCenter = this.map.GetTileID(this.centerX, this.top, 0);
		this.downLeft = this.map.GetTileID(this.left, this.bottom, 0);
		this.downRight = this.map.GetTileID(this.right, this.bottom, 0);
		this.downCenter = this.map.GetTileID(this.centerX, this.bottom, 0);
		this.downLow = this.map.GetTileID(this.centerX, this.below, 0);
		this.center = this.map.GetTileID(this.centerX, this.centerY, 0);
		this.farDownLeft = this.map.GetTileID(this.left - 1, this.bottom, 0);
		this.farDownRight = this.map.GetTileID(this.right + 1, this.bottom, 0);
		this.leftCenter = this.map.GetTileID(this.left, this.centerY, 0);
		this.rightCenter = this.map.GetTileID(this.right, this.centerY, 0);
	}

	public virtual void handleKnockback(float dt)
	{
		if (this.knockBack)
		{
			if (this.scaleX == 1f)
			{
				this.xVel = -2f;
			}
			else
			{
				this.xVel = 2f;
			}
			this.knockBackTimer += 1f * dt;
			if (this.knockBackTimer >= 2f)
			{
				this.knockBackTimer = 0f;
				this.knockBack = false;
			}
		}
	}

	public virtual bool takeHit(int damage, bool isCritical = false, bool ignoreText = false)
	{
		return this.health <= 0;
	}

	public virtual void testSpikeCollision()
	{
		if (!this.hit && !Game.Instance.spikesDisabled && this.map.GetTileID(this.centerX, this.centerY, 1) == 301)
		{
			this.takeHit(1, false, false);
		}
	}

	public virtual void removeSelf()
	{
	}

	public virtual void handleHitAnimation(float dt)
	{
	}
}
