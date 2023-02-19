using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Countly : MonoBehaviour
{
    /*
	private sealed class _KeepAlive_c__Iterator0 : IDisposable, IEnumerator, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				Countly.UpdateSession();
				this._current = new WaitForSeconds(Countly.Instance.KeepAliveSendPeriod);
				this._PC = 1;
				return true;
			case 1u:
				if (Countly.Instance.IsCountlyWorking)
				{
					CountlyUtil.RunCoroutine(Countly.KeepAlive());
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}
    */
	public string ServerURL;

	public string AppKey;

	public string AppVersion;

	public float SleepAfterFailedTry = 5f;

	public float DataCheckPeriod = 10f;

	public float KeepAliveSendPeriod = 30f;

	public bool IsDebugModeOn;

	public bool AutoStart = true;

	public Queue<string> ConnectionQueue;

	public bool IsCountlyWorking;

	public int PackageSizeForEvents = 5;

	private List<CountlyEvent> _eventQueue;

	private float _lastSessionLengthSentOn;

	public static bool SendDataToServer;

	private static Countly _Instance_k__BackingField;

	public static Countly Instance
	{
		get;
		private set;
	}

	private Countly()
	{
	}

	private void Awake()
	{
		if (Countly.Instance != null)
		{
			UnityEngine.Debug.Log("Destroying duplicate game object instance.");
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Countly.Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(this);
		CountlyUtil.DetectDevice();
		this.ConnectionQueue = new Queue<string>();
		this._eventQueue = new List<CountlyEvent>();
	}

	private void Start()
	{
		if (this.AutoStart && this.ServerURL != null && this.AppKey != null && this.AppVersion != null)
		{
			this.OnStart();
		}
	}

	private void OnDestroy()
	{
		//Countly.Log("Destroying...");
		//Countly.Instance.OnStop();
	}

	public void Init(string serverURL, string appKey, string appVersion)
	{
        /*
		Countly.Instance.ServerURL = serverURL;
		Countly.Instance.AppKey = appKey;
		Countly.Instance.AppVersion = appVersion;
		*/      
	}

	public void OnStart()
	{
        /*
		Countly.Log("Starting...");
		Countly.BeginSession();
		this.IsCountlyWorking = true;
		CountlyUtil.RunCoroutine(CountlyWWW.SendDataToServer());
		CountlyUtil.RunCoroutine(Countly.KeepAlive());
		Countly.SendDataToServer = true;
		*/      
	}

	public void OnStop()
	{
        /*
		Countly.Log("Stopping...");
		this.IsCountlyWorking = false;
		Countly.EndSession();
		Countly.SendDataToServer = false;
		*/      
	}

	public void PostEvent(CountlyEvent Event)
	{
        /*
		if (Event == null)
		{
			throw new ArgumentNullException("Event");
		}
		Countly.Log("Event Posted -> " + Event.Key);
		this._eventQueue.Add(Event);
		if (this._eventQueue.Count >= this.PackageSizeForEvents)
		{
			this.QueueEvents();
			this._eventQueue.Clear();
		}
		*/
	}
/*
	public static IEnumerator KeepAlive()
	{
		return new Countly._KeepAlive_c__Iterator0();
	}

	private static void BeginSession()
	{
		string item = string.Format("app_key={0}&device_id={1}&sdk_version=1.0&begin_session=1&metrics={2}", Countly.Instance.AppKey, CountlyUtil.DeviceInformation.UDID, WWW.EscapeURL(CountlyUtil.GetMetrics(Countly.Instance.AppVersion)));
		Countly.Instance.ConnectionQueue.Enqueue(item);
		Countly.Instance._lastSessionLengthSentOn = Time.time;
	}

	private static void UpdateSession()
	{
		int num = Mathf.RoundToInt(Time.time - Countly.Instance._lastSessionLengthSentOn);
		string item = string.Format("app_key={0}&device_id={1}&session_duration={2}", Countly.Instance.AppKey, CountlyUtil.DeviceInformation.UDID, num);
		Countly.Instance.ConnectionQueue.Enqueue(item);
		Countly.Instance._lastSessionLengthSentOn = Time.time;
	}

	private static void EndSession()
	{
		int num = Mathf.RoundToInt(Time.time - Countly.Instance._lastSessionLengthSentOn);
		string text = string.Format("app_key={0}&device_id={1}&end_session=1&session_duration={2}", Countly.Instance.AppKey, CountlyUtil.DeviceInformation.UDID, num);
		if (Countly.Instance._eventQueue.Count > 0)
		{
			string s = CountlyUtil.SeriliazeEvents(Countly.Instance._eventQueue);
			Countly.Instance._eventQueue.Clear();
			text = text + "&events=" + WWW.EscapeURL(s);
		}
		Countly.Instance.ConnectionQueue.Enqueue(text);
		CountlyWWW.Send(text, 250);
	}

	private void QueueEvents()
	{
		string item = string.Format("app_key={0}&device_id={1}&events={2}", this.AppKey, CountlyUtil.DeviceInformation.UDID, WWW.EscapeURL(CountlyUtil.SeriliazeEvents(this._eventQueue)));
		Countly.Log("Events are packaged. Package is sending...");
		Countly.Instance.ConnectionQueue.Enqueue(item);
	}
    */
	public static void Log(string message)
	{
		if (Countly.Instance.IsDebugModeOn)
		{
			//UnityEngine.Debug.Log("Countly:\t" + message);
		}
	}
	
}
