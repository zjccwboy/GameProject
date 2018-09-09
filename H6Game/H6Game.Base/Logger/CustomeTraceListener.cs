using System;
using System.Diagnostics;
using System.IO;

namespace H6Game.Base
{
    public class CustomeTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            File.AppendAllText(@"d:\log.txt", message);
        }

        public override void WriteLine(string message)
        {
            File.AppendAllText(@"d:\log.txt", message + Environment.NewLine);
        }
    }
}
