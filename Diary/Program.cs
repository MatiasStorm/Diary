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
 
     
     */

namespace Diary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello welcome to your Diary Program (Press a key to continue)");
            Console.WriteLine(DateTime.Now.ToString().Split(' ')[0]);
            bool running = true;
            Console.ReadKey();
            while (running)
            {
                string option = MainMenu();
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

        static string MainMenu()
        {
            Console.Clear();
            string[] options = { "New File", "Search Files", "Edit File", "View File"};
            Console.WriteLine("(Main menu) Select a function:");
            Menu menu = new Menu(options, Console.CursorTop);
            menu.Dispaly();
            return menu.run();
        }

        static void Write()
        {
            Console.Clear();
            string[] messages = { "Headline of the day:", "Main Text:", "Highlight of the day:", "Low point of the day:", "Rating of the day (1-10):" };
            List<List<string>> textLists = new List<List<string>>();

            for(int i = 0; i < messages.Length; i++)
            {
                Console.WriteLine(messages[i]);
                List<string> lines = new List<string>();
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
                textLists.Add(lines);
            }

            string k = YesOrNo("Finished Writing?");
            string fileName = DateTime.Now.ToString().Split(' ')[0] + ".txt";
            if (k == "y" || k == "Y")
            {
                DiaryFile file = new DiaryFile(fileName);
                file.WriteAll(textLists);

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
            Menu menu = new Menu(options, 1);
            menu.AddOption("Go Back");
            menu.Dispaly();
            string option = menu.run();
            if(option != "q" && option != "Go Back")
            {
                EditFile(option);
            }
        }

        static void EditFile(string fileName)
        {
            string[] options = { "Headline", "Main Text", "Highlight", "Low Point", "Rating", "Done Editing!" };
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

        static void EditParagraph(string fileName, string option)
        {
            DiaryFile file = new DiaryFile(fileName);
            Dictionary<string, Func<List<string>>> functions = new Dictionary<string, Func<List<string>>>();
            functions.Add("Headline", file.GetHeadline);
            functions.Add("Main Text", file.GetMain);
            functions.Add("Highlight", file.GetHighligt);
            functions.Add("Low Point", file.GetLowPoint);
            functions.Add("Rating", file.GetRating);

            List<string> text = functions[option]();

            Console.Clear();
            Console.WriteLine("Editing " + option);


            Editor Editor = new Editor(text, 1);
            Editor.Display();
            Console.ReadKey();
        }
    }
}
