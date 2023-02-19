using System;

namespace PlayHaven
{
	public class Error
	{
		public int code;

		public string description = string.Empty;

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"code: ",
				this.code,
				", description: ",
				this.description
			});
		}
	}
}
