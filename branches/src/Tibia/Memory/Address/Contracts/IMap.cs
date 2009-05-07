namespace Tibia.Memory.Address.Contracts {

    public interface IMap {
        uint Pointer { get; }

        uint FullLightNop { get; }
        byte[] FullLightNopDefault { get; }
        byte[] FullLightNopValue { get; }

        uint FullLight { get; }
        uint FullLightDefault { get; }
        uint FullLightValue { get; }

        uint StepDist { get; }
        uint StepObjectDist { get; }
        uint ObjectCountDist { get; }
        uint ObjectsDist { get; }
        uint ObjectIdDist { get; }
        uint ObjectDataDist { get; }
        uint ObjectDataExDist { get; }
        uint ObjectsMax { get; }
        uint TilesMax { get; }
    }
}
