using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Keyrox.Shared;
using Keyrox.Shared.Memory;

namespace Tibia.Memory {
    public class TibiaMemoryProvider {

        /// <summary>
        /// Initializes a new instance of the <see cref="TibiaMemoryProvider"/> class.
        /// </summary>
        /// <param name="process">The process.</param>
        public TibiaMemoryProvider(Process process) {
            Addresses = AddressProvider.GetAddress(process);
            if (Addresses == null) {
                throw new Exception("This Tibia version isn't supported!");
            }
            else {
                this.Process = process;
                Provider = new MemoryProvider(process);
            }
        }

        #region "[rgn] Public Properties   "
        public Process Process { get; set; }
        public MemoryAddresses Addresses { get; set; }
        public Reader Reader { get { return Provider.Reader; } }
        public Writer Writer { get { return Provider.Writer; } }
        public MemoryProvider Provider { get; set; }
        public IntPtr HandleProcess { get { return Process.Handle; } }
        #endregion
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return Addresses != null ? Addresses.Version : base.ToString();

        }
    }
}
