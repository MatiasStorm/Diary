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
                        Edit();
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
            ConsoleKeyInfo k = Console.ReadKey();
            while(k.KeyChar != 'q')
            {
                if(k.Key == ConsoleKey.UpArrow)
                {
                    menu.Up();
                }
                else if(k.Key == ConsoleKey.DownArrow)
                {
                    menu.Down();
                }
                else if(k.Key == ConsoleKey.Enter)
                {
                    menu.ResetCursor();
                    return menu.ReturnSelected();
                }
                k = Console.ReadKey();
                menu.ResetCursor();
            }
            return "q";
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

            string k = YesOrNo();

            if (k == "y" || k == "Y")
            {
                DiaryFile file = new DiaryFile(DateTime.Now.ToString().Split(' ')[0] + ".txt");
                file.WriteAll(textLists);

                Console.Write("File has been created... (Press key to continue)");
                Console.ReadKey();
            }
            else
            {
                // Possibility to edit
                Edit();
            }
        }

        static string YesOrNo()
        {
            string k = "";
            while (k != "y" && k != "Y" &&
                  k != "n" && k != "N")
            {
                Console.Write("Finished Writing? [Y/n]: ");
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

        static void Edit()
        {

        }
    }
}
