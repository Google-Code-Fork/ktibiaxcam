using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
    /// <summary>
    /// Memory Address to DatItems.
    /// </summary>
    internal class DatItem : IDatItem {

        public uint Width { get { return 0; } }
        public uint Height { get { return 4; } }
        public uint Unknown1 { get { return 8; } }
        public uint Layers { get { return 12; } }
        public uint PatternX { get { return 16; } }
        public uint PatternY { get { return 20; } }
        public uint PatternDepth { get { return 24; } }
        public uint Phase { get { return 28; } }
        public uint Sprites { get { return 32; } }
        public uint Flags { get { return 36; } }
        public uint WalkSpeed { get { return 40; } }
        public uint TextLimit { get { return 44; } } // If it is readable/writable
        public uint LightRadius { get { return 48; } }
        public uint LightColor { get { return 52; } }
        public uint ShiftX { get { return 56; } }
        public uint ShiftY { get { return 60; } }
        public uint WalkHeight { get { return 64; } }
        public uint Automap { get { return 68; } } // Minimap color
        public uint LensHelp { get { return 72; } }
    }
}
