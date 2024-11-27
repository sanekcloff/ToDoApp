using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    // Класс для создания файла Sqlite в проекте
    public class SqliteLocalDBContext : AbstractContext
    {
        private static string SetLocalSqliteConnect()
        {
            var dir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent;
            return $@"Data source={dir}\Data\LocalDB\ToDoAppDb.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SetLocalSqliteConnect());
        }
    }
}
