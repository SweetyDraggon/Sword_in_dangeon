using System;
using UnityEngine;

public class PersistentObjectsScript : MonoBehaviour
{
	public string originalSceneName = string.Empty;

	public GameObject gameCoreObject;

	public GameObject audioManagerObject;

	public GameObject transitionObject;

	public GameObject storeKitObject;

	public GameObject achievementHandler;

	public GameObject windowManagerObject;

	public GameObject testFlightObject;

	public GameObject iCloudManager;

	private void Awake()
	{
		if (this.originalSceneName == string.Empty)
		{
			this.originalSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		}
		GameObject gameObject = GameObject.Find("PersistentObjects");
		if (gameObject != null && gameObject != base.gameObject)
		{
			if (this.originalSceneName != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			this.initFirstObjects();
		}
	}

	private void initFirstObjects()
	{
		Achievements.Instance.Authenticate();
		this.testFlightObject.GetComponent<TestFlightManager>().initTestFlight();
		this.gameCoreObject.GetComponent<GameCore>().initGameCore();
		this.audioManagerObject.GetComponent<AudioManager>().initAudioManager();
		this.transitionObject.GetComponent<TransitionScript>().initTransitioner();
		this.achievementHandler.GetComponent<AchievementHandler>().initAchievementHandler();
		this.windowManagerObject.GetComponent<WindowManager>().initWindowManager();
		this.gameCoreObject.GetComponent<GameCore>().postInit();
		if (this.testFlightObject != null)
		{
			UnityEngine.Object.DontDestroyOnLoad(this.testFlightObject);
		}
		if (this.iCloudManager != null)
		{
			UnityEngine.Object.DontDestroyOnLoad(this.iCloudManager);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
	}
}
