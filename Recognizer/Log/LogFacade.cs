using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Recognizer.BoW;

namespace Recognizer.Log
{
    static class LogFacade
    {
        private static readonly string LOG_DIR = Properties.Settings.Default.LogDir;

        private const string BOW_FILE = "bow.txt";

        static LogFacade()
        {
            Directory.CreateDirectory(LOG_DIR);
        }

        public static void LogBOW(BagOfWords bow)
        {
            string filepath = CreateFilePath(BOW_FILE);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (var kvp in bow.OrderByDescending(kvp => kvp.Value))
                {
                    writer.WriteLine(string.Format("{0}\t{1}", kvp.Value, kvp.Key));
                }
            }
        }

        private static string CreateFilePath(string filename)
        {
            string ext = Path.GetExtension(filename);
            string name = Path.GetFileNameWithoutExtension(filename);

            StringBuilder sb = new StringBuilder(name);
            sb.Append('_').Append(NowStr()).Append(ext);
            string finalname = sb.ToString();

            return Path.Combine(LOG_DIR, finalname);
        }

        private static string NowStr()
        {
            return DateTime.Now.ToString("dd_MM_yyyy-HH_mm"); 
        }
    }
}
