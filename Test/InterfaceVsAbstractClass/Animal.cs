using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.InterfaceVsAbstractClass
{
    public class InterfaceVsAbstractClass
    {
        public static InterfaceVsAbstractClass Instance
        {
            get
            {
                Dog dog = new Dog();
                dog.Run();
                dog.Sleep();
                dog.Eat();
                Animal dogAbstract = new Dog(); //abstract dog can only sleep
                dog.Sleep();
                IRunning dogRunner = new Dog(); //dog runner can only run
                dogRunner.Run();
                IEating dogEater = new Dog(); //dog eater can only eat
                dogEater.Eat();
                return null;
            }
        }
    }
    public class Dog : Animal, IRunning, IEating
    {
        public void Eat()
        {
            Console.WriteLine("Dog eats");
        }

        public void Run()
        {
            Console.WriteLine("Dog runs");
        }
    }
    public abstract class Animal
    {
        public string Name { get; set; }
        public Animal()
        {
            Console.WriteLine("Something");
        }
        public virtual void Sleep()
        {
            Console.WriteLine("Sleep");
        }
    }
    public interface IRunning
    {
        void Run();
    }
    public interface IEating
    {
        void Eat();
    }
}
