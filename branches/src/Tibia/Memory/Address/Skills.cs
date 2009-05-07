using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to Skills.
	/// </summary>
	internal class Skills : ISkills {

        public Skills(uint playerExpAddress) {
            PlayerExpAddress = playerExpAddress;
        }

        public uint PlayerExpAddress { get; set; }

        public uint Fist { get { return PlayerExpAddress - 76; } }
        public uint Fist_Perc { get { return PlayerExpAddress - 104; } }
        public uint Club { get { return PlayerExpAddress - 72; } }
        public uint Club_Perc { get { return PlayerExpAddress - 100; } }
        public uint Sword { get { return PlayerExpAddress - 68; } }
        public uint Sword_Perc { get { return PlayerExpAddress - 96; } }
        public uint Axe { get { return PlayerExpAddress - 64; } }
        public uint Axe_Perc { get { return PlayerExpAddress - 92; } }
        public uint Distance { get { return PlayerExpAddress - 60; } }
        public uint Distance_Perc { get { return PlayerExpAddress - 88; } }
        public uint Shielding { get { return PlayerExpAddress - 56; } }
        public uint Shielding_Perc { get { return PlayerExpAddress - 84; } }
        public uint Fishing { get { return PlayerExpAddress - 52; } }
        public uint Fishing_Perc { get { return PlayerExpAddress - 80; } }
	}
}
