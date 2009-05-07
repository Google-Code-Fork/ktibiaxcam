using Tibia.Connection;
using Tibia.Memory;
using System.Collections.Generic;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model {
    public class VipUser {
        /// <summary>
        /// Initializes a new instance of the <see cref="VipUser"/> class.
        /// </summary>
        public VipUser() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VipUser"/> class.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <param name="address">The address.</param>
        public VipUser(TibiaMemoryProvider memory, uint address) {
            this.Memory = memory;
            this.Address = address;
        }

        #region "[rgn] Private Variables   "
        private uint id;
        private string name;
        private uint icon;
        private bool isCustom;
        private bool isOnline;
        #endregion

        /// <summary>
        /// Gets or sets the memory.
        /// </summary>
        /// <value>The memory.</value>
        public TibiaMemoryProvider Memory { get; private set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public uint Address { get; private set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public uint Id {
            get {
                if (Address > 0) {
                    uint valor; Memory.Reader.Uint(Address, 4, out valor); return valor;
                }
                else { return id; }
            }
            set {
                if (Address > 0) {
                    Memory.Writer.Uint(Address, value, 4);
                }
                else { id = value; }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get {
                if (Address > 0) { return Memory.Reader.String(Address + Memory.Addresses.VipList.VipName); }
                else { return name; }
            }
            set {
                if (Address > 0) { Memory.Writer.String(Address + Memory.Addresses.VipList.VipName, value); }
                else { name = value; }
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public uint Icon {
            get {
                if (Address > 0) { uint valor; Memory.Reader.Uint(Address, 2, out valor); return valor; }
                else { return icon; }
            }
            set {
                if (Address > 0) { Memory.Writer.Uint(Address, value, 2); }
                else { icon = value; }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is custom.
        /// </summary>
        /// <value><c>true</c> if this instance is custom; otherwise, <c>false</c>.</value>
        public bool IsCustom {
            get {
                if (Address > 0) {
                    uint valor;
                    Memory.Reader.Uint(Memory.Addresses.VipList.VipColor, 1, out valor);
                    return valor > 0 ? true : false;
                }
                else { return isCustom; }
            }
            set {
                if (Address > 0) {
                    uint valor = value ? 0x01 : (uint)0x0;
                    Memory.Writer.Uint(Memory.Addresses.VipList.VipColor, valor, 1);
                }
                else { isCustom = value; }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value><c>true</c> if this instance is online; otherwise, <c>false</c>.</value>
        public bool IsOnline {
            get {
                if (Address > 0) {
                    uint online;
                    Memory.Reader.Uint(Address + Memory.Addresses.VipList.VipStat, 1, out online);
                    return online == 1 ? true : false;
                }
                else { return isOnline; }
            }
            set {
                if (Address > 0) {
                    uint online = value ? 0x1 : (uint)0x0;
                    Memory.Writer.Uint(Address, online, 1);
                }
                else { isOnline = value; }
            }
        }
    }
}
