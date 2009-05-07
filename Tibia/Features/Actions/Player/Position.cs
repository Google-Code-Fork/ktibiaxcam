using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Player {
	public class Position {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		/// <param name="connection">Connection Source.</param>
		public Position(ConnectionProvider connection) {
			Memory = connection.Memory;
			Connection = connection;
		}

		#region "[rgn] Public Properties "
		private ConnectionProvider connection;
		public ConnectionProvider Connection {
			get { return connection; }
			set { connection = value; }
		}
		private TibiaMemoryProvider memory;
		public TibiaMemoryProvider Memory {
			get { return memory; }
			set { memory = value; }
		}
		#endregion

		/// <summary>
		/// Stop all Player Actions.
		/// </summary>
		public void Stop() {
			PacketBuilder Builder = new PacketBuilder(0xBE, connection);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Follow the Defined Creature.
		/// </summary>
		public void Follow(uint creatureID) {
			#region " Packet Structure Analyze "
			//SZ    ID [ CreatID ]
			//--------------------
			//05 00 A2 4F 2D 45 01
			#endregion

			PacketBuilder Builder = new PacketBuilder(0xA2, connection);
			Builder.Append(creatureID);
			Connection.Send(Builder.GetPacket());
			Connection.Memory.Writer.Uint(Connection.Memory.Addresses.BattleList.GreenSQuare, creatureID, 4);
		}

		/// <summary>
		/// Logout the Current Main Player.
		/// </summary>
		public void Logout() {
			PacketBuilder Builder = new PacketBuilder(0x14, connection);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Move the Player to Defined Location.
		/// </summary>
		public void Go(Location to) {
			Model.Player nPlayer = new Model.Player(Connection);
			Memory.Writer.Uint(Memory.Addresses.Player.GoTo_X, to.X, 2);
			Memory.Writer.Uint(Memory.Addresses.Player.GoTo_Y, to.Y, 2);
			Memory.Writer.Uint(Memory.Addresses.Player.GoTo_Z, to.Z, 1);
			nPlayer.IsWalking = true;
		}

		/// <summary>
		/// Turn the Player to Defined Direction.
		/// </summary>
		public void Turn(uint direction) {
			PacketBuilder Builder = new PacketBuilder();
			switch (direction) {
				case 0: Builder = new PacketBuilder(0x6F, connection); break;
				case 1: Builder = new PacketBuilder(0x70, connection); break;
				case 2: Builder = new PacketBuilder(0x71, connection); break;
				case 3: Builder = new PacketBuilder(0x72, connection); break;
			}
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Walk one SQM to the Defined Direction.
		/// </summary>
		public void Walk(uint direction) {
			PacketBuilder Builder = new PacketBuilder();
			switch (direction) {
				case 0: Builder = new PacketBuilder(0x65, connection); break; /* /\ | Y -1 */
				case 1: Builder = new PacketBuilder(0x66, connection); break; /* >> | X -1 */
				case 2: Builder = new PacketBuilder(0x67, connection); break; /* \/ | Y +1 */
				case 3: Builder = new PacketBuilder(0x68, connection); break; /* << | X +1 */
			}
			Connection.Send(Builder.GetPacket());
		}
	}
}
