using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Tester
{

    class Program
    {
        static void Main(string[] args)
        {
            ShowInfo("message");

            Console.Read();
        }


        static void ShowInfo(string message, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            Console.WriteLine("{0}:{1} - {2} {3}", file, line, member, message);
        }
    }
}