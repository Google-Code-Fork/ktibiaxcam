using Tibia.Features;
using Tibia.Features.Model;
using Tibia.Features.Model.Items;
using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;

namespace Tibia.Features.Actions.Items {
	public class Stack {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		/// <param name="connection">Connection Source.</param>
		public Stack(ConnectionProvider connection) {
			Memory = connection.Memory;
			Connection = connection;
		}

		#region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public TibiaMemoryProvider Memory { get; set; }
		#endregion

		/// <summary>
		/// Drop the stacked item to defined location.
		/// </summary>
		public void ContainerToGround(ISlot slotSource, Location sqm) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------------
			//DROP ITEM                                                
			//---------------------------------------------------------
			//00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16       
			//---------------------------------------------------------
			//0F 00 78 FF FF 40 00 00 25 0B 00 34 7D CC 7D 07 01       
			//SZ    ID       BP    ST ITEMD ST [ X ] [ Y ] ZZ QT       
			//---------------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(slotSource.Container.Position);
			Builder.Append(0x00);
			Builder.Append(slotSource.Position);
			Builder.Append(slotSource.Item.Id);
			Builder.Append(slotSource.Position);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(slotSource.Item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of stacked Item to defined Container Slot.
		/// </summary>
        public void ContainerToContainer(ISlot fromSlot, ISlot toSlot) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------
			// SZ    ID       BP    ST  ITM  ST       BP    ST QT
			// 0F 00 78 FF FF 41 00 02 7E 0C 02 FF FF 40 00 02 01
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			//---------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(fromSlot.Container.Position);
			Builder.Append(0x00);
			Builder.Append(fromSlot.Position);
			Builder.Append(fromSlot.Item.Id);
			Builder.Append(fromSlot.Position);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(toSlot.Container.Position);
			Builder.Append(0x00);
			Builder.Append(toSlot.Position);
			Builder.Append(fromSlot.Item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of stacked Item to defined Inventory Slot.
		/// </summary>
        public void ContainerToSlot(ISlot fromSlot, InventoryID toSloT) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------
			// SZ    ID       BP    ST  ITM  ST       ST       QT
			// 0F 00 78 FF FF 40 00 03 CD 0C 03 FF FF 06 00 00 04
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			//---------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(fromSlot.Container.Position);
			Builder.Append(0x00);
			Builder.Append(fromSlot.Position);
			Builder.Append(fromSlot.Item.Id);
			Builder.Append(fromSlot.Position);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(toSloT.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(fromSlot.Item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Drop the stacked item to defined location.
		/// </summary>
        public void SlotToGround(IItem item, Location sqm) {
			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(item.Slot.Id.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(item.Id);
			Builder.Append(0x00);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of stacked Item to defined Inventory Slot.
		/// </summary>
        public void SlotToSlot(IItem item, InventoryID toSlot) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------
			// SZ    ID       ST        ITM           ST       QT
			// 0F 00 78 FF FF 06 00 00 D6 0C 00 FF FF 0A 00 00 01
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			//---------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0XFF);
			Builder.Append(item.Slot.Id.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(item.Id);
			Builder.Append(0x00);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(toSlot.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of stacked Item to defined Container Slot.
		/// </summary>
        public void SlotToContainer(IItem item, ISlot toSlot) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------
			// SZ    ID       ST        ITM           BP    ST QT
			// 0F 00 78 FF FF 0A 00 00 7E 0C 00 FF FF 40 00 01 01
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			//---------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(0xFF);
			Builder.Append(0XFF);
			Builder.Append(item.Slot.Id.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(item.Id);
			Builder.Append(0x00);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(toSlot.Container.Position);
			Builder.Append(0x00);
			Builder.Append(toSlot.Position);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Move a ammount of defined item from sqm to sqm on ground.
		/// </summary>
        public void GroundToGround(IItem item, Location from, Location to, uint stackPosition) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------
			// SZ    ID [ X ] [ Y ] Z  ITMID OD [ X ] [ Y ] Z  QT
			// 0F 00 78 90 7F AC 7C 08 25 0B 02 90 7F AB 7C 08 01
			// 0F 00 78 93 7F A8 7C 08 B3 0D 01 92 7F A8 7C 08 01
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			//---------------------------------------------------
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(from.X);
			Builder.Append(from.Y);
			Builder.Append(from.Z);
			Builder.Append(item.Id);
			Builder.Append(stackPosition);
			Builder.Append(to.X);
			Builder.Append(to.Y);
			Builder.Append(to.Z);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of ground Item to defined Inventory Slot.
		/// </summary>
        public void GroundToSlot(IItem item, InventoryID destiny, Location sqm, uint stackPosition) {
			#region " Packet Structure Analyze "
			// SZ    ID   X     Y   ZZ  ITM  ??       ST       QT
			//---------------------------------------------------
			// 0F 00 78 14 7E ED 7B 07 CD 0C 01 FF FF 06 00 00 04
			//---------------------------------------------------
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16
			#endregion

			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(item.Id);
			Builder.Append(stackPosition);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(destiny.GetHashCode());
			Builder.Append(0x00);
			Builder.Append(0x00);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Send the ammount of stacked Item to defined Container Slot.
		/// </summary>
        public void GroundToContainer(IItem item, ISlot destiny, Location sqm, uint stackPosition) {
			var Builder = new PacketBuilder(0x78, Connection);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(item.Id);
			Builder.Append(stackPosition);
			Builder.Append(0xFF);
			Builder.Append(0xFF);
			Builder.Append(destiny.Container.Position);
			Builder.Append(0x00);
			Builder.Append(destiny.Position);
			Builder.Append(item.Count);
			Connection.Send(Builder.GetPacket());
		}
	}
}
