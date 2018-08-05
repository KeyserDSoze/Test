using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;   //using static added

namespace Test.CSharpV6
{
    public class Version6  //Roselyn is finally released, C# compiler written in C#
    {
        public string A { get; set; } = "Default";  //Property initializers
        public string Get() => this.A + " " + this.A;  //Expression bodied members
        //index initializer for list
        private List<string> messages = new List<string>
        {
            "Page not Found",
            "Page moved, but left a forwarding address.",
            "The web server can't come out to play today."
        };
        //index initializer for dictionary
        private Dictionary<int, string> webErrors = new Dictionary<int, string>
        {
            [404] = "Page not Found",
            [302] = "Page moved, but left a forwarding address.",
            [500] = "The web server can't come out to play today."
        };
        public string FirstName { private get; set; } //a public property can have a private set or get
        public string LastName { get; private set; } //a public property can have a private set or get
        public Version6()
        {
            try
            {
                try
                {
                    Pow(2, 4);  //before you'd use Math.Pow(2, 4)
                    string defaultValueForNullPropagation = new B().PropertyB?[0] ?? String.Empty;  //in case of anything null set a default value "String.Empty" //Null propagator
                                                                                                    //interpolated string through special character $ at the start
                    string name = "Horace at Lab Horace";
                    int age = 21;
                    Console.WriteLine($"He asked, \"Is your name {name}?\", but didn't wait for a reply :-{{");
                    Console.WriteLine($"{name} is {age} year{(age == 1 ? "" : "s")} old.");
                    string path = @"C:\example.csv"; //verbatim identification
                    B b = new B() { PropertyB = new List<string>() };
                    switch (typeof(B).GetProperties().First().Name)
                    {
                        case nameof(b.PropertyB):  //added nameof
                            Console.WriteLine("check the name of property");
                            break;
                    }
                    throw new Exception();
                }
                catch (Exception er) when (LogErrorFalse(er))  //added when clause in Exception, if return false the exception is not caputered and the managing of exception is demanded to who calls this method
                {
                    Console.WriteLine("Catch 1 works");
                    //Await in catch/finally blocks now is available
                }
                finally
                {
                    Console.WriteLine("Finally 1 works");
                    //Await in catch/finally blocks now is available
                }
            }
            catch (Exception ex) when (LogErrorTrue(ex))  //added when clause in Exception, if return true the exception is caputered and enter in catch
            {
                Console.WriteLine("Catch 2 works");
                //Await in catch/finally blocks now is available
            }
            finally
            {
                Console.WriteLine("Finally 2 works");
                //Await in catch/finally blocks now is available
            }
        }
        public static bool LogErrorFalse(Exception er)
        {
            Console.WriteLine(er.Message);
            return false;
        }
        public static bool LogErrorTrue(Exception er)
        {
            Console.WriteLine(er.Message);
            return true;
        }
    }
    public struct B
    {
        public List<string> PropertyB { get; set; }
    }

}
