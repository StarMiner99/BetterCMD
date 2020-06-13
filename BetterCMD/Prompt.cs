using System;
using System.ComponentModel.DataAnnotations;

namespace BetterCMD
{
    public class Prompt
    {
        private string promptFormat;
        public Prompt(string promptFormat)
        {
            this.promptFormat = promptFormat;
        }

        public void printPrompt ()
        {
            /* example
                test #14 yellow{hostname} *9 {user} \*>
            */
            
            //check for first special char
            int nextFg = promptFormat.IndexOf("#");
            int nextBg = promptFormat.IndexOf("*");
            int nextSc = promptFormat.IndexOf("\\");
            int nextSb = promptFormat.IndexOf("{");
            int nextSe = promptFormat.IndexOf("}");
            int[] special = new int[5] {nextFg, nextBg, nextSc, nextSb, nextSe};

            for (int i = 0; i < promptFormat.Length; i++)
            {
                if (promptFormat[i] == '#')
                {
                    int nextWhiteSpace = promptFormat.IndexOf(" ", i);
                    int colorCode = Int32.Parse(promptFormat.Substring(i+1, nextWhiteSpace - i));
                    Console.ForegroundColor = (ConsoleColor)colorCode;
                    i += promptFormat.Substring(i, nextWhiteSpace - i).Length;
                } else if (promptFormat[i] == '*')
                {
                    int nextWhiteSpace = promptFormat.IndexOf(" ", i);
                    int colorCode = Int32.Parse(promptFormat.Substring(i+1, nextWhiteSpace - i));
                    Console.BackgroundColor = (ConsoleColor)colorCode;
                    i += promptFormat.Substring(i, nextWhiteSpace - i).Length;
                } else if (promptFormat[i] == '~')
                {
                    Console.Write(promptFormat[++i]);
                } else if (promptFormat[i] == '{')
                {
                    
                }
                else
                {
                    Console.Write(promptFormat[i]);
                }
            }
        }
    }
}