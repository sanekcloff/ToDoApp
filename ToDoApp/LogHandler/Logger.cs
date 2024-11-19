using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHandler
{
    public static class Logger
    {
        static readonly string PROJ_DIR = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        const string LOGS_DIR = "Logs";
        const string LOG_NAME = "Log Observer";
        static readonly string LOG_FILE_DIR = $"{PROJ_DIR}\\{LOGS_DIR}\\Logs.txt";
        private static List<string> logs;
        static Logger()
        {
            logs = new List<string>();

            if(File.Exists(LOG_FILE_DIR))
                File.Delete(LOG_FILE_DIR);

            Directory.CreateDirectory(LOGS_DIR);
        }
        public static void Start()
        {
            Process loggerFile = new Process();
            loggerFile.StartInfo.FileName = "LogHandler.exe";
            loggerFile.Start();
        }
        public async static void AddLog(string message)
        {
            await Task.Run( () => 
                {
                    logs.Add($"[{DateTime.Now.ToString("G")}]" + LOG_NAME + ": " + message);
                });
            WriteToFile();
            Debug.WriteLine(message);
        }
        public static void DisplayLogs()
        {
            while(true)
            {
                if (File.Exists(LOG_FILE_DIR))
                {
                    var logs = File.ReadAllLines(LOG_FILE_DIR);
                    foreach (var log in logs)
                    {
                        Console.WriteLine(log);
                    }
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
        private static void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(LOG_FILE_DIR))
            {
                logs.ForEach(log => writer.WriteLine(log));
            }
        }
    }
}
