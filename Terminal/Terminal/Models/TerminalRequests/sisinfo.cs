using System;
using System.Diagnostics;

public partial class Program
{
    public static void Main(string[] args)
    {
        RunCommand("systeminfo");
    }

    public static void RunCommand(string command)
    {
        Process process = new Process();

        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "cmd.exe";
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.UseShellExecute = false;
        processStartInfo.CreateNoWindow = true;
        processStartInfo.Arguments = "/c " + command;

        process.StartInfo = processStartInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);

        process.WaitForExit();
    }
}
