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
        static readonly string LOGS_FILE_DIR = @$"{PROJ_DIR}\{LOGS_DIR}\{DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss")}.txt";

        static Process loggerFile;

        public static bool isEnableConsoleLog { get; private set; } = false;

        static Logger()
        {
            logs = new List<string>();

            if(File.Exists(LOGS_FILE_DIR))
                File.Delete(LOGS_FILE_DIR);

            Directory.CreateDirectory(LOGS_DIR);

            loggerFile = new Process();
            loggerFile.StartInfo.FileName = "LogHandler.exe";
            AddLog($"Запись отладки: {DateTime.Now.ToString("G")}");
        }

        public static void Start()
        {
            try
            {
                loggerFile.Start();
            }
            catch (Exception ex)
            {
                AddLog($"{ex.GetType().Name} - {ex.Message}");
            }
        }
        public static void Stop()
        {
            try
            {
                loggerFile.Close();
                loggerFile.CloseMainWindow();
            }
            catch (Exception ex)
            {
                AddLog($"{ex.GetType().Name} - {ex.Message}");
            }
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
        public static void EnableConsole()
        {
            isEnableConsoleLog = true;
            Start();
        }
        public static void DisableConsole()
        {
            isEnableConsoleLog = false;
            Stop();
        }
    }
}
