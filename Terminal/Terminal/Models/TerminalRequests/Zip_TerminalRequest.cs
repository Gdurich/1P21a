﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Models.TerminalRequests.Base;

//namespace Terminal.Models.TerminalRequests
//{
   /* public class Zip_TerminalRequest : TerminalRequest
    {
        public override string CommandKeyword => "zip";

        public override TerminalResponse Execute(string[] commandArgs)
        {
            if (commandArgs.Length < 3)
            {
                return new TerminalResponse(false, "Usage: zip <source_directory> <destination_zip_file>");
            }

            string sourceDirectory = commandArgs[1];
            string destinationZipFile = commandArgs[2];

            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectory, destinationZipFile);

                return new TerminalResponse(true, $"Directory '{sourceDirectory}' zipped successfully to '{destinationZipFile}'.");
            }
            catch (Exception ex)
            {
                return new TerminalResponse(false, $"Error zipping directory: {ex.Message}");
            }
        }
    }
}
   */