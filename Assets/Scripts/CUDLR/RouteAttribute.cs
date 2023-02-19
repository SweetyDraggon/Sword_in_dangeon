using System;
using System.Text.RegularExpressions;

namespace CUDLR
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RouteAttribute : Attribute
	{
		public delegate void Callback(RequestContext context);

		public Regex m_route;

		public Regex m_methods;

		public bool m_runOnMainThread;

		public RouteAttribute.Callback m_callback;

		public RouteAttribute(string route, string methods = "(GET|HEAD)", bool runOnMainThread = true)
		{
			this.m_route = new Regex(route, RegexOptions.IgnoreCase);
			this.m_methods = new Regex(methods);
			this.m_runOnMainThread = runOnMainThread;
		}
	}
}
