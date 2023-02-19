/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayHaven
{
	[AddComponentMenu("PlayHaven/Content Requester")]
	public class PlayHavenContentRequester : MonoBehaviour
	{
		public enum WhenToRequest
		{
			Awake,
			Start,
			OnEnable,
			OnDisable,
			Manual
		}

		public enum InternetConnectivity
		{
			WiFiOnly,
			CarrierNetworkOnly,
			WiFiAndCarrierNetwork,
			Always = 100
		}

		public enum MessageType
		{
			None,
			Send,
			Broadcast,
			Upwards
		}

		public enum ExhaustedAction
		{
			None,
			DestroySelf,
			DestroyGameObject,
			DestroyRoot
		}

		private sealed class _RequestCoroutine_c__Iterator2 : IDisposable, IEnumerator, IEnumerator<object>
		{
			internal bool refetch;

			internal int _PC;

			internal object _current;

			internal bool ___refetch;

			internal PlayHavenContentRequester __f__this;

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
					if (this.__f__this.whenToRequest == PlayHavenContentRequester.WhenToRequest.Manual && this.__f__this.requestDelay > 0f)
					{
						this._current = new WaitForSeconds(this.__f__this.requestDelay);
						this._PC = 1;
						return true;
					}
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this.__f__this.requestIsInProgress)
				{
					if (UnityEngine.Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("request is in progress; not making another request");
					}
				}
				else if (this.__f__this.exhausted)
				{
					if (Application.isEditor)
					{
						UnityEngine.Debug.LogWarning("content requester has been exhausted");
					}
				}
				else if (this.__f__this.Manager.IsPlacementSuppressed(this.__f__this.placement))
				{
					if (UnityEngine.Debug.isDebugBuild)
					{
						UnityEngine.Debug.LogWarning("content requester is suppressed");
					}
				}
				else
				{
					this.__f__this.refetch = this.refetch;
					if (this.__f__this.Manager)
					{
						if (this.__f__this.placement.Length > 0)
						{
							this.__f__this.Manager.OnDismissContent += new DismissHandler(this.__f__this.HandlePlayHavenManagerOnDismissContent);
							this.__f__this.Manager.OnErrorContentRequest += new ErrorHandler(this.__f__this.HandlePlayHavenManagerContentRequestOnError);
							if (this.__f__this.rewardMayBeDelivered)
							{
								this.__f__this.Manager.OnRewardGiven -= new RewardTriggerHandler(this.__f__this.HandlePlayHavenManagerOnRewardGiven);
								this.__f__this.Manager.OnRewardGiven += new RewardTriggerHandler(this.__f__this.HandlePlayHavenManagerOnRewardGiven);
							}
							this.__f__this.requestIsInProgress = true;
							this.__f__this.contentRequestId = this.__f__this.Manager.ContentRequest(this.__f__this.placement, this.__f__this.showsOverlayImmediately);
						}
						else if (UnityEngine.Debug.isDebugBuild)
						{
							UnityEngine.Debug.LogError("placement value not set in PlayHaventContentRequester");
						}
					}
					this.__f__this.uses++;
					if (this.__f__this.limitedUse && !this.__f__this.rewardMayBeDelivered && this.__f__this.uses >= this.__f__this.maxUses)
					{
						this.__f__this.Exhaust();
					}
					this._PC = -1;
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

		public PlayHavenContentRequester.WhenToRequest whenToRequest = PlayHavenContentRequester.WhenToRequest.Manual;

		public string placement = string.Empty;

		public PlayHavenContentRequester.WhenToRequest prefetch = PlayHavenContentRequester.WhenToRequest.Manual;

		public PlayHavenContentRequester.InternetConnectivity connectionForPrefetch;

		public bool refetchWhenUsed;

		public bool showsOverlayImmediately;

		public bool rewardMayBeDelivered;

		public PlayHavenContentRequester.MessageType rewardMessageType = PlayHavenContentRequester.MessageType.Broadcast;

		public bool useDefaultTestReward;

		public string defaultTestRewardName = string.Empty;

		public int defaultTestRewardQuantity = 1;

		public float requestDelay;

		public bool limitedUse;

		public int maxUses;

		public PlayHavenContentRequester.ExhaustedAction exhaustAction;

		private PlayHavenManager playHaven;

		private bool exhausted;

		private int uses;

		private int contentRequestId;

		private int prefetchRequestId;

		private bool requestIsInProgress;

		private bool prefetchIsInProgress;

		private bool refetch;

		public int RequestId
		{
			get
			{
				return (this.contentRequestId == 0) ? this.prefetchRequestId : this.contentRequestId;
			}
			private set
			{
				this.prefetchRequestId = value;
				this.contentRequestId = value;
			}
		}

		public bool IsExhausted
		{
			get
			{
				return this.exhausted;
			}
		}

		private PlayHavenManager Manager
		{
			get
			{
				if (!this.playHaven)
				{
					this.playHaven = PlayHavenManager.Instance;
				}
				return this.playHaven;
			}
		}

		private void Awake()
		{
			this.refetch = this.refetchWhenUsed;
			if (this.whenToRequest == PlayHavenContentRequester.WhenToRequest.Awake)
			{
				if (this.requestDelay > 0f)
				{
					base.Invoke("Request", this.requestDelay);
				}
				else
				{
					this.Request();
				}
			}
			else if (this.prefetch == PlayHavenContentRequester.WhenToRequest.Awake)
			{
				this.PreFetch();
			}
		}

		private void OnEnable()
		{
			if (this.whenToRequest == PlayHavenContentRequester.WhenToRequest.OnEnable)
			{
				if (this.requestDelay > 0f)
				{
					base.Invoke("Request", this.requestDelay);
				}
				else
				{
					this.Request();
				}
			}
			else if (this.prefetch == PlayHavenContentRequester.WhenToRequest.OnEnable)
			{
				this.PreFetch();
			}
		}

		private void OnDisable()
		{
			if (this.whenToRequest == PlayHavenContentRequester.WhenToRequest.OnDisable)
			{
				this.Request();
			}
			else if (this.prefetch == PlayHavenContentRequester.WhenToRequest.OnDisable)
			{
				this.PreFetch();
			}
		}

		private void OnDestroy()
		{
			if (this.Manager)
			{
				this.Manager.OnRewardGiven -= new RewardTriggerHandler(this.HandlePlayHavenManagerOnRewardGiven);
				this.Manager.OnDismissContent -= new DismissHandler(this.HandlePlayHavenManagerOnDismissContent);
				this.Manager.OnErrorContentRequest -= new ErrorHandler(this.HandlePlayHavenManagerContentRequestOnError);
			}
		}

		private void Start()
		{
			if (this.whenToRequest == PlayHavenContentRequester.WhenToRequest.Start)
			{
				if (this.requestDelay > 0f)
				{
					base.Invoke("Request", this.requestDelay);
				}
				else
				{
					this.Request();
				}
			}
			else if (this.prefetch == PlayHavenContentRequester.WhenToRequest.Start)
			{
				this.PreFetch();
			}
		}

		public void Request()
		{
			this.Request(this.refetchWhenUsed);
		}

		public void Request(bool refetch)
		{
			base.StartCoroutine(this.RequestCoroutine(refetch));
		}

		public void PreFetch()
		{
			this.RequestId = 0;
			bool flag = true;
			switch (this.connectionForPrefetch)
			{
			case PlayHavenContentRequester.InternetConnectivity.WiFiOnly:
				flag = (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork);
				break;
			case PlayHavenContentRequester.InternetConnectivity.CarrierNetworkOnly:
				flag = (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork);
				break;
			case PlayHavenContentRequester.InternetConnectivity.WiFiAndCarrierNetwork:
				flag = (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork);
				break;
			}
			if (!flag)
			{
				return;
			}
			if (this.prefetchIsInProgress)
			{
				if (UnityEngine.Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log("prefetch request is in progress; not making another request");
				}
				return;
			}
			if (this.Manager)
			{
				if (this.placement.Length > 0)
				{
					this.prefetchIsInProgress = true;
					this.Manager.OnSuccessPreloadRequest += new SuccessHandler(this.HandleManagerOnSuccessPreloadRequest);
					this.Manager.OnDismissContent += new DismissHandler(this.HandlePlayHavenManagerOnDismissContent);
					if (UnityEngine.Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("Making content preload request for placement: " + this.placement);
					}
					this.prefetchRequestId = this.Manager.ContentPreloadRequest(this.placement);
				}
				else if (UnityEngine.Debug.isDebugBuild)
				{
					UnityEngine.Debug.LogError("placement value not set in PlayHaventContentRequester");
				}
			}
		}

		public void GiveReward(Reward reward)
		{
			this.HandlePlayHavenManagerOnRewardGiven(-1, reward);
		}

		private void HandleManagerOnSuccessPreloadRequest(int requestId)
		{
			if (requestId == this.RequestId)
			{
				this.prefetchIsInProgress = false;
				if (UnityEngine.Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log("prefetch of placement successful: " + this.placement);
				}
				this.Manager.OnSuccessPreloadRequest -= new SuccessHandler(this.HandleManagerOnSuccessPreloadRequest);
				this.Manager.OnDismissContent -= new DismissHandler(this.HandlePlayHavenManagerOnDismissContent);
			}
		}

		private void HandlePlayHavenManagerContentRequestOnError(int hash, Error error)
		{
			if (this.Manager && this.contentRequestId == hash)
			{
				this.Manager.OnDismissContent -= new DismissHandler(this.HandlePlayHavenManagerOnDismissContent);
				this.Manager.OnErrorContentRequest -= new ErrorHandler(this.HandlePlayHavenManagerContentRequestOnError);
				this.Manager.OnSuccessPreloadRequest -= new SuccessHandler(this.HandleManagerOnSuccessPreloadRequest);
				if (this.rewardMayBeDelivered)
				{
					this.Manager.OnRewardGiven -= new RewardTriggerHandler(this.HandlePlayHavenManagerOnRewardGiven);
				}
			}
		}

		private void HandlePlayHavenManagerOnRewardGiven(int hashCode, Reward reward)
		{
			if (this.contentRequestId == hashCode || hashCode == -1)
			{
				switch (this.rewardMessageType)
				{
				case PlayHavenContentRequester.MessageType.Send:
					base.SendMessage("OnPlayHavenRewardGiven", reward);
					break;
				case PlayHavenContentRequester.MessageType.Broadcast:
					base.BroadcastMessage("OnPlayHavenRewardGiven", reward);
					break;
				case PlayHavenContentRequester.MessageType.Upwards:
					base.SendMessageUpwards("OnPlayHavenRewardGiven", reward);
					break;
				}
				this.Manager.OnRewardGiven -= new RewardTriggerHandler(this.HandlePlayHavenManagerOnRewardGiven);
			}
		}

		private void HandlePlayHavenManagerOnDismissContent(int hashCode, DismissType dismissType)
		{
			if (this.RequestId == hashCode)
			{
				this.requestIsInProgress = false;
				if (this.prefetchIsInProgress && dismissType == DismissType.NoContent)
				{
					this.HandleManagerOnSuccessPreloadRequest(this.RequestId);
					return;
				}
				if (this.Manager)
				{
					this.Manager.OnDismissContent -= new DismissHandler(this.HandlePlayHavenManagerOnDismissContent);
					this.Manager.OnErrorContentRequest -= new ErrorHandler(this.HandlePlayHavenManagerContentRequestOnError);
					this.Manager.OnSuccessPreloadRequest -= new SuccessHandler(this.HandleManagerOnSuccessPreloadRequest);
				}
				switch (this.rewardMessageType)
				{
				case PlayHavenContentRequester.MessageType.Send:
					base.SendMessage("OnPlayHavenContentDismissed", dismissType, SendMessageOptions.DontRequireReceiver);
					break;
				case PlayHavenContentRequester.MessageType.Broadcast:
					base.BroadcastMessage("OnPlayHavenContentDismissed", dismissType, SendMessageOptions.DontRequireReceiver);
					break;
				case PlayHavenContentRequester.MessageType.Upwards:
					base.SendMessageUpwards("OnPlayHavenContentDismissed", dismissType, SendMessageOptions.DontRequireReceiver);
					break;
				}
				if (!this.exhausted && this.limitedUse && this.uses >= this.maxUses)
				{
					this.Exhaust();
				}
				else if (this.refetch && dismissType != DismissType.NoContent)
				{
					this.PreFetch();
				}
			}
		}

		private void RequestPlayHavenContent()
		{
			if (this.requestDelay > 0f)
			{
				base.Invoke("Request", this.requestDelay);
			}
			else
			{
				this.Request();
			}
		}

		private void Exhaust()
		{
			this.exhausted = true;
			switch (this.exhaustAction)
			{
			case PlayHavenContentRequester.ExhaustedAction.DestroySelf:
				UnityEngine.Object.Destroy(this);
				break;
			case PlayHavenContentRequester.ExhaustedAction.DestroyGameObject:
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			case PlayHavenContentRequester.ExhaustedAction.DestroyRoot:
				UnityEngine.Object.Destroy(base.transform.root.gameObject);
				break;
			}
		}

		private IEnumerator RequestCoroutine(bool refetch)
		{
			PlayHavenContentRequester._RequestCoroutine_c__Iterator2 _RequestCoroutine_c__Iterator = new PlayHavenContentRequester._RequestCoroutine_c__Iterator2();
			_RequestCoroutine_c__Iterator.refetch = refetch;
			_RequestCoroutine_c__Iterator.___refetch = refetch;
			_RequestCoroutine_c__Iterator.__f__this = this;
			return _RequestCoroutine_c__Iterator;
		}
	}
}
*/