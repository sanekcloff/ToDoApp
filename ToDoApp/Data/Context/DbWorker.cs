using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{ 
    // Класс для работы с базой данных
    public static class DbWorker
    {
        // Инициализация контекста при первом вызове DbWorker
        static DbWorker()
        {
            UseSqliteLocal();
        }
        // Поле хранящее экземляр базы данных
        private static AbstractContext abstractContext = null!;

        // Свойство для доступа к значению поля
        public static AbstractContext AbstractContext 
        { 
            get => abstractContext; 
        }
        // Методы установки определённого контескста для работы с приложением
        public static void UseSqlite() => abstractContext = new SqliteDBContext();        
        public static void UseSqliteLocal() => abstractContext = new SqliteLocalDBContext();        
        public static void UseSqlServer() => abstractContext = new SQLServerDBContext();        
    }
}
