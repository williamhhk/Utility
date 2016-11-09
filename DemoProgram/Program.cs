using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Reactive.Threading.Tasks;

namespace DemoProgram
{
    class Program
    {
        public static ObservableCollection<string> Results = new ObservableCollection<string>();
        static void Main(string[] args)
        {

            ////Generate values 0,1,2 
            //var s1 = Observable.Range(0, 3);
            ////Generate values 5,6,7,8,9 
            //var s2 = Observable.Range(5, 5);
            //s1.Concat(s2)
            //.Subscribe(Console.WriteLine);



            //CancellationTokenSource cts = new CancellationTokenSource();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //stopwatch.Stop();
            //Console.WriteLine($"Do other works.....");
            //Console.WriteLine($"{stopwatch.ElapsedMilliseconds}");
            //Console.WriteLine($"Do other works.....");
            //Thread.Sleep(10000);
        }

        static void DoSomeWork(int val)
        {
            // Pretend to do something.
            Thread.SpinWait(val);
            Console.WriteLine("Done.....");
        }

        public static async Task<CimInstance> MainAsync(String[] args, CancellationToken token)
        {
            
            IObservable<CimInstance> allVolumes = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            //using (allVolumes.Subscribe(x => Console.WriteLine($"{x}")))
            //{
            //    Console.ReadLine();
            //}
            allVolumes.Subscribe(x => Console.WriteLine($"{x}"));
            return await allVolumes.LastAsync();
        }

        public static async Task<CimInstance> WaitForFirstResultAndReturn()
        {
            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            IObservable<CimInstance> observable2 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_DiskDrive");
            var tsk = await observable1.Merge(observable2).FirstAsync();
            Console.WriteLine(tsk.CimInstanceProperties["DeviceId"].Value.ToString());
            return tsk;
        }

        public static async void WaitForAllReturn()
        {
            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_DiskDrive");
            IObservable<CimInstance> observable2 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            await observable1.Merge(observable2).ForEachAsync(x=> Console.WriteLine($"{x}"));
        }

 
        public static async Task<string> Zip()
        {

            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            IObservable<CimInstance> observable2 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_DiskDrive");
            var tsk = await observable1.Zip(observable2, (x1, x2) => string.Join(" ", x1.CimInstanceProperties["DriveLetter"].Value.ToString(), x2.CimInstanceProperties["Model"].Value.ToString()));
            Console.WriteLine(tsk.ToString());
            return tsk;
        }

        public static async Task<CimInstance> Merge()
        {

            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            IObservable<CimInstance> observable2 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_DiskDrive");
            var tsk = await observable1.Merge(observable2).Timeout(TimeSpan.FromMilliseconds(10000));
            Console.WriteLine(tsk.ToString());
            return tsk;
        }

        public static void SubcribeTest()
        {
            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            var tsk = observable1.Subscribe(value => Console.WriteLine("1st subscription received {0}", value),
                ex => Console.WriteLine("Caught an exception : {0}", ex));
        }


        public static void SubcribeTest1()
        {
            IObservable<CimInstance> observable1 = QueryAsync(@"root\cimv2", "SELECT * FROM Win32_Volume");
            var result = observable1.ToArray();
            result.Subscribe(
           arr => {
               Console.WriteLine("Received array");
               foreach (var value in arr)
               {
                   Console.WriteLine(value);
               }
           },
           () => Console.WriteLine("Completed")
           );
        }



        public static CimAsyncMultipleResults<CimInstance> QueryAsync(string nameSpace, string queryExpression)
        {
            return CimSession.Create(null).QueryInstancesAsync(nameSpace, "WQL", queryExpression);
        }

        public static IEnumerable<CimInstance> Query(string nameSpace, string queryExpression)
        {
            return CimSession.Create(null).QueryInstances(nameSpace, "WQL", queryExpression);
        }
    }
}
