using Tibia.Memory;
using Tibia.Features.Structures;
using Tibia.Features.Model.Items;
using Tibia.Features.Common;

namespace Tibia.Features.Model.Contracts {
    public interface ICreature {
        TibiaMemoryProvider Memory { get; }
        uint Address { get; }

        string Name { get; set; }
        uint Id { get; }
        uint HpBar { get; set; }
        uint WalkSpeed { get; set; }
        uint Addon { get; set; }
        bool IsWalking { get; set; }
        bool IsVisible { get; set; }

        Distance Distance(Player Player);
        Location Location { get; set; }
        Light Light { get; set; }

        OutfitKind Outfit { get; set; }
        Skull Skull { get; set; }
        PartyRank Party { get; set; }
        CreatureType Type { get; set; }

        void Follow();
        void Shoot(Slot RuneSlot);
        void Shoot(uint RuneID);
        void Attack();

    }
}
