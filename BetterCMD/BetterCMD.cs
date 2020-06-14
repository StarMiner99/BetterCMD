using System;

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
                Environment.SetEnvironmentVariable("CMDPrompt", "#14 BetterCMD #1 *10 nice~#1 test~~",
                    EnvironmentVariableTarget.User); //configure prompt
                Console.WriteLine("BetterCMD will work after you start it again!");
            }
            else
            {
                
                Prompt prompt = new Prompt("#14 BetterCMD #1 *10 nice~#1 test~~ {user}@{hostname}:{dirLin}>"); //create new prompt
                prompt.printPrompt(); //print the prompt

            }
        }
    }
        
}