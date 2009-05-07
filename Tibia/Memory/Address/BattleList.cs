using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
    /// <summary>
    /// Memory Addresses to BattleList.
    /// </summary>
    internal class BattleList : IBattleList {

        public BattleList(uint playerExpAddress) {
            PlayerExpAddress = playerExpAddress;
        }

        private uint PlayerExpAddress { get; set; }

        public uint Start { get { return PlayerExpAddress + 108; } }
        public uint Ended { get { return Start + (DistC * Max); } }
        public uint Max { get { return 150; } }
        public uint DistC { get { return 0xA0; } }

        public uint RedSQuare { get; set; }
        public uint GreenSQuare { get { return RedSQuare - 4; } }
        public uint WhiteSQuare { get { return GreenSQuare - 8; } }

        public uint Target_BList_ID { get { return RedSQuare - 8; } }
        public uint Target_BList_Type { get { return RedSQuare - 5; } }
    }
}
