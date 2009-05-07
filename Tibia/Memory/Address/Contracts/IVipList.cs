namespace Tibia.Memory.Address.Contracts {

	public interface IVipList {

		uint VipBegin { get; }
		uint VipCount { get; }
		uint VipMaxD { get; }
		uint VipName { get; }
		uint VipStat { get; }
		uint VipDist { get; }
		uint VipIcon { get; }
		uint VipColor { get; }
	}
}
