namespace Tibia.Memory.Address.Contracts {

    public interface IDatItem {
        uint Width { get; }
        uint Height { get; }
        uint Unknown1 { get; }
        uint Layers { get; }
        uint PatternX { get; }
        uint PatternY { get; }
        uint PatternDepth { get; }
        uint Phase { get; }
        uint Sprites { get; }
        uint Flags { get; }
        uint WalkSpeed { get; }
        uint TextLimit { get; } // If it is readable/writable
        uint LightRadius { get; }
        uint LightColor { get; }
        uint ShiftX { get; }
        uint ShiftY { get; }
        uint WalkHeight { get; }
        uint Automap { get; } // Minimap color
        uint LensHelp { get; }
    }
}
