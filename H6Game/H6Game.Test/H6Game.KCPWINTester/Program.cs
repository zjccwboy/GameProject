using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.KCPWINTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var conv = 100001;
            var kcp = KCP.KcpCreate((uint)conv, new IntPtr(conv));
            KCP.KcpSetoutput(kcp, (bts, len, k, user) => { Output(bts, len, k, user); return len; });
            KCP.KcpNodelay(kcp, 1, 10, 1, 1);
            KCP.KcpWndsize(kcp, 256, 256);
            KCP.KcpSetmtu(kcp, 470);

            var send = "1234567890";

            byte[] bytes = Encoding.Default.GetBytes(send);
            var size = bytes.Length;
            unsafe
            {
                TypedReference reference = __makeref(bytes);
                //IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length);
                //Marshal.Copy(bytes, 0, intPtr, bytes.Length);
                //KCP.KcpSendEx(kcp, bytes, 2, bytes.Length - 2);
                IntPtr intPtr = new IntPtr(&reference);
                KCP.KcpSend(kcp, intPtr, size);

                KCP.KcpUpdate(kcp, 1);

            }
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// KCP发送回调函数
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="count"></param>
        /// <param name="user"></param>
        static void Output(IntPtr bytes, int count, IntPtr kcp, IntPtr user)
        {
            int put = KCP.KcpInput(kcp, bytes, 0, count);

            var n = KCP.KcpPeeksize(kcp);

        }
    }
}
