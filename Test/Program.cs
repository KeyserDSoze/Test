using System;
using Test.Laziness;
using Test.ImplicitOper;
using System.Collections.Generic;
using Test.CSharpV5;
using System.Threading.Tasks;
using Test.CSharpV6;
using Test.CSharpV7;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args) //async main from C# 7.1
        {
            //await Fifth();
            //await Sixth();
            await Seventh();
            Console.ReadLine();
        }
        static async Task Fifth()
        {
            Version5 version5 = new Version5();
            await version5.MakeItReal();
            await version5.ShotMeDownBangBang();
        }
        static async Task Sitxh()
        {
            Version6 version6 = new Version6();
        }
        static async Task Seventh()
        {
            Version7 version7 = new Version7();
        }

        static void First()
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
            List<string> examples = new List<string>();
            examples.Add("a");
            examples.Add("b");
            examples.Add("c");
            Console.WriteLine(examples.FindX("a")); //output first "a" by descending
            LocalMultithread.MakeItReal();
            Multithread.MakeItReal();
            Customer c = new Customer("Test");
            Console.WriteLine("Waiting to get values from Orders");
            Console.ReadLine();
            foreach (var x in c.Orders)
            {
                Console.WriteLine(x.X.ToString());
            }
            Console.ReadLine();
        }
    }
}
