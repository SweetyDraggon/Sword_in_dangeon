using System;
using TMPro;
using UnityEngine;

public class XPProgress : MonoBehaviour
{
	public int level;

	public int xp;

	public int xpMax;

	public TextMeshPro label;

	public tk2dClippedSprite barSprite;

	private void Start()
	{
	}

	private void Update()
	{
		this.label.text = Localisation.GetString("LVL")  + this.level.ToString();
		this.barSprite.clipTopRight = new Vector2((float)this.xp / (float)this.xpMax, 1f);
	}
}
