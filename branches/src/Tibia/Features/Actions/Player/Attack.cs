using System;
using Tibia.Features.Model;
using Tibia.Features.Model.Items;
using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Player {
	public class Attack {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		/// <param name="connection">Connection Source.</param>
		public Attack(ConnectionProvider connection) {
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
		/// Shoot a Rune against the defined Target.
		/// </summary>
		public void Shoot(Slot runeSlot, Creature target) {
			#region " Packet Structure Analyze "
			// SZ      ID         BP     SLOT  HMM         X      Y    Z   ??     QD
			//----------------------------------------------------------------------
			// 11  00  83  FF FF  40  00  00  7E 0C  00  15 7E  ED 7B  07  63 00  01
			//----------------------------------------------------------------------
			// 00  01  02  03 04  05  06  07  08 09  10  11 12  13 14  15  16 17  18
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x83, connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(runeSlot.Container.Position);
			Builder.Append(0x00);
			Builder.Append(runeSlot.Position);
			Builder.Append(runeSlot.Item.Id);
			Builder.Append(runeSlot.Position);
			Builder.Append(target.Location.X);
			Builder.Append(target.Location.Y);
			Builder.Append(target.Location.Z);
			Builder.Append(0x63);
			Builder.Append(0x00);
			Builder.Append(0x01); //TODO: Try increase this value. (Quantity)
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Attack the Defined Creature.
		/// </summary>
		public void Creature(Creature target) {
			Model.Player nPlayer = new Model.Player(Connection);
			nPlayer.Target = target;

			PacketBuilder Builder = new PacketBuilder(0xA1, connection);
			Builder.Append(target.Id);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Change the Current Attack Mode.
		/// </summary>
		public void SetMode(AttackMode mode) {
			Memory.Writer.Uint(Memory.Addresses.Client.AttackMode, Convert.ToUInt32(mode.Type.GetHashCode()), 1);
			Memory.Writer.Uint(Memory.Addresses.Client.FollowMode, Convert.ToUInt32(mode.Follow.GetHashCode()), 1);

			PacketBuilder Builder = new PacketBuilder(0xA0, connection);
			Builder.Append(mode.Type.GetHashCode());      //[01 = OFFENSIVE | 02 = BALANCED | 03 = DEFENSIVE]
			Builder.Append(mode.Follow.GetHashCode());    //[00 = STAND| 01 = CHASE]
			if (mode.AttackUnmarkedPlayers) { Builder.Append(0x00); } else { Builder.Append(0x01); }
			Connection.Send(Builder.GetPacket());
		}
	}
}
