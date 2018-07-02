using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Laziness;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c = new Customer("Test");
            Console.WriteLine("Waiting to get values from Orders");
            Console.ReadLine();
            foreach(var x in c.Orders)
            {
                Console.WriteLine(x.X.ToString());
            }
            Console.ReadLine();
        }
    }
}
