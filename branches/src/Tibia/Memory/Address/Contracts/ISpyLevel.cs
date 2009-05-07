namespace Tibia.Memory.Address.Contracts {

    public interface ISpyLevel {

        byte[] Nops { get; }

        uint NameSpy1 { get; set; }
        uint NameSpy2 { get; set; }

        uint NameSpy1Default { get; }
        uint NameSpy2Default { get; }

        uint LevelSpy1 { get; set; }
        uint LevelSpy2 { get; set; }
        uint LevelSpy3 { get; set; }
        uint LevelSpyPtr { get; set; }

        byte[] LevelSpyDefault { get; }

        byte LevelSpyAdd1 { get; }
        uint LevelSpyAdd2 { get; }
        uint Z_Axis_Default { get; }
    }
}
