using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using Tibia.Features.Structures;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes
{
    public class UpdateCreatureTextPacket : PipePacket
    {
        int creatureID;
        string creatureName;
        Location textLoc = new Location();
        string newText;

        public int CreatureID
        {
            get { return creatureID; }
        }

        public string CreatureName
        {
            get { return creatureName; }
        }

        public Location TextLoc
        {
            get { return textLoc; }
        }

        public string NewText
        {
            get { return newText; }
        }

        public UpdateCreatureTextPacket(ConnectionProvider connection)
            : base(connection)
        {
            pipetype = PipePacketType.UpdateCreatureText;
            PacketSource = PacketSource.Pipe;
        }

        public UpdateCreatureTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.UpdateCreatureText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();
                textLoc.X = p.GetShort();
                textLoc.Y = p.GetShort();
                newText = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static UpdateCreatureTextPacket Create(ConnectionProvider connection, int CreatureID, string CreatureName, Location TextLoc, string NewText)
        {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.UpdateCreatureText);
            p.AddLong(CreatureID);
            p.AddString(CreatureName);
            p.AddShort(TextLoc.X);
            p.AddShort(TextLoc.Y);
            p.AddString(NewText);

            return new UpdateCreatureTextPacket(connection, p.GetPacket());
        }
    }
}
