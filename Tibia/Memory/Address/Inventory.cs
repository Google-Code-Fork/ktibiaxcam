using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to Inventory.
	/// </summary>
	internal class Inventory : IInventory {

        public uint Head { get; set; }
        public uint Necklace { get { return Head + 12; } }
        public uint Backpack { get { return Head + 24; } }
        public uint Armor { get { return Head + 36; } }
        public uint Right { get { return Head + 48; } }
        public uint Left { get { return Head + 60; } }
        public uint Legs { get { return Head + 72; } }
        public uint Feet { get { return Head + 84; } }
        public uint Ring { get { return Head + 96; } }
        public uint Ammo { get { return Head + 108; } }

        public uint SlotDistCount { get { return 4; } }

        public uint Right_Count { get { return Right + SlotDistCount; } }
        public uint Left_Count { get { return Left + SlotDistCount; } }
        public uint Ammo_Count { get { return Ammo + SlotDistCount; } }
	}
}
