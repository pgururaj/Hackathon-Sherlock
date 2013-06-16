using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloSherlock.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World from .NET!");
            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine(string.Format("{0} at {1}", i.ToString(), DateTime.Now.ToShortTimeString()));
            }
        }
    }
}
