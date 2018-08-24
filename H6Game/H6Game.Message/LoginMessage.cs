using H6Game.Entitys.Enums;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    [MessageType(MessageType.LoginResponeMessage)]
    [ProtoContract]
    public class LoginResponeMessage : IMessage
    {
        [ProtoMember(1)]
        public LoginResutlCode Result { get; set; }

    }


    [MessageType(MessageType.LoginRequestMessage)]
    public class LoginRequestMessage : IMessage
    {
        [ProtoMember(1)]
        public LoginType LoginType { get; set; }

        [ProtoMember(2)]
        public string OpenId { get; set; }

        [ProtoMember(3)]
        public string PhoneNumber { get; set; }

        [ProtoMember(4)]
        public string SMSNumber { get; set; }

        [ProtoMember(5)]
        public string AlipayId { get; set; }

        [ProtoMember(6)]
        public string Account { get; set; }

        [ProtoMember(7)]
        public string Password { get; set; }


    }
}
