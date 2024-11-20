using ToDoApplication.Command;
using ToDoApplication.Views;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Data.Context;
using Data.Models;
using ToDoApplication.Handlers;
using ToDoApplication.Notifiers;
using LogHandler;

namespace ToDoApplication.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        public AuthorizationViewModel()
        {

            LoginCommand = new RelayCommand(o =>
            {
                if (!string.IsNullOrEmpty(login) || !string.IsNullOrEmpty(password))
                {
                    
                    var user = UserService.Find(login, password)!;
                    if (user != null)
                    {
                        var newWindnow = new MainView(user);
                        var currentWindow = Application.Current.MainWindow;
                        Application.Current.MainWindow = newWindnow;
                        newWindnow.Show();
                        currentWindow.Close();
                        MessageNotifier.Information("Успешный вход!");
                        Logger.AddLog($"Вход под учётной запись {user.Fullname}!");
                    }
                    else
                    {
                        MessageNotifier.Warnig("Некоректные данные!");
                        Logger.AddLog($"Ввод некорректных данных!");
                    }
                }
                else
                {
                    MessageNotifier.Warnig("Введите данные!");
                    Logger.AddLog($"Ввод пустых полей для входа!");
                }
            });
            RegisterCommand = new RelayCommand(o =>
            {
                var user = User.Create(lastname, firstname, middlename, login, password);
                if (InputValidator.IsValid(user) && ValueHandler.IsCorrect(user))
                    UserService.Add(user);
                else
                    ValueHandler.IsCorrect(user);
            });
        }

        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string login = string.Empty;
        private string password = string.Empty;

        public string Firstname { get => firstname; set => Set(ref firstname, value, nameof(Firstname)); }
        public string Lastname { get => lastname; set => Set(ref lastname, value, nameof(Lastname)); }
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }
        public string Login { get => login; set => Set(ref login, value, nameof(Login)); }
        public string Password { get => password; set => Set(ref password, value, nameof(Password)); }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; } 
    }
}
