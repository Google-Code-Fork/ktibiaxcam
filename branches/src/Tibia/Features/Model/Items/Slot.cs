using System;
using Tibia.Features.Model.Contracts;
using Tibia.Connection;
using Tibia.Memory;
using Tibia.Features.Structures;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.Items {
	public class Slot : ISlot {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Slot"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="ownerContainer">The owner container.</param>
        /// <param name="position">The position.</param>
		public Slot(uint address, IContainer ownerContainer, int position) {
			this.address = address;
			this.container = ownerContainer;
			this.position = position;
			id = InventoryID.Container;
			memory = Container.Memory;
			connection = Container.Connection;
		}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Slot"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="id">The id.</param>
		public Slot(uint address, ConnectionProvider connection, InventoryID id) {
			this.address = address;
			this.id = id;
            this.memory = connection.Memory;
			this.connection = connection;
		}

		#region "[rgn] Private Variables "
		private ConnectionProvider connection;
		private TibiaMemoryProvider memory;
		private int position;
        private IItem item;
		private InventoryID id;
		private uint address;
		private IContainer container;
		private Actions.Items.Stack stackActions;
		#endregion

		#region "[rgn] Public Properties "
		public ConnectionProvider Connection {
			get { return connection; }
		}
		public TibiaMemoryProvider Memory {
			get { return memory; }
		}
		public uint Address {
			get { return address; }
		}
		public InventoryID Id {
			get { return id; }
			set { id = value; }
		}
		public IContainer Container {
			get { return container; }
		}
		public int Position {
			get { return position; }
		}
        public IItem Item {
			get {
				if (item == null) { item = new Item(); }
				if (item.Slot == null) { item.SetOwner(this); }
				return item;
			}
			set {
				item = value;
				if (value != null) { item.SetOwner(this); }
			}
		}
		protected Actions.Items.Stack StackActions {
			get {
				if (connection == null) { throw new Exception("This Slot dont have a Connection Provider!"); }
				if (connection.State != ConnectionState.Connected) { throw new Exception("There is no available connetion to perform this Action!"); }

				if (stackActions == null) {
					stackActions = new Actions.Items.Stack(Connection);
				}
				return stackActions;
			}
		}
		#endregion

		#region "[rgn] Public Actions    "
		/// <summary>
		/// Drop the stacked item to defined location.
		/// </summary>
		public void Drop(Location sqm) {
			if (Id == InventoryID.Container) {
				StackActions.ContainerToGround(this, sqm);
			}
			else {
				StackActions.SlotToGround(Item, sqm);
			}
		}
		/// <summary>
		/// Send the ammount of stacked Item to defined Container Slot.
		/// </summary>
		public void Stack(ISlot toSlot) {
			if (Id == InventoryID.Container && toSlot.Id == InventoryID.Container) {
				StackActions.ContainerToContainer(this, toSlot);
			}
			else if (Id != InventoryID.Container && toSlot.Id == InventoryID.Container) {
				StackActions.SlotToContainer(Item, toSlot);
			}
			else if (Id == InventoryID.Container && toSlot.Id != InventoryID.Container) {
				StackActions.ContainerToSlot(this, toSlot.Id);
			}
			else if (Id != InventoryID.Container && toSlot.Id != InventoryID.Container) {
				StackActions.SlotToSlot(Item, toSlot.Id);
			}
		}
		#endregion

		/// <summary>
		/// Return Slot {ID:0}.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return "Slot {" + Id + ":" + Position + "}";
		}
	}
}
