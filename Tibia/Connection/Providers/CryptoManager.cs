using System;
using Keyrox.Shared.Criptography;
using Keyrox.Shared.Criptography.Contracts;
using Tibia.Connection.Providers.Contracts;

namespace Tibia.Connection.Providers {
    public class CryptoManager : ICryptoManager {

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoManager"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public CryptoManager(ConnectionProvider source) {
            ConnectionSource = source;

            if (source.Memory.Addresses.NumVersion >= 8.4M) {
                Xtea = new XTeaCheckSum();
            }
            //else if (source.Memory.Addresses.NumVersion >= 8.0M) {
            //    Xtea = new XTea();
            //}
            else { AvoidCriptograph = true; }

        }

        #region "[rgn] Public Properties   "
        public ICriptoProvider Xtea { get; set; }
        public bool AvoidCriptograph { get; set; }
        public uint KeyAddress { get; set; }
        public ConnectionProvider ConnectionSource { get; set; }
        #endregion

        /// <summary>
        /// Cryptographes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public byte[] Cryptograph(byte[] data, byte[] key) {
            if (!AvoidCriptograph && key != null) {
                Xtea.Key = key;
                return Xtea.Encode(data);
            }
            else if (AvoidCriptograph) { return data; }
            else { throw new Exception("Key was not initialized!"); }
        }

        /// <summary>
        /// Cryptographes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public byte[] Cryptograph(byte[] data) {
            return Cryptograph(data, ConnectionSource.XTeaKey);
        }

        /// <summary>
        /// Des the cryptograph.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public byte[] DeCryptograph(byte[] data, byte[] key) {
            if (!AvoidCriptograph && key != null) {
                Xtea.Key = key;
                return Xtea.Decode(data);
            }
            else if (AvoidCriptograph) { return data; }
            else { throw new Exception("Key was not initialized!"); }
        }

        /// <summary>
        /// Des the cryptograph.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public byte[] DeCryptograph(byte[] data) {
            return DeCryptograph(data, ConnectionSource.XTeaKey);
        }

    }
}
