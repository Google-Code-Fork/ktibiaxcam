using System;
using System.Net;
using Tibia.Connection.Events;
using Tibia.Connection.Model;
using Tibia.Memory;

namespace Tibia.Connection.Providers.Contracts {
	public interface IConnection {
		ConnectionState State { get; }
		PacketSource PacketsToSave { get; set; }

		IPAddress IPServer { get; set; }
		IPAddress IPLocal { get; set; }
		IPAddress IPLoginServer { get; set; }

		int ProcessID { get; set; }
		string ServerName { get; set; }
		bool IsOtServer { get; set; }

        int ServerPort { get; set; }
        int LocalPort { get; }
        int LoginServerPort { get; set; }

        TibiaMemoryProvider Memory { get; }

		event EventHandler<EventArgs> OnBeforeConnect;
		event EventHandler<EventArgs> OnConnect;
		event EventHandler<EventArgs> OnAfterConnect;

		event EventHandler<EventArgs> OnBeforeDisconnect;
		event EventHandler<EventArgs> OnDisconnect;
		event EventHandler<EventArgs> OnAfterDisconnect;
		event EventHandler<PacketEventArgs> PacketChanged;

		CryptoManager Xtea { get; set; }
		CryptoManager RSA { get; set; }

		void Connect();
		void Send(Packet Data);
		void Disconnect();
	}
}
