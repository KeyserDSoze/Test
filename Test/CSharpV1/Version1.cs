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
            if((boolNullable == true | boolNullable == false) & (boolNullable == true | boolNullable == false)) { } // with | (logic or) and & (logic and) i check al the expression
            if ((boolNullable == true || boolNullable == false) && (boolNullable == true | boolNullable == false)) { } // with || (logic or) and && (logic and) i check al the expression until it's true
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
        }

        //public static unsafe void UnsafeTest()
        //{
        //    Point pt = new Point();
        //    Point* pp = &pt;
        //    pp->x = 123;
        //    pp->y = 456;
        //    Console.WriteLine("{0} {1}", pt.x, pt.y);
        //}
        //struct Point
        //{
        //    public int x, y;
        //}
    }
    public interface IInterface
    {
        void Method();
        void Method(string param);
    }
    public abstract class Abastract
    {

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
        public string Property { get; set; }
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
        public void AddDelegate(params FunctionDelegate[] delegatedFunctions) //method to add one or more delegate of the same type //with params i can pass many parameters to my method
        {
            this.DelegatedFunction = delegatedFunctions.First();
            foreach (FunctionDelegate delegateFunction in delegatedFunctions.Skip(1)) this.DelegatedFunction += delegateFunction;
        }
        private static void DefaultDelegateFunction(Class @class)
        {
            Console.WriteLine("Delegate to see value of property: " + @class.Property);
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
