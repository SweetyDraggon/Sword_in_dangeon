using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CUDLR
{
	public class Console
	{
		private sealed class _RegisterAttributes_c__AnonStorey7
		{
			internal CommandAttribute.CallbackSimple cbs;

			internal void __m__0(string[] args)
			{
				this.cbs();
			}
		}

		private const int MAX_LINES = 100;

		private const int MAX_HISTORY = 50;

		private const string COMMAND_OUTPUT_PREFIX = "> ";

		private static Console instance;

		private CommandTree m_commands;

		private List<string> m_output;

		private List<string> m_history;

		private string m_help;

		private Queue<QueuedCommand> m_commandQueue;

		public static Console Instance
		{
			get
			{
				if (Console.instance == null)
				{
					Console.instance = new Console();
				}
				return Console.instance;
			}
		}

		private Console()
		{
			this.m_commands = new CommandTree();
			this.m_output = new List<string>();
			this.m_history = new List<string>();
			this.m_commandQueue = new Queue<QueuedCommand>();
			this.RegisterAttributes();
		}

		public static void Update()
		{
			while (Console.Instance.m_commandQueue.Count > 0)
			{
				QueuedCommand queuedCommand = Console.Instance.m_commandQueue.Dequeue();
				queuedCommand.command.m_callback(queuedCommand.args);
			}
		}

		public static void Queue(CommandAttribute command, string[] args)
		{
			QueuedCommand item = default(QueuedCommand);
			item.command = command;
			item.args = args;
			Console.Instance.m_commandQueue.Enqueue(item);
		}

		public static void Run(string str)
		{
			if (str.Length > 0)
			{
				Console.LogCommand(str);
				Console.Instance.RecordCommand(str);
				Console.Instance.m_commands.Run(str);
			}
		}

		[Command("clear", "clears console output", false)]
		public static void Clear()
		{
			Console.Instance.m_output.Clear();
		}

		[Command("help", "prints commands", false)]
		public static void Help()
		{
			Console.Log(string.Format("Commands:{0}", Console.Instance.m_help));
		}

		public static string Complete(string partialCommand)
		{
			return Console.Instance.m_commands.Complete(partialCommand);
		}

		public static void LogCommand(string cmd)
		{
			Console.Log("> " + cmd);
		}

		public static void Log(string str)
		{
			if (str == "Profiler is only supported in Unity Pro." || str == "\n" || str == "\r" || str == " " || str == string.Empty || str.Length == 1)
			{
				return;
			}
			Console.Instance.m_output.Add(str);
			if (Console.Instance.m_output.Count > 100)
			{
				Console.Instance.m_output.RemoveAt(0);
			}
		}

		public static void LogCallback(string logString, string stackTrace, LogType type)
		{
			Console.Log(logString);
			if (type != LogType.Log)
			{
				Console.Log(stackTrace);
			}
		}

		public static string Output()
		{
			return string.Join("\n", Console.Instance.m_output.ToArray());
		}

		public static void RegisterCommand(string command, string desc, CommandAttribute.Callback callback, bool runOnMainThread = true)
		{
			if (command == null || command.Length == 0)
			{
				throw new Exception("Command String cannot be empty");
			}
			CommandAttribute commandAttribute = new CommandAttribute(command, desc, runOnMainThread);
			commandAttribute.m_callback = callback;
			Console.Instance.m_commands.Add(commandAttribute);
			Console expr_41 = Console.Instance;
			expr_41.m_help += string.Format("\n{0} : {1}", command, desc);
		}

		private void RegisterAttributes()
		{
			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				Type type = types[i];
				MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
				for (int j = 0; j < methods.Length; j++)
				{
					MethodInfo methodInfo = methods[j];
					CommandAttribute[] array = methodInfo.GetCustomAttributes(typeof(CommandAttribute), true) as CommandAttribute[];
					if (array.Length != 0)
					{
						CommandAttribute.Callback callback = Delegate.CreateDelegate(typeof(CommandAttribute.Callback), methodInfo, false) as CommandAttribute.Callback;
						if (callback == null)
						{
							CommandAttribute.CallbackSimple cbs = Delegate.CreateDelegate(typeof(CommandAttribute.CallbackSimple), methodInfo, false) as CommandAttribute.CallbackSimple;
							if (cbs != null)
							{
								callback = delegate(string[] args)
								{
									cbs();
								};
							}
						}
						if (callback == null)
						{
							UnityEngine.Debug.LogError(string.Format("Method {0}.{1} takes the wrong arguments for a console command.", type, methodInfo.Name));
						}
						else
						{
							CommandAttribute[] array2 = array;
							for (int k = 0; k < array2.Length; k++)
							{
								CommandAttribute commandAttribute = array2[k];
								if (string.IsNullOrEmpty(commandAttribute.m_command))
								{
									UnityEngine.Debug.LogError(string.Format("Method {0}.{1} needs a valid command name.", type, methodInfo.Name));
								}
								else
								{
									commandAttribute.m_callback = callback;
									this.m_commands.Add(commandAttribute);
									this.m_help += string.Format("\n{0} : {1}", commandAttribute.m_command, commandAttribute.m_help);
								}
							}
						}
					}
				}
			}
		}

		public static string PreviousCommand(int index)
		{
			return (index < 0 || index >= Console.Instance.m_history.Count) ? null : Console.Instance.m_history[index];
		}

		private void RecordCommand(string command)
		{
			this.m_history.Insert(0, command);
			if (this.m_history.Count > 50)
			{
				this.m_history.RemoveAt(this.m_history.Count - 1);
			}
		}

		[Route("^/console/out$", "(GET|HEAD)", true)]
		public static void Output(RequestContext context)
		{
			context.Response.WriteString(Console.Output(), "text/plain");
		}

		[Route("^/console/run$", "(GET|HEAD)", true)]
		public static void Run(RequestContext context)
		{
			string text = context.Request.QueryString.Get("command");
			if (!string.IsNullOrEmpty(text))
			{
				Console.Run(text);
			}
			context.Response.StatusCode = 200;
			context.Response.StatusDescription = "OK";
		}

		[Route("^/console/commandHistory$", "(GET|HEAD)", true)]
		public static void History(RequestContext context)
		{
			string text = context.Request.QueryString.Get("index");
			string input = null;
			if (!string.IsNullOrEmpty(text))
			{
				input = Console.PreviousCommand(int.Parse(text));
			}
			context.Response.WriteString(input, "text/plain");
		}

		[Route("^/console/complete$", "(GET|HEAD)", true)]
		public static void Complete(RequestContext context)
		{
			string text = context.Request.QueryString.Get("command");
			string input = null;
			if (text != null)
			{
				input = Console.Complete(text);
			}
			context.Response.WriteString(input, "text/plain");
		}
	}
}
