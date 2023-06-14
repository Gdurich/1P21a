using System;
using Terminal.Models.TerminalRequests;

namespace Terminal.Models.TerminalRequests
{
    public class Mkdir_TerminalRequest : TerminalRequest
    {
        public override string CommandKeyword => "mkdir";

        public override TerminalResponse Execute(string[] commandArgs)
        {
            if (commandArgs.Length < 2)
            {
                return new TerminalResponse(false, "Usage: mkdir <directory_name>");
            }

            string directoryName = commandArgs[1];
            string fullPath = CommandHandler.CurrentDirectoryPath + "/" + directoryName;

            try
            {
                System.IO.Directory.CreateDirectory(fullPath);

                return new TerminalResponse(true, $"Directory '{directoryName}' created successfully.");
            }
            catch (Exception ex)
            {
                return new TerminalResponse(false, $"Error creating directory: {ex.Message}");
            }
        }
    }
}
