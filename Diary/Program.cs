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
            List<string> functions = new List<string>();
            functions.Add("New File");
            functions.Add("Search Files");
            functions.Add("Edit Files");

            Console.Clear();
            foreach(string f in functions)
            {
                Console.WriteLine(f);
            }
            //Console.WriteLine();
            //Console.Write("input: ");

            var originalpos = Console.CursorTop;

            var k = Console.ReadKey();
            var conIndex = 1;
            var funcIndex = functions.Count;

            while (k.KeyChar != 'q')
            {

                if (k.Key == ConsoleKey.UpArrow)
                {
                    funcIndex--;

                    Console.SetCursorPosition(0, Console.CursorTop - conIndex);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(functions.ElementAt(funcIndex));
                    Console.ResetColor();
                    Console.SetCursorPosition(7, originalpos);
                    Console.Write(funcIndex);
                    Console.SetCursorPosition(0, Console.CursorTop - conIndex);

                    conIndex++;

                }
                if(k.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, originalpos + 1);
                    Console.WriteLine(functions.ElementAt(funcIndex));

                }

                Console.SetCursorPosition(8, originalpos);
                k = Console.ReadKey();
            }


            //Console.WriteLine("Hello welcome to you Diary Program");
            //Console.Write("Select a function: n for New File, s for Search, v for View excisting files");
            //Console.ReadKey();
            //Console.WriteLine("How was your day");

            //Console.ReadKey
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
