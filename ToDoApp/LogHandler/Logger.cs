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
        public static void GetAllLogFiles()
        {
            try
            {
                var files = Directory.GetFiles($@"{PROJ_DIR}\{LOGS_DIR}");
                DisplayLogs(MenuSelector(files));

            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Указанная папка не найдена.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к указанной папке.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        private static void DisplayLogs(string dir)
        {
            while (true)
            {
                if (File.Exists(dir))
                {
                    var logs = File.ReadAllLines(dir);
                    foreach (var log in logs)
                    {
                        Console.WriteLine(log);
                    }
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
        private static string MenuSelector(ICollection<string> files)
        {
            var selectedTab = files.Count-1;
            var menuTabs = files.Order().ToList();
            while (true)
            {
                foreach (var tab in menuTabs)
                {
                    if (menuTabs.IndexOf(tab) == selectedTab)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[{menuTabs.IndexOf(tab)}]\t{Path.GetFileName(tab)}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"[{menuTabs.IndexOf(tab)}]\t{Path.GetFileName(tab)}");
                    }
                }
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedTab == 0)
                            selectedTab = menuTabs.Count-1;
                        else
                            selectedTab--;
                        Console.Clear();
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedTab == menuTabs.Count-1)
                            selectedTab = 0;
                        else
                            selectedTab++;
                        Console.Clear();
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        return menuTabs[selectedTab];

                    default:
                        Console.Clear();
                        break;
                }
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
