using System;
using TMPro;
using UnityEngine;

[Serializable]
public class LevelText
{
	public tk2dTextMesh levelText;
	public TextMeshPro alternativeLevelText;
	private tk2dTextMesh levelTextShadow;

	public bool isActive;

	public int state;

	public float alpha;

	public float idleTimer;

	public void update(float dt)
	{
		if (!Game.Instance.paused && this.isActive)
		{
			this.handleAnimation(dt);
		}
	}

	public void activate(int level)
	{
		this.levelText.gameObject.SetActive(true);
		if (level > 0)
		{
			this.levelText.text = Localisation.GetString("Level") + level.ToString();
			alternativeLevelText.text = this.levelText.text;
		}
		else if (level == 0)
		{
			//this.levelText.text = "King's_Court";
			this.levelText.text = Localisation.GetString("KING`S_COURT");
			alternativeLevelText.text = this.levelText.text;
		}
		else if (level == -1)
		{
			this.levelText.text =  Localisation.GetString("Olaf's_Caravan");
			alternativeLevelText.text = this.levelText.text;
		}
		else if (level == -2)
		{
			this.levelText.text =  Localisation.GetString("Tutorial");
			alternativeLevelText.text = this.levelText.text;
		}
		this.isActive = true;
		this.state = 2;
	}

	public void fadeOut()
	{
		if (!this.isActive || this.state == 5)
		{
			return;
		}
		this.state = 5;
		this.idleTimer = 0f;
	}

	public void handleAnimation(float dt)
	{
		if (this.state == 2)
		{
			this.alpha += 1f * dt;
			if (this.alpha >= 1f)
			{
				this.alpha = 1f;
				this.state = 3;
			}
		}
		else if (this.state == 3)
		{
			this.idleTimer += dt;
			if (this.idleTimer >= 2f)
			{
				this.idleTimer = 0f;
				this.state = 4;
			}
		}
		else if (this.state == 4)
		{
			this.alpha -= 1f * dt;
			if (this.alpha <= 0.05f)
			{
				this.alpha = 0f;
				this.state = 1;
				this.isActive = false;
				this.levelText.gameObject.SetActive(false);
			}
		}
		else if (this.state == 5)
		{
			this.alpha -= 2f * dt;
			if (this.alpha <= 0.05f)
			{
				this.alpha = 0f;
				this.state = 1;
				this.isActive = false;
				this.levelText.gameObject.SetActive(false);
			}
		}
		Color color = this.levelText.color;
		color.a = this.alpha;
		this.levelText.color = color;
		if (this.levelTextShadow == null)
		{
			this.levelTextShadow = this.levelText.transform.Find("Shadow").GetComponent<tk2dTextMesh>();
		}
		if (this.levelTextShadow != null)
		{
			color = this.levelTextShadow.color;
			color.a = 0.5019608f * this.alpha;
			this.levelTextShadow.color = color;
		}
	}
}
