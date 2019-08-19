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
            AddSpaceToEndOfLines();
        }

        private void AddSpaceToEndOfLines()
        {
            for(int i = 0; i < text.Count; i++)
            {
                text[i] += " ";
            }
        }


        public void Display()
        {
            SetCursorToCurrentPosition();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
            Select();
        }

        public void Run()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.KeyChar != 'q')
            {
                KeyEvents(key);
                key = Console.ReadKey();
            }
        }

        public void KeyEvents(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    Up();
                    break;
                case ConsoleKey.DownArrow:
                    Down();
                    break;
                case ConsoleKey.LeftArrow:
                    Left();
                    break;
                case ConsoleKey.RightArrow:
                    Right();
                    break;
                case ConsoleKey.Enter:
                    Enter();
                    break;
                case ConsoleKey.Backspace:
                    BackSpace();
                    break;
                case ConsoleKey.Spacebar:
                    Type(' ');
                    break;
                case ConsoleKey.Delete:
                    Delete();
                    break;
                default:
                    Type(key.KeyChar);
                    break;
            }
        }

        private void Select()
        {
            SetCursorStartOfLine();
            WriteDeselected(0, col);
            WriteSelected(col, 1);
            WriteDeselected(col + 1);
            SetCursorToCurrentPosition();
        }

        private void SetCursorToCurrentPosition()
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
            DecrementColumn();
            Select();
        }

        public void Right()
        {
            IncrementColumn();
            Select();
        }

        private void IncrementColumn()
        {
            string line = text[textIndex];
            int lineLength = line.Length - 1;
            if(col < lineLength)
            {
                col++;
            }
        }

        private void DecrementColumn()
        {
            if (col > 0) col--;
        }

        private void SetColumn(int column)
        {
            col = column;
        }

        public void Delete()
        {
            Deselect();
            if (col < text[textIndex].Length - 1)
            {
                RemoveSelectedFromLine();
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
                Deselect();
                RemoveCharBeforeSelected();
                DecrementColumn();
                Select();
            }
        }

        private void RemoveCharBeforeSelected()
        {
            string line = text[textIndex].Substring(0, Math.Max(0, col - 1));
            line += text[textIndex].Substring(col);
            text[textIndex] = line;
        }

        private void RemoveSelectedFromLine()
        {
            string line = text[textIndex].Substring(0, Math.Max(0, col));
            line += text[textIndex].Substring(col + 1);
            text[textIndex] = line;
        }

        public void Enter()
        {
            string newLine = SplitLineAtSelected();
            AddLineToText(newLine);
            FillConsoleLineFromSelected(' ');
            SetColumn(0);
            IncrementRow();
            Select();
        }

        private void FillConsoleLineFromSelected(char character)
        {
            Console.SetCursorPosition(col, consoleRow);
            Console.Write(new string(character, Console.WindowWidth));
        }

        private string SplitLineAtSelected() // Rename
        {
            string lineEnd = text[textIndex].Substring(col);
            text[textIndex] = text[textIndex].Substring(0, col);
            if (text[textIndex].Last() != ' ') text[textIndex] += ' ';
            return lineEnd;
        }

        private void AddLineToText(string line)
        {
            if (textIndex + 1 == text.Count)
            {
                text.Add(line);
                maxRow++;
            }
            else
            {
                text[textIndex + 1] = line + text[textIndex + 1];
            }
        }


        public void Type(char character)
        {
            InsertInSelectedArear(character);
            IncrementColumn();
            Select();
        }

        public void InsertInSelectedArear(char character)
        {
            string line = text[textIndex].Substring(0, Math.Max(0, col));
            line += character + text[textIndex].Substring(col);
            text[textIndex] = line;
        }
    }
}
