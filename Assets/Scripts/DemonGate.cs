using System;

public class DemonGate : MovieClip
{
	public DemonDoor door;

	public DoorPuff puff;

	public bool doorOpening;

	public Rectangle doorRect;

	public bool open;

	public bool inRange;

	public bool textShown;

	public bool warping;

	public bool activatedPortal;

	public override void init()
	{
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.door = base.transform.Find("Door").GetComponent<DemonDoor>();
		this.door.init();
		this.door.SetPosition(this.x, this.y);
		this.puff = base.transform.Find("Puff").GetComponent<DoorPuff>();
		this.puff.init();
		this.puff.SetPosition(this.x, this.y);
		this.puff.deactivate();
		this.doorOpening = false;
		this.open = false;
		this.warping = false;
		this.activatedPortal = false;
		this.doorRect = new Rectangle(this.x - 32f, this.y - 48f, 64, 112);
	}

	public override void update(float dt)
	{
		base.update(dt);
		this.door.update(dt);
		this.puff.update(dt);
	}

	public override void onEnterFrame(float dt)
	{
		this.handleAnimation(dt);
		this.testPlayerRange();
		if (this.doorOpening)
		{
			this.handlePuff();
		}
		if (this.open)
		{
			this.testPlayerCollision();
		}
		else
		{
			this.testOpen();
		}
	}

	public void testPlayerCollision()
	{
		if (this.doorRect.Intersects(Game.Instance.player.collisionRect))
		{
			if (!this.activatedPortal && !Game.Instance.player.warping)
			{
				this.textShown = true;
				Game.Instance.hud.SetTutorialText(Localisation.GetString("Press_B_to_enter_the_portal!"));
				Game.Instance.hud.FadeTutorialTextIn();
			}
			if (Game.Instance.player.warping)
			{
				Game.Instance.hud.FadeTutorialTextOut();
			}
			if (Game.Instance.player.attackPressed)
			{
				this.activatedPortal = true;
				Game.Instance.hud.EnableControls(false);
				Game.Instance.player.cancelAttack();
				if (!this.warping)
				{
					this.warping = true;
					Game.Instance.player.warp();
				}
			}
		}
		else if (this.textShown)
		{
			base.Invoke("FadeOut", 0.3f);
			this.textShown = false;
		}
	}

	public void FadeOut()
	{
		Game.Instance.hud.FadeTutorialTextOut();
	}

	public void handleAnimation(float dt)
	{
		if (this.frame < 1)
		{
			this.frame = 1;
		}
		base.gotoAndStop(this.frame - 1);
	}

	public void handlePuff()
	{
		if (this.open)
		{
			return;
		}
		if (!this.door.isActive)
		{
			this.doorOpening = false;
			this.puff.activate();
			this.open = true;
		}
	}

	public void testOpen()
	{
		if (this.open)
		{
			return;
		}
		if (Game.Instance.keyFound && this.inRange)
		{
			this.door.activate();
			this.doorOpening = true;
		}
		if (Game.Instance.map.dungeonLevel == 0 || Game.Instance.map.dungeonLevel == -1)
		{
			this.door.activate();
			this.doorOpening = true;
		}
	}

	public void testPlayerRange()
	{
		float num;
		if (Game.Instance.player.x <= this.x)
		{
			num = this.x - Game.Instance.player.x;
		}
		else
		{
			num = Game.Instance.player.x - this.x;
		}
		float num2;
		if (Game.Instance.player.y <= this.y)
		{
			num2 = this.y - Game.Instance.player.y;
		}
		else
		{
			num2 = Game.Instance.player.y - this.y;
		}
		if (num < 200f && num2 < 100f)
		{
			this.inRange = true;
		}
		else
		{
			this.inRange = false;
		}
	}

	public void removeSelf()
	{
	}
}
