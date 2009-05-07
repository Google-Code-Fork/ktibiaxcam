namespace Tibia.Memory.Address.Contracts {

	public interface IPlayer {
		uint InGame { get; }
		uint ID { get; }
		uint Exp { get; }
        uint PlayerZ { get; }
		uint Flags { get; }
		uint Level { get; }
		uint Level_Perc { get; }
		uint MLevel { get; }
		uint MLevel_Perc { get; }
		uint Mana { get; }
		uint Mana_Max { get; }
		uint HP { get; }
		uint HP_Max { get; }
		uint Soul { get; }
		uint Cap { get; }
		uint Stamina { get; }
		uint GoTo_X { get; }
		uint GoTo_Y { get; }
		uint GoTo_Z { get; }
		uint Target_ID { get; }
		uint Target_Type { get; }
	}
}
