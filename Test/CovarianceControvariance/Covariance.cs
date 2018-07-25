using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CovarianceControvariance
{
    #region example1
    public interface ICovariant<out T> { }
    public interface IContravariant<in T> { }

    public class Covariant<T> : ICovariant<T> { }
    public class Contravariant<T> : IContravariant<T> { }

    public class Fruit { }
    public class Apple : Fruit { }

    public class Example
    {
        public void Covariance()
        {
            List<Fruit> fruits = new List<Fruit>();
            
            ICovariant<Fruit> fruit = new Covariant<Fruit>();
            ICovariant<Apple> apple = new Covariant<Apple>();

            Covariant(fruit);
            Covariant(apple); //apple is being upcasted to fruit, without the out keyword this will not compile
        }

        public void Contravariance()
        {
            IContravariant<Fruit> fruit = new Contravariant<Fruit>();
            IContravariant<Apple> apple = new Contravariant<Apple>();

            Contravariant(fruit); //fruit is being downcasted to apple, without the in keyword this will not compile
            Contravariant(apple);
        }

        public void Covariant(ICovariant<Fruit> fruit) { }

        public void Contravariant(IContravariant<Apple> apple) { }
    }
    #endregion
    #region example1
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
        //public void Covariance()
        //{
        //    Animal a = new Animal();
        //    Cat b = new Cat();
        //    a = b;
        //}

        //public void Contravariance()
        //{
        //    Animal a = new Animal();
        //    Cat b = new Cat();
        //    b = a;
        //}
    }
    #endregion
    #region example2
    public class Person
    {
        public string Name { get; set; }
    }

    public class Teacher : Person
    {
        public string Surname { get; set; }
    }

    public class MailingList
    {
        public void Add(IEnumerable<Person> people) {  }
    }
    public class Example2
    {
        public Example2()
        {
            MailingList mailingList = new MailingList();
            List<Teacher> teach = new List<Teacher>();
            List<Person> person = new List<Person>();
            mailingList.Add(teach);
            mailingList.Add(person);
        }
    }
    #endregion
}

