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

        protected bool Set<T> (ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged (string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (propertyName));
        }

        protected Window OpenWindow(Window window)
        {
            var temp = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            temp.Close();
            window.Show();
            return window;
        }
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
        protected void AppClose()
        {
            Application.Current.Shutdown();
        }
    }
}
