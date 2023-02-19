using System;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
	public int health;

	public int healthMax;

	public tk2dClippedSprite barSprite;

	public new tk2dTextMesh name;

	private void Start()
	{
	}

	private void Update()
	{
		this.barSprite.clipTopRight = new Vector2((float)this.health / (float)this.healthMax, 1f);
	}
}
