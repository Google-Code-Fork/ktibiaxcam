using Tibia.Features.Model.Items;
using Tibia.Features.Structures;

namespace Tibia.Features.Model.Contracts {
    public interface IItem {
        Slot Slot { get; }
        uint Id { get; set; }
        uint Count { get; set; }
        bool GetFlag(DatItemFlag flag);

        void Use();
        void Use(bool openInNewWindow);
        void UseOnGround(Location SQM, uint TileID, uint StackPosition);
        void UseOnCreature(Creature Creature, bool DropAfterUse);
        void UseOnCreature(Creature Creature);
        void UseOnSelf(bool DropAfterUse);
        void UseOnSelf();
        void SetOwner(Slot itemOwnerSlot);

        DatItem DatItem { get; }
        DatReader DatReader { get; }
    }
}
