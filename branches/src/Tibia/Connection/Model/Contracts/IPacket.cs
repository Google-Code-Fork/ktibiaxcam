using System;
using Tibia.Connection.Providers;

namespace Tibia.Connection.Model.Contracts {
    public interface IPacket {
        uint Id { get; set; }
        int DataSize { get; }
        int TrueSize { get; }

        byte[] Data { get; set; }
        byte[] EncryptedData { get; set; }

        ConnectionProvider ConnectionSource { get; set; }
        PacketSource PacketSource { get; set; }
        IncomingPacketType InType { get; }
        OutgoingPacketType OutType { get; }

        string ToHexString();
        string ToDecString();
        string ToCryptHexString();

        Exception CryptoException { get; }
        bool IsValid { get; }
        DateTime InterceptedDate { get; set; }
    }
}
