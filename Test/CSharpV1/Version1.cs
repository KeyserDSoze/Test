using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CSharpV1
{
    [Serializable] //attributem it's possible to create a custom attribute, it's possible to upload a dll with params [DllImport("user32.dll", ExactSpelling=false, SetLastError=false)]
    public class Version1
    {
        public Version1()
        {
            Class @class = new Class()
            {
                Property = "Sample"         //set a property
            };
            string sampleString = "string in memory";
            @class.RefMethod(ref sampleString);   //pass a referenced variable to a method
            Console.WriteLine("Ref param value: " + sampleString);  //now it's changed here too
            @class.InMethod(sampleString);
            @class.OutMethod(out sampleString);
            Console.WriteLine("out param value: " + sampleString);  //now it's re-assigned in OutMethod
            @class.ParamsMethod(sampleString, sampleString, sampleString);  //you can pass any number of objects
            EventHandler<string> onFire = (sender, eventArgs) => Console.WriteLine(eventArgs); // create an event
            @class.Event += onFire; //subscribe an event
            @class.Event -= onFire; //unsubscribe an event
            @class.Event += onFire; //subscribe an event again
            @class.Method(); //call a method of an instance of a class
            IInterface @struct = new Struct("sample"); //polymorphism through interface
            Abastract @abstract = new Class(); //polymorphism through abstract class
            @struct.Method(); //call a method of an instance of a struct
            Struct? structNullable = null; //with question mark you can assing to a struct a null reference
            bool? boolNullable = null; //as before it's valid for primitive too
            string NullReference = boolNullable?.ToString();  //avoid null reference
            #region conditions
            if((boolNullable == true | boolNullable == false) & (boolNullable == true | boolNullable == false)) { } // with | (logic or) and & (logic and) i check all the expression
            if ((boolNullable == true || boolNullable == false) && (boolNullable == true | boolNullable == false)) { } // with || (logic or) and && (logic and) i check all the expression until it's true
            #endregion
            #region operations
            int x = 1;
            x++;
            ++x;
            x--;
            --x;
            x += 1; // exists -= , /= , %= , *= , ^= too
            x = checked(2147483647 + x); //check the overflow
            x = unchecked(2147483647 + x); //uncheck the overflow
            checked //check the overflow in this block
            {
                x = 2147483647 + x;
            }
            unchecked //uncheck the overflow in this block
            {
                x = 2147483647 + x;
            }
            x = x << 1; //left binary shift
            x = x >> 1; //right binary shift
            x >>= 1; //right binary shift and assignment
            bool check = true;
            check &= false; //make logic && and after assign to object
            bool check2 = check ? true : false; //operator :?
            int? xx = null;
            int y = xx ?? -1;// Set y to the value of x if x is NOT null; otherwise, if x == null, set y to -1.
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortDigits = digits.Where((digit, index) => digit.Length < index); //lambda operator for lamda expression
            foreach (var sD in shortDigits) Console.WriteLine(sD);
            #endregion
            #region object and dynamic
            object obj = new object();  //object for compiler in assembly
            //obj.Method(); this possibility in c# is not exist.
            dynamic dyn = new Class();  //object at runtime, it's possible to invoke method or get/set properties and other
            dyn.Method();
            #endregion
            #region cycling break and goto
            bool checkForIt = true;
            while (checkForIt)
            {
                checkForIt = false;
                goto label;
                Console.WriteLine("code that isn't executing");
            }
            label: Console.WriteLine("go here");
            foreach (string s in new string[2] { "a", "b" })
            {
                if (s == "b")
                {
                    break;
                }
            }
            do
            {
                Console.WriteLine("do while checking");
            } while (checkForIt);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(string.Format("normal iteration in for: {0} and a test {1}", i.ToString(), "of string format"));
            }
            #endregion
            #region cast and conversion
            object[] objArray = new object[6];
            objArray[0] = new Object();
            objArray[1] = new Object();
            objArray[2] = "Hello World";
            objArray[3] = 123;
            objArray[4] = 123.4;
            objArray[5] = null;

            for (int i = 0; i < objArray.Length; ++i)
            {
                string s = objArray[i] as string;  //cast null if it doesn't match
                //string k = (string)objArray[i];   //it's a normal cast, but in this case during runtime it throws a new exception when it tries to cast any kind of non-string types
                Console.Write("{0}:", i);
                if (s != null)
                {
                    Console.WriteLine(" is a string '" + s + "'");
                }
                else
                {
                    Console.WriteLine(" is not a string");
                }
            }
            #endregion
        }

        static string A; //memorized in Managed Heap like a pointer for anyone uses it
        string B; //memorized in Heap the value but this class and string B is memorized in Stack (the structure)
        public static unsafe void UnsafeTest() //pointer in C# to manage the Heap
        {
            Point pt = new Point();
            Point* pp = &pt;
            pp->x = 123;
            pp->y = 456;
            Console.WriteLine("{0} {1}", pt.x, pt.y);
        }
        //https://www.c-sharpcorner.com/article/C-Sharp-heaping-vs-stacking-in-net-part-i/
        struct Point
        {
            public int x, y;
        }
    }
    public interface IInterface
    {
        void Method();
        void Method(string param);
    }
    public abstract class Abastract
    {
        public virtual void MakeSomething()
        {
            Console.WriteLine("Make it");
        }
    }
    //creation deletage
    public delegate void FunctionDelegate(Class @class);
    //class
    public class Class : Abastract, IInterface
    {
        public string Param;

        public event EventHandler<string> Event;
        public FunctionDelegate DelegatedFunction = DefaultDelegateFunction;  //create a field with declaration of delegate created before, furthermore with a default value (you can find the static method at the end of this class)
        public event FunctionDelegate DelegationAsEvent;

        //create a property
        public string Property { get; set; } //to be honest this auto implementation of properties through { get; set; } is real since C# 3.0
        public Class() { }
        public Class(string param)
        {
            this.Param = param;
        }
        public void Method()
        {
            this.Event?.Invoke(this, "Fire Event!"); //fire an event
        }
        public void Method(string param)
        {
            this.DelegatedFunction?.Invoke(this); //call a delegated method
        }
        public override void MakeSomething()
        {
            Console.WriteLine("Special Make it");
            base.MakeSomething();
        }
        public void AddDelegate(params FunctionDelegate[] delegatedFunctions) //method to add one or more delegate of the same type
        {
            this.DelegatedFunction = delegatedFunctions.First();
            foreach (FunctionDelegate delegateFunction in delegatedFunctions.Skip(1)) this.DelegatedFunction += delegateFunction;
        }
        private static void DefaultDelegateFunction(Class @class)
        {
            Console.WriteLine("Delegate to see value of property: " + @class.Property);
        }
        public void RefMethod(ref string param)
        {
            param += " passed here"; //if you change value of param here, you'll find it changed in method that called it
        }
        public void InMethod(in string param) //from C# 7.2
        {
            //param += " passed here";   //with modifier "in" it's not possible to change the param in method
        }
        public void OutMethod(out string param)
        {
            param = "changed here";  //you must assign before of all
            param += " after you can add any value"; //and after you can change it
            //you find this value in method that called it without needing to use return
        }
        public void ParamsMethod(params string[] @params)  //with params i can pass many parameters to my method
        {
            foreach(string param in @params)
            {
                Console.WriteLine("inserted " + param);
            }
        }
    }
    //struct, like a class but more light
    public struct Struct : IInterface
    {
        public string Param;
        public Struct(string param)
        {
            this.Param = param;
        }

        public void Method()
        {

        }

        public void Method(string param)
        {
            throw new NotImplementedException();
        }
    }
        //Classes Only:
        //Can support inheritance
        //Are reference(pointer) types
        //The reference can be null
        //Have memory overhead per new instance

        //Structs Only:
        //Cannot support inheritance
        //Are value types
        //Are passed by value(like integers)
        //Cannot have a null reference(unless Nullable is used)
        //Do not have a memory overhead per new instance - unless 'boxed'
}
