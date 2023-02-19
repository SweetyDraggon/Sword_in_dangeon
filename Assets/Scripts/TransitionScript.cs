using System;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
	private static TransitionScript instance;

	public FadeLayer fadeLayer;

	public tk2dSprite loadingSprite;

	private GameObject hud;

	public static TransitionScript Instance
	{
		get
		{
			return TransitionScript.instance;
		}
	}

	public void initTransitioner()
	{
		TransitionScript.instance = this;
		if (this.fadeLayer == null)
		{
			this.fadeLayer = base.transform.Find("FadeLayer").GetComponent<FadeLayer>();
		}
		this.updatePosition();
		this.fadeLayer.gameObject.SetActive(true);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void updatePosition()
	{
		this.hud = GameObject.Find("HUD");
		if (this.hud != null)
		{
			this.fadeLayer.transform.position = new Vector3(this.hud.transform.position.x, this.hud.transform.position.y, 0f);
		}
	}
}
