using Tibia.Connection;
using Tibia.Memory;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Messages {
	public class Server {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
		public Server(ConnectionProvider connection) {
			this.Connection = connection;
		}

		#region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
		#endregion

		/// <summary>
		/// Send a Message to Default Channel.
		/// </summary>
		public void Default(string message, MessageType type) {

			#region "[rgn] Packet Structure Analyze "
			//---------------------------------------
			//SZ      ID  SZ NM                      
			//---------------------------------------
			//07 00 96 01 03 00 61 65 77             
			//0B 00 96 01 07 00 62 6C 7A 69 6E 68 61 
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x96, Connection);
			Builder.Append(type.GetHashCode());
			Builder.Append(message, true);
			Builder.SetPacketSource(PacketSource.Client);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send a Message to Default Channel.
		/// </summary>
		public void Default(string message) {

			#region "[rgn] Packet Structure Analyze "
			//---------------------------------------
			//SZ      ID  SZ NM                      
			//---------------------------------------
			//07 00 96 01 03 00 61 65 77             
			//0B 00 96 01 07 00 62 6C 7A 69 6E 68 61 
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x96, Connection);
			Builder.Append(MessageType.Normal.GetHashCode());
			Builder.Append(message, true);
			Builder.SetPacketSource(PacketSource.Client);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send a Message to Default Channel.
		/// </summary>
		public void Trade(string message) {

			#region "[rgn] Packet Structure Analyze "
			//------------------------------------------------------------------------
			//SZ    ID          SZ    MSG                                                      
			//------------------------------------------------------------------------
			//14 00 96 05 05 00 0E 00 73 65 6C 6C 20 63 72 6F 73 73 20 62 6F 77
			//0A 00 96 05 05 00 04 00 73 65 6C 6C 
			#endregion

            PacketBuilder Builder = new PacketBuilder(0x96, Connection);
			Builder.Append(ChannelType.Trade.GetHashCode());
			Builder.Append(0x05);
			Builder.Append(0x00);
			Builder.Append(message, true);
			Builder.SetPacketSource(PacketSource.Client);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Opens Trade Channel.
		/// </summary>
		public void OpenTradeChannel() {

            #region "[rgn] Packet Structure Analyze "
            //------------------------------------------------------------------------
            //SZ    ID CHANNEL
            //------------------------------------------------------------------------
            //03 00 98 05 00
            #endregion

            PacketBuilder Builder = new PacketBuilder(0x98, Connection);
			Builder.Append(0x05, 2);
			Builder.SetPacketSource(PacketSource.Client);
			Connection.Send(Builder.GetPacket());
			
		}

		/// <summary>
		/// Send a Private Message.
		/// </summary>
		public void Private(string playerName, string message) {

			#region "[rgn] Packet Structure Analyze "
			//------------------------------------------------------------------------
			//SZ      ID  SZ NM                         SZ MS                         
			//------------------------------------------------------------------------
			//16 00 96 04 08 00 53 75 64 75 64 69 67 75 08 00 62 6C 7A 20 63 61 72 61 
			//00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 
			#endregion

            PacketBuilder Builder = new PacketBuilder(0x96, Connection);
			Builder.Append(ChannelType.GameChat.GetHashCode());
			Builder.Append(playerName, true);
			Builder.Append(message, true);
			Builder.SetPacketSource(PacketSource.Client);
			Connection.Send(Builder.GetPacket());
		}

	}
}
