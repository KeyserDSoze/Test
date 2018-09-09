using System;
using Test.Laziness;
using Test.ImplicitOper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.CSharpV1;
using Test.CSharpV2;
using Test.CSharpV3;
using Test.CSharpV4;
using Test.CSharpV5;
using Test.CSharpV6;
using Test.CSharpV7;
using Test.CSharpV7_;
using Test.ClassVsAbstractClass;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args) //async main from C# 7.1
        {
            //await First();
            //await Second();
            //await Third();
            //await Fourth();
            //await Fifth();
            //await Sixth();
            //await Seventh();
            //await SeventhPlus();
            //var x = MythOfCave.Instance;
            var x = Power.PowerApi.Instance;
            Console.ReadLine();
        }
        static async Task First()
        {
            Version1 version1 = new Version1();
        }
        static async Task Second()
        {
            Version2 version2 = new Version2();
        }
        static async Task Third()
        {
            Version3 version3 = new Version3();
        }
        static async Task Fourth()
        {
            Version4 version4 = new Version4();
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
        static async Task SeventhPlus()
        {
            Version7Plus version7plus = new Version7Plus();
        }
        static void Zero()
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
