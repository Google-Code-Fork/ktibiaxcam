using System;
using System.Drawing;
using Tibia.Connection.Model;
using Tibia.Features.Structures;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes {
    public class DisplayCreatureTextPacket : PipePacket {
        int creatureID;
        string creatureName;
        Location textloc = new Location();
        Color color;
        int red;
        int green;
        int blue;
        ClientFont font;
        string text;

        public int CreatureID {
            get { return creatureID; }
        }

        public string CreatureName {
            get { return creatureName; }
        }

        public Location TextLoc {
            get { return textloc; }
        }

        public Color Color {
            get { return color; }
        }

        public ClientFont Font {
            get { return font; }
        }

        public string Text {
            get { return text; }
        }

        public DisplayCreatureTextPacket(ConnectionProvider connection)
            : base(connection) {
            pipetype = PipePacketType.DisplayCreatureText;
            PacketSource = PacketSource.Pipe;
        }

        public DisplayCreatureTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection) {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet) {
            if (base.ParseData(packet)) {
                if (pipetype != PipePacketType.DisplayCreatureText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                creatureID = p.GetLong();
                creatureName = p.GetString();
                textloc.X = p.GetShort();
                textloc.Y = p.GetShort();
                red = p.GetInt();
                green = p.GetInt();
                blue = p.GetInt();
                color = Color.FromArgb(red, green, blue);
                font = (ClientFont)p.GetInt();
                text = p.GetString();

                index = p.Index;
                return true;
            }
            else {
                return false;
            }
        }

        public static DisplayCreatureTextPacket Create(ConnectionProvider connection, int creatureID, string creatureName, Location loc, Color color, ClientFont font, string text) {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.DisplayCreatureText);
            p.AddLong(creatureID);
            p.AddString(creatureName);
            p.AddShort(loc.X);
            p.AddShort(loc.Y);
            p.AddInt(color.R);
            p.AddInt(color.G);
            p.AddInt(color.B);
            p.AddInt((int)font);
            p.AddString(text);

            return new DisplayCreatureTextPacket(connection, p.GetPacket());
        }
    }
}