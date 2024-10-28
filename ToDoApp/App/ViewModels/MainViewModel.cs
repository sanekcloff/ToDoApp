using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.Command;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(User user)
        {
            currentUser = user;
        }
        private User currentUser;

        public User User { get => currentUser; set => Set(ref currentUser, value, nameof(User)); }
    }
}
