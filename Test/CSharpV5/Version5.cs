using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CSharpV5
{
    public class Version5  //release date 2012, C# changes the paradigm. http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf
    {
        //It's important to note that there is a small cost to using async and it's not recommended for tight loops.
        public Version5()
        {
            ShowCallerInfo();  //example of caller info attributes
        }
        public static void ShowCallerInfo([CallerMemberName] string callerName = null,   //This helps in avoiding unnecessary hard-coding or number of constants for just sake of property name
                                          [CallerFilePath] string callerFilePath = null,
                                          [CallerLineNumber] int callerLine = -1)
        {
            Console.WriteLine("Caller Name: {0}", callerName);
            Console.WriteLine("Caller FilePath: {0}", callerFilePath);
            Console.WriteLine("Caller Line number: {0}", callerLine);
        }
        public async Task MakeItReal()
        {
            A a = new A();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token; //CancellationToken.None
            Task<A> task = this.Set<A>(a, cancellationToken, new MyProgress<long>());
            cancellationToken.Register(() => { Console.WriteLine("cancelled"); });
            source.Cancel();
            await task;
            if(task.IsCompleted) a = task.Result;
            Task<int> task1 = Task<int>.Factory.StartNew(() => 1);
            int i = task1.Result; //The Result property blocks the calling thread until the task finishes.
            Console.WriteLine(i.ToString());
        }
        public async Task ShotMeDownBangBang()
        {
            Player player = new Player() { Name = "KeyserDSoze" };
            await Task.Run(() => player.SetBang(29));  //Use Task.Run to call processes that are related to expensive CPU consumption.
            //Furthermore there's a bit difference between Task.Run and await async. 
            //Task.Run move the execution in another thread and use the entire thread and CPU for that thread in any case.

            List<Player> players = await player.CalculateWhoHit();
            List<Task> strikes = new List<Task>();
            foreach (Player enemy in players) {
                strikes.Add(enemy.Bang(10));    //set damage done to every player in async, parallelize them
            }
            await Task.WhenAll(strikes);  //wait completation of all async method, it's possible to use "await Task.WhenAny" too (for example to get status of all thread)
        }
        //asyn in depth https://docs.microsoft.com/en-en/dotnet/standard/async-in-depth
        public async Task<T> Set<T>(T t, CancellationToken cancellationToken, IProgress<long> progress)
        {
            progress.Report(0);
            dynamic @dynamic = t as dynamic;
            progress.Report(25);
            @dynamic.Property = "Sample";
            progress.Report(50);
            await Task.Delay(2000);
            if (cancellationToken.IsCancellationRequested) return default;
            Console.WriteLine("In this case is not efficient.");
            progress.Report(75);
            //you need to have some async operations and wait for their finishing. Only in this case method makes sense to be an async method
            //without await in async method at runtime it doesn't make anything
            progress.Report(100);  //percentage of work done
            return @dynamic;
        }
        //An asynchronous method that is based on TAP can do a small amount of work synchronously, such as validating arguments and initiating the asynchronous operation, before it returns the resulting task. Synchronous work should be kept to the minimum so the asynchronous method can return quickly.
        //Asynchronous methods may be invoked from user interface (UI) threads, and any long-running synchronous work could harm the responsiveness of the application.
        //Multiple asynchronous methods may be launched concurrently. Therefore, any long-running work in the synchronous portion of an asynchronous method could delay the initiation of other asynchronous operations, thereby decreasing the benefits of concurrency.
        //In some cases, the amount of work required to complete the operation is less than the amount of work required to launch the operation asynchronously. Reading from a stream where the read operation can be satisfied by data that is already buffered in memory is an example of such a scenario. 
    }
    public class A
    {
        public string Property { get; set; }
    }
    public class MyProgress<T> : IProgress<T>
    {
        public void Report(T value)
        {
            Console.WriteLine("percentage of work done: " + value.ToString());
        }
    }
    public class Player
    {
        public string Name { get; set; }
        private int lastHit;
        public int HitPoint { get; set; } = 100;
        public async Task Bang(int damage)
        {
            await Task.Delay(200);
            this.HitPoint -= damage;
            Console.WriteLine(damage.ToString() + " damage done to " + this.Name);
        }
        public async Task<List<Player>> CalculateWhoHit()
        {
            await Task.Delay(200);
            //using lastHit to calculate a list of players struck by virtual projectile
            List<Player> players = new List<Player>();
            players.Add(new Player() { Name = "Theos" });
            players.Add(new Player() { Name = "Dracon" });
            players.Add(new Player() { Name = "Dave The Beauty" });
            return players;
        }
        public async Task SetBang(int x)
        {
            await Task.Delay(100);
            this.lastHit = x;
        }
    }
}
