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
            for (int i = 0; i < text.Count; i++) text[i] += " ";
            maxRow = (text.Count - 1) + startRow;
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
            Select();
        }

        public void Run()
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
                else if (k.Key == ConsoleKey.LeftArrow)
                {
                    Left();
                }
                else if (k.Key == ConsoleKey.RightArrow)
                {
                    Right();
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
                    Type(' ');
                }
                k = Console.ReadKey();
            }
        }
        private void Select()
        {
            Console.SetCursorPosition(0, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[row - startRow].Substring(0, col));

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text[row - startRow][col]);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[row - startRow].Substring(col + 1));
            Console.SetCursorPosition(col, row);
            Console.ResetColor();
        }

        private void Deselect()
        {
            Console.SetCursorPosition(0, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[row - startRow]);
        }

        public void Up()
        {
            if(row > startRow)
            {
                Deselect();
                row -= 1;
                int lineLength = text[row - startRow].Length - 1;
                col = col > lineLength ? lineLength : col;
            }
                Select();
        }

        public void Down()
        {
            if (row < maxRow)
            {
                Deselect();
                row += 1;
                int lineLength = text[row - startRow].Length - 1;
                col = col > lineLength ? lineLength : col;
            }
                Select();
        }

        public void Left()
        {
            if(col > 0)
            {
                Deselect();
                col -= 1;
            }
                Select();
        }

        public void Right()
        {
            if(col < text[row - startRow].Length - 1)
            {
                Deselect();
                col += 1;
            }
            Select();
        }

        public void Delete()
        {

        }

        public void BackSpace()
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
            Console.ResetColor();
            string line = text[row - startRow].Substring(0, Math.Max(0, col - 1));
            line += text[row - startRow].Substring(col);
            text[row - startRow] = line;
            col -= 1;
            Select();
        }

        public void Enter()
        {

        }

        public void Type(char letter)
        {
            string line = text[row- startRow].Substring(0, Math.Max(0, col));
            line += letter + text[row - startRow].Substring(col);
            text[row - startRow] = line;
            col += 1;
            Select();
        }
        
    }
}
