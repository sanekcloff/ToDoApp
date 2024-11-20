using Data.Context;
using Data.Models;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.Command;
using ToDoApplication.Handlers;

namespace ToDoApplication.ViewModels
{
    public class ObjectiveEditorViewModel : ViewModelBase
    {
        public ObjectiveEditorViewModel(User user, Objective objective)
        {
            Objective = objective;
            if (objective != null) 
            {
                Title = objective.Title;
                Description = objective.Description!;
                SelectedAssigner = Objective.Assignee;
            }
            else
            {
                SelectedAssigner = Assigners.FirstOrDefault()!;
            }
            SaveCommand = new RelayCommand(o =>
            {
                if (objective == null)
                {
                    var objective = Objective.Create(user, SelectedAssigner!, Title, Description!);
                    if (InputValidator.IsValid(objective))
                        ObjectiveService.Add(objective);
                }
                else
                {
                    if (InputValidator.IsValid(objective))
                        ObjectiveService.Update(objective, Title, Description!, SelectedAssigner);
                }
            });
        }
        private string title = string.Empty;
        private string description = string.Empty;
        private User selectedAssigner;

        private Objective objective;
        private List<User> assigners = DbWorker.AbstractContext.Users.ToList();

        public Objective Objective { get => objective; set => Set(ref objective, value, nameof(Objective)); }
        public List<User> Assigners { get => assigners; set => Set(ref assigners, value, nameof(Assigners)); }
        public User SelectedAssigner { get => selectedAssigner; set => Set(ref selectedAssigner, value, nameof(SelectedAssigner)); }
        public string Description { get => description; set => Set(ref description, value, nameof(Description)); }
        public string Title { get => title; set => Set(ref title, value, nameof(Title)); }

        public RelayCommand SaveCommand { get; }
    }
}
