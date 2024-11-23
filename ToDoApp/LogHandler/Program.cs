using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogHandler.Logger;

namespace LogHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Отображение логов
            GetAllLogFiles();
            Console.ReadLine();
        }
    }
}
