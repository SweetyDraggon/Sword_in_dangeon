using System;
using UnityEngine;
using CoreGame;
using TMPro;

public class Cutscene : MonoBehaviour
{
	public FadeLayer fadeLayer;
	public Vector3 fadeLayerPosition;

	public CutsceneFrame[] frames;

	public CutsceneFrame currentFrame;

	public int currentFrameNum;

	public int maxFrameNum;

	public tk2dSprite picture;

    public TextMeshPro line1;

    public TextMeshPro line2;

    public TextMeshPro tapContinue;

	public bool canSkip;

	public bool typing;

	public float nextLetterTime;

	public float typingDelay = 0.25f;

	public int currentLetter;

	public int currentLine;

	public bool dialogDone;

	private Vector3 pictureOffset;

	public int tapFadeState = 1;

	public float tapFadeAlpha = 1f;

	public float tapCancelDelay = 0.5f;

	public bool startedLoad;
    public float alpha = 1f;
    public bool isIntro = true;
    public int state = 1;
    private void Awake()
	{
		//AudioManager.Instance.PlayMusic("cutscene");
		this.fadeLayer = TransitionScript.Instance.fadeLayer;
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeInComplete += new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeOutComplete -= new FadeEvent(this.fadeOutComplete);
		this.fadeLayer.onFadeOutComplete += new FadeEvent(this.fadeOutComplete);
		this.startedLoad = false;
		if (PlayerPrefs.HasKey("cutscene_outro"))
		{
			this.isIntro = (PlayerPrefs.GetInt("cutscene_outro", 1) <= 0);
			PlayerPrefs.DeleteKey("cutscene_outro");
			PlayerPrefs.Save();
		}
		if (this.isIntro)
		{
			this.currentFrameNum = 0;
			this.maxFrameNum = 5;
		}
		else
		{
			this.currentFrameNum = 5;
			this.maxFrameNum = 8;
		}
		if (!GameCore.Instance.IS_IPAD)
		{
			this.pictureOffset = new Vector3(0f, -10f, 0f);
			this.tapContinue.transform.localPosition += new Vector3(0f, 10f, 0f);
		}
		this.updateFrame();
		fadeLayer.transform.position = fadeLayerPosition;
	}

	private void Start()
	{
		this.fadeLayer.FadeOut();
	}

	private void Update()
	{
		if (this.canSkip)
		{
			this.tapCancelDelay -= 0.0333333351f;
			if (this.tapCancelDelay < 0f)
			{
				this.tapCancelDelay = 0f;
			}
		}
		if (!this.dialogDone)
		{
			//if (this.typing)
			//{
				this.advanceText(Time.deltaTime);
				//this.tapContinue.color = new Color(1f, 1f, 1f, 0.5f);
				this.tapFadeAlpha = 0.5f;
				//if (Input.GetMouseButtonDown(0))
				//{
					this.cancelTyping();
				//}
			//}
		}
		else
		{
			this.handleTapText();
			if (Input.GetMouseButtonDown(0))
			{
				this.advanceFrame();
			}
		}

        if (this.state == 1)
        {
            this.alpha -= 0.03f * Utils.GetScaledTime(30);
            if (this.alpha < 0.2f)
            {
                this.state = 2;
                this.alpha = 0.2f;
            }
        }
        else if (this.state == 2)
        {
            this.alpha += 0.03f * Utils.GetScaledTime(30);
            if (this.alpha >= 0.96f)
            {
                this.state = 1;
                this.alpha = 0.96f;
            }
        }
        Color color = this.tapContinue.color;
        color.a = this.alpha;
        this.tapContinue.color = color;
    }

    public void advanceText(float dt)
	{
		//this.nextLetterTime -= dt;
		//if (this.nextLetterTime <= 0f)
		//{
		//	this.nextLetterTime = this.typingDelay;
		//	this.currentLetter++;
			if (this.currentLine == 0)
			{
				if (this.currentLetter >= this.currentFrame.text1.Length)
				{
					this.currentLetter = 0;
					this.currentLine = 1;
					this.line1.text = this.currentFrame.text1;
				}
				else
				{
					this.line1.text = this.currentFrame.text1.Substring(0, this.currentLetter);
				}
				//if (this.currentFrame.text1.Substring(this.currentLetter, 1) != " ")
				//{
				//	AudioManager.Instance.PlaySound("typewrite");
				//}
				//if (this.currentFrame.text1.Substring(this.currentLetter, 1) == " ")
				//{
				//	this.nextLetterTime = 0f;
				//}
			}
			//else
			//{
				if (this.currentLetter >= this.currentFrame.text2.Length)
				{
					this.currentLetter = 0;
					this.line2.text = this.currentFrame.text2;
					this.dialogDone = true;
				}
				else
				{
					this.line2.text = this.currentFrame.text2.Substring(0, this.currentLetter);
				}
				if (this.currentFrame.text2.Substring(this.currentLetter, 1) != " ")
				{
					//AudioManager.Instance.PlaySound("typewrite");
				}
				if (this.currentFrame.text2.Substring(this.currentLetter, 1) == " ")
				{
					this.nextLetterTime = 0f;
				}
			//}
		//}
	}

