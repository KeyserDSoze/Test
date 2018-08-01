using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CSharpV2
{
    public class Version2
    {
        public Version2()
        {
            int? nullableInt = null;  //there's the possibility to instantiate a null reference with ? in declaration type
            Nullable<bool> nullableBool = null;   //there's the possibility to instantiate a null reference with generic Nullable class
            int y = nullableInt ?? -1; //check if nullableInt is null and set in null case a default value (-1 in this case)
            Example<ClassWithNestedClass> example = new Example<ClassWithNestedClass>();
            example.exampleDelegate = new ExampleDelegate<ClassWithNestedClass>(StaticClass.StaticMethod);  //Delegate Example without Inference C# 1.0
            example.exampleDelegate = StaticClass.StaticMethod;   //Delegate Example with Inference C# 2.0
            var keywords = new CSharpBuiltInTypes(); //instantiate an iterator class
            Console.WriteLine("First Iterator Elements: " + keywords.First()); //take first element
            Console.WriteLine(String.Join(",", StaticClass.EvenSequence(5, 49)));  //call method that return the even number list between 5 and 49
        }
    }
    public delegate void ExampleDelegate<T>(T x) where T : IExample;   //delegate with a generic parameter T
    public interface IExample
    {

    }
    public static class StaticClass
    {
        public static void StaticMethod(IExample x)
        {
            Console.WriteLine("Static Method Called!!!");
        }
        public static System.Collections.Generic.IEnumerable<int> EvenSequence(int firstNumber, int lastNumber)   //method that return an enumerator with only even number between input range
        {
            // Yield even numbers in the range.  
            for (int number = firstNumber; number <= lastNumber; number++)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }
    }
    public class Example<T> where T : IExample   //T is a generic type, you can pass tha class that you want to use as T parameter,
    {                                            //and so you can use your class for more than 1 class.
        private string name = "Hello";           //In this case you can see that the T is a restrictive T, with the clause where, in fact T can be only a IExample and so on
        List<string> listOfString;               //List of a generic type, in this case a list of string
        T Params { get; set; }                   //create a property with the generic T in input
        Action<string> Action = delegate (string x) { Console.WriteLine("Fire Event: " + x + "!"); };  //anonymous function through delegation
        public ExampleDelegate<ClassWithNestedClass> exampleDelegate;
        public string Name
        {
            get                   //public get
            {
                this.Action?.Invoke("getting the name");              //fire of an action instantiated through an anonymous function
                return name;
            }
            protected set         //but protected set
            {
                name = value;
            }
        }
    }
    public partial class ClassWithNestedClass : IExample   //both class must be in the same assembly (same namespace in same project, it's not correct if they are in different project with same namespace)
    {                                                      //they must have same access modifier, if you set public, both must have public
        partial class NestedClass { }                      //you can extend anykind of interface or class or abstract class, the final product is a simple sum between extended stuff of any partial class
    }

    public partial class ClassWithNestedClass
    {
        partial class NestedClass { }
    }
    public class CSharpBuiltInTypes : IEnumerable<string>    //create an enumerator class (list of string in this specific case)
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "object";
            yield return "byte";
            yield return "uint";
            yield return "ulong";
            yield return "float";
            yield return "char";
            yield return "bool";
            yield return "ushort";
            yield return "decimal";
            yield return "int";
            yield return "sbyte";
            yield return "short";
            yield return "long";
            yield return "void";
            yield return "double";
            yield return "string";
        }
        // The IEnumerable.GetEnumerator method is also required
        // because IEnumerable<T> derives from IEnumerable.
        System.Collections.IEnumerator
          System.Collections.IEnumerable.GetEnumerator()
        {
            // Invoke IEnumerator<string> GetEnumerator() above.
            return GetEnumerator();
        }
        private string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        public IEnumerable<string> BottomToTop
        {
            get
            {
                for (int index = 0; index < days.Length; index++)
                {
                    yield return days[index];
                }
            }
        } 
    }
}
