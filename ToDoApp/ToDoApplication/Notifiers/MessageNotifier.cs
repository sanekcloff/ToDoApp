using LogHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApplication.Notifiers
{
    // Вызов всплывающих окон вместе с логирование действия
    internal abstract class MessageNotifier
    {
        internal static void Warnig(string message, string caption = "Предупреждение")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            Logger.AddLog(message);
        }
        internal static void Error(string message, string caption = "Ошибка")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            Logger.AddLog(message);
        }
        internal static void Information(string message, string caption = "Информация")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            Logger.AddLog(message);
        }
        internal static void Stop(string message, string caption = "Стоп")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Stop);
            Logger.AddLog(message);
        }
    }
}
