using System;
using Keyrox.Shared.Objects;
using Tibia.Connection;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;
using Tibia.Features.Model.Items;
using Tibia.Features.Structures;

namespace Tibia.Features.Model {
    public class Item : BaseItem, IItem {

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() { }

        #region "[rgn] Constructors      "
        public Item(ConnectionProvider connectionSource) {
            connection = connectionSource;
        }
        public Item(uint id) {
            base.Id = id;
        }
        public Item(uint id, ConnectionProvider connectionSource) {
            base.Id = id;
            Connection = connectionSource;
        }
        public Item(uint id, uint count) {
            base.Id = id;
            Count = count;
        }
        public Item(uint id, uint count, ConnectionProvider connection) {
            base.Id = id;
            Count = count;
            Connection = connection;
        }
        public Item(Slot slot) {
            Slot = slot;
            connection = slot.Connection;
            if (slot.Item.Id > 0) {
                base.Id = slot.Item.Id;
            }
        }
        #endregion

        #region "[rgn] Private Variables "
        private Actions.Items.Use useActions;
        protected ConnectionProvider connection;
        #endregion

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection {
            get { return connection; }
            set { connection = value; }
        }
        protected Actions.Items.Use UseActions {
            get {
                if (connection == null) { 
                    throw new Exception("This Item dont have a Connection Provider!"); 
                }
                else if (connection.State != ConnectionState.Connected) { 
                    throw new Exception("There is no available connetion to perform this Action!"); 
                }
                else if (useActions == null) {
                    useActions = new Actions.Items.Use(Connection);
                }
                return useActions;
            }
        }
        #endregion

        #region "[rgn] Use Item Actions  "
        /// <summary>
        /// Use the Item stacked on defined Slot.
        /// </summary>
        public void Use() {
            UseActions.InContainer(Slot, 0);
        }
        /// <summary>
        /// Open a Container Item stacked on defined Slot.
        /// </summary>
        public void Use(bool openInNewWindow) {
            if (openInNewWindow) { UseActions.InContainer(Slot, new Player(Connection).Containers.Count + 1); }
            else { Use(); }
        }
        /// <summary>
        /// Use the Defined Item to the Ground.
        /// e.g: Rope on Ground, Obsidian Knife on Creature, Shovel on Role, etc.
        /// </summary>
        public void UseOnGround(Location sqm, uint tileID, uint stackPosition) {
            UseActions.OnGround(Slot, sqm, tileID, stackPosition);
        }
        /// <summary>
        /// Use the Defined Item on a Player in Defined SQM and Drop after use if needed.
        /// e.g: Use Fluids on Creature, Use Runes on Creature, etc.
        /// </summary>
        public void UseOnCreature(Creature creature, bool dropAfterUse) {
            UseActions.OnPlayer(Slot, creature.Location, dropAfterUse);
        }
        /// <summary>
        /// Use the Defined Item on a Player in Defined SQM and Drop after use if needed.
        /// e.g: Use Fluids on Creature, Use Runes on Creature, etc.
        /// </summary>
        public void UseOnCreature(Creature creature) {
            UseActions.OnPlayer(Slot, creature.Location, false);
        }
        /// <summary>
        /// Use the Defined Item on a Player in Defined SQM and Drop after use if needed.
        /// e.g: Use Fluids, Use Runes, etc.
        /// </summary>
        public void UseOnSelf(bool dropAfterUse) {
            UseActions.OnPlayer(Slot, new Player(Slot.Connection).Location, dropAfterUse);
        }
        /// <summary>
        /// Use the Defined Item on a Player in Defined SQM and Drop after use if needed.
        /// e.g: Use Fluids, Use Runes, etc.
        /// </summary>
        public void UseOnSelf() {
            UseActions.OnPlayer(Slot, new Player(Slot.Connection).Location, false);
        }
        #endregion
        
        /// <summary>
        /// Sets the owner.
        /// </summary>
        /// <param name="itemOwnerSlot">The item owner slot.</param>
        public void SetOwner(Slot itemOwnerSlot) {
            if (itemOwnerSlot != null && itemOwnerSlot.Connection != null) {
                Slot = itemOwnerSlot;
                connection = itemOwnerSlot.Connection;
            }
        }

        /// <summary>
        /// Return Item {Id}.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            if (!string.IsNullOrEmpty(this.Name)) {
                return this.Name  + " {" + Id + ")";
            }
            return "Item {" + Id + ")";
        }
    }
}
