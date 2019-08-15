using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    class DiaryFile
    {
        private static readonly string folderPath = Environment.CurrentDirectory.Replace("bin\\Debug", "files\\");
        readonly string fileName;
        readonly string filePath;
        public DiaryFile(string _fileName)
        {
            fileName = _fileName;
            filePath = Path.Combine(folderPath, fileName);
        }

        public static string[] GetFileNames()
        {
            string[] filePaths = Directory.GetFiles(folderPath, "*.txt");
            string[] fileNames = new string[filePaths.Length];
            for(int i = 0; i < filePaths.Length; i++)
            {
                fileNames[i] = filePaths[i].Replace(folderPath, "");
            }
            return fileNames;
        }

        public void WriteAll(List<List<string>> text)
        {
            WriteHeadline(text[0]);
            WriteMain(text[1]);
            WriteHighlight(text[2]);
            WriteLowPoint(text[3]);
            WriteRating(text[4]);
        }

        public void WriteHeadline(List<string> text)
        {
            File.AppendAllText(filePath, "h\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "\n");
        }

        public void WriteMain(List<string> text)
        {
            File.AppendAllText(filePath, "m\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "\n");
        }

        public void WriteHighlight(List<string> text)
        {
            File.AppendAllText(filePath, "hi\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "\n");
        }

        public void WriteLowPoint(List<string> text)
        {
            File.AppendAllText(filePath, "l\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "\n");
        }

        public void WriteRating(List<string> text)
        {
            File.AppendAllText(filePath, "r\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "\n");
        }
    }
}
