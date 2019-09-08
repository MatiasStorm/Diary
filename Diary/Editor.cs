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
        private int column = 0;
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
            SetCursorToStartPosition();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
            Select();
        }

        private void SetCursorToStartPosition()
        {
            Console.SetCursorPosition(0, startRow);
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
            WriteDeselected(0, column);
            WriteSelected(column, 1);
            WriteDeselected(column + 1);
            SetCursorToCurrentPosition();
        }

        private void SetCursorToCurrentPosition()
        {
            Console.SetCursorPosition(column, consoleRow);
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
                column = column > lineLength ? lineLength : column;
            }
        }

        private void DecrementRow()
        {
            if(consoleRow > startRow)
            {
                consoleRow--;
                textIndex--;
                int lineLength = text[textIndex].Length - 1;
                column = column > lineLength ? lineLength : column;
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
            if(column < lineLength)
            {
                column++;
            } else if (consoleRow < maxRow)
            {
                IncrementRow();
                SetColumn(0);
            }
        }

        private void DecrementColumn()
        {
            if (column > 0)
            {
                column--;
            } else if (consoleRow > startRow)
            {
                Deselect();
                DecrementRow();
                SetColumn(text[textIndex].Length - 1);
            }
        }

        private void SetColumn(int value)
        {
            column = value;
        }

        public void Delete()
        {
            Deselect();
            if (column < text[textIndex].Length - 1)
            {
                RemoveCharcterFromSelectedLine(column);
                Select();
            }
            else if(consoleRow < maxRow)
            {
                ClearConsoleFromLine(startRow);
                MergeLines(textIndex, textIndex + 1);
                Display();
            }
        }

        public void BackSpace()
        {
            if(column == 0 && consoleRow > startRow)
            {
                ClearConsoleFromLine(startRow);
                SetColumn(text[textIndex - 1].Length);
                MergeLines(textIndex - 1, textIndex);                
                DecrementRow();
                Display();
            }
            else
            {
                Deselect();
                RemoveCharcterFromSelectedLine(column - 1);
                DecrementColumn();
                Select();
            }
        }

        private void MergeLines(int first, int last)
        {
                string line = text[last];
                text.Remove(line);
                maxRow--;
                text[first] += line;
        }

        private void RemoveCharcterFromSelectedLine(int col)
        {
            string line = text[textIndex].Substring(0, Math.Max(0, col));
            line += text[textIndex].Substring(col + 1);
            text[textIndex] = line;
        }

        public void Enter()
        {
            string newLine = SplitLineAtSelected();
            text.Insert(textIndex + 1, newLine);
            maxRow++;

            ClearConsoleFromLine(consoleRow - 1);
            SetColumn(0);
            IncrementRow();
            Display();
        }

        private void ClearConsoleFromLine(int lineNumber)
        {
            for(int row = lineNumber; row < maxRow; row++)
            {
                ClearLine(row);
            }
        }

        private void ClearLine(int lineIndex)
        {
            Console.SetCursorPosition(0, lineIndex + startRow);
            Console.WriteLine(new String(' ', Console.WindowWidth));
        }

        private string SplitLineAtSelected() // Rename
        {
            string lineEnd = text[textIndex].Substring(column);
            text[textIndex] = text[textIndex].Substring(0, column);
            if(text[textIndex].Length == 0)
            {
                text[textIndex] += ' ';
            }
            else if(text[textIndex].Last() != ' ')
            {
                text[textIndex] += ' ';
            }
            return lineEnd;
        }

        public void Type(char character)
        {
            InsertInSelectedArear(character);
            IncrementColumn();
            Select();
        }

        public void InsertInSelectedArear(char character)
        {
            string line = text[textIndex].Substring(0, Math.Max(0, column));
            line += character + text[textIndex].Substring(column);
            text[textIndex] = line;
        }
    }
}
