using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
    /// <summary>
    /// Memory Addresses to Map.
    /// </summary>
    internal class Map : IMap {

        public uint Pointer { get; set; }

        public uint FullLightNop { get; set; }
        public byte[] FullLightNopDefault { get { return new byte[] { 0x7E, 0x05 }; } }
        public byte[] FullLightNopValue { get { return new byte[] { 0x90, 0x90 }; } }

        public uint FullLight { get; set; }
        public uint FullLightDefault { get { return 0x80; } }
        public uint FullLightValue { get { return 0xFF; } }
        
        public uint StepDist { get { return 0xAC; } }
        public uint StepObjectDist { get { return 0x0C; } }
        public uint ObjectCountDist { get { return 0x0; } }
        public uint ObjectsDist { get { return 0x4; } }
        public uint ObjectIdDist { get { return 0x0; } }
        public uint ObjectDataDist { get { return 0x4; } }
        public uint ObjectDataExDist { get { return 0x8; } }
        public uint ObjectsMax { get { return 0x0D; } }
        public uint TilesMax { get { return 0x7E0; } }
    }
}
