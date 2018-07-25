using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CovarianceControvariance
{
    public interface ITypeA<in T> { }
    public interface ITypeB<out T> { }
    public class Animal
    {
        public int Type { get; set; }
    }
    public class Dog : Animal
    {
        public int NumberOfEyes { get; set; } = 2;
    }
    public class Cat : Animal
    {
        public int NumberOfMouth { get; set; } = 1;
    }

    public class Example3
    {
        public void Covariance()
        {
            ITypeA<Dog> typeA = GetAnimal();
        }

        static ITypeA<Animal> GetAnimal()
        {
            return null;
        }

        public void Contravariance()
        {
            ITypeB<Animal> typeB = GetDog();
        }

        static ITypeB<Dog> GetDog()
        {
            return null;
        }
    }
}
