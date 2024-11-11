using Data.Models;
using Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.Command;
using ToDoApplication.Views;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(User user)
        {
            CurrentUser = user;
            UpdateLists();

            CreateCommand = new RelayCommand(o =>
            {
                new ObjectiveEditorView(CurrentUser).ShowDialog();
                UpdateLists();
            });
            UpdateCommand = new RelayCommand(o =>
            {
                new ObjectiveEditorView(CurrentUser, SelectedCreatedObjective).ShowDialog();
                UpdateLists();
            });
            DeleteCommand = new RelayCommand(o =>
            {
                ObjectiveService.DeleteObjective(SelectedCreatedObjective);
                UpdateLists();
            });
            HideCommand = new RelayCommand(o =>
            {
                ObjectiveService.Hide(SelectedCreatedObjective);
                UpdateLists();
            });
            ExecuteCommand = new RelayCommand(o =>
            {
                ObjectiveService.Execute(SelectedAssignedObjective);
                UpdateLists();
            });
        }

        private User currentUser;
        private List<Objective> createdObjectives = new List<Objective>();
        private List<Objective> assignedObjectives = new List<Objective>();
        private Objective selectedCreatedObjective;
        private Objective selectedAssignedObjective;

        public User CurrentUser 
        { 
            get => currentUser; 
            set => Set(ref currentUser, value, nameof(User)); 
        }
        public Objective SelectedCreatedObjective { get => selectedCreatedObjective; set => Set(ref selectedCreatedObjective, value, nameof(SelectedCreatedObjective)); }
        public Objective SelectedAssignedObjective { get => selectedAssignedObjective; set => Set(ref selectedAssignedObjective, value, nameof(SelectedAssignedObjective)); }
        public List<Objective> CreatedObjectives { get => createdObjectives; set => Set(ref createdObjectives, value, nameof(CreatedObjectives)); }
        public List<Objective> AssignedObjectives { get => assignedObjectives; set => Set(ref assignedObjectives, value, nameof(AssignedObjectives)); }

        public RelayCommand CreateCommand { get; }
        public RelayCommand UpdateCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand HideCommand { get; }
        public RelayCommand ExecuteCommand { get; }

        private void UpdateLists(object? sender = null!, PropertyChangedEventArgs e = null!)
        {
            CreatedObjectives = CurrentUser.CreatedObjectives.Where(co=>co.IsDeleted==false).ToList();
            AssignedObjectives = CurrentUser.AssignedObjectives.Where(ao => ao.IsDeleted == false).ToList();
        }
    }
}
