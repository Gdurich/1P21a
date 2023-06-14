using System;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
	internal class TreeTerminalRequest : TerminalRequest
	{
		public TreeTerminalRequest()
		{
			CommandName = "tree";
		}

		public override void Execute(CommandHandler handler, string commandBody = "")
		{
			string path = CommandHandler.CurrentDirectoryPath;
			int indentSize = 4;
			PrintTree(path, indentSize, "");

			base.Execute(handler, commandBody);
		}

		private static void PrintTree(string path, int indentSize, string indent)
		{
			DirectoryInfo directory = new DirectoryInfo(path);

			Console.WriteLine(indent + directory.Name);

			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files)
			{
				try
				{
					Console.WriteLine($"{indent}├{new string('─', indentSize)}{file.Name}");
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine($"{indent}├{new string('─', indentSize)}[No access]: {file.Name}");
				}
			}

			DirectoryInfo[] subDirectories = directory.GetDirectories();
			foreach (DirectoryInfo subDirectory in subDirectories)
			{
				try
				{
					PrintTree(subDirectory.FullName, indentSize, $"{indent}{new string(' ', indentSize)}");
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine($"{indent}├{new string('─', indentSize)}[No access]: {subDirectory.Name}");
				}
			}
		}
	}
}
