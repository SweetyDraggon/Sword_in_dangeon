/*
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayHaven
{
	[AddComponentMenu("PlayHaven/Manager")]
	public class PlayHavenManager : MonoBehaviour
	{
		public enum WhenToOpen
		{
			Awake,
			Start,
			Manual
		}

		public enum WhenToGetNotifications
		{
			Disabled,
			Awake,
			Start,
			OnEnable,
			Manual,
			Poll
		}

		public enum WhenToRegisterForPushNotifications
		{
			Disabled,
			Awake,
			Start,
			OnEnable,
			Manual
		}

		public interface IPlayHavenRequest
		{
			int HashCode
			{
				get;
			}

			void Send();

			void Send(bool showsOverlayImmediately);

			void TriggerEvent(string eventName, Hashtable eventData);
		}

		internal class OpenRequest : PlayHavenManager.IPlayHavenRequest
		{
			private int hashCode;

			private static PlayHavenManager.SuccessHandler __f__am_cache8;

			private static PlayHavenManager.ErrorHandler __f__am_cache9;

			public event PlayHavenManager.SuccessHandler OnSuccess;

			public event PlayHavenManager.ErrorHandler OnError;

			public event PlayHavenManager.DismissHandler OnDismiss;

			public event PlayHavenManager.RewardHandler OnReward;

			public event PlayHavenManager.PurchaseHandler OnPurchasePresented;

			public event PlayHavenManager.GeneralHandler OnWillDisplay;

			public event PlayHavenManager.GeneralHandler OnDidDisplay;

			public int HashCode
			{
				get
				{
					return this.hashCode;
				}
			}

			public OpenRequest()
			{
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public OpenRequest(string customUDID)
			{
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public void Send()
			{
				this.Send(false);
			}

			public void Send(bool showsOverlayImmediately)
			{
				if (Application.isEditor)
				{
					UnityEngine.Debug.Log("PlayHaven: open request");
					Hashtable hashtable = new Hashtable();
					hashtable["notification"] = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					hashtable2["data"] = hashtable;
					hashtable2["hash"] = this.hashCode;
					hashtable2["name"] = "success";
					string json = hashtable2.toJson();
					PlayHavenManager.Instance.HandleNativeEvent(json);
				}
				else
				{
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log(string.Format("PlayHaven: open request (id={0})", this.hashCode));
					}
					PlayHavenManager.obj_PlayHavenFacade.Call("openRequest", new object[]
					{
						this.hashCode
					});
				}
			}

			public void TriggerEvent(string eventName, Hashtable eventData)
			{
				if (string.Compare(eventName, "success") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Open request success!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					this.OnSuccess(this, eventData);
				}
				else if (string.Compare(eventName, "error") == 0)
				{
					UnityEngine.Debug.LogError("PlayHaven: Open request failed!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					this.OnError(this, eventData);
				}
			}
		}

		internal class MetadataRequest : PlayHavenManager.IPlayHavenRequest
		{
			protected string mPlacement;

			private int hashCode;

			private static PlayHavenManager.SuccessHandler __f__am_cache9;

			private static PlayHavenManager.ErrorHandler __f__am_cacheA;

			private static PlayHavenManager.GeneralHandler __f__am_cacheB;

			private static PlayHavenManager.GeneralHandler __f__am_cacheC;

			public int HashCode
			{
				get
				{
					return this.hashCode;
				}
			}

			public MetadataRequest(string placement)
			{
				this.mPlacement = placement;
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public void Send()
			{
				this.Send(false);
			}

			public void Send(bool showsOverlayImmediately)
			{
				if (Application.isEditor)
				{
					UnityEngine.Debug.Log("PlayHaven: metadata request (" + this.mPlacement + ")");
					Hashtable hashtable = new Hashtable();
					hashtable["type"] = "badge";
					hashtable["value"] = "1";
					Hashtable hashtable2 = new Hashtable();
					hashtable2["notification"] = hashtable;
					Hashtable hashtable3 = new Hashtable();
					hashtable3["data"] = hashtable2;
					hashtable3["hash"] = this.hashCode;
					hashtable3["name"] = "success";
					hashtable3["content"] = this.mPlacement;
					string json = hashtable3.toJson();
					PlayHavenManager.Instance.HandleNativeEvent(json);
				}
				else
				{
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log(string.Format("PlayHaven: metadata request (id={0}, placement={1})", this.hashCode, this.mPlacement));
					}
					PlayHavenManager.obj_PlayHavenFacade.Call("metaDataRequest", new object[]
					{
						this.hashCode,
						this.mPlacement
					});
				}
			}

			public void TriggerEvent(string eventName, Hashtable eventData)
			{
				if (string.Compare(eventName, "success") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Metadata request success!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnSuccess(this, eventData);
				}
				else if (string.Compare(eventName, "willdisplay") == 0)
				{
					//this.OnWillDisplay(this);
				}
				else if (string.Compare(eventName, "diddisplay") == 0)
				{
					//this.OnDidDisplay(this);
				}
				else if (string.Compare(eventName, "error") == 0)
				{
					UnityEngine.Debug.LogError("PlayHaven: Metadata request failed!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnError(this, eventData);
				}
			}
		}

		internal class ContentRequester : PlayHavenManager.IPlayHavenRequest
		{
			protected string mPlacement;

			protected string mContentID;

			protected string mMessageID;

			private int hashCode;

			private static PlayHavenManager.DismissHandler __f__am_cacheB;

			private static PlayHavenManager.ErrorHandler __f__am_cacheC;

			private static PlayHavenManager.RewardHandler __f__am_cacheD;

			private static PlayHavenManager.PurchaseHandler __f__am_cacheE;

			private static PlayHavenManager.GeneralHandler __f__am_cacheF;

			private static PlayHavenManager.GeneralHandler __f__am_cache10;

			public int HashCode
			{
				get
				{
					return this.hashCode;
				}
			}

			public ContentRequester()
			{
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public ContentRequester(string placement)
			{
				this.mPlacement = placement;
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public ContentRequester(string contentID, string messageID)
			{
				this.mContentID = contentID;
				this.mMessageID = messageID;
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public void Send()
			{
				this.Send(false);
			}

			public void Send(bool showsOverlayImmediately)
			{
				if (Application.isEditor)
				{
					UnityEngine.Debug.Log("PlayHaven: content request (" + this.mPlacement + ")");
					Hashtable hashtable = new Hashtable();
					hashtable["notification"] = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					hashtable2["data"] = hashtable;
					hashtable2["hash"] = this.hashCode;
					hashtable2["name"] = "dismiss";
					string json = hashtable2.toJson();
					PlayHavenManager.Instance.HandleNativeEvent(json);
				}
				else
				{
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log(string.Format("PlayHaven: content request (id={0}, placement={1})", this.hashCode, this.mPlacement));
					}
					PlayHavenManager.obj_PlayHavenFacade.Call("contentRequest", new object[]
					{
						this.hashCode,
						this.mPlacement
					});
				}
			}

			public void TriggerEvent(string eventName, Hashtable eventData)
			{
				if (string.Compare(eventName, "reward") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Reward unlocked");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnReward(this, eventData);
				}
				else if (string.Compare(eventName, "purchasePresentation") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Purchase presented");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnPurchasePresented(this, eventData);
				}
				else if (string.Compare(eventName, "dismiss") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Content was dismissed!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
				//this.OnDismiss(this, eventData);
				}
				else if (string.Compare(eventName, "willdisplay") == 0)
				{
				//this.OnWillDisplay(this);
				}
				else if (string.Compare(eventName, "diddisplay") == 0)
				{
				//this.OnDidDisplay(this);
				}
				else if (string.Compare(eventName, "error") == 0)
				{
					UnityEngine.Debug.LogError("PlayHaven: Content error!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
				///	this.OnError(this, eventData);
				}
			}
		}

		internal class ContentPreloadRequester : PlayHavenManager.IPlayHavenRequest
		{
			protected string mPlacement;

			private int hashCode;

			private static PlayHavenManager.SuccessHandler __f__am_cache9;

			private static PlayHavenManager.DismissHandler __f__am_cacheA;

			private static PlayHavenManager.ErrorHandler __f__am_cacheB;

			public int HashCode
			{
				get
				{
					return this.hashCode;
				}
			}

			public ContentPreloadRequester(string placement)
			{
				this.mPlacement = placement;
				this.hashCode = this.GetHashCode();
				PlayHavenManager.sRequests.Add(this.hashCode, this);
			}

			public void Send()
			{
				this.Send(false);
			}

			public void Send(bool showsOverlayImmediately)
			{
				if (Application.isEditor)
				{
					UnityEngine.Debug.Log("PlayHaven: content preload request (" + this.mPlacement + ")");
					Hashtable hashtable = new Hashtable();
					hashtable["notification"] = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					hashtable2["data"] = hashtable;
					hashtable2["hash"] = this.hashCode;
					hashtable2["name"] = "dismiss";
					string json = hashtable2.toJson();
					PlayHavenManager.Instance.HandleNativeEvent(json);
				}
				else
				{
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log(string.Format("PlayHaven: content preload request (id={0}, placement={1})", this.hashCode, this.mPlacement));
					}
					PlayHavenManager.obj_PlayHavenFacade.Call("preloadRequest", new object[]
					{
						this.hashCode,
						this.mPlacement
					});
				}
			}

			public void TriggerEvent(string eventName, Hashtable eventData)
			{
				if (string.Compare(eventName, "gotcontent") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Preloaded content");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnSuccess(this, eventData);
				}
				else if (string.Compare(eventName, "error") == 0)
				{
					UnityEngine.Debug.LogError("PlayHaven: Content error!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
					//this.OnError(this, eventData);
				}
				else if (string.Compare(eventName, "dismiss") == 0)
				{
					UnityEngine.Debug.Log("PlayHaven: Content was dismissed!");
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log("JSON (trigger event): " + eventData.toJson());
					}
				//	this.OnDismiss(this, eventData);
				}
			}
		}

		public delegate void SuccessHandler(PlayHavenManager.IPlayHavenRequest request, Hashtable responseData);

		public delegate void ErrorHandler(PlayHavenManager.IPlayHavenRequest request, Hashtable errorData);

		public delegate void RewardHandler(PlayHavenManager.IPlayHavenRequest request, Hashtable rewardData);

		public delegate void PurchaseHandler(PlayHavenManager.IPlayHavenRequest request, Hashtable purchaseData);

		public delegate void DismissHandler(PlayHavenManager.IPlayHavenRequest request, Hashtable dismissData);

		public delegate void GeneralHandler(PlayHavenManager.IPlayHavenRequest request);

		public delegate void CancelRequestHandler(int requestId);

		public delegate void APNSDidRegisterHandler();

		public delegate void APNSDidFailToRegisterHandler(int code, string description);

		public delegate bool OpenUrlFromRemoteNotificationHandler(string url);

		public delegate bool OpenContentFromRemoteNotificationHandler(int hash);

		public const int NO_HASH_CODE = 0;

		public static string KEY_LAUNCH_COUNT = "playhaven-launch-count";

		public static string kPHMessageIDKey = "mi";

		public static string kPHContentIDKey = "ci";

		public static string kPHURIKey = "uri";

		public bool paused;

		public string token = string.Empty;

		public string secret = string.Empty;

		public string tokenAndroid = string.Empty;

		public string secretAndroid = string.Empty;

		public bool enableAndroidPushNotifications;

		public string googleCloudMessagingProjectNumber = string.Empty;

		public bool enableApplePushNotifications;

		public PlayHavenManager.WhenToRegisterForPushNotifications whenToRegisterForPush = PlayHavenManager.WhenToRegisterForPushNotifications.Manual;

		public bool doNotDestroyOnLoad = true;

		public bool defaultShowsOverlayImmediately;

		public bool maskShowsOverlayImmediately;

		public PlayHavenManager.WhenToOpen whenToSendOpen;

		public PlayHavenManager.WhenToGetNotifications whenToGetNotifications = PlayHavenManager.WhenToGetNotifications.Start;

		public string badgeMoreGamesPlacement = "more_games";

		public float notificationPollDelay = 1f;

		public float notificationPollRate = 15f;

		public bool cancelAllOnLevelLoad;

		public int suppressContentRequestsForLaunches;

		public string[] suppressedPlacements;

		public string[] suppressionExceptions;

		public bool showContentUnitsInEditor = true;

		public bool maskNetworkReachable;

		[HideInInspector]
		public bool lockToken;

		[HideInInspector]
		public bool lockSecret;

		[HideInInspector]
		public bool lockTokenAndroid;

		[HideInInspector]
		public bool lockSecretAndroid;

		private static PlayHavenManager _instance;

		private int launchCount;

		private string badge = string.Empty;

		private string customUDID = string.Empty;

		private static bool wasWarned;

		private bool optOutStatus;

		private ArrayList requestsInProgress = new ArrayList(8);

		private static Hashtable sRequests = new Hashtable();

		private bool networkReachable = true;

		private static AndroidJavaObject obj_PlayHavenFacade;

































		public event RequestCompletedHandler OnRequestCompleted;

		public event BadgeUpdateHandler OnBadgeUpdate;

		public event RewardTriggerHandler OnRewardGiven;



		public event WillDisplayContentHandler OnWillDisplayContent;

		public event DidDisplayContentHandler OnDidDisplayContent;

		public event PlayHaven.SuccessHandler OnSuccessOpenRequest;

		public event PlayHaven.SuccessHandler OnSuccessPreloadRequest;

		public event PlayHaven.DismissHandler OnDismissContent;

		public event PlayHaven.ErrorHandler OnErrorOpenRequest;

		public event PlayHaven.ErrorHandler OnErrorContentRequest;

		public event PlayHaven.ErrorHandler OnErrorMetadataRequest;

		public event PlayHavenManager.CancelRequestHandler OnSuccessCancelRequest;

		public event PlayHavenManager.CancelRequestHandler OnErrorCancelRequest;

		[Obsolete("Make Cross Promotion requests with ContentRequest() and attach to the OnDismissContent event instead.", false)]
		public event SimpleDismissHandler OnDismissCrossPromotionWidget;

		[Obsolete("Make Cross Promotion requests with ContentRequest() and attach to the OnDismissContent event instead.", false)]
		public event PlayHaven.ErrorHandler OnErrorCrossPromotionWidget;

		public static PlayHavenManager Instance
		{
			get
			{
				if (!PlayHavenManager._instance)
				{
					PlayHavenManager._instance = PlayHavenManager.FindInstance();
				}
				return PlayHavenManager._instance;
			}
		}

		[Obsolete("The lower case version of 'instance' is obsolete. Use the uppercase version 'Instance' instead.", false)]
		public static PlayHavenManager instance
		{
			get
			{
				if (!PlayHavenManager._instance)
				{
					PlayHavenManager._instance = PlayHavenManager.FindInstance();
				}
				return PlayHavenManager._instance;
			}
		}

		public string CustomUDID
		{
			get
			{
				return this.customUDID;
			}
			set
			{
				this.customUDID = value;
			}
		}

		public bool OptOutStatus
		{
			get
			{
				if (Application.isEditor)
				{
					return this.optOutStatus;
				}
				return PlayHavenManager.obj_PlayHavenFacade.Call<bool>("getOptOut", new object[0]);
			}
			set
			{
				if (Application.isEditor)
				{
					this.optOutStatus = value;
				}
				PlayHavenManager.obj_PlayHavenFacade.Call("setOptOut", new object[]
				{
					value
				});
			}
		}

		public string Badge
		{
			get
			{
				return this.badge;
			}
		}

		private void Awake()
		{
			base.gameObject.name = "PlayHavenManager";
			PlayHavenManager._instance = PlayHavenManager.FindInstance();
			this.DetectNetworkReachable();
			if (this.doNotDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
			if (this.suppressContentRequestsForLaunches > 0)
			{
				this.launchCount = PlayerPrefs.GetInt(PlayHavenManager.KEY_LAUNCH_COUNT, 0);
				this.launchCount++;
				PlayerPrefs.SetInt(PlayHavenManager.KEY_LAUNCH_COUNT, this.launchCount);
				PlayerPrefs.Save();
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log("Launch count: " + this.launchCount);
				}
			}
			this.InitializeAndroid();
			if (string.IsNullOrEmpty(this.tokenAndroid))
			{
				UnityEngine.Debug.LogError("PlayHaven token has not been specified in the PlayerHavenManager");
			}
			if (string.IsNullOrEmpty(this.secretAndroid))
			{
				UnityEngine.Debug.LogError("PlayHaven secret has not been specified in the PlayerHavenManager");
			}
			if (this.whenToSendOpen == PlayHavenManager.WhenToOpen.Awake)
			{
				this.OpenNotification();
			}
			if (this.whenToGetNotifications == PlayHavenManager.WhenToGetNotifications.Awake)
			{
				this.BadgeRequest(this.badgeMoreGamesPlacement);
			}
			if (this.whenToRegisterForPush == PlayHavenManager.WhenToRegisterForPushNotifications.Awake)
			{
				this.RegisterForPushNotifications();
			}
		}

		private void OnEnable()
		{
			if (this.whenToGetNotifications == PlayHavenManager.WhenToGetNotifications.OnEnable)
			{
				this.BadgeRequest(this.badgeMoreGamesPlacement);
			}
			if (this.whenToRegisterForPush == PlayHavenManager.WhenToRegisterForPushNotifications.OnEnable)
			{
				this.RegisterForPushNotifications();
			}
		}

		private void Start()
		{
			if (this.whenToSendOpen == PlayHavenManager.WhenToOpen.Start)
			{
				this.OpenNotification();
			}
			this.ParseIntent();
			if (this.whenToGetNotifications == PlayHavenManager.WhenToGetNotifications.Start)
			{
				this.BadgeRequest(this.badgeMoreGamesPlacement);
			}
			else if (this.whenToGetNotifications == PlayHavenManager.WhenToGetNotifications.Poll)
			{
				this.PollForBadgeRequests();
			}
			if (this.whenToRegisterForPush == PlayHavenManager.WhenToRegisterForPushNotifications.Start)
			{
				this.RegisterForPushNotifications();
			}
		}

		private void OnApplicationPause(bool pause)
		{
			if (!pause)
			{
				this.ParseIntent();
			}
		}

		private void OnApplicationQuit()
		{
			if (Application.platform == RuntimePlatform.Android && PlayHavenManager.obj_PlayHavenFacade != null)
			{
				PlayHavenManager.obj_PlayHavenFacade.Dispose();
			}
		}

		private void ParseIntent()
		{
			string a = string.Empty;
			string text = string.Empty;
			if (Application.platform != RuntimePlatform.Android)
			{
				return;
			}
			if (PlayHavenManager.obj_PlayHavenFacade == null)
			{
				return;
			}
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.LogError("PlayHaven ParseIntent, checking for intent data.");
			}
			text = PlayHavenManager.obj_PlayHavenFacade.Call<string>("getCurrentIntentData", new object[0]);
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.Log("received: " + text);
			}
			if (text == null)
			{
				return;
			}
			ArrayList arrayList = MiniJSON.jsonDecode(text) as ArrayList;
			foreach (Hashtable hashtable in arrayList)
			{
				if (hashtable.ContainsKey("type"))
				{
					a = hashtable["type"].ToString();
					if (a == "reward")
					{
						try
						{
							Reward reward = new Reward();
							reward.name = hashtable["name"].ToString();
							reward.quantity = int.Parse(hashtable["quantity"].ToString());
							reward.receipt = hashtable["receipt"].ToString();
							if (this.OnRewardGiven != null)
							{
								this.OnRewardGiven(0, reward);
							}
						}
						catch (Exception ex)
						{
							if (Debug.isDebugBuild)
							{
								UnityEngine.Debug.Log(ex.Message);
							}
						}
					}
					else if (!(a == "purchase"))
					{
						if (!(a == "optin"))
						{
							if (a == "uri" && UnityEngine.Debug.isDebugBuild)
							{
								UnityEngine.Debug.Log("Deep linking detected: " + hashtable["uri"]);
							}
						}
					}
				}
			}
		}

		private void OnLevelWasLoaded(int level)
		{
			if (this.cancelAllOnLevelLoad)
			{
				this.CancelAllPendingRequests();
			}
		}

		public bool IsPlacementSuppressed(string placement)
		{
			if (this.suppressContentRequestsForLaunches <= 0 || this.launchCount >= this.suppressContentRequestsForLaunches)
			{
				return false;
			}
			if (this.suppressedPlacements != null && this.suppressedPlacements.Length > 0)
			{
				string[] array = this.suppressedPlacements;
				for (int i = 0; i < array.Length; i++)
				{
					string a = array[i];
					if (a == placement)
					{
						return true;
					}
				}
				return false;
			}
			if (this.suppressionExceptions != null && this.suppressionExceptions.Length > 0)
			{
				string[] array2 = this.suppressionExceptions;
				for (int j = 0; j < array2.Length; j++)
				{
					string a2 = array2[j];
					if (a2 == placement)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		public void SetKeys(string token, string secret)
		{
			this.SetKeys(token, secret, this.googleCloudMessagingProjectNumber);
		}

		public void SetKeys(string token, string secret, string googleCloudMessagingProjectNumber)
		{
			this.googleCloudMessagingProjectNumber = googleCloudMessagingProjectNumber;
			this.tokenAndroid = token;
			this.secretAndroid = secret;
			if (Application.platform == RuntimePlatform.Android && PlayHavenManager.obj_PlayHavenFacade != null)
			{
				PlayHavenManager.obj_PlayHavenFacade.Call("setKeys", new object[]
				{
					token,
					secret,
					(!this.enableAndroidPushNotifications) ? string.Empty : googleCloudMessagingProjectNumber
				});
			}
		}

		[Obsolete("This method is now obsolete and performs no action", false)]
		public void RegisterActivityForTracking(bool register)
		{
		}

		public int Open()
		{
			return this.SendRequest(RequestType.Open, string.Empty);
		}

		public int Open(string customUDID)
		{
			return this.SendRequest(RequestType.Open, customUDID);
		}

		public int OpenNotification(string customUDID)
		{
			if (this.networkReachable)
			{
				this.CustomUDID = customUDID;
				int num = this.Open(customUDID);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public int OpenNotification()
		{
			if (this.networkReachable)
			{
				int num = this.Open(this.CustomUDID);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public void CancelRequest(int requestId)
		{
		}

		public void CancelAllPendingRequests()
		{
			foreach (int requestId in this.requestsInProgress)
			{
				this.CancelRequest(requestId);
			}
			this.requestsInProgress.Clear();
		}

		[Obsolete("ProductPurchaseResolutionRequest no longer performs any action on Android; use ProductPurchaseTrackingRequest instead.", false)]
		public void ProductPurchaseResolutionRequest(PurchaseResolution resolution)
		{
			this.SendProductPurchaseResolution(resolution);
		}

		public void ProductPurchaseTrackingRequest(Purchase purchase, PurchaseResolution resolution)
		{
			this.SendIAPTrackingRequest(purchase, resolution, null);
		}

		public void ProductPurchaseTrackingRequest(Purchase purchase, PurchaseResolution resolution, byte[] receiptData)
		{
			this.SendIAPTrackingRequest(purchase, resolution, receiptData);
		}

		public int ContentPreloadRequest(string placement)
		{
			if (this.networkReachable)
			{
				int num = this.SendRequest(RequestType.Preload, placement);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public int ContentRequest(string placement)
		{
			if (this.IsPlacementSuppressed(placement))
			{
				return 0;
			}
			if (this.networkReachable)
			{
				int num = this.SendRequest(RequestType.Content, placement, this.defaultShowsOverlayImmediately);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public int ContentRequest(string placement, bool showsOverlayImmediately)
		{
			if (this.IsPlacementSuppressed(placement))
			{
				return 0;
			}
			if (this.networkReachable)
			{
				int num = this.SendRequest(RequestType.Content, placement, showsOverlayImmediately && !this.maskShowsOverlayImmediately);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public int ContentRequest(int requestId)
		{
			if (this.networkReachable)
			{
				this.SendRequestForHashCode(requestId, this.defaultShowsOverlayImmediately);
				this.requestsInProgress.Add(requestId);
				return requestId;
			}
			return 0;
		}

		public int ContentRequest(int requestId, bool showsOverlayImmediately)
		{
			if (this.networkReachable)
			{
				this.SendRequestForHashCode(requestId, showsOverlayImmediately && !this.maskShowsOverlayImmediately);
				this.requestsInProgress.Add(requestId);
				return requestId;
			}
			return 0;
		}

		[Obsolete("This method is obsolete; it assumes that you will have a placement called more_games; instead, simply use ContentRequest() but with the relevant placement.", false)]
		public int ShowCrossPromotionWidget()
		{
			if (this.networkReachable)
			{
				int num = this.SendRequest(RequestType.CrossPromotionWidget, string.Empty, this.defaultShowsOverlayImmediately);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		public int BadgeRequest(string placement)
		{
			if (this.networkReachable && this.whenToGetNotifications != PlayHavenManager.WhenToGetNotifications.Disabled)
			{
				int num = this.SendRequest(RequestType.Metadata, placement);
				this.requestsInProgress.Add(num);
				return num;
			}
			return 0;
		}

		[Obsolete("This method is obsolete; it assumes that you will have a placement called more_games; instead, simply use BadgeRequest() but with the relevant placement.", false)]
		public int BadgeRequest()
		{
			return this.BadgeRequest("more_games");
		}

		public void PollForBadgeRequests()
		{
			base.CancelInvoke("BadgeRequestPolled");
			if (this.notificationPollRate > 0f)
			{
				if (!string.IsNullOrEmpty(this.badgeMoreGamesPlacement))
				{
					base.InvokeRepeating("BadgeRequestPolled", this.notificationPollDelay, this.notificationPollRate);
				}
				else if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.LogError("A more games badge placement is not defined.");
				}
			}
			else if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.LogError("cannot have a notification poll rate <= 0");
			}
		}

		public void StopPollingForBadgeRequests()
		{
			base.CancelInvoke("BadgeRequestPolled");
		}

		public void ClearBadge()
		{
			this.badge = string.Empty;
		}

		public void RegisterForPushNotifications()
		{
			if (this.whenToRegisterForPush != PlayHavenManager.WhenToRegisterForPushNotifications.Disabled)
			{
				if (this.enableAndroidPushNotifications && Application.platform == RuntimePlatform.Android)
				{
					PlayHavenManager.obj_PlayHavenFacade.Call("registerForPushNotifications", new object[0]);
				}
			}
			else if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.Log("Registering for push notifications: DISABLED");
			}
		}

		public void DeregisterFromPushNofications()
		{
			if (this.enableAndroidPushNotifications && Application.platform == RuntimePlatform.Android)
			{
				PlayHavenManager.obj_PlayHavenFacade.Call("deregisterFromPushNofications", new object[0]);
			}
		}

		public static void NativeLog(string message)
		{
		}

		private void HandleCrossPromotionWidgetRequestOnDismiss(PlayHavenManager.IPlayHavenRequest request, Hashtable dismissData)
		{
			if (this.OnDismissCrossPromotionWidget != null)
			{
				this.OnDismissCrossPromotionWidget();
			}
		}

		private void HandleCrossPromotionWidgetRequestOnWillDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			this.NotifyRequestCompleted(request.HashCode);
			if (this.OnWillDisplayContent != null)
			{
				this.OnWillDisplayContent(request.HashCode);
			}
		}

		private void HandleCrossPromotionWidgetRequestOnDidDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			if (this.OnDidDisplayContent != null)
			{
				this.OnDidDisplayContent(request.HashCode);
			}
		}

		private void HandleCrossPromotionWidgetRequestOnError(PlayHavenManager.IPlayHavenRequest request, Hashtable errorData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			Error error = this.CreateErrorFromJSON(errorData);
			if (this.OnErrorCrossPromotionWidget != null)
			{
				this.OnErrorCrossPromotionWidget(request.HashCode, error);
			}
		}

		private void HandleContentRequestOnDismiss(PlayHavenManager.IPlayHavenRequest request, Hashtable dismissData)
		{
			DismissType dismissType = DismissType.Unknown;
			try
			{
				string value = dismissData["type"].ToString();
				dismissType = (DismissType)((int)Enum.Parse(typeof(DismissType), value));
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex.Message);
				}
			}
			if (this.OnDismissContent != null)
			{
				this.OnDismissContent(request.HashCode, dismissType);
			}
		}

		private void HandleContentRequestOnWillDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			this.NotifyRequestCompleted(request.HashCode);
			if (this.OnWillDisplayContent != null)
			{
				this.OnWillDisplayContent(request.HashCode);
			}
		}

		private void HandleContentRequestOnDidDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			if (this.OnDidDisplayContent != null)
			{
				this.OnDidDisplayContent(request.HashCode);
			}
		}

		private void HandleContentRequestOnReward(PlayHavenManager.IPlayHavenRequest request, Hashtable rewardData)
		{
			Reward reward = new Reward();
			try
			{
				reward.receipt = rewardData["receipt"].ToString();
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex.Message);
				}
			}
			try
			{
				reward.name = rewardData["name"].ToString();
				reward.quantity = int.Parse(rewardData["quantity"].ToString());
				if (this.OnRewardGiven != null)
				{
					this.OnRewardGiven(request.HashCode, reward);
				}
			}
			catch (Exception ex2)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex2.Message);
				}
			}
		}

		private void HandleRequestOnPurchasePresented(PlayHavenManager.IPlayHavenRequest request, Hashtable purchaseData)
		{
			Purchase purchase = new Purchase();
			try
			{
				purchase.receipt = purchaseData["receipt"].ToString();
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex.Message);
				}
			}
			try
			{
				purchase.productIdentifier = purchaseData["productIdentifier"].ToString();
				purchase.quantity = int.Parse(purchaseData["quantity"].ToString());
				if (purchaseData["price"] != null)
				{
					purchase.price = double.Parse(purchaseData["price"].ToString());
				}
				if (purchaseData["store"] != null)
				{
					purchase.store = purchaseData["store"].ToString();
				}
			//if (this.OnPurchasePresented != null)
				{
				//this.OnPurchasePresented(request.HashCode, purchase);
				}
			}
			catch (Exception ex2)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex2.Message);
				}
			}
		}

		private void HandleContentRequestOnError(PlayHavenManager.IPlayHavenRequest request, Hashtable errorData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			Error error = this.CreateErrorFromJSON(errorData);
			if (this.OnErrorContentRequest != null)
			{
				this.OnErrorContentRequest(request.HashCode, error);
			}
		}

		private void HandlePreloadRequestOnDismiss(int requestId, DismissType dismissType)
		{
			this.NotifyRequestCompleted(requestId);
		}

		private void HandleMetadataRequestOnError(PlayHavenManager.IPlayHavenRequest request, Hashtable errorData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			Error error = this.CreateErrorFromJSON(errorData);
			if (this.OnErrorMetadataRequest != null)
			{
				this.OnErrorMetadataRequest(request.HashCode, error);
			}
		}

		private void HandleMetadataRequestOnSuccess(PlayHavenManager.IPlayHavenRequest request, Hashtable responseData)
		{
			string a = string.Empty;
			try
			{
				if (responseData.ContainsKey("notification"))
				{
					Hashtable hashtable = responseData["notification"] as Hashtable;
					a = hashtable["type"].ToString();
				}
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex.Message);
				}
				a = string.Empty;
			}
			if (a == "badge")
			{
				try
				{
					Hashtable hashtable2 = responseData["notification"] as Hashtable;
					this.badge = hashtable2["value"].ToString();
					if (this.OnBadgeUpdate != null)
					{
						this.OnBadgeUpdate(request.HashCode, this.badge);
					}
				}
				catch (Exception ex2)
				{
					if (Debug.isDebugBuild)
					{
						UnityEngine.Debug.Log(ex2.Message);
					}
				}
			}
		}

		private void HandleMetadataRequestOnWillDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			this.NotifyRequestCompleted(request.HashCode);
			if (this.OnWillDisplayContent != null)
			{
				this.OnWillDisplayContent(request.HashCode);
			}
		}

		private void HandleMetadataRequestOnDidDisplay(PlayHavenManager.IPlayHavenRequest request)
		{
			if (this.OnDidDisplayContent != null)
			{
				this.OnDidDisplayContent(request.HashCode);
			}
		}

		private void HandleOpenRequestOnError(PlayHavenManager.IPlayHavenRequest request, Hashtable errorData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			Error error = this.CreateErrorFromJSON(errorData);
			if (this.OnErrorOpenRequest != null)
			{
				this.OnErrorOpenRequest(request.HashCode, error);
			}
		}

		private void HandleOpenRequestOnSuccess(PlayHavenManager.IPlayHavenRequest request, Hashtable responseData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			if (this.OnSuccessOpenRequest != null)
			{
				this.OnSuccessOpenRequest(request.HashCode);
			}
		}

		private void HandlePreloadRequestOnSuccess(PlayHavenManager.IPlayHavenRequest request, Hashtable responseData)
		{
			this.NotifyRequestCompleted(request.HashCode);
			if (this.OnSuccessPreloadRequest != null)
			{
				this.OnSuccessPreloadRequest(request.HashCode);
			}
		}

		private void InitializeAndroid()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						PlayHavenManager.obj_PlayHavenFacade = new AndroidJavaObject("com.playhaven.unity3d.PlayHavenFacade", new object[]
						{
							@static,
							this.tokenAndroid,
							this.secretAndroid,
							(!this.enableAndroidPushNotifications) ? string.Empty : this.googleCloudMessagingProjectNumber
						});
					}
				}
			}
		}

		private void DetectNetworkReachable()
		{
			this.networkReachable = (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork);
			this.networkReachable &= !this.maskNetworkReachable;
		}

		private static PlayHavenManager FindInstance()
		{
			PlayHavenManager playHavenManager = UnityEngine.Object.FindObjectOfType(typeof(PlayHavenManager)) as PlayHavenManager;
			if (!playHavenManager)
			{
				GameObject gameObject = GameObject.Find("PlayHavenManager");
				if (gameObject != null)
				{
					playHavenManager = gameObject.GetComponent<PlayHavenManager>();
				}
			}
			if (!playHavenManager && !PlayHavenManager.wasWarned)
			{
				UnityEngine.Debug.LogWarning("unable to locate a PlayHavenManager in the scene");
				PlayHavenManager.wasWarned = true;
			}
			return playHavenManager;
		}

		private void SendProductPurchaseResolution(PurchaseResolution resolution)
		{
			if (!Application.isEditor)
			{
			}
		}

		private void SendIAPTrackingRequest(Purchase purchase, PurchaseResolution resolution, byte[] receiptData)
		{
			if (!Application.isEditor)
			{
				PlayHavenManager.obj_PlayHavenFacade.Call("iapTrackingRequest", new object[]
				{
					purchase.productIdentifier,
					purchase.orderId,
					purchase.quantity,
					purchase.price,
					purchase.store,
					(int)resolution
				});
			}
		}

		private void SendRequestForHashCode(int hash, bool showsOverlayImmediately)
		{
			if (PlayHavenManager.sRequests.ContainsKey(hash))
			{
				PlayHavenManager.ContentRequester contentRequester = (PlayHavenManager.ContentRequester)PlayHavenManager.sRequests[hash];
				contentRequester.Send(showsOverlayImmediately);
			}
		}

		private int SendRequest(RequestType type, string placement)
		{
			return this.SendRequest(type, placement, false);
		}

		private int SendRequest(RequestType type, string placement, bool showsOverlayImmediately)
		{
			PlayHavenManager.IPlayHavenRequest playHavenRequest = null;
			switch (type)
			{
			case RequestType.Open:
				playHavenRequest = new PlayHavenManager.OpenRequest(placement);
			//	playHavenRequest.OnSuccess += new PlayHavenManager.SuccessHandler(this.HandleOpenRequestOnSuccess);
			//	playHavenRequest.OnError += new PlayHavenManager.ErrorHandler(this.HandleOpenRequestOnError);
				break;
			case RequestType.Metadata:
				playHavenRequest = new PlayHavenManager.MetadataRequest(placement);
			//	playHavenRequest.OnSuccess += new PlayHavenManager.SuccessHandler(this.HandleMetadataRequestOnSuccess);
			/////	playHavenRequest.OnError += new PlayHavenManager.ErrorHandler(this.HandleMetadataRequestOnError);
			//	playHavenRequest.OnWillDisplay += new PlayHavenManager.GeneralHandler(this.HandleMetadataRequestOnWillDisplay);
			//	playHavenRequest.OnDidDisplay += new PlayHavenManager.GeneralHandler(this.HandleMetadataRequestOnDidDisplay);
				break;
			case RequestType.Content:
				playHavenRequest = new PlayHavenManager.ContentRequester(placement);
			//playHavenRequest.OnError += new PlayHavenManager.ErrorHandler(this.HandleContentRequestOnError);
			//	playHavenRequest.OnDismiss += new PlayHavenManager.DismissHandler(this.HandleContentRequestOnDismiss);
			//	playHavenRequest.OnReward += new PlayHavenManager.RewardHandler(this.HandleContentRequestOnReward);
			//	playHavenRequest.OnPurchasePresented += new PlayHavenManager.PurchaseHandler(this.HandleRequestOnPurchasePresented);
			///	playHavenRequest.OnWillDisplay += new PlayHavenManager.GeneralHandler(this.HandleContentRequestOnWillDisplay);
			//	playHavenRequest.OnDidDisplay += new PlayHavenManager.GeneralHandler(this.HandleContentRequestOnDidDisplay);
				break;
			case RequestType.Preload:
				playHavenRequest = new PlayHavenManager.ContentPreloadRequester(placement);
			//	playHavenRequest.OnError += new PlayHavenManager.ErrorHandler(this.HandleContentRequestOnError);
			//	playHavenRequest.OnSuccess += new PlayHavenManager.SuccessHandler(this.HandlePreloadRequestOnSuccess);
			//	playHavenRequest.OnDismiss += new PlayHavenManager.DismissHandler(this.HandleContentRequestOnDismiss);
				break;
			case RequestType.CrossPromotionWidget:
				playHavenRequest = new PlayHavenManager.ContentRequester("more_games");
			//	playHavenRequest.OnError += new PlayHavenManager.ErrorHandler(this.HandleCrossPromotionWidgetRequestOnError);
			//	playHavenRequest.OnDismiss += new PlayHavenManager.DismissHandler(this.HandleCrossPromotionWidgetRequestOnDismiss);
			//	playHavenRequest.OnWillDisplay += new PlayHavenManager.GeneralHandler(this.HandleCrossPromotionWidgetRequestOnWillDisplay);
			//	playHavenRequest.OnDidDisplay += new PlayHavenManager.GeneralHandler(this.HandleCrossPromotionWidgetRequestOnDidDisplay);
				break;
			}
			if (playHavenRequest != null)
			{
				playHavenRequest.Send(showsOverlayImmediately);
				return playHavenRequest.HashCode;
			}
			return 0;
		}

		private PlayHavenManager.IPlayHavenRequest GetRequestWithHash(int hash)
		{
			if (PlayHavenManager.sRequests.ContainsKey(hash))
			{
				return (PlayHavenManager.IPlayHavenRequest)PlayHavenManager.sRequests[hash];
			}
			return null;
		}

		private void ClearRequestWithHash(int hash)
		{
			if (PlayHavenManager.sRequests.ContainsKey(hash))
			{
				PlayHavenManager.sRequests.Remove(hash);
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(string.Format("Cleared request (id={0})", hash));
				}
			}
		}

		private Error CreateErrorFromJSON(Hashtable errorData)
		{
			Error error = new Error();
			try
			{
				error.code = int.Parse(errorData["code"].ToString());
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex.Message);
				}
			}
			try
			{
				error.description = errorData["description"].ToString();
			}
			catch (Exception ex2)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(ex2.Message);
				}
			}
			return error;
		}

		private void NotifyRequestCompleted(int requestId)
		{
			this.requestsInProgress.Remove(requestId);
			if (this.OnRequestCompleted != null)
			{
				this.OnRequestCompleted(requestId);
			}
		}

		private void BadgeRequestPolled()
		{
			this.BadgeRequest(this.badgeMoreGamesPlacement);
		}

		public void HandleNativeAPNSEvent(string json)
		{
		}

		public void HandleNativeEvent(string json)
		{
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.Log("JSON (native event): " + json);
			}
			Hashtable hashtable = MiniJSON.jsonDecode(json) as Hashtable;
			int num = int.Parse(hashtable["hash"].ToString());
			string text = hashtable["name"].ToString();
			Hashtable eventData = hashtable["data"] as Hashtable;
			PlayHavenManager.IPlayHavenRequest requestWithHash = this.GetRequestWithHash(num);
			if (requestWithHash != null)
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.Log(string.Format("PlayHaven event={0} (id={1})", text, num));
				}
				requestWithHash.TriggerEvent(text, eventData);
				if (text == "dismiss" || text == "error")
				{
					this.ClearRequestWithHash(num);
				}
			}
			else if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.LogError("Unable to locate request with id=" + num);
			}
		}

		private void RequestCancelSuccess(string hashCodeString)
		{
			int num = Convert.ToInt32(hashCodeString);
			this.ClearRequestWithHash(num);
			if (this.OnSuccessCancelRequest != null)
			{
				this.OnSuccessCancelRequest(num);
			}
		}

		public void RequestCancelFailed(string hashCodeString)
		{
			if (this.OnErrorCancelRequest != null)
			{
				int requestId = Convert.ToInt32(hashCodeString);
				this.OnErrorCancelRequest(requestId);
			}
		}
	}
}
*/