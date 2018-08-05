using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CSharpV4
{
    public class Version4
    {
        public Version4()
        {
            dynamic dynamicObject = new A();
            object @object = new A();
            dynamicObject.Property = "A";  //with dynamic you can treat it as object but you can assign every property, call every method, and so on
            //@object.Property = "A"   //this is a compiler error, you cannot assign nothing to object if you don't cast it before to right class
            //the big difference through dynamic and object is that dynamic works during runtime of program, instead object is compiled in assembly. 
            //It's for this reason that you cannot manage parameters in object rather than how happens with dynamic
            //furthermore you can use dynamic to make any kind of operations at runtime
            int value = 1;
            dynamic dynamicValue = (int)3;
            object objectValue = (int)3;
            dynamicValue += value; //it's possible to do only with dynamic, this is the power of runtime compiling
            //objectValue += value;   //compiler error
            A a = new A();
            a.SampleMethod(b: 5, a: "Test");
            a.SampleMethod("Test", c: 1000); //it's useful here, i want to use default value for 'b' but i want to set 'c'
                                             //a.SampleMethod(c: 10, 5, a: "Test");  // from c# 7.2 .NET Framework 4.7.1
            Func<Base, Derived> f1 = MyMethod;
            // Covariant return type.
            Func<Base, Base> f2 = f1;  //instead Derived it returns Base
            Base b2 = f2(new Base());
            // Contravariant parameter type.
            Func<Derived, Derived> f3 = f1;  //instead an input of Base it receives Derived
            Derived d3 = f3(new Derived());
            // Covariant return type and contravariant parameter type.
            Func<Derived, Base> f4 = f1;  //combo with input Derived instead Base and return Base instead Derived
            Base b4 = f4(new Derived());
            //Func<in T, out TResult>  as you can see it has "in" in input (contravariant) and "out" as output (covariant)
            Func<Derived, Derived> f5 = MyMethod2;
            // Covariant return type and contravariant parameter type.
            Func<MoreDerived, Base> f6 = f5;
            Base t1 = f2(new MoreDerived());
            //Contravariant interface and methods
            FirstImplementation<Base> firstImplementation = new FirstImplementation<Base>();
            firstImplementation.Update(new MoreDerived());
            firstImplementation.Set(new MoreDerived());
            //Covariant interface and methods
            SecondImplementation<MoreDerived> secondImplementation = new SecondImplementation<MoreDerived>();
            Base @base = secondImplementation.Get();
            // https://docs.microsoft.com/it-it/dotnet/standard/generics/covariance-and-contravariance
            //it also was added embedded interop types // https://stackoverflow.com/questions/20514240/whats-the-difference-setting-embed-interop-types-true-and-false-in-visual-studi
        }
        public static Derived MyMethod(Base b)   //covariant method
        {
            return b as Derived ?? new Derived();
        }
        public static MoreDerived MyMethod2(Base b)
        {
            return b as MoreDerived ?? new MoreDerived();
        }
    }

    public class A
    {
        public string Property { get; set; }
        public static dynamic DynamicStaticField;
        public dynamic DynamicProperty { get; set; }
        public dynamic Get(dynamic @dynamic)
        {
            int x = 2;
            dynamic conversion = x as dynamic;
            if(conversion is dynamic)
            {
                Console.WriteLine("Conversion is dynamic");
            }
            return @dynamic + 2 + this.DynamicProperty;
        }
        public void SampleMethod(string a, double b = 2, int c = 10)  //you can set a default value for method parameters.
        {
            Console.WriteLine("SampleMethod");
        }
        //public void SampleMethod(string a, double b = 2, int c) //this is a not valid default value setting. Setted values are all at the end of method.
    }
    public class Base { }
    public class Derived : Base { }
    public class MoreDerived : Derived { }
    public interface IContravariant<in T>
    {
        void Set(T t);
        void Update(T t);
    }
    public class FirstImplementation<T> : IContravariant<T>
    {
        public void Set(T t)
        {
            
        }

        public void Update(T t)
        {
            
        }
    }
    public interface ICovariant<out T>
    {
        T Get();
        //void Update(T t);   //return a compiler error, you cannot pass a parameter covariant to a method in interface
    }
    public class SecondImplementation<T> : ICovariant<T>
    {
        public T Get()
        {
            return default(T);  // you can return in higher c# simply default --> return default;
        }
    }
}
