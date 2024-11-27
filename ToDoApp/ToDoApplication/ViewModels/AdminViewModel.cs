using Data.Context;
using Data.Models;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using ToDoApplication.Command;
using ToDoApplication.Handlers;
using ToDoApplication.Notifiers;
using ToDoApplication.Views;

namespace ToDoApplication.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public AdminViewModel(User user)
        {
            this.user = user;
            SelectedObjective = null!;
            SelectedUser = null!;
            UpdateLists();
            SelectedAssignee = users[0];
            SelectedCreator = users[0];

            WindowCloseCommand = new(o => { AppClose(); });
            AddObjectiveCommand = new RelayCommand(o =>
            {
                try
                {
                    var newObjective = Objective.Create(SelectedCreator, SelectedAssignee, Title, Description!);
                    if (InputValidator.IsValid(newObjective))
                        ObjectiveService.Add(newObjective);
                }
                catch (Exception ex)
                {
                    MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                }
            });
            UpdateObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        if (InputValidator.IsValid(SelectedObjective))
                            ObjectiveService.Update(SelectedObjective, Title, Description!, SelectedAssignee, SelectedCreator);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });
            DeleteObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Delete(SelectedObjective);

                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType().Name} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
            HideObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Hide(SelectedObjective);

                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка скрыть задачу не выбрав её!");
                }
            });
            ShowObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Show(SelectedObjective);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка показать задачу не выбрав её!");
                }
            });
            ExecuteObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Execute(SelectedObjective);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка выполнить задачу не выбрав её!");
                }
            });
            UndoObjectiveCommand = new RelayCommand(o =>
            {
                if (SelectedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Undo(SelectedObjective);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка отменить задачу не выбрав её!");
                }
            });

            AddUserCommand = new RelayCommand(o =>
            {
                try
                {
                    var newUser = User.Create(Lastname, Firstname, Middlename, Login, Password);
                    if (InputValidator.IsValid(newUser))
                        UserService.Add(newUser);
                }
                catch (Exception ex)
                {
                    MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                }
            });
            UpdateUserCommand = new RelayCommand(o =>
            {
                if (SelectedUser != null)
                {
                    try
                    {
                        if (InputValidator.IsValid(User.Create(lastname, firstname, middlename, login, password)))
                            UserService.Update(SelectedUser,Lastname, Firstname, Middlename, Login, Password);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка редактировать пользователя не выбрав его!");
                }
            });
            DeleteUserCommand = new RelayCommand(o =>
            {
                if (SelectedUser != null)
                {
                    try
                    {
                        UserService.Delete(SelectedUser);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType().Name} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка удалить пользователя не выбрав его!");
                }
            });
            HideUserCommand = new RelayCommand(o =>
            {
                if (SelectedUser != null)
                {
                    try
                    {
                        UserService.Hide(SelectedUser);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка скрыть пользователя не выбрав его!");
                }
            });
            ShowUserCommand = new RelayCommand(o =>
            {
                if (SelectedUser != null)
                {
                    try
                    {
                        UserService.Show(SelectedUser);
                    }
                    catch (Exception ex)
                    {
                        MessageNotifier.Warnig($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageNotifier.Error($"{this.GetType().Name} - попытка показать пользователя не выбрав его!");
                }
            });

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateLists;
            timer.Start();
        }

        #region Session
        private User user;
        #endregion

        #region Objective
        private Objective selectedObjective;
        private List<Objective> objectives;
        private User selectedAssignee;
        private User selectedCreator;
        private string title;
        private string description;

        public Objective SelectedObjective 
        { 
            get => selectedObjective; 
            set
            {
                if (Set(ref selectedObjective, value, nameof(SelectedObjective)) && selectedObjective!=null)
                {
                    SelectedAssignee = selectedObjective.Assignee;
                    SelectedCreator = selectedObjective.Creator;
                    Title = selectedObjective.Title;
                    Description = selectedObjective.Description!;
                }
            } 
        }
        public List<Objective> Objectives { get => objectives; set => Set(ref objectives, value, nameof(Objectives)); }
        public User SelectedAssignee { get => selectedAssignee; set => Set(ref selectedAssignee, value, nameof(SelectedAssignee)); }
        public User SelectedCreator { get => selectedCreator; set => Set(ref selectedCreator, value, nameof(SelectedCreator)); }
        public string Title { get => title; set => Set(ref title, value, nameof(Title)); }
        public string Description { get => description; set => Set(ref description, value, nameof(Description)); }
        #endregion
        #region User
        private User selectedUser;
        private List<User> users;
        private string firstname;
        private string lastname;
        private string middlename;
        private string login;
        private string password;

        public User SelectedUser 
        { 
            get => selectedUser; 
            set 
            { 
                if(Set(ref selectedUser, value, nameof(SelectedUser)) && selectedUser != null)
                {
                    Firstname = selectedUser.Firstname;
                    Lastname = selectedUser.Lastname;
                    Middlename = selectedUser.Middlename;
                    Login = selectedUser.Login;
                    Password = selectedUser.Password;
                }
            }  
        }
        public List<User> Users { get => users; set => Set(ref users, value, nameof(Users)); }
        public string Firstname { get => firstname; set => Set(ref firstname, value, nameof(Firstname)); }
        public string Lastname { get => lastname; set => Set(ref lastname, value, nameof(Lastname)); }
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }
        public string Login { get => login; set => Set(ref login, value, nameof(Login)); }
        public string Password { get => password; set => Set(ref password, value, nameof(Password)); }
        #endregion
        #region Methods
        private void UpdateLists(object? sender = null!, EventArgs e = null!)
        {
            Objectives = DbWorker.AbstractContext.Objectives.ToList();
            Users = DbWorker.AbstractContext.Users.ToList();
        }
        #endregion
        #region Commands
        public RelayCommand WindowCloseCommand { get; }
        #region Objective
        public RelayCommand AddObjectiveCommand { get; }
        public RelayCommand UpdateObjectiveCommand { get; }
        public RelayCommand DeleteObjectiveCommand { get; }
        public RelayCommand HideObjectiveCommand { get; }
        public RelayCommand ShowObjectiveCommand { get; }
        public RelayCommand ExecuteObjectiveCommand { get; }
        public RelayCommand UndoObjectiveCommand { get; }
        #endregion
        #region User
        public RelayCommand AddUserCommand { get; }
        public RelayCommand UpdateUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand HideUserCommand { get; }
        public RelayCommand ShowUserCommand { get; }
        #endregion
        #endregion
    }
}
