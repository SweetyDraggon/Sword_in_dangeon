using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class CountlyWWW : MonoBehaviour
{
	private sealed class _SendDataToServer_c__Iterator1 : IDisposable, IEnumerator, IEnumerator<object>
	{
		internal bool _networkProblem___0;

		internal string _data___1;

		internal WWW _response___2;

		internal float _waitingTime___3;

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
				this._networkProblem___0 = (Application.internetReachability == NetworkReachability.NotReachable);
				if (this._networkProblem___0)
				{
					CountlyWWW._numberOfFailedTries++;
				}
				break;
			case 1u:
				Countly.Log("returned -> " + this._response___2.text);
				if (this._response___2.error != null)
				{
					this._current = new WaitForSeconds(Countly.Instance.SleepAfterFailedTry * (float)CountlyWWW._numberOfFailedTries);
					this._PC = 2;
					return true;
				}
				Countly.Instance.ConnectionQueue.Dequeue();
				break;
			case 2u:
				CountlyWWW._numberOfFailedTries++;
				Countly.Log("ERROR -> " + this._response___2.error);
				break;
			case 3u:
				CountlyUtil.RunCoroutine(CountlyWWW.SendDataToServer());
				goto IL_1AA;
			default:
				return false;
			}
			if (Countly.Instance.ConnectionQueue.Count > 0 && !this._networkProblem___0)
			{
				this._data___1 = Countly.Instance.ConnectionQueue.Peek();
				this._response___2 = CountlyWWW.Send(this._data___1, 0);
				this._current = this._response___2;
				this._PC = 1;
				return true;
			}
			if (Countly.SendDataToServer)
			{
				this._waitingTime___3 = Countly.Instance.DataCheckPeriod;
				if (this._networkProblem___0)
				{
					this._waitingTime___3 += Countly.Instance.SleepAfterFailedTry * (float)CountlyWWW._numberOfFailedTries;
				}
				this._current = new WaitForSeconds(this._waitingTime___3);
				this._PC = 3;
				return true;
			}
			IL_1AA:
			this._PC = -1;
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

	private static int _numberOfFailedTries;

	public static IEnumerator SendDataToServer()
	{
		return new CountlyWWW._SendDataToServer_c__Iterator1();
	}

	public static WWW Send(string data, int sleepingTime)
	{
		Countly.Log("call -> " + data);
		WWW result = new WWW(Countly.Instance.ServerURL + "/i?" + data)
		{
			threadPriority = UnityEngine.ThreadPriority.Low
		};
		Thread.Sleep(sleepingTime);
		return result;
	}
}
