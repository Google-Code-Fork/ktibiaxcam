using Tibia.Connection.Providers;
using Tibia.Features.Model.Items;
using Tibia.Memory;

namespace Tibia.Features.Model {
    public class Inventory {
        /// <summary>
        /// Initializes a new instance of the <see cref="Slots"/> class.
        /// </summary>
        /// <param name="connection">The own connection.</param>
        public Inventory(ConnectionProvider connection) {
            Connetion = connection;
        }

        /// <summary>
        /// Gets the memory.
        /// </summary>
        /// <value>The memory.</value>
        private TibiaMemoryProvider Memory { get { return Connetion.Memory; } }

        /// <summary>
        /// Gets or sets the connetion.
        /// </summary>
        /// <value>The connetion.</value>
        private ConnectionProvider Connetion { get; set; }

        /// <summary>
        /// Gets the head inventory slot.
        /// </summary>
        /// <value>The head.</value>
        public Slot Head {
            get { return new Slot(Memory.Addresses.Inventory.Head, Connetion, InventoryID.Head); }
        }
        /// <summary>
        /// Gets the neck inventory slot.
        /// </summary>
        /// <value>The neck inventory slot.</value>
        public Slot Neck {
            get { return new Slot(Memory.Addresses.Inventory.Necklace, Connetion, InventoryID.Neck); }
        }
        /// <summary>
        /// Gets the backpack inventory slot.
        /// </summary>
        /// <value>The backpack inventory slot.</value>
        public Slot Backpack {
            get { return new Slot(Memory.Addresses.Inventory.Backpack, Connetion, InventoryID.Backpack); }
        }
        /// <summary>
        /// Gets the armor inventory slot.
        /// </summary>
        /// <value>The armor inventory slot.</value>
        public Slot Armor {
            get { return new Slot(Memory.Addresses.Inventory.Armor, Connetion, InventoryID.Armor); }
        }
        /// <summary>
        /// Gets the right inventory slot.
        /// </summary>
        /// <value>The right inventory slot.</value>
        public Slot Right {
            get { return new Slot(Memory.Addresses.Inventory.Right, Connetion, InventoryID.Right); }
        }
        /// <summary>
        /// Gets the left inventory slot.
        /// </summary>
        /// <value>The left inventory slot.</value>
        public Slot Left {
            get { return new Slot(Memory.Addresses.Inventory.Left, Connetion, InventoryID.Left); }
        }
        /// <summary>
        /// Gets the legs inventory slot.
        /// </summary>
        /// <value>The legs inventory slot.</value>
        public Slot Legs {
            get { return new Slot(Memory.Addresses.Inventory.Legs, Connetion, InventoryID.Legs); }
        }
        /// <summary>
        /// Gets the feet inventory slot.
        /// </summary>
        /// <value>The feet inventory slot.</value>
        public Slot Feet {
            get { return new Slot(Memory.Addresses.Inventory.Feet, Connetion, InventoryID.Feet); }
        }
        /// <summary>
        /// Gets the ring inventory slot.
        /// </summary>
        /// <value>The ring inventory slot.</value>
        public Slot Ring {
            get { return new Slot(Memory.Addresses.Inventory.Ring, Connetion, InventoryID.Ring); }
        }
        /// <summary>
        /// Gets the ammo inventory slot.
        /// </summary>
        /// <value>The ammo inventory slot.</value>
        public Slot Ammo {
            get { return new Slot(Memory.Addresses.Inventory.Ammo, Connetion, InventoryID.Ring); }
        }

    }
}
