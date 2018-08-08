using System.Net;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// 连接发送辅助类
    /// </summary>
    public class ConnectSender
    {
        /// <summary>
        /// 发送SYN连接请求
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="socket"></param>
        /// <param name="endPoint"></param>
        public static void SendSYN(Packet packet, Socket socket, EndPoint endPoint)
        {
            //发送SYN包
            packet.KcpProtocal = KcpNetProtocal.SYN;

            //握手包不要经过KCP发送
            var bytes = packet.GetHeadBytes(0);
            socket.SendTo(bytes, 0, bytes.Length,SocketFlags.None,  endPoint);
        }

        /// <summary>
        /// 发送ACK应答请求
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="socket"></param>
        /// <param name="endPoint"></param>
        /// <param name="conv"></param>
        public static void SendACK(Packet packet, Socket socket, EndPoint endPoint, int conv)
        {
            packet.KcpProtocal = KcpNetProtocal.ACK;
            packet.MessageId = conv;

            //握手包不经过KCP发送
            var bytes = packet.GetHeadBytes(0);
            socket.SendTo(bytes, bytes.Length, SocketFlags.None,  endPoint);
        }

        /// <summary>
        /// 发送FIN连接断开请求
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="socket"></param>
        /// <param name="endPoint"></param>
        /// <param name="conv"></param>
        public static void SendFIN(Packet packet, Socket socket, EndPoint endPoint, int conv)
        {
            packet.KcpProtocal = KcpNetProtocal.FIN;
            packet.MessageId = conv;

            //握手包不经过KCP发送
            var bytes = packet.GetHeadBytes(0);
            socket.SendTo(bytes, bytes.Length, SocketFlags.None, endPoint);
        }
    }
}
