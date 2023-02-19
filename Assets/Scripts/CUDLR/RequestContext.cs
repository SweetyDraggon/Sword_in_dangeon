using System;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CUDLR
{
	public class RequestContext
	{
		public HttpListenerContext context;

		public Match match;

		public bool pass;

		public string path;

		public int currentRoute;

		public HttpListenerRequest Request
		{
			get
			{
				return this.context.Request;
			}
		}

		public HttpListenerResponse Response
		{
			get
			{
				return this.context.Response;
			}
		}

		public RequestContext(HttpListenerContext ctx)
		{
			this.context = ctx;
			this.match = null;
			this.pass = false;
			this.path = WWW.UnEscapeURL(this.context.Request.Url.AbsolutePath);
			if (this.path == "/")
			{
				this.path = "/index.html";
			}
			this.currentRoute = 0;
		}
	}
}
