﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diary
{
    class Program
    {
        static bool running = true;
        static void Main(string[] args)
        {
            WriteMessage("Hello welcome to your Diary Program (Press a key to continue): \n");
            WaitForKeyPress();
            Run();
        }

        static void Run()
        {
            while (running)
            {
                string option = GetOptionFromMainMenu();
                switch (option)
                {
                    case "New File":
                        Write();
                        break;
                    case "Search Files":
                        Search();
                        break;
                    case "Edit File":
                        EditMenu();
                        break;
                    case "View File":
                        ViewMenu();
                        break;
                    case "Edit Today's File":
                        EditFile(TodaysFileName());
                        break;
                    case "q":
                        running = false;
                        break;
                }
            }
        }

        static void WriteMessage(string message)
        {
            Console.Write(message);
        }

        static void WaitForKeyPress()
        {
            Console.ReadKey();
        }

        static void ClearConsole()
        {
            Console.Clear();
        }

        static string GetOptionFromMainMenu()
        {
            ClearConsole();

            string[] mainMenuOptions = { TodaysFileExcists() ? "Edit Today's File" : "New File",
                                         "Search Files", "Edit File", "View File"};
            WriteMessage("(Main menu) Select a function:\n");

            return GetOptionFromMenu(mainMenuOptions);
        }

        static bool TodaysFileExcists()
        {
            return DiaryFile.GetFileNames().Contains(TodaysFileName());
        }

        static string TodaysFileName()
        {
            return DateTime.Now.ToString().Split(' ')[0] + ".txt";
        }

        static string GetOptionFromMenu(string[] options)
        {
            Menu menu = new Menu(options, Console.CursorTop);
            menu.Dispaly();
            return menu.Run();
        }

        static string GetOptionFromMenu(List<string> options)
        {
            Menu menu = new Menu(options, Console.CursorTop);
            menu.Dispaly();
            return menu.Run();
        }


        static void Write()
        {
            ClearConsole();
            Dictionary<string, List<string>> headlineTextPairs = WriteTextToHeadlines();

            string fileName = TodaysFileName();
            SaveToFile(headlineTextPairs, fileName);

            string yOrN = YesOrNo("Finished Writing?");
            if (yOrN == "n" || yOrN == "N")
            {
                EditFile(fileName);
            }
            else
            {
                WriteMessage("File has been created... (Press key to continue)\n");
                WaitForKeyPress();
            }
        }



        static Dictionary<string, List<string>> WriteTextToHeadlines()
        {
            string[] headlines = DiaryFile.GetHeadlinesArray();
            Dictionary<string, List<string>> headlineTextPairs = new Dictionary<string, List<string>>();

            for (int i = 0; i < headlines.Length; i++)
            {
                string headline = headlines[i];
                List<string> lines = new List<string>();

                Console.WriteLine(headline);

                if (i == 1)
                {
                    string line;
                    while ((line = Console.ReadLine()) != "")
                    {
                        lines.Add(line);
                    }
                }
                else
                {
                    lines.Add(Console.ReadLine());
                    Console.WriteLine();
                }
                headlineTextPairs.Add(headline, lines);
            }
            return headlineTextPairs;
        }
        
        static void SaveToFile(Dictionary<string, List<string>> headlineTextPairs, string fileName)
        {
            DiaryFile.SaveToFile(headlineTextPairs, fileName);
        }

        static string YesOrNo(string message)
        {
            string k = "";
            while (k != "y" && k != "Y" &&
                  k != "n" && k != "N")
            {
                WriteMessage(message + " [Y/n]: ");
                k = Console.ReadLine();
                ClearLineFromCurrentPosition();
            }
            return k;
        }

        static void ClearLineFromCurrentPosition()
        {
                int origPos = Console.CursorTop;
                Console.SetCursorPosition(0, origPos);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, origPos);
        }

        static void ViewMenu()
        {
            string option = "";
            while(option != "q" && option != "Go Back")
            {
                ClearConsole();
                WriteMessage("View Menu (Pick a file to view):\n");
                option = FileMenu();
                if(option != "q" && option != "Go Back")
                {
                    ViewFile(option);
                }
            }
        }

        static string FileMenu()
        {
            List<string> options = DiaryFile.GetFileNames();
            if(!options.Contains("Go Back"))
            {
                options.Add("Go Back");

            }
            return GetOptionFromMenu(options);
        }

        static string FileMenu(List<string> files)
        {
            if(!files.Contains("Go Back"))
            {
                files.Add("Go Back");
            }
            return GetOptionFromMenu(files);
        }

        static void ViewFile(string fileName)
        {
            ClearConsole();
            WriteMessage("Viewing file: " + fileName + " (Press a key to exit)\n");
            Dictionary<string, List<string>>headlineTextPairs = DiaryFile.GetHeadlineTextPairs(fileName);
            string[] headlines = headlineTextPairs.Keys.ToArray();
            List<string>[] text = headlineTextPairs.Values.ToArray();
            for (int i = 0; i < headlines.Length; i++)
            {
                string headline = headlines[i];
                List<string> paragraph = text[i];
                WriteMessage(headline + "\n");
                foreach(string line in paragraph)
                {
                    WriteMessage(line + "\n");
                }
                WriteMessage("\n");
            }
            WaitForKeyPress();
        }

        static void Search()
        {
            string phrase = GetSearchPhrase();
            DisplaySearchResult(phrase);
        }

        static void DisplaySearchResult(string phrase)
        {
            List<string> files = DiaryFile.GetFilesContaining(phrase);
            string option = "";
            while(option != "q" && option != "Go Back")
            {
                ClearConsole();
                WriteMessage("Search results for: " + phrase + "\n");
                option = FileMenu(files);
                if(option != "q" && option != "Go Back")
                {
                    ViewFile(option);
                }
            }
        }

        static string GetSearchPhrase()
        {
            ClearConsole();
            WriteMessage("Type search phrase and press enter:\n");
            return Console.ReadLine();
        }


        static void EditMenu()
        {
            ClearConsole();
            WriteMessage("Edit Menu (Pick a file to edit):\n");

            string option = FileMenu();

            if (option != "q" && option != "Go Back")
            {
                EditFile(option);
            }
        }

        static void EditFile(string fileName)
        {
            List<string> options = DiaryFile.GetHeadlinesList();
            options.Add("Done Editing!");
            string option = "";

            while (option != "Done Editing!")
            {
                ClearConsole();
                WriteMessage("Edit Menu, File: " + fileName + "\n");
                option = GetOptionFromMenu(options);
                if (option != "Done Editing!")
                {
                    DisplayEditor(fileName, option);
                }
            }
        }

        static void DisplayEditor(string fileName, string headline)
        {
            Dictionary<string, List<string>> headlineTextPairs = DiaryFile.GetHeadlineTextPairs(fileName);
            List<string> lines = headlineTextPairs[headline];
            DisplayEditor(headline, lines);
            SaveChanges(headlineTextPairs, fileName);
        }

        static void DisplayEditor(string headline, List<string> lines)
        {
            string yOrN = "";
            while (yOrN != "y" && yOrN != "Y")
            {
                ClearConsole();
                WriteMessage("Editing " + "\'" + headline + "\' (Press 'Esc' to exit)\n");
                EditLines(lines);
                yOrN = YesOrNo("Done editing? ");
            }
        }

        static void SaveChanges(Dictionary<string, List<string>> headlineTextPairs, string fileName)
        {
            string yOrN = YesOrNo("Do you want to save the changes? ");
            if (yOrN == "y" || yOrN == "Y")
            {
                DiaryFile.RemoveFile(fileName);
                SaveToFile(headlineTextPairs, fileName);
            }
        }

        static void EditLines(List<string> lines)
        {

            Editor editor = new Editor(lines, 1);
            editor.Display();
            editor.Run();
            
            Console.SetCursorPosition(0, editor.MaxRow + 2);
        }
    }
}
