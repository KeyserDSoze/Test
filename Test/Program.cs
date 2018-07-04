using System;
using Test.Laziness;
using Test.ImplicitOper;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "o";
            ImplicitOperator implicitOperator = value;
            Console.WriteLine("implicitOperator.C: " + implicitOperator.C);
            implicitOperator += "3";
            Console.WriteLine("implicitOperator.S: " + implicitOperator.S.Count.ToString() + " with values: " + string.Join(",", implicitOperator.S));
            List<string> listOfValues = new List<string>();
            listOfValues.Add("a");
            listOfValues.Add("b");
            implicitOperator += listOfValues;
            Console.WriteLine("implicitOperator.S: " + implicitOperator.S.Count.ToString() + " with values: " + string.Join(",", implicitOperator.S));
            Console.WriteLine("implicitOperator finds: " + implicitOperator.FindValue("a"));
            LocalMultithread.MakeItReal();
            Multithread.MakeItReal();
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
