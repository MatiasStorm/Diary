using System;
using System.Collections.Generic;
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
            ConsoleKeyInfo k = Console.ReadKey();
            while(k.KeyChar != 'q')
            {
                string option = MainMenu(k);
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
                }
            }
        }

        static string MainMenu(ConsoleKeyInfo k)
        {
            string[] options = { "New File", "Search Files", "Edit File" };
            Console.WriteLine("(Main menu) Select a function:");
            Menu menu = new Menu(options, Console.CursorTop);
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
            Console.WriteLine("Opend new document");
            Console.WriteLine();
            Console.WriteLine("Headline of the day: ");
            string headline = Console.ReadLine();
            Console.WriteLine(headline);
        }

        static void Search()
        {

        }

        static void Edit()
        {

        }
    }
}
