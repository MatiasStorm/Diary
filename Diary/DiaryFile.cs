using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    static class DiaryFile
    {
        static readonly string folderPath = Environment.CurrentDirectory.Replace("bin\\Debug", "files\\");
        static readonly string[] headlines = { "h", "m", "hi", "l", "r" };

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

        public static void WriteAll(List<List<string>> text, string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);
            WriteHeadline(text[0], filePath);
            WriteMain(text[1], filePath);
            WriteHighlight(text[2], filePath);
            WriteLowPoint(text[3], filePath);
            WriteRating(text[4], filePath);
        }

        private static void WriteHeadline(List<string> text, string filePath)
        {
            File.AppendAllText(filePath, "h\n");
            File.AppendAllLines(filePath, text);
        }

        private static void WriteMain(List<string> text, string filePath)
        {
            File.AppendAllText(filePath, "m\n");
            File.AppendAllLines(filePath, text);
        }

        private static void WriteHighlight(List<string> text, string filePath)
        {
            File.AppendAllText(filePath, "hi\n");
            File.AppendAllLines(filePath, text);
        }

        private static void WriteLowPoint(List<string> text, string filePath)
        {
            File.AppendAllText(filePath, "l\n");
            File.AppendAllLines(filePath, text);
        }

        private static void WriteRating(List<string> text, string filePath)
        {
            File.AppendAllText(filePath, "r\n");
            File.AppendAllLines(filePath, text);
            File.AppendAllText(filePath, "END");
        }




        //private List<string> TextBetweenHeadlines(string h1, string h2)
        //{
        //    List<string> text = new List<string>();
        //    StreamReader file = new StreamReader(filePath);
        //    string line = file.ReadLine();
        //    while (line != h1)
        //    {
        //        line = file.ReadLine();
        //    }
        //    while ((line = file.ReadLine()) != h2)
        //    {
        //            text.Add(line);
        //    }
        //    file.Close();
        //    return text;
        //}

        //public List<string> GetHeadline()
        //{
        //    return TextBetweenHeadlines("h", "m");
        //}
        //public List<string> GetMain()
        //{
        //    return TextBetweenHeadlines("m", "hi");
        //}
        //public List<string> GetHighligt()
        //{
        //    return TextBetweenHeadlines("hi", "l");
        //}
        //public List<string> GetLowPoint()
        //{
        //    return TextBetweenHeadlines("l", "r");
        //}

        //public List<string> GetRating()
        //{
        //    return TextBetweenHeadlines("r", "END");
        //}

    }
}
