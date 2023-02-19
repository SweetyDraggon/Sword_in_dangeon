using System;

namespace CUDLR
{
	[AttributeUsage(AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		public delegate void CallbackSimple();

		public delegate void Callback(string[] args);

		public string m_command;

		public string m_help;

		public bool m_runOnMainThread;

		public CommandAttribute.Callback m_callback;

		public CommandAttribute(string cmd, string help, bool runOnMainThread = true)
		{
			this.m_command = cmd;
			this.m_help = help;
			this.m_runOnMainThread = runOnMainThread;
		}
	}
}
