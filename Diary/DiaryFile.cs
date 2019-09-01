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
        private static readonly string[] headlines = { "Headline of the day:", "Main Text:", "Highlight of the day:", "Low point of the day:", "Rating of the day (1-10):" };

        public static string[] GetHeadlinesArray()
        {
            return headlines;
        }

        public static List<string> GetHeadlinesList()
        {
            List<string> headlinesList = new List<string>();
            headlinesList.AddRange(headlines);
            return headlinesList;
        }

        public static List<string> GetFileNames()
        {
            string[] filePaths = Directory.GetFiles(folderPath, "*.txt");
            List<string> fileNames = new List<string>();
            for(int i = 0; i < filePaths.Length; i++)
            {
                fileNames.Add(filePaths[i].Replace(folderPath, ""));
            }
            return fileNames;
        }

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(folderPath, fileName);
        }

        public static void SaveToFile(Dictionary<string, List<string>> headlineTextPairs, string fileName)
        {
            string filePath = GetFilePath(fileName);
            foreach(string headline in headlineTextPairs.Keys)
            {
                WriteToFile(headline, headlineTextPairs[headline], filePath);
            }

        }

        private static void WriteToFile(string headline, List<string> lines, string filePath)
        {
            File.AppendAllText(filePath, headline + "\n");
            File.AppendAllLines(filePath, lines);
            File.AppendAllText(filePath, "");
        }


        public static Dictionary<string, List<string>> GetHeadlineTextPairs(string fileName)
        {
            Dictionary<string, List<string>> headlineTextPairs = new Dictionary<string, List<string>>();
            string filePath = GetFilePath(fileName);

            foreach (string headline in headlines)
            {
                List<string> lines = GetTextFromHeadline(headline, filePath);
                headlineTextPairs.Add(headline, lines);
            }
            return headlineTextPairs;

        }


        private static List<string> GetTextFromHeadline(string headline, string filePath)
        {
            List<string> lines = new List<string>();
            StreamReader file = new StreamReader(filePath);

            while (file.ReadLine() != headline) ;

            string line;
            while ((line = file.ReadLine()) != "")
            {
                lines.Add(line);
            }
            file.Close();
            return lines;
        }
    }
}
