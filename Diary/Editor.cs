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
        List<string> text;
        private int consoleRow, maxRow;
        private int col = 0;
        private int textIndex = 0;
        public Editor(List<string> _text, int _startRow)
        {
            text = _text;
            startRow = _startRow;
            consoleRow = startRow;
            maxRow = (text.Count - 1) + startRow;
        }

        public void Display()
        {
            Console.SetCursorPosition(col, consoleRow);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
            Select();
        }

        public void Run()
        {
            ConsoleKeyInfo k = Console.ReadKey();
            while (k.KeyChar != 'q')
            {
                switch (k.Key)
                {
                    case ConsoleKey.UpArrow :
                        Up();
                        break;
                    case ConsoleKey.DownArrow :
                        Down();
                        break;
                    case ConsoleKey.LeftArrow :
                        Left();
                        break;
                    case ConsoleKey.RightArrow :
                        Right();
                        break;
                    case ConsoleKey.Enter :
                        Enter();
                        break;
                    case ConsoleKey.Backspace :
                        BackSpace();
                        break;
                    case ConsoleKey.Spacebar :
                        Type(' ');
                        break;
                    case ConsoleKey.Delete:
                        Delete();
                        break;
                    default:
                        Type(k.KeyChar);
                        break;
                }
                k = Console.ReadKey();
            }
        }
        private void Select()
        {
            SetCursorStartOfLine();
            WriteDeselected(0, col);
            WriteSelected(col, 1);
            WriteDeselected(col + 1);
            SetCursorCurrentPosition();
        }

        private void SetCursorCurrentPosition()
        {
            Console.SetCursorPosition(col, consoleRow);
        }

        private void SetCursorStartOfLine()
        {
            Console.SetCursorPosition(0, consoleRow);
        }

        private void WriteDeselected(int start, int nChars)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[textIndex].Substring(start, nChars));
        }

        private void WriteDeselected(int start)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[textIndex].Substring(start));
        }

        private void WriteSelected(int start, int nChars)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text[textIndex].Substring(start, nChars));
        }

        private void Deselect()
        {
            SetCursorStartOfLine();
            WriteDeselected(0);
        }

        public void Up()
        {
            Deselect();
            DecrementRow();
            Select();
        }

        public void Down()
        {
            Deselect();
            IncrementRow();
            Select();
        }

        private void IncrementRow()
        {
            if (consoleRow < maxRow)
            {
                consoleRow++;
                textIndex++;
                int lineLength = text[textIndex].Length - 1;
                col = col > lineLength ? lineLength : col;
            }
        }

        private void DecrementRow()
        {
            if(consoleRow > startRow)
            {
                consoleRow--;
                textIndex--;
                int lineLength = text[textIndex].Length - 1;
                col = col > lineLength ? lineLength : col;
            }
        }

        public void Left()
        {
            DecrementCol();
            Select();
        }

        public void Right()
        {
            IncrementCol();
            Select();
        }

        private void IncrementCol()
        {
            string line = text[textIndex];
            int lineLength = line.Length - 1;
            if(col < lineLength)
            {
                col++;
            }
            else if(col == lineLength && line[lineLength] != ' ')
            {
                text[textIndex] += " ";
                col++;
            }
        }

        private void DecrementCol()
        {
            if (col > 0) col--;
        }

        public void Delete()
        {
            Console.SetCursorPosition(col + 1, consoleRow);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
            Console.ResetColor();
            if (col < text[textIndex].Length - 1)
            {
                string line = text[textIndex].Substring(0, Math.Max(0, col));
                line += text[textIndex].Substring(col + 1);
                text[textIndex] = line;
                Select();
            }
            else
            {
                // Reverse new line.
            }
        }

        public void BackSpace()
        {
            if(col == 0)
            {
                // Reverse new line.

            }
            else
            {
                Console.SetCursorPosition(col, consoleRow);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
                Console.ResetColor();
                string line = text[textIndex].Substring(0, Math.Max(0, col - 1));
                line += text[textIndex].Substring(col);
                text[textIndex] = line;
                col--;
                Select();
            }
        }

        public void Enter()
        {
            string newLine = text[textIndex].Substring(col);
            text[textIndex] = text[textIndex].Substring(0, col);
            if(textIndex + 1 == text.Count)
            {
                text.Add(newLine);
            }
            else
            {
                text[textIndex + 1] = newLine + text[textIndex + 1];
            }

            Console.SetCursorPosition(col, consoleRow);
            Console.Write(new string(' ', Console.WindowWidth));
            col = 0;
            consoleRow++;
            textIndex++;
            maxRow++;
            Select();
        }

        public void Type(char letter)
        {
            string line = text[textIndex].Substring(0, Math.Max(0, col));
            line += letter + text[textIndex].Substring(col);
            text[textIndex] = line;
            col++;
            Select();
        }
        
    }
}
