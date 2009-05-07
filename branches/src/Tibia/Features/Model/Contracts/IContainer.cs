using Tibia.Memory;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.Contracts {
    public interface IContainer {
        TibiaMemoryProvider Memory { get; }
        ConnectionProvider Connection { get; }
        uint Address { get; }
        int Index { get; set; }
        bool IsOpen { get; set; }
        uint Volume { get; set; }
        uint Ammount { get; }
        uint Position { get; }
        void Close();
        Items.SlotCollection Slots { get; }
    }
}
