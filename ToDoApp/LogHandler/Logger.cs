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
        // Имя папки для хранения фала логов
        const string LOGS_DIR = "Logs";
        // Имя отображаемое как логгер
        const string LOG_NAME = "Log Observer";

        // Список логов
        static List<string> logs;

        // Путь до проекта
        static readonly string PROJ_DIR = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        // Путь до фала логов
        static readonly string LOGS_FILE_DIR = @$"{PROJ_DIR}\{LOGS_DIR}\{DateTime.Now.ToString("dd.MM.yyyy-HH.mm.ss")}.txt";

        // Процесс для запуска логгера с конслью
        static Process loggerFile;

        // Свойство для переключению работы с конслью
        public static bool isEnableConsoleLog { get; private set; } = false;

        // Вызов стартовых инициализаций при первом вызове Logger
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

        // Начать работу консоли
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
        // Остановить работу консоли
        public static void Stop()
        {
            try
            {
                loggerFile.Kill();
            }
            catch (Exception ex)
            {
                AddLog($"{ex.GetType().Name} - {ex.Message}");
            }
        }
        // Запись логов
        public static string AddLog(string message)
        {
            logs.Add($"[{DateTime.Now.ToString("G")}]" + LOG_NAME + ": " + message);
            WriteToFile();
            Debug.WriteLine(message);
            return message;
        }
        // Запись логов асинхронно
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
        // Включени консоли
        public static void EnableConsole()
        {
            isEnableConsoleLog = true;
            Start();
        }
        // Отключени консоли
        public static void DisableConsole()
        {
            isEnableConsoleLog = false;
            Stop();
        }
        // Считавание всех фалов с логами
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
        // Выбор пунктов из меню
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
        // Запись в файл
        private static void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(LOGS_FILE_DIR))
            {
                logs.ForEach(log => writer.WriteLine(log));
            }
        }
    }
}
