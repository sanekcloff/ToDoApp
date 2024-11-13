using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public static class DbWorker
    {
        static DbWorker()
        {
            UseSqlite();
        }
        private static AbstractContext abstractContext = null!;
        public static AbstractContext AbstractContext 
        { 
            get => abstractContext; 
        }

        public static void UseSqlite() => abstractContext = new SqliteDBContext();        
        public static void UseSqlServer() => abstractContext = new SQLServerDBContext();        
    }
}
