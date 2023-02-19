/*
using System;
using System.Runtime.InteropServices;

public class iCloud
{
	[DllImport("__Internal")]
	public static extern bool _iCloudAvailable();

	public static bool iCloudAvailable()
	{
		return false;
	}

	[DllImport("__Internal")]
	public static extern void _showConflictResolutionAlert();

	public static void showConflictResolutionAlert()
	{
	}

	[DllImport("__Internal")]
	public static extern IntPtr _getDocumentsFolder();

	public static string getDocumentsFolder()
	{
		return string.Empty;
	}

	[DllImport("__Internal")]
	public static extern void _copyFileToCloud(string filename);

	public static void copyFileToCloud(string filename)
	{
	}

	[DllImport("__Internal")]
	public static extern void _downloadFileFromCloud(string cloudFilename);

	public static void downloadFileFromCloud(string cloudFilename)
	{
	}
}
*/