using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to Player.
	/// </summary>
	internal class Player : IPlayer {

        public uint InGame { get; set; }
        public uint Exp { get; set; }

        public uint PlayerZ { get; set; }

        public uint ID { get { return Exp + 12; } }
        public uint HP { get { return Exp + 8; } }
        public uint HP_Max { get { return Exp + 4; } }

        public uint Level { get { return Exp - 4; } }
        public uint Level_Perc { get { return Exp - 12; } }
        public uint MLevel { get { return Exp - 8; } }
        public uint MLevel_Perc { get { return Exp - 16; } }

        public uint Mana { get { return Exp - 20; } }
        public uint Mana_Max { get { return Exp - 24; } }

        public uint Soul { get { return Exp - 28; } }
        public uint Cap { get { return Exp - 36; } }
        public uint Stamina { get { return Exp - 32; } }

        public uint Flags { get { return Exp - 108; } }

        public uint GoTo_X { get { return Exp + 80; } }
        public uint GoTo_Y { get { return Exp + 76; } }
        public uint GoTo_Z { get { return Exp + 72; } }

        public uint Target_ID { get; set; }
        public uint Target_Type { get { return Target_ID + 3; } }
	}
}
