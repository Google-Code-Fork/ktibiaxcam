using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class InjectDisplayPacket : PipePacket
    {
        bool injected;

        public bool Injected
        {
            get { return injected; }
        }

        public InjectDisplayPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.InjectDisplayText;
            PacketSource = PacketSource.Pipe;
        }

        public InjectDisplayPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.InjectDisplayText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                injected = Convert.ToBoolean(p.GetByte());

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static InjectDisplayPacket Create(ConnectionProvider connection, bool Injected)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.InjectDisplayText);
            p.AddByte(Convert.ToByte(Injected));

            return new InjectDisplayPacket(connection, p.GetPacket());
        }
    }
}
