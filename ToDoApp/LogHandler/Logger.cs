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
        const string LOGS_DIR = "Logs";
        const string LOG_NAME = "Log Observer";

        static List<string> logs;

        static readonly string PROJ_DIR = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        static readonly string LOGS_FILE_DIR = @$"{PROJ_DIR}\{LOGS_DIR}\Logs.txt";

        static Process loggerFile;

        static Logger()
        {
            logs = new List<string>();

            if(File.Exists(LOGS_FILE_DIR))
                File.Delete(LOGS_FILE_DIR);

            Directory.CreateDirectory(LOGS_DIR);
        }

        public static void Start()
        {
            loggerFile = new Process();
            loggerFile.StartInfo.FileName = "LogHandler.exe";
            loggerFile.Start();
        }
        public static void Stop()
        {
            loggerFile.Close();
        }
        public static string AddLog(string message)
        {
            logs.Add($"[{DateTime.Now.ToString("G")}]" + LOG_NAME + ": " + message);
            WriteToFile();
            Debug.WriteLine(message);
            return message;
        }
        public async static Task<string> AddLogAsync(string message)
        {
            await Task.Run( () => 
                {
                    logs.Add($"[{DateTime.Now.ToString("G")}]" + LOG_NAME + ": " + message);
                    WriteToFile();
                    Debug.WriteLine(message);
                });
            return message;
        }
        public static void DisplayLogs()
        {
            while (true)
            {
                if (File.Exists(LOGS_FILE_DIR))
                {
                    var logs = File.ReadAllLines(LOGS_FILE_DIR);
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
            using (StreamWriter writer = new StreamWriter(LOGS_FILE_DIR))
            {
                logs.ForEach(log => writer.WriteLine(log));
            }
        }
    }
}
