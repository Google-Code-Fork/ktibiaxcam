using System;
using Tibia.Memory;

namespace Tibia.Features.Model {
    public class DatItem {

        /// <summary>
        /// Initializes a new instance of the <see cref="DatItem"/> class.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <param name="address">The address.</param>
        /// <param name="itemId">The item id.</param>
        public DatItem(TibiaMemoryProvider memory, uint address, uint itemId) {
            Memory = memory;
            Address = address;
            Id = itemId;
        }

        /// <summary>
        /// Gets or sets the memory.
        /// </summary>
        /// <value>The memory.</value>
        private TibiaMemoryProvider Memory { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public uint Address { get; set; }

        #region "[rgn] Public Object Properties   "
        public uint Width {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Width); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Width, value); }
        }
        public uint Height {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Height); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Height, value); }
        }
        public uint Unknown1 {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Unknown1); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Unknown1, value); }
        }
        public uint Layers {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Layers); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Layers, value); }
        }
        public uint PatternX {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.PatternX); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.PatternX, value); }
        }
        public uint PatternY {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.PatternY); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.PatternY, value); }
        }
        public uint PatternDepth {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.PatternDepth); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.PatternDepth, value); }
        }
        public uint Phase {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Phase); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Phase, value); }
        }
        public uint Sprites {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Sprites); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Sprites, value); }
        }
        public uint Flags {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Flags); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Flags, value); }
        }
        public uint WalkSpeed {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.WalkSpeed); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.WalkSpeed, value); }
        }
        public uint TextLimit {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.TextLimit); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.TextLimit, value); }
        }
        public uint LightRadius {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.LightRadius); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.LightRadius, value); }
        }
        public uint LightColor {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.LightColor); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.LightColor, value); }
        }
        public uint ShiftX {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.ShiftX); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.ShiftX, value); }
        }
        public uint ShiftY {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.ShiftY); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.ShiftY, value); }
        }
        public uint WalkHeight {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.WalkHeight); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.WalkHeight, value); }
        }
        public uint Automap {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.DatItem.Automap); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.Automap, value); }
        }
        public DatItemDefination LensHelp {
            get { return (DatItemDefination)Memory.Reader.Uint(Address + Memory.Addresses.DatItem.LensHelp); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.DatItem.LensHelp, (uint)value); }
        }
        #endregion

        /// <summary>
        /// Gets the flag.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns></returns>
        public bool GetFlag(DatItemFlag flag) {
            return (Flags & (int)flag) == (int)flag;
        }

        /// <summary>
        /// Sets the flag.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <param name="on">if set to <c>true</c> [on].</param>
        public void SetFlag(DatItemFlag flag, bool on) {
            if (on)
                Flags |= (uint)flag;
            else
                Flags &= ~(uint)flag;
        }

        /// <summary>
        /// Determines whether [has extra byte].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [has extra byte]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasExtraByte() {
            return GetFlag(DatItemFlag.IsStackable) ||
                   GetFlag(DatItemFlag.IsRune) ||
                   GetFlag(DatItemFlag.IsSplash) ||
                   GetFlag(DatItemFlag.IsFluidContainer);
        }

    }
}
