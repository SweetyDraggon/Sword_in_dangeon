/*
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayHaven
{
	[AddComponentMenu("PlayHaven/VGP Handler")]
	public class PlayHavenVGPHandler : MonoBehaviour
	{
		public delegate void PurchaseEventHandler(int requestId, Purchase purchase);

		private static PlayHavenVGPHandler instance;

		private PlayHavenManager playHaven;

		private Hashtable purchases = new Hashtable(4);



		public event PlayHavenVGPHandler.PurchaseEventHandler OnPurchasePresented;

		public static PlayHavenVGPHandler Instance
		{
			get
			{
				if (!PlayHavenVGPHandler.instance)
				{
					PlayHavenVGPHandler.instance = (UnityEngine.Object.FindObjectOfType(typeof(PlayHavenVGPHandler)) as PlayHavenVGPHandler);
				}
				return PlayHavenVGPHandler.instance;
			}
		}

		private void Awake()
		{
			this.playHaven = PlayHavenManager.Instance;
		}

		private void OnEnable()
		{
			//this.playHaven.OnPurchasePresented += new PurchasePresentedTriggerHandler(this.PlayHavenOnPurchasePresented);
		}

		private void OnDisable()
		{
		///	this.playHaven.OnPurchasePresented -= new PurchasePresentedTriggerHandler(this.PlayHavenOnPurchasePresented);
		}

		public void ResolvePurchase(int requestId, PurchaseResolution resolution, bool track)
		{
			this.ResolvePurchase(requestId, resolution, null, track);
		}

		public void ResolvePurchase(int requestId, PurchaseResolution resolution, byte[] receiptData, bool track)
		{
		}

		public void ResolvePurchase(Purchase purchase, PurchaseResolution resolution, bool track)
		{
			this.ResolvePurchase(purchase, resolution, null, track);
		}

		public void ResolvePurchase(Purchase purchase, PurchaseResolution resolution, byte[] receiptData, bool track)
		{
			if (!this.purchases.ContainsValue(purchase))
			{
				if (Debug.isDebugBuild)
				{
					UnityEngine.Debug.LogWarning("PlayHaven VGP handler does not have a record of a purchase with the provided purchase object; will track only if requested.");
				}
				if (track)
				{
					this.playHaven.ProductPurchaseTrackingRequest(purchase, resolution, receiptData);
				}
			}
			else
			{
				int num = -1;
				foreach (int num2 in this.purchases.Keys)
				{
					if (this.purchases[num2] == purchase)
					{
						num = num2;
						break;
					}
				}
				if (num > -1)
				{
					this.purchases.Remove(num);
					if (track)
					{
						this.playHaven.ProductPurchaseTrackingRequest(purchase, resolution, receiptData);
					}
				}
				else
				{
					UnityEngine.Debug.LogError("Unable to determine request identifier for provided purchase object.");
				}
			}
		}

		private void PlayHavenOnPurchasePresented(int requestId, Purchase purchase)
		{
			if (this.OnPurchasePresented != null)
			{
				this.purchases.Add(requestId, purchase);
				this.OnPurchasePresented(requestId, purchase);
			}
		}
	}
}
*/