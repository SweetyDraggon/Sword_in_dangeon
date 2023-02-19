using System;

namespace PlayHaven
{
	public class Purchase
	{
		public string productIdentifier;

		public int quantity;

		public string receipt;

		public string orderId;

		public double price;

		public string store;

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"productIdentifier: ",
				this.productIdentifier,
				", quantity: ",
				this.quantity,
				", receipt: ",
				this.receipt
			});
		}
	}
}
