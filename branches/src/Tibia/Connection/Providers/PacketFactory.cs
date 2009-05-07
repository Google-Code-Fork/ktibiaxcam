using System;
using Tibia.Connection;
using Tibia.Connection.Model;

namespace Tibia.Connection.Providers {
    public sealed class PacketFactory {

        /// <summary>
        /// Binds the packet.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="interceptedDate">The intercepted date.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="source">The source.</param>
        /// <param name="cripto">The cripto.</param>
        /// <returns></returns>
        public static Packet BindPacket(byte[] encryptedData, DateTime interceptedDate, ConnectionProvider connection, PacketSource source) {
            return new Packet {
                EncryptedData = encryptedData,
                ConnectionSource = connection,
                InterceptedDate = interceptedDate,
                PacketSource = source,
            };
        }

        /// <summary>
        /// Binds the packet.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="cripto">The cripto.</param>
        /// <returns></returns>
        public static Packet BindPacket(byte[] data, ConnectionProvider connection) {
            return new Packet {
                Data = data,
                ConnectionSource = connection,
                InterceptedDate = DateTime.Now,
            };
        }
    }
}
