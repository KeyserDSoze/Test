using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Laziness
{
    public class Customer
    {
        public string Name { get; set; }
        private Lazy<List<Order>> _Orders = new Lazy<List<Order>>(() => Order.GetOrders(null));
        public List<Order> Orders { get { return this._Orders.Value; } }
        public Customer(string _Name)
        {
            this.Name = _Name;
        }
    }
    public class Order
    {
        public int X { get; set; }
        public Order(int _X)
        {
            this.X = _X;
        }
        public static List<Order> GetOrders(string _ByName)
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order(1));
            orders.Add(new Order(2));
            orders.Add(new Order(3));
            Console.WriteLine("Get Orders");
            return orders;
        }
    }
}
