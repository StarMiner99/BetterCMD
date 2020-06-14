using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BetterCMD
{
    public class Input
    {
        public static string autoCompleteInput()
        {
            int cursorPos = 0;
            string promptStr = clearCurrentLine();
            

            StringBuilder builder = new StringBuilder();
            ConsoleKeyInfo input = Console.ReadKey(intercept:true);

            while (input.Key != ConsoleKey.Enter)
            {
                string currentInput = builder.ToString();
                if (input.Key == ConsoleKey.Tab)
                {
                    
                }
                else
                {
                    if (input.Key == ConsoleKey.Backspace)
                    {
                        if (cursorPos > 0)
                        {
                            builder.Remove(cursorPos - 1, 1);
                            currentInput = builder.ToString();
                            clearCurrentLine();
                            Console.Write(currentInput);
                            //Console.Write(currentInput[Range.StartAt(cursorPos)]);
                            Console.SetCursorPosition(cursorPos-1 + promptStr.Length, Console.CursorTop);
                            cursorPos--;
                        }
                        
                    } else if (input.Key == ConsoleKey.LeftArrow)
                    {
                        if (cursorPos > 0)
                        {
                            clearCurrentLine();
                            Console.Write(currentInput);
                            Console.SetCursorPosition(cursorPos-1 + promptStr.Length, Console.CursorTop);
                            cursorPos--;
                        }
                    } else if (input.Key == ConsoleKey.RightArrow)
                    {
                        if (cursorPos < currentInput.Length)
                        {
                            clearCurrentLine();
                            Console.Write(currentInput);
                            Console.SetCursorPosition(cursorPos+1 + promptStr.Length, Console.CursorTop);
                            cursorPos++;
                        }
                    }
                    else
                    {
                        char key = input.KeyChar;
                        
                        builder.Insert(cursorPos, key);
                        Console.Write(key);
                        Console.Write(currentInput[Range.StartAt(cursorPos)]);
                        
                        Console.SetCursorPosition(cursorPos+1 + promptStr.Length, Console.CursorTop);
                        cursorPos++;
                    }
                }

                input = Console.ReadKey(intercept:true);
            }
            Console.Write(input.KeyChar);
            
            Console.WriteLine();

            return builder.ToString();
        }
        
        private static string clearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
            Prompt prompt = new Prompt(BetterCMD.promptFormat);
            string promptStr = prompt.printPrompt();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            return promptStr;
        }
    }
}