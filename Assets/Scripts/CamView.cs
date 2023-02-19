using System;
using UnityEngine;

public class CamView : MonoBehaviour
{
	public tk2dCamera cam;

	public float camX;

	public float camY;

	public float camStartX;

	public float camStartY;

	public bool smoothScrolling;

	public float acceleration;

	public float xVel;

	public float yVel;

	public float maxVel;

	private float newPx;

	private float newPy;

	private float oldPx;

	private float oldPy;

	private float pxVel;

	private float pyVel;

	private float maxDistance;

	private bool gotInitialPlayerPosition;

	public Vector2 camCenterOffset;

	public Vector2 offset;

	public float shakeTime;

	public float shakeUpdateTime;

	public Vector2 shakeIntensity;

	private int chaseSpeed;

	public void Awake()
	{
		this.cam = Camera.main.GetComponent<tk2dCamera>();
	}

	public void Start()
	{
		this.smoothScrolling = true;
		this.acceleration = 1f;
		this.maxVel = 6f;
		this.xVel = 0f;
		this.yVel = 0f;
		this.maxDistance = 60f;
		this.chaseSpeed = 5;
		this.offset = new Vector2(0f, 0f);
		this.camCenterOffset = new Vector2(0f, -12f);
	}

	public void update(float dt)
	{
		this.updatePosition(dt);
		this.updateShake(dt);
		Vector3 pos = new Vector3(this.camX + this.offset.x + this.camCenterOffset.x, this.camY + this.offset.y + this.camCenterOffset.y, -10f);
		base.transform.position = this.clampPosition(pos);
	}

	public void screenShake(Vector2 strength, float duration = 0.25f)
	{
		this.shakeTime = duration;
		this.shakeIntensity = strength;
	}

	public void screenShake(float strength = 4f, float duration = 0.25f)
	{
		this.screenShake(new Vector2(strength, strength * 0.5f), duration);
	}

	public void setInitialPosition()
	{
		this.camX = (float)((int)Game.Instance.player.x);
		this.camY = (float)((int)Game.Instance.player.y);
		Vector3 pos = new Vector3(this.camX + this.offset.x + this.camCenterOffset.x, this.camY + this.offset.y + this.camCenterOffset.y, -10f);
		base.transform.position = this.clampPosition(pos);
	}

	public Vector3 clampPosition(Vector3 pos)
	{
		float num = pos.x;
		float num2 = pos.y;
		if (num < (float)(Game.Instance.screenW / 2))
		{
			num = (float)(Game.Instance.screenW / 2);
		}
		if (num2 < (float)(Game.Instance.screenH / 2))
		{
			num2 = (float)(Game.Instance.screenH / 2);
		}
		if (num > (float)(Game.Instance.map.mapW - Game.Instance.screenW / 2))
		{
			num = (float)(Game.Instance.map.mapW - Game.Instance.screenW / 2);
		}
		if (num2 > (float)(Game.Instance.map.mapH - Game.Instance.screenH / 2))
		{
			num2 = (float)(Game.Instance.map.mapH - Game.Instance.screenH / 2);
		}
		return new Vector3(num, num2, pos.z);
	}

	public void setProperStartPosition()
	{
		this.camX = (float)((int)Game.Instance.player.x);
		this.camY = (float)((int)Game.Instance.player.y);
		Vector3 pos = new Vector3(this.camX + this.offset.x + this.camCenterOffset.x, this.camY + this.offset.y + this.camCenterOffset.y, -10f);
		base.transform.position = this.clampPosition(pos);
	}

	public void setStartTilePosition()
	{
		this.camStartX = (float)((int)Mathf.Floor(this.camX / (float)Game.Instance.map.tileW));
		this.camStartY = (float)((int)Mathf.Floor(this.camY / (float)Game.Instance.map.tileH));
	}

