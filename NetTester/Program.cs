using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTester
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTesting.TestRunner.Initialize();

            NetTesting.TestRunner.LogMethod = (a) =>
            {
                Console.WriteLine(a);
                return true;
            };

            NetTesting.TestRunner.LogErrorMethod = (a) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(a);
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            };

            NetTesting.TestRunner.LogSuccessMethod = (a) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(a);
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            };

            NetTesting.TestRunner.RunTests();
            Console.ReadLine();
        }
    }
}
