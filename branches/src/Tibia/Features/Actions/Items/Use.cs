using Tibia.Features.Model.Items;
using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;

namespace Tibia.Features.Actions.Items {
	public class Use {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		/// <param name="connection">Connection Source.</param>
		public Use(ConnectionProvider connection) {
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
		/// Use the Item stacked on defined Slot.
		/// e.g: Open Containers, Eat Food, etc.
		/// </summary>
        public void InContainer(ISlot slotSource, int openInPosition) {

			PacketBuilder Builder = new PacketBuilder(0x82, connection);
			if (slotSource.Id == InventoryID.Container) {
                
                #region " Packet Structure Analyze "
                //------------------------------------
                //SZ    ID       BP    ST [ ID] ST PS 
                //------------------------------------
                //0A 00 82 FF FF 40 00 00 26 0B 00 01 
                //------------------------------------
                #endregion

				Builder.Append(0xFF);
				Builder.Append(0XFF);
				Builder.Append(slotSource.Container.Position);
				Builder.Append(0x00);
				Builder.Append(slotSource.Position);
				Builder.Append(slotSource.Item.Id);
				Builder.Append(slotSource.Position);
			}
			else {

                #region " Packet Structure Analyze "
                //------------------------------------
                //SZ    ID       ST      [ ID ]    PS 
                //------------------------------------
                //0A 00 82 FF FF 03 00 00 25 0B 00 00
                //------------------------------------
                #endregion

                Builder.Append(0xFF);
                Builder.Append(0XFF);
                Builder.Append(slotSource.Id.GetHashCode());
                Builder.Append(0x00);
                Builder.Append(0x00);
                Builder.Append(slotSource.Item.Id);
                Builder.Append(0x00);                
			}
			Builder.Append(openInPosition);
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Use the Defined Item in Ground.
		/// e.g: Eat Food in Ground, Up Stairs, Open Container.
		/// </summary>
		public void InGround(Location sqm, uint itemId, uint stackPosition, bool isContainer) {
			#region " Packet Structure Analyze "
			//------------------------------------
			//Bag                                 
			//------------------------------------
			//0A 00 82 15 7E ED 7B 07 25 0B 01 01 
			//------------------------------------
			//00 01 02 03 04 05 06 07 08 09 10 11 
			//------------------------------------
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x82, connection);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(itemId);
			Builder.Append(stackPosition);

			if (isContainer) { Builder.Append(new Model.Player(Connection).Containers.Count); }
			else { Builder.Append(0x00); }
			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Use the Defined Item to the Ground.
		/// e.g: Rope on Ground, Obsidian Knife on Creature, Shovel on Role, etc.
		/// </summary>
		public void OnGround(ISlot slotSource, Location sqm, uint tileID, uint stackPosition) {
			#region " Packet Structure Analyze "
			//---------------------------------------------------------
			//USE ROPE                                                 
			//---------------------------------------------------------
			//00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 
			//---------------------------------------------------------
			//11 00 83 FF FF 40 00 0D BB 0B 0D 64 7F C3 7B 0A 82 01 00 
			//SZ    ID       BP    ST ROPE  ST [ X ] [ Y ] ZZ [TID] ?? 
			//---------------------------------------------------------
			#endregion

			PacketBuilder Builder = new PacketBuilder(0x83, connection);
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

			Builder.Append(tileID);
			Builder.Append(stackPosition);

			Connection.Send(Builder.GetPacket());
		}

		/// <summary>
		/// Use the Defined Item on a Player in Defined SQM and Drop after use if needed.
		/// e.g: Use Fluids on Players, Use Runes on Players, etc.
		/// </summary>
        public void OnPlayer(ISlot slotSource, Location sqm, bool dropAfterUse) {
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
			Builder.Append(slotSource.Container.Position);
			Builder.Append(0x00);
			Builder.Append(slotSource.Position);
			Builder.Append(slotSource.Item.Id);
			Builder.Append(slotSource.Position);
			Builder.Append(sqm.X);
			Builder.Append(sqm.Y);
			Builder.Append(sqm.Z);
			Builder.Append(0x63);
			Builder.Append(0x00);
			Builder.Append(0x01); //TODO: Test increase this ammount.
			Connection.Send(Builder.GetPacket());

			if (dropAfterUse) {
				System.Threading.Thread.Sleep(300);
				new Stack(Connection).ContainerToGround(slotSource, sqm);
			}
		}

        /// <summary>
        /// Use defined item on player target as HotKey.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        public void OnTarget(uint itemId){
            //TODO: Disparar a runa com hotkey.
        }

        /// <summary>
        /// Use defined item on player himself as HotKey.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        public void OnSelf(uint itemId) {
            //TODO: Disparar a runa com hotkey.
        }

		/// <summary>
		/// Close the Defined Container.
		/// </summary>
		public void CloseContainer(IContainer containerSource) {
			//TODO: Need Test this Feature. Probably is a Client Packet.
			PacketBuilder Builder = new PacketBuilder(0x6F, connection);
			Builder.Append(containerSource.Index);
			Connection.Send(Builder.GetPacket());
		}
	}
}