	public void updateShake(float dt)
	{
		if (this.shakeTime > 0f)
		{
			this.shakeTime -= dt / 30f;
			if (this.shakeTime <= 0f)
			{
				this.offset = new Vector2(0f, 0f);
			}
			else
			{
				this.shakeUpdateTime -= dt / 30f;
				if (this.shakeUpdateTime < 0f)
				{
					this.shakeUpdateTime = 0f;
					this.offset = new Vector2(UnityEngine.Random.Range(-this.shakeIntensity.x, this.shakeIntensity.x), UnityEngine.Random.Range(-this.shakeIntensity.y, this.shakeIntensity.y));
				}
			}
		}
	}

	public void updatePosition(float dt)
	{
		if (this.smoothScrolling)
		{
			this.chaseTarget(dt);
		}
		else
		{
			this.camX = (float)((int)Game.Instance.player.x);
			this.camY = (float)((int)Game.Instance.player.y);
		}
		if (this.camX < (float)(Game.Instance.screenW / 2))
		{
			this.camX = (float)(Game.Instance.screenW / 2);
			this.xVel = 0f;
		}
		if (this.camY < (float)(Game.Instance.screenH / 2))
		{
			this.camY = (float)(Game.Instance.screenH / 2);
		}
		if (this.camX > (float)(Game.Instance.map.mapW - Game.Instance.screenW / 2))
		{
			this.camX = (float)(Game.Instance.map.mapW - Game.Instance.screenW / 2);
			this.xVel = 0f;
		}
		if (this.camY > (float)(Game.Instance.map.mapH - Game.Instance.screenH / 2))
		{
			this.camY = (float)(Game.Instance.map.mapH - Game.Instance.screenH / 2);
		}
		int num = 8;
		Game.Instance.map.background.transform.position = new Vector2(Mathf.Round((this.camX - (float)(Game.Instance.screenW / 2)) * -1f / (float)num), Mathf.Round((this.camY - (float)(Game.Instance.screenH / 2)) * -1f / (float)num + (float)(Game.Instance.screenH / num)));
	}

	private void chaseTarget(float dt)
	{
		this.newPx = (float)((int)Game.Instance.player.x);
		this.newPy = (float)((int)Game.Instance.player.y);
		this.getPlayerSpeed();
		if (this.camX < this.newPx)
		{
			this.xVel = (float)this.getCameraHorizontalSpeed();
		}
		else if (this.camX > this.newPx)
		{
			this.xVel = (float)(this.getCameraHorizontalSpeed() * -1);
		}
		else
		{
			this.xVel = 0f;
		}
		if (this.camY < this.newPy)
		{
			this.yVel = (float)this.getCameraVerticalSpeed();
		}
		else if (this.camY > this.newPy)
		{
			this.yVel = (float)(this.getCameraVerticalSpeed() * -1);
		}
		else
		{
			this.yVel = 0f;
		}
		this.camX += this.xVel * dt;
		this.camY += this.yVel * dt;
		this.oldPx = this.newPx;
		this.oldPy = this.newPy;
	}

	private void getPlayerSpeed()
	{
		if (this.newPx >= this.oldPx)
		{
			this.pxVel = this.newPx - this.oldPx;
		}
		else
		{
			this.pxVel = this.oldPx - this.newPx;
		}
		if (this.newPy >= this.oldPy)
		{
			this.pyVel = this.newPy - this.oldPy;
		}
		else
		{
			this.pyVel = this.oldPy - this.newPy;
		}
	}

	private int getCameraHorizontalSpeed()
	{
		float num;
		if (this.camX < this.newPx)
		{
			num = this.newPx - this.camX;
		}
		else
		{
			num = this.camX - this.newPx;
		}
		if (num == 0f)
		{
			return 0;
		}
		return (int)Mathf.Round(num / (float)this.chaseSpeed);
	}

	private int getCameraVerticalSpeed()
	{
		float num;
		if (this.camY < this.newPy)
		{
			num = this.newPy - this.camY;
		}
		else
		{
			num = this.camY - this.newPy;
		}
		if (num == 0f)
		{
			return 0;
		}
		return (int)Mathf.Round(num / (float)this.chaseSpeed);
	}

	public void setupCam()
	{
	}
}
