using System;
using Tibia.Connection;
using Tibia.Connection.Model;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class RemoveCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;

        public int CreatureID
        {
            get { return creatureID; }
        }

        public string CreatureName
        {
            get { return creatureName; }
        }

        public RemoveCreatureTextPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.RemoveCreatureText;
            PacketSource = PacketSource.Pipe;
        }

        public RemoveCreatureTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveCreatureText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveCreatureTextPacket Create(ConnectionProvider connection, int CreatureID, string CreatureName)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.RemoveCreatureText);
            p.AddLong(CreatureID);
            p.AddString(CreatureName);

            return new RemoveCreatureTextPacket(connection, p.GetPacket());
        }
    }
}
