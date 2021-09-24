using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!" + DateTime.Now.ToString());
            Task.Run(() => new Test().Run());
            Console.WriteLine("1--" + DateTime.Now.ToString());
            Console.ReadLine();
        }
    }
}
