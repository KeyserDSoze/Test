using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Laziness
{
    public class MUltithread
    {
        public static void MakeItReal()
        {
            Lazy<int> number = new Lazy<int>(() => Thread.CurrentThread.ManagedThreadId);
            Thread t1 = new Thread(() => Console.WriteLine("number on t1 = {0} ThreadID = {1}", number.Value, Thread.CurrentThread.ManagedThreadId));
            t1.Start();
            Thread t2 = new Thread(() => Console.WriteLine("number on t2 = {0} ThreadID = {1}", number.Value, Thread.CurrentThread.ManagedThreadId));
            t2.Start();
            Thread t3 = new Thread(() => Console.WriteLine("number on t3 = {0} ThreadID = {1}", number.Value, Thread.CurrentThread.ManagedThreadId));
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
        }
    }
}
