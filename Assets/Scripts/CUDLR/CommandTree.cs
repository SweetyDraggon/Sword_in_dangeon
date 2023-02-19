using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CUDLR
{
	internal class CommandTree
	{
		private Dictionary<string, CommandTree> m_subcommands;

		private CommandAttribute m_command;

		private static string[] emptyArgs = new string[0];

		public CommandTree()
		{
			this.m_subcommands = new Dictionary<string, CommandTree>();
		}

		public void Add(CommandAttribute cmd)
		{
			this._add(cmd.m_command.ToLower().Split(new char[]
			{
				' '
			}), 0, cmd);
		}

		private void _add(string[] commands, int command_index, CommandAttribute cmd)
		{
			if (commands.Length == command_index)
			{
				this.m_command = cmd;
				return;
			}
			string key = commands[command_index];
			if (!this.m_subcommands.ContainsKey(key))
			{
				this.m_subcommands[key] = new CommandTree();
			}
			this.m_subcommands[key]._add(commands, command_index + 1, cmd);
		}

		public string Complete(string partialCommand)
		{
			return this._complete(partialCommand.Split(new char[]
			{
				' '
			}), 0, string.Empty);
		}

		public string _complete(string[] partialCommand, int index, string result)
		{
			if (partialCommand.Length == index && this.m_command != null)
			{
				return result;
			}
			if (partialCommand.Length == index)
			{
				Console.LogCommand(result);
				foreach (string current in this.m_subcommands.Keys)
				{
					Console.Log(result + " " + current);
				}
				return result + " ";
			}
			if (partialCommand.Length == index + 1)
			{
				string text = partialCommand[index];
				if (this.m_subcommands.ContainsKey(text))
				{
					result += text;
					return this.m_subcommands[text]._complete(partialCommand, index + 1, result);
				}
				List<string> list = new List<string>();
				foreach (string current2 in this.m_subcommands.Keys)
				{
					if (current2.StartsWith(text))
					{
						list.Add(current2);
					}
				}
				if (list.Count == 1)
				{
					return result + list[0] + " ";
				}
				if (list.Count > 1)
				{
					Console.LogCommand(result + text);
					foreach (string current3 in list)
					{
						Console.Log(result + current3);
					}
				}
				return result + text;
			}
			else
			{
				string text2 = partialCommand[index];
				if (!this.m_subcommands.ContainsKey(text2))
				{
					return result;
				}
				result = result + text2 + " ";
				return this.m_subcommands[text2]._complete(partialCommand, index + 1, result);
			}
		}

		public void Run(string commandStr)
		{
			Regex regex = new Regex("\".*?\"|[^\\s]+");
			MatchCollection matchCollection = regex.Matches(commandStr);
			string[] array = new string[matchCollection.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = matchCollection[i].Value.Replace("\"", string.Empty);
			}
			this._run(array, 0);
		}

		private void _run(string[] commands, int index)
		{
			if (commands.Length == index)
			{
				this.RunCommand(CommandTree.emptyArgs);
				return;
			}
			string key = commands[index].ToLower();
			if (!this.m_subcommands.ContainsKey(key))
			{
				this.RunCommand(commands.Skip(index).ToArray<string>());
				return;
			}
			this.m_subcommands[key]._run(commands, index + 1);
		}

		private void RunCommand(string[] args)
		{
			if (this.m_command == null)
			{
				Console.Log("command not found");
			}
			else if (this.m_command.m_runOnMainThread)
			{
				Console.Queue(this.m_command, args);
			}
			else
			{
				this.m_command.m_callback(args);
			}
		}
	}
}
