namespace Tibia.Memory.Address.Contracts {

	public interface ICreature {

        uint Pointer { get; set; }
		uint Type { get; }
		uint Name { get; }
		uint X { get; }
		uint Y { get; }
		uint Z { get; }
		uint IsWalking { get; }
		uint WalkSpeed { get; }
		uint Direction { get; }
		uint IsVisible { get; }
		uint Light { get; }
		uint LightColor { get; }
		uint HPBar { get; }
		uint Skull { get; }
		uint Party { get; }
		uint OutFit { get; }
		uint AddOn { get; }
	}
}
