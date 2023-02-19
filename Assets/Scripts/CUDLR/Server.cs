using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace CUDLR
{
	public class Server : MonoBehaviour
	{
		public delegate void FileHandlerDelegate(RequestContext context, bool download);

		private sealed class _HandleRequests_c__Iterator0 : IDisposable, IEnumerator, IEnumerator<object>
		{
			internal RequestContext _context___0;

			internal Queue<RequestContext> __s_33___1;

			internal int _PC;

			internal object _current;

			internal Server __f__this;

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
					break;
				case 1u:
					break;
				default:
					return false;
				}
				IL_21:
				if (Server.mainRequests.Count != 0)
				{
					this._context___0 = null;
					this.__s_33___1 = Server.mainRequests;
					lock (this.__s_33___1)
					{
						this._context___0 = Server.mainRequests.Dequeue();
					}
					this.__f__this.HandleRequest(this._context___0);
					goto IL_21;
				}
				this._current = new WaitForEndOfFrame();
				this._PC = 1;
				return true;
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

		private sealed class _RegisterFileHandlers_c__AnonStorey8
		{
			internal Server.FileHandlerDelegate callback;

			internal void __m__2(RequestContext context)
			{
				this.callback(context, true);
			}

			internal void __m__3(RequestContext context)
			{
				this.callback(context, false);
			}
		}

		[SerializeField]
		public int Port = 55055;

		private static Thread mainThread;

		private static string fileRoot;

		private static HttpListener listener = new HttpListener();

		private static List<RouteAttribute> registeredRoutes;

		private static Queue<RequestContext> mainRequests = new Queue<RequestContext>();

		private static Dictionary<string, string> fileTypes = new Dictionary<string, string>
		{
			{
				"js",
				"application/javascript"
			},
			{
				"json",
				"application/json"
			},
			{
				"jpg",
				"image/jpeg"
			},
			{
				"jpeg",
				"image/jpeg"
			},
			{
				"gif",
				"image/gif"
			},
			{
				"png",
				"image/png"
			},
			{
				"css",
				"text/css"
			},
			{
				"htm",
				"text/html"
			},
			{
				"html",
				"text/html"
			},
			{
				"ico",
				"image/x-icon"
			}
		};

		private static Func<KeyValuePair<string, string>, string> __f__am_cache7;

		public virtual void Awake()
		{
			Server.mainThread = Thread.CurrentThread;
			Server.fileRoot = Path.Combine(Application.streamingAssetsPath, "CUDLR");
			this.RegisterRoutes();
			Server.RegisterFileHandlers();
			UnityEngine.Debug.Log("Starting CUDLR Server on port : " + this.Port);
			Server.listener.Prefixes.Add("http://*:" + this.Port + "/");
			Server.listener.Start();
			Server.listener.BeginGetContext(new AsyncCallback(this.ListenerCallback), null);
			base.StartCoroutine(this.HandleRequests());
		}

		private void RegisterRoutes()
		{
			Server.registeredRoutes = new List<RouteAttribute>();
			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				Type type = types[i];
				MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
				for (int j = 0; j < methods.Length; j++)
				{
					MethodInfo methodInfo = methods[j];
					RouteAttribute[] array = methodInfo.GetCustomAttributes(typeof(RouteAttribute), true) as RouteAttribute[];
					if (array.Length != 0)
					{
						RouteAttribute.Callback callback = Delegate.CreateDelegate(typeof(RouteAttribute.Callback), methodInfo, false) as RouteAttribute.Callback;
						if (callback == null)
						{
							UnityEngine.Debug.LogError(string.Format("Method {0}.{1} takes the wrong arguments for a console route.", type, methodInfo.Name));
						}
						else
						{
							RouteAttribute[] array2 = array;
							for (int k = 0; k < array2.Length; k++)
							{
								RouteAttribute routeAttribute = array2[k];
								if (routeAttribute.m_route == null)
								{
									UnityEngine.Debug.LogError(string.Format("Method {0}.{1} needs a valid route regexp.", type, methodInfo.Name));
								}
								else
								{
									routeAttribute.m_callback = callback;
									Server.registeredRoutes.Add(routeAttribute);
								}
							}
						}
					}
				}
			}
		}

		private static void FindFileType(RequestContext context, bool download, out string path, out string type)
		{
			path = Path.Combine(Server.fileRoot, context.match.Groups[1].Value);
			string key = Path.GetExtension(path).ToLower().TrimStart(new char[]
			{
				'.'
			});
			if (download || !Server.fileTypes.TryGetValue(key, out type))
			{
				type = "application/octet-stream";
			}
		}

		private static void WWWFileHandler(RequestContext context, bool download)
		{
			string text;
			string contentType;
			Server.FindFileType(context, download, out text, out contentType);
			WWW wWW = new WWW(text);
			while (!wWW.isDone)
			{
				Thread.Sleep(0);
			}
			if (string.IsNullOrEmpty(wWW.error))
			{
				context.Response.ContentType = contentType;
				if (download)
				{
					context.Response.AddHeader("Content-disposition", string.Format("attachment; filename={0}", Path.GetFileName(text)));
				}
				context.Response.WriteBytes(wWW.bytes);
				return;
			}
			if (wWW.error.StartsWith("Couldn't open file"))
			{
				context.pass = true;
			}
			else
			{
				context.Response.StatusCode = 500;
				context.Response.StatusDescription = string.Format("Fatal error:\n{0}", wWW.error);
			}
		}

		private static void FileHandler(RequestContext context, bool download)
		{
			string path;
			string type;
			Server.FindFileType(context, download, out path, out type);
			if (File.Exists(path))
			{
				context.Response.WriteFile(path, type, download);
			}
			else
			{
				context.pass = true;
			}
		}

		private static void RegisterFileHandlers()
		{
			string arg = string.Format("({0})", string.Join("|", (from x in Server.fileTypes
			select x.Key).ToArray<string>()));
			RouteAttribute routeAttribute = new RouteAttribute(string.Format("^/download/(.*\\.{0})$", arg), "(GET|HEAD)", true);
			RouteAttribute routeAttribute2 = new RouteAttribute(string.Format("^/(.*\\.{0})$", arg), "(GET|HEAD)", true);
			bool flag = Server.fileRoot.Contains("://");
			routeAttribute.m_runOnMainThread = flag;
			routeAttribute2.m_runOnMainThread = flag;
			Server.FileHandlerDelegate callback = new Server.FileHandlerDelegate(Server.FileHandler);
			if (flag)
			{
				callback = new Server.FileHandlerDelegate(Server.WWWFileHandler);
			}
			routeAttribute.m_callback = delegate(RequestContext context)
			{
				callback(context, true);
			};
			routeAttribute2.m_callback = delegate(RequestContext context)
			{
				callback(context, false);
			};
			Server.registeredRoutes.Add(routeAttribute);
			Server.registeredRoutes.Add(routeAttribute2);
		}

		private void OnEnable()
		{
			Application.RegisterLogCallback(new Application.LogCallback(Console.LogCallback));
		}

		private void OnDisable()
		{
			Application.RegisterLogCallback(null);
		}

		private void Update()
		{
			Console.Update();
		}

		private void ListenerCallback(IAsyncResult result)
		{
			RequestContext context = new RequestContext(Server.listener.EndGetContext(result));
			this.HandleRequest(context);
			Server.listener.BeginGetContext(new AsyncCallback(this.ListenerCallback), null);
		}

		private void HandleRequest(RequestContext context)
		{
			try
			{
				bool flag = false;
				while (context.currentRoute < Server.registeredRoutes.Count)
				{
					RouteAttribute routeAttribute = Server.registeredRoutes[context.currentRoute];
					Match match = routeAttribute.m_route.Match(context.path);
					if (match.Success)
					{
						if (routeAttribute.m_methods.IsMatch(context.Request.HttpMethod))
						{
							if (routeAttribute.m_runOnMainThread && Thread.CurrentThread != Server.mainThread)
							{
								Queue<RequestContext> obj = Server.mainRequests;
								lock (obj)
								{
									Server.mainRequests.Enqueue(context);
								}
								return;
							}
							context.match = match;
							routeAttribute.m_callback(context);
							flag = !context.pass;
							if (flag)
							{
								break;
							}
						}
					}
					context.currentRoute++;
				}
				if (!flag)
				{
					context.Response.StatusCode = 404;
					context.Response.StatusDescription = "Not Found";
				}
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				context.Response.StatusDescription = string.Format("Fatal error:\n{0}", ex);
				UnityEngine.Debug.LogException(ex);
			}
			context.Response.OutputStream.Close();
		}

		private IEnumerator HandleRequests()
		{
			Server._HandleRequests_c__Iterator0 _HandleRequests_c__Iterator = new Server._HandleRequests_c__Iterator0();
			_HandleRequests_c__Iterator.__f__this = this;
			return _HandleRequests_c__Iterator;
		}
	}
}
