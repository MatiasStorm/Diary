using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    class Editor
    {
        readonly int startRow;
        readonly int maxRow;
        List<string> text;
        private int row, col;
        public Editor(List<string> _text, int _startRow)
        {
            startRow = _startRow;
            row = startRow;
            col = 0;
            text = _text;
            maxRow = text.Count + startRow;
        }

        public void Display()
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
            Console.SetCursorPosition(col, row);
        }

        public void run()
        {
            ConsoleKeyInfo k = Console.ReadKey();
            while (k.KeyChar != 'q')
            {

                if (k.Key == ConsoleKey.UpArrow)
                {
                    Up();
                }
                else if (k.Key == ConsoleKey.DownArrow)
                {
                    Down();
                }
                else if (k.Key == ConsoleKey.Enter)
                {
                    Enter();
                }
                else if (k.Key == ConsoleKey.Backspace)
                {
                    BackSpace();
                }
                else if (Char.IsLetter(k.KeyChar))
                {
                    Type(k.KeyChar);
                }
                else if(k.Key == ConsoleKey.Spacebar)
                {
                    Space();
                }

                k = Console.ReadKey();
                ResetCursor();
            }
        }

        public void Up()
        {

        }

        public void Down()
        {

        }

        public void Left()
        {

        }

        public void Right()
        {

        }

        public void Delete()
        {

        }

        public void BackSpace()
        {

        }

        public void Enter()
        {

        }

        public void Type(char letter)
        {

        }
        
        public void Space()
        {

        }
    }
}
