namespace Tibia.Memory.Address.Contracts {

	public interface IBattleList {
		uint Start { get; }
		uint Ended { get; }
		uint DistC { get; }
		uint RedSQuare { get; }
		uint GreenSQuare { get; }
		uint WhiteSQuare { get; }
		uint Target_BList_ID { get; }
		uint Target_BList_Type { get; }
	}
}
