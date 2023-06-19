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
    internal class CCleaner : TerminalRequest
    {
        List<string> extension = new List<string>();

        public CCleaner()
        {
            CommandName = "ccleaner";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                if (string.IsNullOrEmpty(commandBody))
                    throw new Exception("Set directory path or name!");

                if (Directory.Exists(commandBody))
                {
                    DirectoryInfo directory = new DirectoryInfo(commandBody);
                    StartAll(directory);
                }
                else if (Directory.Exists(Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody)))
                {
                    DirectoryInfo directory = new DirectoryInfo(Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody));
                    StartAll(directory);
                }
                else
                    throw new Exception("Input correct path");
                    
            }
            catch(Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);

                return;
            }
            
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
                        Console.WriteLine($"{file.FullName} || was deleted\n");
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
           
        }
    }
}
