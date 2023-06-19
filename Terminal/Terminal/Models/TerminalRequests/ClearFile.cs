using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class ClearFile : TerminalRequest
    {
        List<string> extension = new List<string>();
        string message = "Deleted files: [";

        public ClearFile()
        {
            CommandName = "clearfile";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                switch (commandBody)
                {
                    case "..":
                        if (!handler.SetCurrentDirectory(CommandHandler.CurrentDirectoryPath
                            .Substring(0, CommandHandler.CurrentDirectoryPath.LastIndexOf('\\'))))
                        {
                            throw new Exception("Directory not exists");
                        }
                        break;
                    default:
                        if (!handler.SetCurrentDirectory(commandBody)) throw new Exception("Directory not exists");
                        break;
                }
            }
            catch(Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }

            DirectoryInfo directory = new DirectoryInfo(CommandHandler.CurrentDirectoryPath);
            StartAll(directory);
        }

        public void RegistryInfo()
        {
            var x32Tree = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).GetSubKeyNames();

            for (int i = 0; i < x32Tree.Length - 1; i++)
            {
                if(x32Tree[i][0] == '.') {
                    RegistryOut(x32Tree[i]);
                }
            }
        }

        public void RegistryOut(string registry)
        {
            if (!(registry == "\udd00쬂Ƚ"))
                try
                {
                    extension.Add(registry);
                }
                catch(Exception ex)
                {
                    ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
                }
                
        }


        public void DeleteNonRegistered(DirectoryInfo directory)
        {           
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    string name = file.Name;
                    string ext = name.Substring(name.LastIndexOf('.'));

                    if (!extension.Contains(ext))
                    {
                        message += $"{file.FullName}\n";
                        file.Delete();
                    }
                }
                catch(Exception ex)
                {
                    ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
                }
                
                    

            }

            DirectoryInfo[] directories = directory.GetDirectories();

            foreach(DirectoryInfo directory1 in directories)
            {
                DeleteNonRegistered(directory1);
            }
        }

        public void StartAll(DirectoryInfo directory)
        {
            RegistryInfo();

            DeleteNonRegistered(directory);
            
            Console.WriteLine(message + ']');
        }
    }
}
