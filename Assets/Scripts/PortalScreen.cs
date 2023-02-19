using System;
using System.Collections.Generic;
using UnityEngine;

public class PortalScreen : MonoBehaviour
{
	public int pageNum = 1;

	public LevelSelect levelSelect;

	public GameObject buttonLevel;

	public GameObject buttonBossLevel;

	public List<Vector2> buttonPositions;

	private void Start()
	{
		for (int i = 0; i < this.buttonPositions.Count; i++)
		{
			GameObject gameObject;
			if (i == this.buttonPositions.Count - 1)
			{
				gameObject = (UnityEngine.Object.Instantiate(this.buttonBossLevel) as GameObject);
			}
			else
			{
				gameObject = (UnityEngine.Object.Instantiate(this.buttonLevel) as GameObject);
			}
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = this.buttonPositions[i];
			LevelSelectButton component = gameObject.GetComponent<LevelSelectButton>();
			int num = (this.pageNum - 1) * 13 + (i * 3 + 1);
			if (i == this.buttonPositions.Count - 1)
			{
				component.levelSelect = this.levelSelect;
				component.levelNum = num;
				component.SetBossNum(this.pageNum);
			}
			else
			{
				component.levelSelect = this.levelSelect;
				component.SetLevelNum(num);
			}
			if (Main.playerStats.levelReached < num)
			{
				component.locked = true;
			}
			component.enabled = !component.locked;
			component.UpdateSprites();
		}
	}

	private void Update()
	{
	}
}
