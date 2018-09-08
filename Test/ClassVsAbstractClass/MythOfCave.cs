using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ClassVsAbstractClass
{
    public class MythOfCave
    {
        public static MythOfCave Instance
        {
            get
            {
                AJar goldJar = new GoldJar("blue");
                goldJar.Carry();
                AJar silverJar = new SilverJar("red");
                silverJar.Carry();
                AJar myrrhJar = new MyrrhJar("green");
                myrrhJar.Carry();
                return null;
            }
        }
    }
    public abstract class AJar
    {
        private string carriedObject = "Nothing";
        public string Color { get; set; } = "Black";
        public AJar(string color)
        {
            this.Color = color;
            Console.WriteLine($"A {this.Color} Jar");
        }
        public abstract void Carry();
        public virtual void SetCarriedObject(string carriedObject)
        {
            this.carriedObject = carriedObject;
            Console.WriteLine("Carring " + this.carriedObject);
        }
    }
    public class GoldJar : AJar
    {
        public GoldJar(string color) : base(color)
        {
            this.SetCarriedObject("Gold");
        }
        public override void Carry()
        {
            Console.WriteLine("Carry with handles");
        }
        public override void SetCarriedObject(string carriedObject)
        {
            Console.WriteLine("Make something more specific than base");
            base.SetCarriedObject(carriedObject);
        }
    }
    public class SilverJar : AJar
    {
        public SilverJar(string color) : base(color)
        {
            this.SetCarriedObject("Silver");
        }
        public override void Carry()
        {
            Console.WriteLine("Carry on head");
        }
        public override void SetCarriedObject(string carriedObject)
        {
            Console.WriteLine("Make something more specific than base");
            base.SetCarriedObject(carriedObject);
        }
    }
    public class MyrrhJar : AJar
    {
        public MyrrhJar(string color) : base(color)
        {
            this.SetCarriedObject("Myrrh");
        }
        public override void Carry()
        {
            Console.WriteLine("Carry on head");
        }
        public override void SetCarriedObject(string carriedObject)
        {
            Console.WriteLine("Make something more specific than base");
            base.SetCarriedObject(carriedObject);
        }
    }
}
