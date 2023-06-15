using System;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Copy_TerminalRequest : TerminalRequest
    {
        public Copy_TerminalRequest()
        {
            CommandName = "copy";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                string[] args = commandBody.Split(' ');
                if (args.Length != 2)
                {
                    throw new ArgumentException("Invalid arguments. Usage: copy <source> <destination>");
                }

                string sourcePath = args[0];
                string destinationPath = args[1];

                string sourceFullPath = GetFullPath(sourcePath);
                string destinationFullPath = GetFullPath(destinationPath);

                if (!File.Exists(sourceFullPath))
                {
                    throw new FileNotFoundException("Source file not found.");
                }

                File.Copy(sourceFullPath, destinationFullPath, true);

                ConsoleHelper.WriteColorLine("File copied successfully.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        private string GetFullPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            else
            {
                return Path.Combine(CommandHandler.CurrentDirectoryPath, path);
            }
        }
    }
}
