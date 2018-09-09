using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Power
{
    public class PowerApi
    {
        public static PowerApi Instance
        {
            get
            {
                FinalApi finalApi = new FinalApi(new Api());
                finalApi.Pay(5);
                ConcretionSpecificApi concretionSpecificApi = new ConcretionSpecificApi();
                concretionSpecificApi.Pay(4);
                concretionSpecificApi.GetService(1);
                concretionSpecificApi.ToString();
                UserCheck userCheck = new UserCheck(new List<IHasCustomerBase> { new Api(), new AnotherApi() /*, new OtherApi()*/ });
                Console.WriteLine("Exists? "+ userCheck.UserExists("example"));
                return null;
            }
        }
    }
    public abstract class AApi
    {
        public abstract void pay(double x);
        public void Pay(double x)
        {
            try
            {
                Console.WriteLine("Do something");
                this.pay(x);
                Console.WriteLine("Do something else");
            }
            catch
            {
                Console.WriteLine("Catch something");
            }
        }
    }
    public interface IHasCustomerBase
    {
        List<(string, string)> GetCustomerBase();
    }
    public interface IHasService
    {
        List<(int, Dictionary<string, string>)> GetService();
    }
    public class Api : AApi, IHasCustomerBase, IHasService
    {
        public List<(string, string)> GetCustomerBase()
        {
            Console.WriteLine("Get CB");
            return (new List<(string, string)> { ("example", "user"), ("example2", "user2") }); //create a list of 2 users
        }

        public List<(int, Dictionary<string, string>)> GetService()
        {
            Console.WriteLine("Get Service");
            return (new List<(int, Dictionary<string, string>)> { (1, new Dictionary<string, string>()), (2, new Dictionary<string, string>()) }); //create a list of 2 services
        }

        public override void pay(double x)
        {
            Console.WriteLine("Something, more specific than abstraction");
        }
    }
    public class AnotherApi : AApi, IHasCustomerBase, IHasService
    {
        public List<(string, string)> GetCustomerBase()
        {
            Console.WriteLine("Get another CB");
            return (new List<(string, string)> { ("example3", "user3"), ("example4", "user4") }); //create a list of 2 users
        }

        public List<(int, Dictionary<string, string>)> GetService()
        {
            Console.WriteLine("Get another Service");
            return (new List<(int, Dictionary<string, string>)> { (3, new Dictionary<string, string>()), (4, new Dictionary<string, string>()) }); //create a list of 2 services
        }

        public override void pay(double x)
        {
            Console.WriteLine("Something, more specific than abstraction but not equals to Api, for example it could be a call to an external webservice");
        }
    }
    public class FinalApi
    {
        private AApi api;
        public FinalApi(AApi api)
        {
            this.api = api;  //dependency injection
        }
        public void Pay(double x)
        {
            this.api.Pay(x);
        }
    }
    public class ConcretionSpecificApi
    {
        private Api api = new Api(); //specific dependency injection
        public void Pay(double x)
        {
            this.api.Pay(x);
        }
        public Dictionary<string,string> GetService(int id)
        {
            foreach(var x in this.api.GetService())
            {
                if (x.Item1 == id) return x.Item2;
            }
            return new Dictionary<string, string>();
        }
        public override string ToString()
        {
            return this.api.GetService().FirstOrDefault().Item1 + "-" + this.api.GetCustomerBase().FirstOrDefault().Item1;  //tostring of first service and first user
        }
    }
    public class UserCheck
    {
        private List<IHasCustomerBase> apisWithCustomerBase;
        public UserCheck(List<IHasCustomerBase> apisWithCustomerBase)
        {
            this.apisWithCustomerBase = apisWithCustomerBase;
        }
        public bool UserExists(string id)  //check if exists a user with that id in every integration api
        {
            foreach(IHasCustomerBase apiWithCustomerBase in this.apisWithCustomerBase)
            {
                foreach (var user in apiWithCustomerBase.GetCustomerBase()) {
                    if (user.Item1 == id) return true;
                }
            }
            return false;
        }
    }
}
