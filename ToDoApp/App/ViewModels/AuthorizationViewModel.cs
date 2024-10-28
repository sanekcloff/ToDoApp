using ToDoApplication.Command;
using ToDoApplication.Views;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoApplication.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        public AuthorizationViewModel()
        {
            LoginCommand = new RelayCommand(o =>
            {
                var user = UserService.Find(login, password);
                if (user != null)
                {
                    var newWindnow = new MainView(user);
                    var currentWindow = Application.Current.MainWindow;
                    Application.Current.MainWindow = newWindnow;
                    newWindnow.Show();
                    currentWindow.Close();
                }

            });
            RegisterCommand = new RelayCommand(o =>
            {
                UserService.AddUser(lastname, firstname, middlename, login, password);
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
