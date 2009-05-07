using Tibia.Features.Model.Contracts;
using Tibia.Features.Model.Items;
using Tibia.Memory;

namespace Tibia.Features.Model {
    public class BaseItem : IBaseItem {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseItem"/> class.
        /// </summary>
        public BaseItem() { }

        #region "[rgn] Private Variables "
        private DatReader datReader;
        private DatItem datItem;
        private string name;
        private uint id;
        private uint count;
        private Slot slot;
        private TibiaMemoryProvider memory;
        #endregion

        #region "[rgn] Public Properties "
        public uint Weigth {
            get;
            set;
        }
        public virtual uint Volume {
            get;
            set;
        }
        public Slot Slot {
            get { return slot; }
            set {
                if (value != null && value.Memory != null) {
                    this.memory = value.Memory;
                }
                slot = value;
            }
        }
        public TibiaMemoryProvider Memory {
            get {
                if (memory == null && slot != null) {
                    memory = slot.Memory;
                }
                return memory;
            }
            set { memory = value; }
        }
        public virtual string Name {
            get {
                if (name != null) {
                    return name;
                }
                if (Slot != null) {
                    return "Slot - Item {" + Slot.Position + "}";
                }
                return "";
            }
            set { name = value; }
        }
        public virtual uint Id {
            get {
                //Avoid Shadow Items.
                if (Slot != null && Slot.Container != null) {
                    if (Slot.Position >= slot.Container.Ammount) return id;
                }
                //If this Item have a Owner Slot.
                if (Memory != null && Slot != null) {
                    //Get a Slot Item from Container.
                    if (Slot.Container != null) {
                        uint IDDist = Memory.Addresses.Containers.Item_IDDist + Slot.Address;
                        Memory.Reader.Uint(IDDist, 2, out id);
                    }
                    //Get a Slot Item from Player.
                    else {
                        Memory.Reader.Uint(Slot.Address, 2, out id);
                    }
                }
                return id;
            }
            set {
                //Avoid Shadow Items.
                if (Slot != null && Slot.Container != null) {
                    if (Slot.Position >= slot.Container.Ammount) { id = value; return; }
                }
                //If this Item have a Owner Slot.
                if (Memory != null && Slot != null) {
                    //Set a Slot Item to Container.
                    if (Slot.Container != null) {
                        uint IDDist = Memory.Addresses.Containers.Item_IDDist + Slot.Address;
                        Memory.Writer.Uint(IDDist, value, 2);
                    }
                    //Set a Slot Item to Player.
                    else {
                        Memory.Writer.Uint(Slot.Address, value, 2);
                    }
                }
                id = value;
            }
        }
        public uint Count {
            get {
                //Avoid Shadow Items.
                if (Slot != null && Slot.Container != null) {
                    if (Slot.Position >= slot.Container.Ammount) return count;
                }
                //If this Item have a Owner Slot.
                if (Memory != null && Slot != null) {
                    //Get a Slot Item from Container.
                    if (Slot.Container != null) {
                        uint CTDist = Memory.Addresses.Containers.Item_CountDist + Slot.Address;
                        Memory.Reader.Uint(CTDist, 1, out count);
                    }
                    //Get a Slot Item from Player.
                    else {
                        Memory.Reader.Uint(Slot.Address + 4, 1, out count);
                    }
                }
                return count;
            }
            set {
                //Avoid Shadow Items.
                if (Slot != null && Slot.Container != null) {
                    if (Slot.Position >= slot.Container.Ammount) { count = value; return; }
                }
                //If this Item have a Owner Slot.
                if (Memory != null && Slot != null) {
                    //Set a Slot Item to Container.
                    if (Slot.Container != null) {
                        uint CTDist = Memory.Addresses.Containers.Item_CountDist + Slot.Address;
                        Memory.Writer.Uint(CTDist, value, 1);
                    }
                    //Set a Slot Item to Player.
                    else {
                        Memory.Writer.Uint(Slot.Address + 4, value, 1);
                    }
                }
                count = value;
            }
        }
        #endregion

        #region "[rgn] Public Methods    "
        public bool GetFlag(DatItemFlag flag) {
            if (DatItem != null) {
                return DatItem.GetFlag(flag);
            }
            return false;
        }
        public void SetFlag(DatItemFlag flag, bool on) {
            if (DatItem != null) {
                DatItem.SetFlag(flag, on);
            }
        }
        public void SetMemory(TibiaMemoryProvider memory) {
            if (memory != null)
                this.memory = memory;
        }
        #endregion
                
        /// <summary>
        /// Gets or sets the dat reader.
        /// </summary>
        /// <value>The dat reader.</value>
        public DatReader DatReader {
            get {
                if (datReader == null && Memory != null) {
                    datReader = new DatReader(Memory);
                }
                return datReader;
            }
        }
        
        /// <summary>
        /// Gets the dat item.
        /// </summary>
        /// <value>The dat item.</value>
        public DatItem DatItem {
            get {
                if (datItem == null && DatReader != null) {
                    datItem = DatReader.GetItem(this.id);
                }
                return datItem;
            }
        }

    }
}
