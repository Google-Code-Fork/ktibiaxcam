using Tibia.Memory;

namespace Tibia.Features.Model {
    public class DatReader {

        /// <summary>
        /// Initializes a new instance of the <see cref="DatReader"/> class.
        /// </summary>
        /// <param name="memory">The memory.</param>
        public DatReader(TibiaMemoryProvider memory) {
            Memory = memory;
            BaseAddress = Memory.Reader.Uint(Memory.Addresses.Client.DatPointer);
            ItemInfoAddress = Memory.Reader.Uint(BaseAddress + 8);
        }

        #region "[rgn] Public Properties "
        public TibiaMemoryProvider Memory { get; set; }
        public uint BaseAddress { get; set; }
        public uint ItemInfoAddress { get; set; }
        #endregion

        /// <summary>
        /// Items the count.
        /// </summary>
        /// <returns></returns>
        public uint ItemCount() {
            return Memory.Reader.Uint(BaseAddress + 4);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        public DatItem GetItem(uint itemId) {
            return new DatItem(Memory, ItemInfoAddress + 0x4C * (itemId - 100), itemId);
        }
    }
}
