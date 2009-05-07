using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class RemoveAllTextPacket : PipePacket
    {
        public RemoveAllTextPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.RemoveAllText;
            PacketSource = PacketSource.Pipe;
        }

        public RemoveAllTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveAllText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                index = p.Index;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveAllTextPacket Create(ConnectionProvider connection)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.RemoveAllText);
            return new RemoveAllTextPacket(connection, p.GetPacket());
        }
    }
}