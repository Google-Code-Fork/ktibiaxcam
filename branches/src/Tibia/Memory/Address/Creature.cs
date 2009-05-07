using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to Creatures.
	/// </summary>
    internal class Creature : ICreature {
       
        public uint Pointer { get; set; }
       
		public uint Type { get { return 0x000003 + Pointer; } }       
		public uint Name { get { return 0x000004 + Pointer; } }       
		public uint X { get { return 0x000024 + Pointer; } }       
		public uint Y { get { return 0x000028 + Pointer; } }
		public uint Z { get { return 0x00002C + Pointer; } }
		public uint IsWalking { get { return 0x00004C + Pointer; } }
		public uint WalkSpeed { get { return 0x00008C + Pointer; } }
		public uint Direction { get { return 0x000050 + Pointer; } }
		public uint IsVisible { get { return 0x000090 + Pointer; } }
		public uint Light { get { return 0x000078 + Pointer; } }
		public uint LightColor { get { return 0x00007C + Pointer; } }
		public uint HPBar { get { return 0x000088 + Pointer; } }
		public uint Skull { get { return 0x000094 + Pointer; } }
		public uint Party { get { return 0x000098 + Pointer; } }
		public uint OutFit { get { return 0x000060 + Pointer; } }
		public uint AddOn { get { return 0x000074 + Pointer; } }
	}
}