	public void handleTapText()
	{
		//if (this.tapFadeState == 1)
		//{
		//	this.tapFadeAlpha -= 0.03f * Utils.GetScaledTime(30);
		//	if (this.tapFadeAlpha < 0.2f)
		//	{
		//		this.tapFadeState = 2;
		//		this.tapFadeAlpha = 0.2f;
		//	}
		//}
		//else if (this.tapFadeState == 2)
		//{
		//	this.tapFadeAlpha += 0.03f * Utils.GetScaledTime(30);
		//	if (this.tapFadeAlpha >= 0.96f)
		//	{
		//		this.tapFadeState = 1;
		//		this.tapFadeAlpha = 0.96f;
		//	}
		//}
		//Color color = this.tapContinue.color;
		//color.a = this.tapFadeAlpha;
		//this.tapContinue.color = color;
	}

	public void updateFrame()
	{
		this.currentFrame = this.frames[this.currentFrameNum];
		this.picture.SetSprite(this.currentFrame.image);
		this.picture.transform.localPosition = new Vector3(0f, 40f, 0f) + new Vector3(this.currentFrame.imageOffset.x, this.currentFrame.imageOffset.y, 0f) + this.pictureOffset;
		//this.centerText(this.line1, this.currentFrame.text1);
		//this.centerText(this.line2, this.currentFrame.text2);
		this.line1.text = string.Empty;
		this.line2.text =  string.Empty;
		this.typing = false;
		this.currentLetter = 0;
		this.currentLine = 0;
		this.dialogDone = false;
		this.nextLetterTime = 0f;
	}

	public void cancelTyping()
	{
		if (this.tapCancelDelay > 0f)
		{
			return;
		}
		this.tapCancelDelay = 0.25f;
		this.dialogDone = true;
		this.line1.text = Localisation.GetString (this.currentFrame.text1);
		this.line2.text = Localisation.GetString(this.currentFrame.text2);
	}

	public void advanceFrame()
	{
		if (!this.canSkip)
		{
			return;
		}
		if (this.tapCancelDelay > 0f)
		{
			return;
		}
		this.tapCancelDelay = 0.25f;
		this.canSkip = false;
		this.currentFrameNum++;
		this.fadeLayer.FadeIn();
	}

	//public void centerText(tk2dTextMesh textField, string text)
	//{
	//	//Vector2 meshDimensionsForString = textField.GetMeshDimensionsForString(text);
	//	//textField.transform.localPosition = new Vector3(meshDimensionsForString.x * -0.5f, textField.transform.localPosition.y, textField.transform.localPosition.z);
	//}

	public void fadeInComplete()
	{
		if (this.currentFrameNum >= this.maxFrameNum)
		{
			if (!this.startedLoad)
			{
				this.startedLoad = true;
				if (this.currentFrameNum > 5)
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
					GameCore.Instance.introCutsceneSeen = true;
				}
			}
		}
		else
		{
			this.canSkip = false;
			this.updateFrame();
			this.fadeLayer.FadeOut();
		}
	}

	public void fadeOutComplete()
	{
		this.typing = true;
		this.canSkip = true;

		advanceFrame();
	}

	private void OnDestroy()
	{
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeOutComplete -= new FadeEvent(this.fadeOutComplete);
	}

	public void SkipAllCutScene()
    {
		this.fadeLayer.onFadeInComplete += new FadeEvent(this.SkipAllFadeOut);
		this.fadeLayer.FadeIn();
	}

	private void SkipAllFadeOut()
    {
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.SkipAllFadeOut);
		this.startedLoad = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
		GameCore.Instance.introCutsceneSeen = true;
	}
}
