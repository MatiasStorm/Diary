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


            Console.WriteLine("Hello welcome to your Diary Program");
            Console.WriteLine("Select a function:");
            MainMenu();
            //Console.ReadKey();
            //Console.WriteLine("How was your day");

            //Console.ReadKey
        }

        static void MainMenu()
        {
            string[] options = { "New File", "Search Files", "Edit File" };
            Menu menu = new Menu(options, Console.CursorTop);
            var k = Console.ReadKey();

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
                    string option = menu.ReturnSelected();
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
                k = Console.ReadKey();
                menu.ResetCursor();
            }

        }

        static void Write()
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
