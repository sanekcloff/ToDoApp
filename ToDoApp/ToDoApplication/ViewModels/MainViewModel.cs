using Data.Context;
using Data.Models;
using Data.Services;
using LogHandler;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using ToDoApplication.Command;
using ToDoApplication.Notifiers;
using ToDoApplication.Views;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(User user)
        {
            CurrentUser = user;

            CreateCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new ObjectiveEditorView(CurrentUser));
            });
            UpdateCommand = new RelayCommand(o =>
            {
                if(SelectedCreatedObjective != null)
                {
                    try
                    {
                        OpenWindowDialog(new ObjectiveEditorView(CurrentUser, SelectedCreatedObjective));
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
            DeleteCommand = new RelayCommand(o =>
            {
                if (SelectedCreatedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Delete(SelectedCreatedObjective);

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
            HideCommand = new RelayCommand(o =>
            {
                if (SelectedCreatedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Hide(SelectedCreatedObjective);

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
            ShowCommand = new RelayCommand(o =>
            {
                if (SelectedCreatedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Show(SelectedCreatedObjective);
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
            ExecuteCommand = new RelayCommand(o =>
            {
                if (SelectedAssignedObjective != null)
                {
                    try
                    {
                        ObjectiveService.Execute(SelectedAssignedObjective);
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
            WindowCloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateLists;
            timer.Start();
        }

        private User currentUser;
        private List<Objective> createdObjectives = new();
        private List<Objective> assignedObjectives = new();
        private Objective selectedCreatedObjective;
        private Objective selectedAssignedObjective;
        private string searchCreated;
        private string searchAssignee;

        public User CurrentUser 
        { 
            get => currentUser; 
            set => Set(ref currentUser, value, nameof(User)); 
        }
        public Objective SelectedCreatedObjective { get => selectedCreatedObjective; set => Set(ref selectedCreatedObjective, value, nameof(SelectedCreatedObjective)); }
        public Objective SelectedAssignedObjective { get => selectedAssignedObjective; set => Set(ref selectedAssignedObjective, value, nameof(SelectedAssignedObjective)); }
        public List<Objective> CreatedObjectives { get => createdObjectives; set => Set(ref createdObjectives, value, nameof(CreatedObjectives)); }
        public List<Objective> AssignedObjectives { get => assignedObjectives; set => Set(ref assignedObjectives, value, nameof(AssignedObjectives)); }
        public string SearchCreated { get => searchCreated; set => Set(ref searchCreated, value, nameof(SearchCreated)); }
        public string SearchAssignee { get => searchAssignee; set => Set(ref searchAssignee, value, nameof(SearchAssignee)); }

        public RelayCommand CreateCommand { get; }
        public RelayCommand UpdateCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand HideCommand { get; }
        public RelayCommand ShowCommand { get; }
        public RelayCommand ExecuteCommand { get; }
        public RelayCommand WindowCloseCommand { get; }
        

        private void UpdateLists(object? sender = null!, EventArgs e = null!)
        {
            var objectives = DbWorker.AbstractContext.Objectives.Where(co => co.Creator == CurrentUser).Include(o => o.Creator).Include(o => o.Assignee).ToList();
            CreatedObjectives = SearchCreatedObjectives(objectives).ToList();
            AssignedObjectives = DbWorker.AbstractContext.Objectives.Where(co=> co.Assignee == CurrentUser && co.IsDeleted==false).Include(o=>o.Creator).Include(o=>o.Assignee).ToList();
        }
        private ICollection<Objective> SearchCreatedObjectives(ICollection<Objective> objectives)
        {
            if (string.IsNullOrEmpty(searchCreated))
            {
                return objectives;
            }
            else
            {
                return objectives.Where(o=>o.Title.ToLower().Contains(searchCreated.ToLower())).ToList();
            }
        }
    }
}