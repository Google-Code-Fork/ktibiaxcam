using Tibia.Memory;
using Tibia.Features.Structures;
using Tibia.Features.Model.Items;

namespace Tibia.Features.Model.Contracts {
    public interface ISlot {
        uint Address { get; }
        int Position { get; }

        InventoryID Id { get; }
        IItem Item { get; set; }
        IContainer Container { get; }

        TibiaMemoryProvider Memory { get; }
        void Drop(Location SQM);
        void Stack(ISlot ToSlot);
    }
}
