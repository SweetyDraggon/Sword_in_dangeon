using System;

public class TutorialSign : MovieClip
{
	public Rectangle collisionRect;

	public int type;

	public string text;

	public bool textShown;

	public void initWithType(int t)
	{
		this.type = t;
		this.reset();
	}

	public void reset()
	{
		if (this.type == 1)
		{
			this.text = "Press_the_A_button_to_jump";
		}
		if (this.type == 2)
		{
			this.text = "Hold_A_button_to_jump_higher";
		}
		if (this.type == 3)
		{
			this.text = "HOLD_the_B_button_to_swing_your_weapon";
		}
		if (this.type == 4)
		{
			this.text = "Press_and_hold_A_button_to_drop_through_platforms";
		}
		if (this.type == 5)
		{
			this.text = "Keep_an_eye_out_for_hidden_areas";
		}
		if (this.type == 6)
		{
			this.text = "If_you_get_lost_open_your_map_screen";
		}
		if (this.type == 7)
		{
			this.text = "Slay_enemies_to_gain_experience";
		}
		if (this.type == 8)
		{
			this.text = "Find_the_key_in_each_level_to_open_the_gateway_to_the_next_level";
		}
		this.textShown = false;
		this.alpha = 1f;
		this.scaleX = 1f;
		this.scaleY = 1f;
		this.x = (float)((int)base.transform.position.x);
		this.y = (float)((int)base.transform.position.y);
		this.collisionRect = new Rectangle(this.x, this.y, 64, 64);
	}

	public override void onEnterFrame(float dt)
	{
		if (!Game.Instance.paused)
		{
			this.testPlayerCollision();
			base.applyTransform();
		}
	}

	public void testPlayerCollision()
	{
		if (this.collisionRect.Intersects(Game.Instance.player.collisionRect))
		{
			this.textShown = true;
			Game.Instance.hud.SetTutorialText(Localisation.GetString( text));
			Game.Instance.hud.FadeTutorialTextIn();
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

	public void removeSelf()
	{
	}
}
