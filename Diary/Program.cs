using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TODO:
    - Create a console option selection class, which can return the selected value.
    - Create a diary class for writting in the diary
    - Create a file class for opening, writting and searching files.
    - Add a settings menu.
     
     */

namespace Diary
{
    class Program
    {
        static bool running = true;
        static void Main(string[] args)
        {
            WriteMessage("Hello welcome to your Diary Program (Press a key to continue)");
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
                    case "q":
                        running = false;
                        break;
                }
            }
        }

        static void WriteMessage(string message)
        {
            Console.WriteLine(message);
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

            string[] mainMenuOptions = { "New File", "Search Files", "Edit File", "View File"};
            WriteMessage("(Main menu) Select a function:");

            return GetOptionFromMenu(mainMenuOptions);

        }

        static string GetOptionFromMenu(string[] options)
        {
            Menu menu = new Menu(options, Console.CursorTop);
            menu.Dispaly();
            return menu.run();
        }


        static void Write()
        {
            ClearConsole();

            string[] headlines = DiaryFile.GetHeadlinesArray();

            Dictionary<string, List<string>> headlineTextPairs = new Dictionary<string, List<string>>();


            for(int i = 0; i < headlines.Length; i++)
            {
                string headline = headlines[i];
                List<string> lines = new List<string>();

                Console.WriteLine(headline);

                if(i == 1)
                {
                    string line;
                    while((line = Console.ReadLine()) != "")
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

            string k = YesOrNo("Finished Writing?");
            string fileName = DateTime.Now.ToString().Split(' ')[0] + ".txt";
            if (k == "y" || k == "Y")
            {

                DiaryFile.SaveToFile(headlineTextPairs, fileName);
                Console.Write("File has been created... (Press key to continue)");
                Console.ReadKey();
            }
            else
            {
                // Possibility to edit
                EditFile(fileName);
            }


        }

        static string YesOrNo(string message)
        {
            string k = "";
            while (k != "y" && k != "Y" &&
                  k != "n" && k != "N")
            {
                Console.Write(message + " [Y/n]: ");
                int origPos = Console.CursorTop;
                k = Console.ReadLine();

                // Clear line:
                Console.SetCursorPosition(0, origPos);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, origPos);
            }
            return k;
        }

        static void ViewMenu()
        {

        }

        static void ViewFile()
        {

        }

        static void Search()
        {

        }

        static void EditMenu()
        {
            Console.Clear();
            string[] options = DiaryFile.GetFileNames();
            Console.WriteLine("Edit Menu (Pick a file to edit):");
            Menu fileMenu = new Menu(options, 1);
            fileMenu.AddOption("Go Back");
            fileMenu.Dispaly();
            string option = fileMenu.run(); // change to filename
            if(option != "q" && option != "Go Back")
            {
                EditFile(option);
            }
        }

        static void EditFile(string fileName)
        {
            List<string> options = DiaryFile.GetHeadlinesList();
            options.Add("Done Editing!");
            string option = "";
            Menu menu = new Menu(options, 1);

            while (option != "Done Editing!")
            {
                Console.Clear();
                Console.WriteLine("Edit Menu, File: " + fileName);
                menu.Dispaly();
                option = menu.run();
                if (option != "Done Editing!")
                {
                    EditParagraph(fileName, option);
                }
            }
        }

        static void EditParagraph(string fileName, string headline)
        {
            Dictionary<string, List<string>> headlineTextPairs = DiaryFile.GetHeadlineTextPairs(fileName);

            List<string> text = headlineTextPairs[headline];

            Console.Clear();
            Console.WriteLine("Editing " + "\'" + headline + "\'");


            Editor editor = new Editor(text, 1);
            editor.Display();
            editor.Run();
            Console.ReadKey();
        }
    }
}
