using H6Game.Base;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDistributed
{
    [MessageCMD((int)MessageCMD.TestCMD1)]
    public class TestHandler : AHandler<string>
    {
        protected override void Dispatcher(string response, int messageId)
        {
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/Dispatcher", response);
        }
    }
}
