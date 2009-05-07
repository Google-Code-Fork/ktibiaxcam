using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
    /// <summary>
    /// Memory Addresses to SpyLevel and NameSpy.
    /// </summary>
    internal class SpyLevel : ISpyLevel {

        public byte[] Nops { get { return new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }; } }

        public uint NameSpy1 { get; set; }
        public uint NameSpy2 { get; set; }

        public uint NameSpy1Default { get { return 19061; } }
        public uint NameSpy2Default { get { return 16501; } }

        public uint LevelSpy1 { get; set; }
        public uint LevelSpy2 { get; set; }
        public uint LevelSpy3 { get; set; }
        public uint LevelSpyPtr { get; set; }

        public byte[] LevelSpyDefault { get { return new byte[] { 0x89, 0x86, 0x88, 0x2A, 0x00, 0x00 }; } }

        public byte LevelSpyAdd1 { get { return 28; } }
        public uint LevelSpyAdd2 { get { return 0x2A88; } }
        public uint Z_Axis_Default { get { return 7; } }
    }
}
