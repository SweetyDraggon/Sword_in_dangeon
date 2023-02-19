using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
	public FadeLayer fadeLayer;

	public float animationSpeed = 0.75f;

	public LeanTweenType animationTweenType = LeanTweenType.easeInOutExpo;

	private bool isAnimating;

	public int currentPage = 1;

	public int numPages = 5;

	public float screenTension = 5f;

	public GameObject pager;

	public GameObject portalScreenPrefab;

	public List<PortalScreen> portalScreens;

	public Vector2 offset;

	public List<tk2dSprite> pagerDots;

	private Vector2 pageStartPos;

	private Vector2 touchFirstPressPos;

	private Vector2 touchCurrentSwipe;

	private void Awake()
	{
		Application.targetFrameRate = 30;
		this.fadeLayer = TransitionScript.Instance.fadeLayer;
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
		this.fadeLayer.onFadeInComplete += new FadeEvent(this.fadeInComplete);
		TransitionScript.Instance.updatePosition();
		this.fadeLayer.gameObject.SetActive(true);
		Main.loadPrefs();
	}

	private void OnDestroy()
	{
		this.fadeLayer.onFadeInComplete -= new FadeEvent(this.fadeInComplete);
	}

	private void Start()
	{
		this.portalScreens = new List<PortalScreen>();
		for (int i = 0; i < this.numPages; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.portalScreenPrefab) as GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3((float)i * 480f, 0f, 0f);
			gameObject.name = "PortalScreen" + (i + 1);
			PortalScreen component = gameObject.GetComponent<PortalScreen>();
			component.pageNum = i + 1;
			component.levelSelect = this;
			this.portalScreens.Add(component);
		}
		this.currentPage = GameCore.Instance.levelSelectLastPage;
		this.SetupPager();
		this.UpdatePager();
		base.gameObject.transform.position = this.getPagePosition();
		this.fadeLayer.FadeOut();
	}

	private void Update()
	{
		this.SwipeDetection();
	}

	public void SwipeDetection()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.pageStartPos = this.getPagePosition();
			this.touchFirstPressPos = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
		}
		if (Input.GetMouseButton(0))
		{
			Vector2 vector = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			this.touchCurrentSwipe = new Vector2(vector.x - this.touchFirstPressPos.x, vector.y - this.touchFirstPressPos.y);
			base.transform.localPosition = new Vector3(this.pageStartPos.x + this.touchCurrentSwipe.x / this.screenTension, 0f, 0f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector2 vector2 = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			this.touchCurrentSwipe = new Vector2(vector2.x - this.touchFirstPressPos.x, vector2.y - this.touchFirstPressPos.y);
			if (this.touchCurrentSwipe.x <= -256f && this.currentPage != this.numPages)
			{
				this.nextPage();
			}
			else if (this.touchCurrentSwipe.x >= 256f && this.currentPage != 1)
			{
				this.previousPage();
			}
			else
			{
				LeanTween.moveX(base.gameObject, this.getPagePosition().x, this.animationSpeed).setEase(this.animationTweenType).setOnComplete(new Action(this.animatePageTurnComplete));
			}
		}
	}

	public void SetupPager()
	{
		this.pagerDots = new List<tk2dSprite>();
		GameObject gameObject = this.pager.transform.Find("Dot").gameObject;
		gameObject.name = "Dot1";
		this.pagerDots.Add(gameObject.GetComponent<tk2dSprite>());
		for (int i = 1; i < this.numPages; i++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject) as GameObject;
			gameObject2.name = "Dot" + (i + 1);
			gameObject2.transform.parent = this.pager.transform;
			gameObject2.transform.localPosition = new Vector3((float)i * 10f, 0f, 0f);
			this.pagerDots.Add(gameObject2.GetComponent<tk2dSprite>());
		}
		this.pager.transform.localPosition += new Vector3((float)(this.numPages - 1) * 10f * -0.5f, 0f, 0f);
	}

	public void UpdatePager()
	{
		for (int i = 0; i < this.pagerDots.Count; i++)
		{
			if (i + 1 == this.currentPage)
			{
				this.pagerDots[i].color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				this.pagerDots[i].color = new Color(1f, 1f, 1f, 0.2f);
			}
		}
	}

	public Vector2 getPagePosition()
	{
		if (this.currentPage < 1)
		{
			this.currentPage = 1;
		}
		if (this.currentPage > 5)
		{
			this.currentPage = 5;
		}
		return new Vector2((float)(this.currentPage - 1) * -480f + this.offset.x, this.offset.y);
	}

	public void previousPage()
	{
		if (this.currentPage == 1)
		{
			return;
		}
		this.currentPage--;
		if (this.currentPage < 1)
		{
			this.currentPage = 1;
		}
		this.animatePageTurn();
		GameCore.Instance.levelSelectLastPage = this.currentPage;
	}

	public void nextPage()
	{
		if (this.currentPage == this.numPages)
		{
			return;
		}
		this.currentPage++;
		if (this.currentPage > this.numPages)
		{
			this.currentPage = this.numPages;
		}
		this.animatePageTurn();
		GameCore.Instance.levelSelectLastPage = this.currentPage;
	}

	public void animatePageTurn()
	{
		if (this.isAnimating)
		{
			LeanTween.cancel(base.gameObject);
		}
		this.isAnimating = true;
		this.UpdatePager();
		LeanTween.moveX(base.gameObject, this.getPagePosition().x, this.animationSpeed).setEase(this.animationTweenType).setOnComplete(new Action(this.animatePageTurnComplete));
	}

	public void animatePageTurnComplete()
	{
		this.isAnimating = false;
	}

	public void loadLevel(int levelNum)
	{
		if (this.fadeLayer.isAnimating)
		{
			return;
		}
		Component[] componentsInChildren = base.GetComponentsInChildren<CustomUIButton>();
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			CustomUIButton customUIButton = (CustomUIButton)array[i];
			customUIButton.enabled = false;
		}
		GameCore.Instance.dungeonLevelFromMenu = levelNum;
		this.fadeLayer.FadeIn();
	}

	public void closePortalClicked()
	{
		UnityEngine.Debug.Log("Close portal clicked!");
		this.loadLevel(-1);
	}

	public void fadeInComplete()
	{
		GameCore.Instance.levelSelectLastPage = this.currentPage;
		Main.savePrefs();
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
	}
}
