using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CovarianceControvariance
{
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
        public void Add(IEnumerable<Person> people) { }
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
}
