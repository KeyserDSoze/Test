using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.FieldMethodProperty
{
    public class FieldMethodProperty
    {
        public static FieldMethodProperty Instance
        {
            get
            {
                Sample sample = new Sample() { Property = 4 };
                sample.SetField(5);
                Console.WriteLine(sample.Sum(6).ToString());
                return null;
            }
        }
    }
    public class Sample
    {
        public int Property { get; set; }
        private int Field;
        public void SetField(int field)
        {
            this.Field = field;
        }
        public int GetField()
        {
            return this.Field;
        }
        public int Sum(int value)
        {
            return this.Property + this.Field + value;
        }
    }
}
