using Tibia.Features.Structures;
using Tibia.Features.Model.Items;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.Contracts {
    public interface IPlayer {
        ConnectionProvider Connection { get; }
        uint ExpToNextLevel();
        bool IsConnected { get; }
        uint Exp { get; set; }
        uint Cap { get; set; }
        Creature Target { get; set; }

        Flags Flags { get; set; }
        PointsMax Soul { get; set; }
        PointsMax Mana { get; set; }
        PointsMax Hp { get; set; }
        PointsLeft Level { get; set; }
        PointsLeft MagicLevel { get; set; }
        ContainerCollection Containers { get; }

        void Drink(Slot SlotSource, bool DropAfterDrink);
        void Drink(uint FluidID);
        void Stop();
        void Logout();
        void Go(Location To);
        void Turn(uint Direction);
        void Walk(uint Direction);
        void SetAttackMode(AttackMode Mode);

    }
}
