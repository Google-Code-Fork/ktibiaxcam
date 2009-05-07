using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Features.Model.Contracts;

namespace Tibia.Features.Model.Items {
    /// <summary>
    /// Collection of Container Slots.
    /// </summary>
    public class SlotCollection : List<ISlot> {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SlotCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public SlotCollection(IContainer owner) { Initialize(owner); }

        /// <summary>
        /// Load all slots of defined container.
        /// </summary>
        public void Initialize(IContainer owner) {
            uint ItemDist = owner.Memory.Addresses.Containers.ItemDisT;
            uint LastSlot = owner.Address + (ItemDist * owner.Volume);

            int SlotPosition = 0;
            for (uint n = owner.Address; n < LastSlot; n += ItemDist) {
                Add(new Slot(n, owner, SlotPosition)); SlotPosition++;
            }
        }
        
        /// <summary>
        /// Searches the specified item ID.
        /// </summary>
        /// <param name="itemID">The item ID.</param>
        /// <returns></returns>
        public ISlot Search(uint itemID) {
            for (int i = 0; i < Count; i++) {
                if (this[i].Item.Id == itemID) { return this[i]; }
            }
            return null;
        }

        /// <summary>
        /// Searches all.
        /// </summary>
        /// <param name="itemID">The item ID.</param>
        /// <returns></returns>
        public IList<ISlot> SearchAll(uint itemID) {
            var res = new List<ISlot>();
            for (int i = 0; i < Count; i++) {
                if (this[i].Item.Id == itemID) { res.Add(this[i]); }
            }
            return res;
        }

        /// <summary>
        /// Get the first empty slot of this Collection.
        /// </summary>
        public ISlot NextEmptySlot() {
            for (int i = 0; i < Count; i++) {
                if (this[i].Item.Id == 0) { return this[i]; }
            }
            return null;
        }

        /// <summary>
        /// Get the next empty slot after defined Index.
        /// </summary>
        public ISlot NextEmptySlot(int index) {
            if (index >= Count) { throw new IndexOutOfRangeException("Slot Index out of Collection Range!"); }
            for (int i = index; i < Count; i++) {
                if (this[i].Item.Id == 0) { return this[i]; }
            }
            return null;
        }

        /// <summary>
        /// Verify if this Colleciton have any empty Slot.
        /// </summary>
        public bool HasEmptySlot() {
            for (int i = 0; i < Count; i++) {
                if (this[i].Item.Id == 0) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Gets the containers inside.
        /// </summary>
        /// <returns></returns>
        public List<ISlot> GetContainersInside() {
            var slots = new List<ISlot>();
            foreach (var slot in this) {
                if (slot.Item.Id > 0) {
                    if (slot.Item.GetFlag(DatItemFlag.IsContainer)) { slots.Add(slot); }
                }
            }
            return slots;
        }

        /// <summary>
        /// Return Count {0}.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format("Count: {0}", Count);
        }
    }
}
