using System.Collections.Generic;
using System.Linq;
using Keyrox.Shared.Objects;
using Tibia.Connection;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;

namespace Tibia.Features.Model.Items {
    /// <summary>
    /// Collection of Container Slots.
    /// </summary>
    public class ContainerCollection : List<IContainer> {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerCollection"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ContainerCollection(ConnectionProvider connection) {
            Connection = connection;
            Initialize();
        }

        /// <summary>
        /// Gets the <see cref="Tibia.Features.Model.Items.Container"/> with the specified position.
        /// </summary>
        /// <value></value>
        public IContainer this[uint position] {
            get{
                var bps = this.Where(bp => bp.Position == position);
                return bps.Count() > 0 ? bps.First() : null;
            }
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public ConnectionProvider Connection { get; set; }

        /// <summary>
        /// Load All Opened Containers.
        /// </summary>
        public void Initialize() {
            //Start and End pointers to Get the Container List.
            uint BPStarT = Connection.Memory.Addresses.Containers.StarT;
            uint BPEndeD = Connection.Memory.Addresses.Containers.EndeD;
            uint BPBDisT = Connection.Memory.Addresses.Containers.BPDisT;

            //Store the Containers Position on Containers List.
            int BPPosition = 0; int BPSkipped = 0;

            for (uint i = BPStarT; i < BPEndeD; i += BPBDisT) {
                //Verify if this Container is Opened.
                uint IsOpen; Connection.Memory.Reader.Uint(i, 1, out IsOpen);

                //If this Container is opened, Add to List.
                if (IsOpen == 0x01) {
                    Add(new Container(Connection, i, BPPosition));
                    BPPosition++;
                }
                // If Closed Increase the Skipped Containers.
                else { BPSkipped++; }

                //If got more than 20 closed BP's get out.
                if (BPSkipped >= 20) { return; }
            }
        }

        /// <summary>
        /// Search the specified item on opened containers.
        /// </summary>
        public ISlot SearchItem(uint itemID) {
            Initialize();
            foreach (IContainer container in this) {
                foreach (ISlot slot in container.Slots) {
                    if (slot.Item.Id == itemID) {
                        return slot;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get the Container on Last Position.
        /// </summary>
        public IContainer GetLastContainer() {
            Initialize();
            if (Count > 0) {
                return this[Count - 1];
            }
            return null;
        }

        /// <summary>
        /// Gets the opened containers with free slots.
        /// </summary>
        /// <returns></returns>
        public List<IContainer> GetContainersWithFreeSlots() {
            Initialize();
            if (Count > 0) {
                var result = new List<IContainer>();
                foreach (var bp in this) {
                    if (bp.Ammount < bp.Volume) {
                        result.Add(bp);
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Gets the containers with containers inside.
        /// </summary>
        /// <returns></returns>
        public List<IContainer> GetContainersWithContainersInside() {
            //var containers = Data.Context.Item.Service.LoadContainers();
            Initialize();
            if (Count > 0) {
                var result = new List<IContainer>();
                foreach (var bp in this) {
                    if (bp.Ammount > 0) {
                        foreach (var slot in bp.Slots) {
                            if (slot.Item.Id > 0) {
                                if (slot.Item.GetFlag(DatItemFlag.IsContainer)) {
                                    result.Add(bp);
                                    break;
                                }
                                //var bps = (from ibp in containers 
                                //           where ibp.idt_item.ToUInt32() == slot.Item.Id 
                                //           select ibp);
                                //if (bps.Count() > 0) { result.Add(bp); }
                            }
                        }
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Return Count {0}.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            Initialize();
            return string.Format("Count: {0}", Count);
        }
    }
}
