using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Tes
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new AllTasks();
            //a.a().Wait();
             a.a().Wait();
            
            Console.ReadLine();
            var list = new List<int>();
           
        }
        
    }
}
