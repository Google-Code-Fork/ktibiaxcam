using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
	/// <summary>
	/// Memory Addresses to VipList.
	/// </summary>
	internal class VipList : IVipList {

        public uint VipBegin { get; set; }
		public uint VipCount { get { return 0x0; } } //TODO:
		public uint VipMaxD { get { return 0x64; } }
		public uint VipName { get { return 0x04; } }
		public uint VipStat { get { return 0x22; } }
		public uint VipIcon { get { return 0x28; } }
		public uint VipDist { get { return 0x2C; } }
		public uint VipColor { get { return 0x28; } } //Must Be Online | 0 = green, 1 = white

	}
}
