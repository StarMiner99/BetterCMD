using System;
using System.Drawing;

namespace BetterCMD
{
    class BetterCMD
    {
        static string promptFormat = Environment.GetEnvironmentVariable("CMDPrompt");
        static void Main(string[] args)
        {
            if (promptFormat == null)
            {
                Console.WriteLine("Configuring BetterCMD, Please Wait...");
                Environment.SetEnvironmentVariable("CMDPrompt", "#14 BetterCMD #1 *10 nice~#1 test~~");
                promptFormat = Environment.GetEnvironmentVariable("CMDPrompt");
            }
            Prompt prompt = new Prompt(promptFormat);
            prompt.printPrompt();
        }
    }
}