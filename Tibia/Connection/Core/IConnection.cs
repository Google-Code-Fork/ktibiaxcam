using System;
using System.Net;
using Tibia.Connection.Common;
using Tibia.Memory;
using Tibia.Connection.CriptoProviders;
using Tibia.Connection.Packets;

namespace Tibia.Connection {
	public interface IConnection {
		ConnectionState State { get; }
		PacketSource PacketsToSave { get; set; }

		IPAddress IPServer { get; set; }
		IPAddress IPLocal { get; set; }
		IPAddress IPLoginServer { get; set; }

		int ProcessID { get; set; }
		string ServerName { get; set; }
		bool IsOtServer { get; set; }

		int BasePort { get; set; }
		MemoryProvider Memory { get; }

		event EventHandler<EventArgs> OnBeforeConnect;
		event EventHandler<EventArgs> OnConnect;
		event EventHandler<EventArgs> OnAfterConnect;

		event EventHandler<EventArgs> OnBeforeDisconnect;
		event EventHandler<EventArgs> OnDisconnect;
		event EventHandler<EventArgs> OnAfterDisconnect;
		event EventHandler<PacketEventArgs> PacketChanged;

		ICryptoManager Xtea { get; set; }
        ICryptoManager RSA { get; set; }

		void Connect();
		void Send(Packet Data);
		void Disconnect();
	}
}
