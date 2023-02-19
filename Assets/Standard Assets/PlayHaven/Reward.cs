using System;

namespace PlayHaven
{
	public class Reward
	{
		public string receipt;

		public string name;

		public int quantity;

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"name: ",
				this.name,
				", quantity: ",
				this.quantity,
				", receipt: ",
				this.receipt
			});
		}
	}
}
