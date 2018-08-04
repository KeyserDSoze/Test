using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.CSharpV3
{
    public class Version3
    {
        Func<int, int, int> function;  //declaration for expression tree
        public Version3()
        {
            int value = 0;
            var implicityTypedValue = value; //implicity typed local variables, introduction of var
            #region anonymous type and query
            var v = new { Amount = 108, Message = "Hello" };  //anonymous type
            var anonArray = new[] { new { Name = "apple", Diameter = 4 }, new { Name = "grape", Diameter = 1 } };   //anonymous array
            var anonQuery =
                    from anon in anonArray            //Query expression 
                    let radius = anon.Diameter / 2    //let to create a new parameter by an operation
                    where anon.Diameter > 0           //Where clause
                    orderby anon.Diameter descending  //order by clause
                    group anon by radius into anonGroup   //group by
                    select new { anonGroup };   //it possible to have an anonymous ouput, if you don't specify a parameter name, the compiler uses the same of the object wich refers
            foreach (var vv in anonQuery)
            {
                Console.WriteLine("Value={0}", vv.anonGroup.Key);
            }
            //It's possible to make join, subquery
            //var categoryQuery =
            //    from cat in categories
            //    join prod in products on cat equals prod.Category
            //    select new { Category = cat, Name = prod.Name };
            //var queryGroupMax =
            //        from student in students
            //        group student by student.GradeLevel into studentGroup
            //        select new
            //        {
            //            Level = studentGroup.Key,
            //            HighestScore =
            //                (from student2 in studentGroup
            //                 select student2.Scores.Average())
            //                 .Max()
            //        };
            #endregion
            List<string> listOfString = new List<string>();
            listOfString = listOfString.FindAll(x => x == "Hello"); //lamba expression in FindAll method
            function = (c, b) => c + b;  //function with expression tree and instanciation through lambda expression
            Expression<Func<int, int, int>> expression = (c, b) => c + b; //linq expression tree
            string exampleForExtensionMethod = "Hello World";
            Console.WriteLine("Word Count: " + exampleForExtensionMethod.WordCount().ToString()); //using of extension method WordCount
            A a = new A() { Property = "Test" };  //object intializer
            List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  //collection initializer
        }
    }
    public static class Non_GenericStaticClass //Extension method must be defined in a non-generic static class
    {
        public static int WordCount(this string input)   //extesion method
        {
            return input.Trim().Replace(" ", "").Length;
        }
    }
    partial class A
    {
        public string Property { get; set; }
        partial void OnSomethingHappened(string s);  //partial method has same signatures, must return void, no access modifiers are allowed, are implicitly private
    }

    // This part can be in a separate file.
    partial class A
    {
        // Comment out this method and the program will still compile.
        partial void OnSomethingHappened(String s)
        {
            Console.WriteLine("Something happened: {0}", s);
        }
    }
}
