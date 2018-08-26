using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ClassVsStruct
{
    public interface IBase
    {
        void SetField(int field);
    }
    public class ExampleOfClass : IBase
    {
        public int Property { get; set; }
        public int Field;
        public ExampleOfClass()
        {

        }
        public ExampleOfClass(int field)
        {
            this.Field = field;
        }
        public void SetField(int field)
        {
            this.Field = field;
        }
        public override string ToString()
        {
            return this.Property.ToString() + " - " + this.Field.ToString();
        }
    }
    public class ExampleOfInheritance : ExampleOfClass
    {
        public ExampleOfInheritance(int field) : base(field)
        {
            this.Field = field + 1;
        }
        public void SetMoreField(int field)
        {
            this.Field = field + 1;
        }
    }
    public struct ExampleOfStruct : IBase
    {
        public int Property { get; set; }
        public int Field;
        //Constructor doesn't exist in struct
        //public ExampleOfStruct()
        //{

        //}
        //public ExampleOfStruct(int field)
        //{
        //    this.Field = field;
        //}
        public void SetField(int field)
        {
            this.Field = field;
        }
        public override string ToString()
        {
            return this.Property.ToString() + " - " + this.Field.ToString();
        }
    }
    public class ClassVsStruct
    {
        public static ClassVsStruct Instance
        {
            get
            {
                ExampleOfClass exampleOfClass = new ExampleOfClass(1) { Property = 3 };
                exampleOfClass.SetField(5);
                Console.WriteLine(exampleOfClass.ToString());
                ExampleOfInheritance exampleOfInheritance = new ExampleOfInheritance(4) { Property = 4 };
                exampleOfInheritance.SetField(7);
                exampleOfInheritance.SetMoreField(7);
                Console.WriteLine(exampleOfInheritance.ToString());
                ExampleOfStruct exampleOfStruct = new ExampleOfStruct() { Property = 3};
                exampleOfStruct.SetField(4);
                Console.WriteLine(exampleOfStruct.ToString());
                return null;
            }
        }
    }
}
