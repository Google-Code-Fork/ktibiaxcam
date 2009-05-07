using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class RemoveTextPacket : PipePacket
    {
        string textname;

        public string TextName
        {
            get { return textname; }
        }

        public RemoveTextPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.RemoveText;
            PacketSource = Tibia.Connection.PacketSource.Pipe;
        }

        public RemoveTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                textname = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveTextPacket Create(ConnectionProvider connection, string TextName)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.RemoveText);
            p.AddString(TextName);

            return new RemoveTextPacket(connection, p.GetPacket());
        }

    }
}
