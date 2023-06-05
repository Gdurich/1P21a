using System;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        RunCommand("systeminfo");
    }

    public static void RunCommand(string command)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process
        {
            StartInfo = processStartInfo
        };

        process.Start();

        while (!process.StandardOutput.EndOfStream)
        {
            string output = process.StandardOutput.ReadLine();
            Console.WriteLine(output);
        }
    }
}
