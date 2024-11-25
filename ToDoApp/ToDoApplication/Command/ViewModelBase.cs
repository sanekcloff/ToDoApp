using LogHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;

namespace ToDoApplication.Command
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Установка нового значения и вызов события
        protected bool Set<T> (ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        // Метод вызова обновления визуала при обновлении свойства
        protected virtual void OnPropertyChanged (string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (propertyName));
        }
        // Обычное открытие окна
        protected Window OpenWindow(Window window)
        {
            var temp = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            temp.Close();
            window.Show();
            return window;
        }
        // Открытие диалогового окна
        protected Window OpenWindowDialog(Window window)
        {
            try
            {
                Application.Current.MainWindow.Effect = new BlurEffect();
                window.ShowDialog();
                Application.Current.MainWindow.Effect = null;
            }
            catch (Exception ex)
            {
                Logger.AddLog($"{ex.GetType().Name} - {ex.Message}");
            }
            
            return window;
        }
        // Закртие приложения
        protected void AppClose()
        {
            Application.Current.Shutdown();
        }
    }
}
