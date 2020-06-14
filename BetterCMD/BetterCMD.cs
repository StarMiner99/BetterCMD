using System;
using System.IO;

namespace BetterCMD
{
    class BetterCMD
    {
        private static readonly string promptFormat = Environment.GetEnvironmentVariable("CMDPrompt", EnvironmentVariableTarget.User); // get the prompt

        static void Main(string[] args)
        {
            if (promptFormat == null) //prompt has not been configured
            {
                Console.WriteLine("Configuring BetterCMD, Please Wait...");
                Environment.SetEnvironmentVariable("CMDPrompt", "#14 BetterCMD #2 {user}@{hostname}-#13 {dirWin}#1 > ",
                    EnvironmentVariableTarget.User); //configure prompt
                Console.WriteLine("BetterCMD will work after you start it again!");
            }
            else
            {
                
                Prompt prompt = new Prompt(promptFormat); //create new prompt
                try
                {
                    Directory.SetCurrentDirectory(args[0]);
                }
                catch (Exception)
                {
                    Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                    Directory.SetCurrentDirectory("..");
                }
                
                while (true)
                {
                    prompt.printPrompt(); //print the prompt
                    string input = prompt.readCommandInput(); // read the input
                    prompt.handleCommand(input); // execute command
                }
            }
        }
    }
        
}