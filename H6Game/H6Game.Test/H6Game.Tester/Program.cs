using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Tester
{

    class Program
    {
        static void Main(string[] args)
        {

            var classA = new ClassA();

            var a = nameof(TestEnum.A);

            var arr = Enum.GetNames(typeof(TestEnum));

            var name = Enum.GetName(typeof(TestEnum), TestEnum.A);

            var day = DateTime.Now.ToString("-yyyy/MM/dd HH:mm:ss ");

            //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
            //ILogger logger = loggerFactory.CreateLogger<Program>();
            //logger.LogInformation("This is a test of the emergency broadcast system.");
            //logger.LogDebug("This is a test of the emergency broadcast system.");
            //logger.LogError("This is a test of the emergency broadcast system.");
            //logger.LogTrace("This is a test of the emergency broadcast system.");
            //logger.LogWarning("This is a test of the emergency broadcast system.");
            Console.Read();
        }

        public class ClassA
        {
            public int TestInt => 100;
            public ClassA()
            {
                var a = TestInt;
            }
        }

        public enum TestEnum
        {
            A,
            B,
            C,
        }
    }
}