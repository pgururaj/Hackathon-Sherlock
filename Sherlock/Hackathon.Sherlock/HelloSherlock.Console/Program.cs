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
            System.Console.WriteLine("Hello World from .NET!!!");
            System.Console.WriteLine("Length of args" + args.Length.ToString());

            for (int i = 0; i < 8; i++)
            {
                System.Console.WriteLine(string.Format("{0} index value is {1}", i.ToString(), args[i].ToString()));
            }


            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine(string.Format("{0} at {1}", i.ToString(), DateTime.Now.ToShortTimeString()));
            }
        }
    }
}
