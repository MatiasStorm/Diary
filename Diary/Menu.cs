using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    class Menu
    {
        private List<string> options;
        private int index;
        private readonly int startIndex;
        private int originalPos;
        public Menu(string[] _options, int _startIndex)
        {
            options = _options.ToList();
            startIndex = _startIndex;
            index = startIndex;
        }

        public Menu(List<string> _options, int _startIndex)
        {
            options = _options;
            startIndex = _startIndex;
            index = startIndex;
        }

        public void Dispaly()
        {
            index = startIndex;
            Console.SetCursorPosition(0, index);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string f in this.options)
            {
                Console.WriteLine(f);
            }
            originalPos = Console.CursorTop;
            Console.SetCursorPosition(0, index);
            Select();
        }

        public void AddOption(string option)
        {
            options.Add(option);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(option);
            originalPos = Console.CursorTop;
            Console.SetCursorPosition(0, index);
            Select();
        }

        public string Run()
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
                    ResetCursor();
                    return ReturnSelected();
                }
                k = Console.ReadKey();
                ResetCursor();
            }
            return "q";
        }

        public void Up()
        {
            if (index < options.Count + startIndex  && index > startIndex)
            {
                Deselect();
                
            }
            if(index > startIndex)
            {
                index--;
                Select();
            }
        }
        public void Down()
        {
            if(index >= startIndex && index < (options.Count + startIndex)- 1)
            {
                Deselect();
            }
            if(index < (options.Count + startIndex) - 1)
            {
                index++;
                Select();
            }
        }

        public void ResetCursor()
        {
            Console.SetCursorPosition(0, originalPos);
        }

        private void Select()
        {
            Console.SetCursorPosition(0, index);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(options[index - startIndex]);
            Console.ResetColor();
            Console.SetCursorPosition(0, originalPos);
        }

        private void Deselect()
        {
            Console.SetCursorPosition(0, index);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(options[index - startIndex]);
            Console.ResetColor();
            Console.SetCursorPosition(0, originalPos);
        }

        public string ReturnSelected()
        {
            return options[index - startIndex];
        }
    }
}
