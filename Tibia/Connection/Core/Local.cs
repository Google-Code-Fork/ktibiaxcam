using System;
using Tibia.Connection.Providers;
using Tibia.Connection.Model;
using Tibia.Memory;

namespace Tibia.Connection.Core {
    public class Local : ConnectionProvider {

        public Local(TibiaMemoryProvider memory, int processID) : base(memory, processID) { }

		/// <summary>
		/// Send Data to Connection.
		/// </summary>
		/// <param name="data">Data Information.</param>
		public override void Send(Packet data) {
            return;
		}

		/// <summary>
		/// Initialize all Connection Features.
		/// </summary>
		protected override void Connection_OnConnect(object sender, EventArgs e) {
            return;
		}

		/// <summary>
		/// Stop all current Actions and Timers.
		/// </summary>
		protected override void Connection_OnDisconnect(object sender, EventArgs e) {
            return;
		}
		/// <summary>
		/// ReInitalize current Connection.
		/// </summary>
		protected override void Connection_OnAfterDisconnect(object sender, EventArgs e) {
			Initialize();
		}

		/// <summary>
		/// Return the Connection Type.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Local Connection!";
		}
    }
}
