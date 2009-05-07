using System;
using System.Diagnostics;
using System.Linq;

namespace Tibia.Memory {
    public sealed class AddressProvider {

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public static MemoryAddresses GetAddress(Process process) {
            try {
                if (process.CanReadMainModule()) {
                    var addrs = AddressFileProvider.GetAddressess();
                    if (addrs != null) {
                        var myAddr = (from addr in addrs where addr.ClientVersion == process.MainModule.FileVersionInfo.ProductVersion select addr);
                        return myAddr.Count() > 0 ? new MemoryAddresses(myAddr.First()) : null;
                    }
                }
                return null;
            }
            catch (Exception) { return null; }
        }

    }
}
