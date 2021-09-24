using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tes
{
    public class AllTasks
    {
        public int result { get; set; }
        public async Task a()
        {
            var tasks = new List<Task<IEnumerable<int>>>();
            var t = new Tes();
           
            tasks.Add(Task.Run(() => t.task1()));
            tasks.Add(Task.Run(() => t.task2()));
            tasks.Add(Task.Run(() => t.task3()));
            await Task.WhenAll(tasks);
            var result = new List<int>();
            foreach (var task in tasks)
            {
                IEnumerable<int> intList = task.Result;
                result.AddRange(intList);
            }
            foreach (var r in result)
            {
                Console.WriteLine("result:" + r.ToString());
            }
            Console.WriteLine("total:" + result.Count());
            
           
        }


    }
}

