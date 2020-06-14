using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace BetterCMD
{
    public class Prompt
    {
        private readonly string promptFormat;
        public Prompt(string promptFormat)
        {
            this.promptFormat = promptFormat;
        }

        public void printPrompt ()
        {
            
            //print the prompt
            for (int i = 0; i < promptFormat.Length; i++)
            {
                //check if char is a special char
                if (promptFormat[i] == '#') //foreground color
                {
                    int nextWhiteSpace = promptFormat.IndexOf(" ", i); //get next whitespace
                    
                    int colorCode = Int32.Parse(promptFormat.Substring(i+1, nextWhiteSpace - i)); //parse the color code to int
                    Console.ForegroundColor = (ConsoleColor)colorCode; //convert it to Consoleolor
                    
                    i += promptFormat.Substring(i, nextWhiteSpace - i).Length; //skip the color code chars to prevent to be printed
                    
                } else if (promptFormat[i] == '*') // background color
                {
                    int nextWhiteSpace = promptFormat.IndexOf(" ", i); //get next whitespace
                    
                    int colorCode = Int32.Parse(promptFormat.Substring(i+1, nextWhiteSpace - i)); //parse the color code to int
                    Console.BackgroundColor = (ConsoleColor)colorCode; //convert it to ConsoleColor
                    
                    i += promptFormat.Substring(i, nextWhiteSpace - i).Length; //skip the color code chars to prevent to be printed
                    
                } else if (promptFormat[i] == '~')
                {
                    Console.Write(promptFormat[++i]); //write the car after the ~ and skip 1 char
                    
                } else if (promptFormat[i] == '{')
                {
                    int closeBracket = promptFormat.IndexOf("}", i); //get next closed bracket
                    
                    string varName = promptFormat.Substring(i + 1, closeBracket - i-1); // get the variable name
                    
                    //compare it
                    if (varName == "user") //user
                    {
                        Console.Write(Environment.UserName);
                    } else if (varName == "hostname") //hostname (lowercase)
                    {
                        Console.Write(Environment.MachineName.ToLower());
                    } else if (varName == "dirWin") //current working dir in windows style
                    {
                        Console.Write(Environment.CurrentDirectory);
                    } else if (varName == "dirLin") //current working dir in linux style
                    {
                        Console.Write(Environment.CurrentDirectory.Replace("\\","/"));
                    }
                    
                    
                    i += promptFormat.Substring(i, closeBracket - i).Length; //skip the brackets and variable name
                }
                else
                {
                    Console.Write(promptFormat[i]); // nothing special just print the char
                }
            }
        }

        public string readCommandInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            string input = Console.ReadLine();
            return input;
        }

        public void handleCommand(string command)
        {
            //get special commands (cd, exit, ...)
            
            //extract command from args for special comands
            string commandOnly = null;
            try
            {
                commandOnly = command.Substring(0, command.IndexOf(' '));
            }
            catch (Exception)
            {
                commandOnly = null;
            }
            
            if (commandOnly == "cd")
            {
                Directory.SetCurrentDirectory(command.Substring(command.IndexOf(' ')+1, command.Length-command.IndexOf(' ')-1));
            } else if (command == "exit")
            {
                Environment.Exit(0);
            }
            else
            {
                Process p = new Process(); // create the cmd process
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c " + command;
                p.StartInfo = startInfo;
                p.Start();
                Console.WriteLine(p.StandardOutput.ReadToEnd());
                p.WaitForExit();
            }

            
        }
    }
}