using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class SetConstantPacket : PipePacket
    {
        string constantname;
        int value;

        public string ConstantName
        {
            get { return constantname; }
        }

        public int Value
        {
            get { return value; }
        }

        public SetConstantPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.SetConstant;
            PacketSource = Tibia.Connection.PacketSource.Pipe;
        }

        public SetConstantPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.SetConstant) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                constantname = p.GetString();
                value = p.GetLong();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SetConstantPacket Create(ConnectionProvider connection, string ConstantName, int Value)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.SetConstant);
            p.AddString(ConstantName);
            p.AddLong(Value);
            return new SetConstantPacket(connection, p.GetPacket());
        }
    }
}