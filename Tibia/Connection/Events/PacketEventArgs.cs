using System;
using Tibia.Connection;
using Tibia.Connection.Model;

namespace Tibia.Connection.Events {
	public class PacketEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEventArgs"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="source">The source.</param>
		public PacketEventArgs(Packet packet, PacketSource source) {
			Packet = packet;
			Source = source;
		}
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public PacketSource Source { get; private set; }

        /// <summary>
        /// Gets or sets the packet.
        /// </summary>
        /// <value>The packet.</value>
        public Packet Packet { get; private set; }
	}
}
