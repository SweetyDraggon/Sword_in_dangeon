using System;
using System.Collections;
using UnityEngine;

public class Splash : MonoBehaviour
{
	[SerializeField] private float splashTime;

	public FadeLayer fadeLayer;

	public GameObject logosContainer;

	//public tk2dSprite ravenous;

	//public tk2dSprite woblyware;

	public bool canSkip;

	public float frameDisplayTime;

	public int currentFrameNum;

	public bool startedLoad;

	public int shakeCount;

	public float originalX;

	private static string chartboostAppId = "53d9692d89b0bb3ad3c02428";

	private static string chartboostAppSignature = "d9f488e0dc2e9e4242d2ffcd51a37b82b41ae763";

	private void Awake()
	{
	//	if (GameCore.Instance.playhavenEnabled)
	//	{
	//	}
	//	AudioManager.Instance.PlayMusic("title");
	//this.fadeLayer = TransitionScript.Instance.fadeLayer;
	//	this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
	//	this.fadeLayer.onFadeInComplete += new FadeEvent(this.fadeInComplete);
	//	this.fadeLayer.onFadeOutComplete -= new FadeEvent(this.fadeOutComplete);
	//	this.fadeLayer.onFadeOutComplete += new FadeEvent(this.fadeOutComplete);
	//	this.fadeLayer.FadeOut();
	//	//this.startedLoad = false;
	//	//this.currentFrameNum = 0;
	//	//this.ravenous.color = new Color(1f, 1f, 1f, 0f);
	//	//this.woblyware.color = new Color(1f, 1f, 1f, 1f);
	//	//UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");	
	}

    private void Start()
    {
		StartCoroutine(LoadTutleScreen());
	}

    private IEnumerator LoadTutleScreen()
    {
		Fader.Instance.FadeOut();
		yield return new WaitForSeconds(0.8f);
		yield return new WaitForSeconds(splashTime);
		Fader.Instance.FadeIn();
		yield return new WaitForSeconds(0.8f);
		UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
	}

	//private void Update()
	//{
	//	if (this.frameDisplayTime > 0f)
	//	{
	//		this.frameDisplayTime -= 0.0333333351f;
	//	}
	//	if (this.frameDisplayTime < 0f)
	//	{
	//		this.advanceFrame();
	//	}
	//}

	public void advanceFrame()
	{
		if (!this.canSkip)
		{
			return;
		}
		if (this.frameDisplayTime > 0f)
		{
			return;
		}
		this.canSkip = false;
		this.currentFrameNum++;
		this.fadeLayer.FadeIn();
	}

	public void fadeInComplete()
	{
		//if (this.currentFrameNum >= 2)
		//{
		//	if (!this.startedLoad)
		//	{
		//		this.startedLoad = true;
		//		UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
		//	}
		//}
		//else
		//{
		//	if (this.currentFrameNum == 1)
		//	{
		//		//this.ravenous.color = new Color(1f, 1f, 1f, 1f);
		//		//this.woblyware.color = new Color(1f, 1f, 1f, 0f);
		//	}
		//	this.canSkip = false;
		//	this.fadeLayer.FadeOut();
		//}
		Invoke(nameof(LoadTutleScreen), splashTime);

	}

	public void fadeOutComplete()
	{
		this.canSkip = true;
		if (this.currentFrameNum == 1)
		{
			this.frameDisplayTime = 4f;
			this.shakeLogo();
		}
		else
		{
			this.frameDisplayTime = 2f;
		}
	}

	public void shakeLogo()
	{
		float num = 0f;
		float num2 = 10f;
		switch (this.shakeCount)
		{
		case 0:
			num = 0f;
			break;
		case 1:
			AudioManager.Instance.PlaySound("ravenous");
			num = num2 * -0.4f;
			break;
		case 2:
			num = num2 * 0.4f;
			break;
		case 3:
			num = num2 * -0.666f;
			break;
		case 4:
			num = num2 * 0.666f;
			break;
		case 5:
			num = -num2;
			break;
		case 6:
			num = num2;
			break;
		case 7:
			num = num2 * -0.8f;
			break;
		case 8:
			num = num2 * 0.8f;
			break;
		case 9:
			num = -num2;
			break;
		case 10:
			num = num2;
			break;
		case 11:
			num = num2 * -0.53333f;
			break;
		case 12:
			num = num2 * 0.53333f;
			break;
		case 13:
			num = num2 * -0.26666f;
			break;
		case 14:
			num = num2 * 0.26666f;
			break;
		case 15:
			return;
		}
		this.shakeCount++;
		float time = (this.shakeCount != 1) ? 0.1f : 0.3f;
		//LeanTween.moveLocal(this.ravenous.gameObject, new Vector3(this.originalX + num, 0f, 0f), time).setEase(LeanTweenType.easeOutSine).setOnComplete(new Action(this.shakeLogo));
	}
}
