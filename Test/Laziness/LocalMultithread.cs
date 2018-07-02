using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Laziness
{
    public class LocalMultithread
    {
        public static void MakeItReal()
        {
            ThreadLocal<int> threadLocalNumber = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);
            Thread t4 = new Thread(() => Console.WriteLine("threadLocalNumber on t4 = {0} ThreadID = {1}", threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            t4.Start();
            Thread t5 = new Thread(() => Console.WriteLine("threadLocalNumber on t5 = {0} ThreadID = {1}", threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            t5.Start();
            Thread t6 = new Thread(() => Console.WriteLine("threadLocalNumber on t6 = {0} ThreadID = {1}", threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            t6.Start();
            t4.Join();
            t5.Join();
            t6.Join();
        }
    }
}
