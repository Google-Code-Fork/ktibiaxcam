using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KTibiaX.Cam.Events {
    public class ProcessEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessEventArgs"/> class.
        /// </summary>
        /// <param name="process">The process.</param>
        public ProcessEventArgs(Process process) {
            this.Process = process;
        }

        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public Process Process { get; set; }

    }
}
