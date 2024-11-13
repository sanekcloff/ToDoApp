using Data.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ToDoApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            DatabaseFacade facade = new DatabaseFacade(DbWorker.AbstractContext);
            facade.EnsureCreated();
        }
    }

}
