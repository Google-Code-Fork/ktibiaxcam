using System;
using System.Collections.Generic;
using System.Linq;
using Keyrox.Shared.Objects;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;

namespace Tibia.Features.Model.Items {
    public class Container : Item, IContainer {

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="connectionSource">The connection source.</param>
        /// <param name="address">The address.</param>
        /// <param name="position">The position.</param>
        public Container(ConnectionProvider connectionSource, uint address, int position)
            : base(connectionSource) {
            Address = address;
            Index = position;
            SetMemory(connectionSource.Memory);
        }

        #region "[rgn] Public Properties "
        public uint Address { get; private set; }
        public bool IsOpen {
            get { uint Value; Memory.Reader.Uint(Address + Memory.Addresses.Containers.BPIsOpenDist, 1, out Value); return Convert.ToBoolean(Value); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.Containers.BPIsOpenDist, Convert.ToUInt32(value), 1); }
        }
        public override uint Id {
            get { uint Value; Memory.Reader.Uint(Address + Memory.Addresses.Containers.BPIdDist + Address, 2, out Value); return Value; }
            set { Memory.Writer.Uint(Address + Memory.Addresses.Containers.BPIdDist + Address, value, 2); }
        }
        public override string Name {
            get { return Memory.Reader.String(Memory.Addresses.Containers.BPNameDist + Address); }
            set { Memory.Writer.String(Memory.Addresses.Containers.BPNameDist + Address, value, true); }
        }
        public override uint Volume {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Containers.BPVolumeDist + Address, 1, out Value); return Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Containers.BPVolumeDist + Address, value, 1); }
        }
        public uint Ammount {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Containers.BPAmountDist + Address, 1, out Value); return Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Containers.BPAmountDist + Address, value, 1); }
        }
        public int Index { get; set; }
        public uint Position {
            get { return Convert.ToUInt32(Index + 0x40); }
        }
        public SlotCollection Slots {
            get { return new SlotCollection(this); }
        }
        #endregion

        #region "[rgn] Public Actions    "
        /// <summary>
        /// Close this Container.
        /// </summary>
        public void Close() {
            UseActions.CloseContainer(this);
        }
        #endregion

        /// <summary>
        /// Gets the slots with container inside.
        /// </summary>
        /// <returns></returns>
        public List<ISlot> GetSlotsWithContainer() {
            //var containers = Data.Context.Item.Service.LoadContainers();
            var result = new List<ISlot>();
            if (Ammount > 0) {
                foreach (var slot in Slots) {
                    if (slot.Item.Id > 0) {
                        if (slot.Item.GetFlag(DatItemFlag.IsContainer)) {
                            result.Add(slot);
                            break;
                        }
                        //var islot = slot;
                        //var bps =
                        //    (from ibp in containers where ibp.idt_item.ToUInt32() == islot.Item.Id select ibp);
                        //if (bps.Count() > 0) { result.Add(slot); }
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Return Name {Volume}.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Name + "{" + Volume + "}";
        }

    }
}
