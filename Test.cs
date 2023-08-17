using System.Diagnostics;
using System.Reflection;
using Microsoft.Coyote.Runtime;

namespace CoyoteTesting;

public class Test
{
    [Microsoft.Coyote.SystematicTesting.Test]
    public static void TestThreads()
    {
        var thread = new Thread(() =>
        {
            Console.WriteLine($"Inner Thread start");
            var _ = new LockInStaticClass();
            Console.WriteLine($"Inner Thread end");
        });
        thread.Start();

        var _ = new LockInStaticClass();
        
        thread.Join();
    }
    
    public class LockInStaticClass
    {
        static LockInStaticClass()
        {
            Console.WriteLine($"Static ctor start");
            SchedulingPoint.Interleave();
            Console.WriteLine($"Static ctor end");
        }
    }
}