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
                    
                    int autoBegin = currentInput.LastIndexOf(' ', cursorPos-1);
                    //Console.Write(autoBegin);

                    if (autoBegin != -1)
                    {
                        string prefix = currentInput.Substring(autoBegin, cursorPos-autoBegin);
                        if (prefix != " ")
                        {
                            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
                            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                            string[] fileDir = new string[dirs.Length + files.Length];
                            
                            Array.Copy(dirs, fileDir, dirs.Length);
                            Array.Copy(files, 0, fileDir, dirs.Length, files.Length);

                            foreach (string item in fileDir)
                            {
                                int indexInAr = Array.IndexOf(fileDir, item);
                                fileDir[indexInAr] = item.Replace(Directory.GetCurrentDirectory() + "\\", "");
                            }

                            
                            
                            prefix = prefix.Remove(0, 1);

                            List<string> matches = new List<string>();
                            
                            
                            foreach (string item in fileDir)
                            {
                                if (item.StartsWith(prefix))
                                {
                                    matches.Add(item);
                                }
                            }

                            string[] matchArray = matches.ToArray();
                            if (matchArray.Length == 1)
                            {
                                Console.WriteLine();
                                builder.Remove(autoBegin + 1, prefix.Length);
                                builder.Insert(autoBegin +1, matchArray[0] + " ");
                                //builder.Append(behindInsert);
                                clearCurrentLine();
                                Console.Write(builder.ToString());
                                Console.SetCursorPosition(cursorPos + (matchArray[0].Length+1-prefix.Length) + promptStr.Length, Console.CursorTop);
                                cursorPos += matchArray[0].Length +1- prefix.Length;
                            } else if (matchArray.Length > 1)
                            {
                                Console.WriteLine();
                                foreach (var item in matchArray)
                                {
                                    Console.WriteLine(item);
                                }

                                clearCurrentLine();
                                Console.Write(builder.ToString());
                                Console.SetCursorPosition(cursorPos + promptStr.Length, Console.CursorTop);

                            }

                        }
                    }
                    
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
            int currentLine = Console.CursorTop;
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