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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        MainViewModel viewModel;
        public MainView(User user)
        {
            InitializeComponent();
            viewModel = new MainViewModel(user);
            DataContext = viewModel;
            Title = $"Окно пользователя: {user.Fullname}";
            MinimazeButton.Visibility = Visibility.Hidden;
        }

        private void DragBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed) DragMove();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MinimazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                FullscreenButton.Visibility = Visibility.Visible;
                MinimazeButton.Visibility = Visibility.Hidden;
                resizeThumb.Visibility = Visibility.Visible;
            }
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                MinimazeButton.Visibility = Visibility.Visible;
                FullscreenButton.Visibility = Visibility.Hidden;
                resizeThumb.Visibility = Visibility.Hidden;
            }
        }

        private void resizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (this.Height + e.VerticalChange > 300)
            {
                this.Height = this.Height + e.VerticalChange;
            }
            if (this.Width + e.HorizontalChange > 400)
            {
                this.Width = this.Width + e.HorizontalChange;
            }
        }
    }
}
