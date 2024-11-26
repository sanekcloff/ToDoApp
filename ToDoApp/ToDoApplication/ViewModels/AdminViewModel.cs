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
                    var objective = Objective.Create(SelectedCreator, SelectedAssignee, Title, Description!);
                    if (InputValidator.IsValid(objective))
                        ObjectiveService.Add(objective);
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
                            ObjectiveService.Update(SelectedObjective, Title, Description!, SelectedAssignee);
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

        public User SelectedUser { get => selectedUser; set => Set(ref selectedUser, value, nameof(SelectedUser)); }
        public List<User> Users { get => users; set => Set(ref users, value, nameof(Users)); }
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
        public RelayCommand AddObjectiveCommand { get; }
        public RelayCommand UpdateObjectiveCommand { get; }
        public RelayCommand DeleteObjectiveCommand { get; }
        public RelayCommand HideObjectiveCommand { get; }
        public RelayCommand ShowObjectiveCommand { get; }
        public RelayCommand ExecuteObjectiveCommand { get; }
        public RelayCommand UndoObjectiveCommand { get; }
        #endregion
    }
}
