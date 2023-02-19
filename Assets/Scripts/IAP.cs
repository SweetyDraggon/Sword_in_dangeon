using System;

[Serializable]
public class IAP
{
	public string productIdentifier;

	public int coins;

	public IAP(string sku, int amt)
	{
		this.productIdentifier = sku;
		this.coins = amt;
	}
}
