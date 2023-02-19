using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
	public Button activateButton;
	private static List<BackButton> allBackButtons = new List<BackButton>();

	#region Enable Disable

	private void OnEnable()
	{
		// Reset Time
		ResetTimer();
		allBackButtons.Add(this);
	}
	private void OnDisable()
	{
		// Reset Time
		ResetTimer();
		allBackButtons.Remove(this);
	}

	#endregion


	#region Timer

	private const float waitTime = 0.5f;
	private static float timePassed = 0f;
	private static bool finishedCounting = false;

	private void ResetTimer()
	{
		timePassed = 0f;
		finishedCounting = false;
	}

	/// <summary>
	/// Return True if finished counting Time, and False if timer working
	/// </summary>
	/// <returns></returns>
	protected bool TimerFinished()
	{
		if (finishedCounting)
			return true;
		if (!NextInList())
			return false;

		timePassed += Time.unscaledDeltaTime;   // <- Unscaled Time
												//      deltaTime;	// <- Classic Time
		if (timePassed > waitTime)
		{
			finishedCounting = true;
		}
		return finishedCounting;
	}

	#endregion


	protected void Update()
	{
		if (!TimerFinished())
			return;
		
		

		if (DetectTouch())
			PerformBackAction();
	}


	protected void PerformBackAction()
	{
		if (!NextInList())
			return;
		// Reset Time
		ResetTimer();

		if (activateButton) // Checking if button even exist
			activateButton.onClick.Invoke();
	}


	/// <summary>
	/// Check if Activator button is clicked
	/// </summary>
	/// <returns></returns>
	private bool DetectTouch()
	{
		return Input.GetKey(KeyCode.Escape);
	}

	/// <summary>
	/// Check if this button is first in a waiting list
	/// </summary>
	/// <returns></returns>
	private bool NextInList()
	{
		return allBackButtons.IndexOf(this) == allBackButtons.Count - 1;
	}




}
