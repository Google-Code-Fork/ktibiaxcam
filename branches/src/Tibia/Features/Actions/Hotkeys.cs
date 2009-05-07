using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions {
	public class Hotkey {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		/// <param name="connection">Connection Source.</param>
		public Hotkey(ConnectionProvider connection) {
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
		/// Shoot a Rune against a defined Creature.
		/// </summary>
		public void Shoot(uint creatureID, uint runeID) {
			#region " Packet Structure Analyze "
			//SZ    ID                [ UH]    [Player ID] 
			//0D 00 84 FF FF 00 00 00 58 0C 00 63 14 32 00 
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x84, connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(runeID);
			Builder.Append(0x00);
			Builder.Append(creatureID);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Drink any kind of Fluid Item.
		/// </summary>
		public void DrinkFluid(uint fluidID) {
			PacketBuilder Builder = new PacketBuilder(0x84, connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(fluidID);
			if (connection.IsOtServer) {
				Builder.Append(0x02);
			}
			else { Builder.Append(0x0A); }
			Builder.Append(new Model.Player(Connection).Id);
			Connection.Send(Builder.GetPacket());
		}

	}
}
