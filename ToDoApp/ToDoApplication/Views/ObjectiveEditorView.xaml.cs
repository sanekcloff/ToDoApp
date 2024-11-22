using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoApplication.ViewModels;

namespace ToDoApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для ObjectiveEditorView.xaml
    /// </summary>
    public partial class ObjectiveEditorView : Window
    {
        ObjectiveEditorViewModel viewModel;
        public ObjectiveEditorView(User user, Objective objective = null!)
        {
            InitializeComponent();
            viewModel = new(user,objective);
            DataContext = viewModel;
        }

        private void DragBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed) DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
