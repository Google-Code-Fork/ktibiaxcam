namespace Tibia.Memory.Address.Contracts {

	public interface IInventory {
		uint Head { get; }
		uint Necklace { get; }
		uint Backpack { get; }
		uint Armor { get; }
		uint Right { get; }
		uint Left { get; }
		uint Legs { get; }
		uint Feet { get; }
		uint Ring { get; }
		uint Ammo { get; }
		uint Right_Count { get; }
		uint Left_Count { get; }
		uint Ammo_Count { get; }
	}
}
