using System;
using System.Drawing;
using Tibia.Connection.Model;
using Tibia.Features.Structures;
using Tibia.Connection;
using Keyrox.Shared.Objects;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Pipes {
    public class DisplayTextPacket : PipePacket {
        private string textname;
        private Location loc = new Location();
        private int red;
        private int green;
        private int blue;
        private Color color;
        private ClientFont font;
        private string text;

        public string TextName {
            get { return textname; }
        }

        public Location Loc {
            get { return loc; }
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

        public DisplayTextPacket(ConnectionProvider connection)
            : base(connection) {
            pipetype = PipePacketType.DisplayText;
            PacketSource = PacketSource.Pipe;
        }

        public DisplayTextPacket(ConnectionProvider connection, byte[] data)
            : this(connection) {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet) {
            if (base.ParseData(packet)) {
                if (pipetype != PipePacketType.DisplayText) { return false; }
                PipePacketBuilder p = new PipePacketBuilder(ConnectionSource, packet, 3);
                textname = p.GetString();
                loc.X = p.GetInt();
                loc.Y = p.GetInt();
                loc.Z = 0; // No need
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

        public static DisplayTextPacket Create(ConnectionProvider connection, string textId, Location loc, Color color, ClientFont font, string text) {
            PipePacketBuilder p = new PipePacketBuilder(connection, PipePacketType.DisplayText);
            p.AddString(textId);
            p.AddInt(loc.X.ToInt32());
            p.AddInt(loc.Y.ToInt32());
            p.AddInt(color.R);
            p.AddInt(color.G);
            p.AddInt(color.B);
            p.AddInt((int)font);
            p.AddString(text);

            return new DisplayTextPacket(connection, p.GetPacket());
        }


    }
}
