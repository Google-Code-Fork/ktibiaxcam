using Tibia.Connection.Providers;

namespace Tibia.Connection.Model.Contracts {
    public interface IPipe {
        
        ConnectionProvider Connection { get; set; }
        
        string Name { get; set; }
        
        bool Connected { get; }

        void Send(Packet packet);
    }
}
