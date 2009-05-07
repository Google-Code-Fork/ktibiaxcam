using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to Containers.
	/// </summary>
    internal class Containers : IContainers {

        public uint StarT { get; set; }

		public uint BPDisT { get { return 0x0001EC; } }
        public uint Max { get { return 16; } }
        public uint EndeD { get { return StarT + (BPDisT * Max); } }

		public uint BPIsOpenDist { get { return 0x000000; } }
		public uint BPIdDist { get { return 0x000004; } }
		public uint BPNameDist { get { return 0x000010; } }
		public uint BPVolumeDist { get { return 0x000030; } }
		public uint BPAmountDist { get { return 0x000038; } }
		public uint ItemDisT { get { return 0x00000C; } }
        public uint Item_IDDist { get { return 0x00003C; } }
		public uint Item_CountDist { get { return 0x000040; } }
	}
}
