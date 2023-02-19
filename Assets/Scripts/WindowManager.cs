using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
	public GameObject container;

	public FadeLayer fadeLayer;

	private static WindowManager instance;

	public List<WindowMapping> windows;

	public List<CustomWindow> activeWindows;

	public Vector3 onScreenPosition = new Vector3(0f, 0f, 0f);

	public Vector3 offScreenPosition = new Vector3(0f, -400f, 0f);

	public Camera hudCamera;

	public float animationSpeed = 0.625f;

	public int windowCount;

	private static Predicate<CustomWindow> __f__am_cacheA;

	public static WindowManager Instance
	{
		get
		{
			return WindowManager.instance;
		}
	}

	public void initWindowManager()
	{
		this.fadeLayer.alpha = 0f;
		this.fadeLayer.fadeMax = 0.5f;
		this.fadeLayer.fadeSpeed = 0.35f;
		this.fadeLayer.updateSprite();
		WindowManager.instance = this;
		base.GetComponent<WindowManager>().enabled = true;
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
		UnityEngine.Object.DontDestroyOnLoad(this.fadeLayer.gameObject);
		this.activeWindows = new List<CustomWindow>();
	}

	private void Start()
	{
		this.OnLevelWasLoaded(-1);
	}

	private void OnLevelWasLoaded(int level)
	{
		GameObject gameObject = GameObject.Find("HUD");
		if (gameObject)
		{
			this.container.transform.position = gameObject.transform.position;
		}
		else
		{
			this.container.transform.position = new Vector3(0f, 0f, this.container.transform.position.z);
		}
	}

	private void Update()
	{
		this.handleBackButton();
		if (this.activeWindows.Count > 0)
		{
			CustomWindow customWindow = this.activeWindows[this.activeWindows.Count - 1];
			if (customWindow.isShown && !customWindow.isAnimating && !customWindow.buttonsEnabled)
			{
				customWindow.EnableButtons(true);
			}
		}
		this.updateWindowCount();
	}

	public void updateWindowCount()
	{
		this.windowCount = this.activeWindows.Count;
		foreach (CustomWindow current in this.activeWindows)
		{
			if (current.GetComponent<MapScreen>() != null)
			{
				this.windowCount--;
			}
		}
	}

	public void DisableAllButtons()
	{
		foreach (CustomWindow current in this.activeWindows)
		{
			current.EnableButtons(false);
		}
	}

	public CustomWindow ShowMenu(CustomWindow window, float zOrder = -9999f)
	{
		this.DisableAllButtons();
		this.updateWindowCount();
		window.transform.parent = base.transform;
		window.TweakSortOrder(this.windowCount);
		if (zOrder == -9999f)
		{
			window.zOrder = -1f + (float)(-(float)(this.windowCount + 1)) * 0.1f;
		}
		else
		{
			window.zOrder = zOrder;
		}
		window.SetCamera(this.hudCamera);
		window.EnableButtons(false);
		window.SlideIn(new Vector3(this.onScreenPosition.x, this.onScreenPosition.y, window.zOrder), new Vector3(this.offScreenPosition.x, this.offScreenPosition.y, window.zOrder), this.animationSpeed);
		this.activeWindows.Add(window);
		return window;
	}

	public CustomWindow ShowMenu(string menuname)
	{
		foreach (WindowMapping current in this.windows)
		{
			if (current.name == menuname)
			{
				CustomWindow window = UnityEngine.Object.Instantiate(current.windowPrefab) as CustomWindow;
				return this.ShowMenu(window, current.zOrder);
			}
		}
		return null;
	}

	public void HideMenu(CustomWindow menu)
	{
		foreach (CustomWindow current in this.activeWindows)
		{
			if (current == menu)
			{
				current.EnableButtons(false);
				current.SlideOut(new Vector3(this.onScreenPosition.x, this.onScreenPosition.y, current.transform.localPosition.z), new Vector3(this.offScreenPosition.x, this.offScreenPosition.y, current.transform.localPosition.z), this.animationSpeed);
				break;
			}
		}
	}

	public void RemoveMenu(CustomWindow menu)
	{
		this.activeWindows.Remove(menu);
		menu.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(menu.gameObject);
	}

	public void ShowShopPage(ShopPage page)
	{
		foreach (WindowMapping current in this.windows)
		{
			if (current.name == "shop_menu")
			{
				CustomWindow customWindow = UnityEngine.Object.Instantiate(current.windowPrefab) as CustomWindow;
				ShopMenu component = customWindow.GetComponent<ShopMenu>();
				component.forceCurrentPage = page;
				this.ShowMenu(customWindow, current.zOrder);
			}
		}
	}

	public void ShowAlertView(string title, string description, string button1Text, string button2Text, GameObject target, string button1Method, string button2Method)
	{
		Debug.LogError("Music" + button1Text);
		foreach (WindowMapping current in this.windows)
		{
			if (current.name == "alert_view")
			{
				CustomWindow customWindow = UnityEngine.Object.Instantiate(current.windowPrefab) as CustomWindow;
				AlertMenu component = customWindow.GetComponent<AlertMenu>();
				component.title = title;
				component.description = description;
				component.button1Text = button1Text;
				component.button2Text = button2Text;
				component.target = target;
				component.onButton1Clicked = button1Method;
				component.onButton2Clicked = button2Method;
				this.ShowMenu(customWindow, -9999f);
			}
		}
	}

	private void handleBackButton()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			
			CustomWindow customWindow = this.activeWindows.FindLast((CustomWindow x) => x.GetType() != typeof(MapScreen) || ((MapScreen)x).isShown);
			LevelSelect levelSelect;
			if (customWindow != null)
			{
				if (customWindow.GetType() == typeof(MapScreen))
				{
					MapScreen mapScreen = customWindow as MapScreen;
					mapScreen.closeClicked();
				}
				else if (customWindow.GetType() == typeof(ShopMenu))
				{
					Debug.Log("shop");
					ShopMenu shopMenu = customWindow as ShopMenu;
					shopMenu.closeClicked();
				}
				else if (customWindow.GetType() == typeof(LevelUpMenu))
				{
					LevelUpMenu levelUpMenu = customWindow as LevelUpMenu;
					levelUpMenu.deactivate();
				}
				else if (customWindow.GetType() == typeof(AlertMenu))
				{
					AlertMenu alertMenu = customWindow as AlertMenu;
					alertMenu.button1Clicked();
				}
				else if (customWindow.GetType() == typeof(PauseMenu))
				{
					PauseMenu pauseMenu = customWindow as PauseMenu;
					pauseMenu.continueClicked();
				}
				else
				{
					WindowManager.Instance.HideMenu(customWindow);
				}
			}
			else if (Game.Instance != null && !Game.Instance.paused)
			{
				Game.Instance.paused = true;
				WindowManager.Instance.ShowMenu("pause_menu");
			}
			else if (UnityEngine.Object.FindObjectOfType(typeof(Cutscene)) != null)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
			}
			else if ((levelSelect = (UnityEngine.Object.FindObjectOfType(typeof(LevelSelect)) as LevelSelect)) != null)
			{
				levelSelect.closePortalClicked();
			}
			else if (UnityEngine.Object.FindObjectOfType(typeof(Splash)) != null)
			{
				Application.Quit();
			}
			else
			{
				UnityEngine.Debug.Log("***WindowManager Showing exit window (No active windows, not on any screen)");
				WindowManager.Instance.ShowMenu("exit_window");
			}
		}
	}
}
