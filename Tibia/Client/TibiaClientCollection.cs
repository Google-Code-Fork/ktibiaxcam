using System.Diagnostics;
using System.Collections.Generic;

namespace Tibia.Client {
    /// <summary>
    /// Collection of Found Client Controls.
    /// </summary>
    public class TibiaClientCollection : List<TibiaClient> {

        /// <summary>
        /// Load Client List.
        /// </summary>
        public void Fill() {
            Process[] nProcessoS = Process.GetProcesses();
            foreach (Process nP in nProcessoS) {
                if (nP.ProcessName == "Tibia") { Add(new TibiaClient(nP)); }
            }
        }

        /// <summary>
        /// Searches the specified client process.
        /// </summary>
        /// <param name="ClientProcess">The client process.</param>
        /// <returns></returns>
        public bool Search(Process ClientProcess) {
            for (int i = 0; i < Count; i++) { 
                if (this[i].Process.Id == ClientProcess.Id) { return true; } 
            }
            return false;
        }

        /// <summary>
        /// Count {0}.
        /// </summary>
        public override string ToString() {
            return string.Format("Count: {0}", Count);
        }
    }
}
