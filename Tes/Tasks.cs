using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tes
{
    public class Tes
    {
      
        public async Task<IEnumerable<int>> task1()
        {
            Console.WriteLine("task1 start: " + DateTime.Now);
            Thread.Sleep(1000);
            Console.WriteLine("task1 end: " + DateTime.Now);

            return new List<int>() { 1 };
           
        }
        public async Task<IEnumerable<int>> task2()
        {
            Console.WriteLine("task2 start: " + DateTime.Now);
            Thread.Sleep(2000);
            Console.WriteLine("task2 end: " + DateTime.Now);

            return new List<int>() { 1 };
        }
        public async Task<IEnumerable<int>> task3()
        {
            Console.WriteLine("task3 start: " + DateTime.Now);
            Thread.Sleep(3000);
            Console.WriteLine("task3 end: " + DateTime.Now);

            return new List<int>() { 1 };
        }
    }
}
