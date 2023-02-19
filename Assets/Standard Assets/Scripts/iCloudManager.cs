using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class iCloudManager : MonoBehaviour
{
	public static event Action<string> cloudLoadGameSuccess;

	public static event Action cloudShowConflictAlert;

	public static event Action cloudHideConflictAlert;

	public static event Action cloudKeepLocalSave;

	public static event Action cloudKeepRemoteSave;

	private void Start()
	{
	}

	public void onCloudLoadGameSuccess(string data)
	{
		if (iCloudManager.cloudLoadGameSuccess != null)
		{
			iCloudManager.cloudLoadGameSuccess(data);
		}
	}

	public void onCloudKeepLocalSave()
	{
		if (iCloudManager.cloudKeepLocalSave != null)
		{
			iCloudManager.cloudKeepLocalSave();
		}
	}

	public void onCloudKeepRemoteSave()
	{
		if (iCloudManager.cloudKeepRemoteSave != null)
		{
			iCloudManager.cloudKeepRemoteSave();
		}
	}

	public void onCloudShowConflictAlert()
	{
		if (iCloudManager.cloudShowConflictAlert != null)
		{
			iCloudManager.cloudShowConflictAlert();
		}
	}

	public void onCloudHideConflictAlert()
	{
		if (iCloudManager.cloudHideConflictAlert != null)
		{
			iCloudManager.cloudHideConflictAlert();
		}
	}
}
