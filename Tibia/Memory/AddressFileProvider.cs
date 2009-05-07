using System;
using System.IO;
using Keyrox.Shared.Files;
using Keyrox.Shared.Objects;
using Tibia.Properties;

namespace Tibia.Memory {
    public sealed class AddressFileProvider {

        /// <summary>
        /// Gets the address file path.
        /// </summary>
        /// <value>The address file path.</value>
        private static string AddressFilePath {
            get {
                return Path.Combine(
                    Environment.CurrentDirectory, 
                    Settings.Default.AddressFileName);
            }
        }

        /// <summary>
        /// Gets the addressess.
        /// </summary>
        /// <returns></returns>
        public static AddressDTOCollection GetAddressess() {
            var fi = new FileInfo(AddressFilePath);
            if (fi.Exists) {
                var xml = fi.Read();
                return xml.Deserialize<AddressDTOCollection>();
            }
            return null;
        }

        /// <summary>
        /// Saves the address.
        /// </summary>
        /// <param name="address">The address.</param>
        public static void SaveAddress(AddressDTOCollection address) {
            var fi = new FileInfo(AddressFilePath);
            if (fi.Exists) { fi.Delete(); }
            fi.Write(address.Serialize());
        }

    }
}
