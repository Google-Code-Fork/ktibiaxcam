using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Available Flags.
    /// </summary>
    public struct Flags {
        /// <summary>
        /// Initializes a new instance of the <see cref="Flags"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        public Flags(FlagType type) {
            this.Type = type;
            this.ID = Convert.ToUInt32(this.Type.GetHashCode());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flags"/> struct.
        /// </summary>
        /// <param name="id">The ID.</param>
        public Flags(uint id) {
            this.ID = id;
            this.Type = FlagType.Merged;

            switch (this.ID) {
                case 000: this.Type = FlagType.None; break;
                case 001: this.Type = FlagType.Poison; break;
                case 002: this.Type = FlagType.Burn; break;
                case 004: this.Type = FlagType.Eletric; break;
                case 008: this.Type = FlagType.Drunk; break;
                case 016: this.Type = FlagType.MShield; break;
                case 032: this.Type = FlagType.Slowed; break;
                case 064: this.Type = FlagType.Haste; break;
                case 128: this.Type = FlagType.Battle; break;
                case 256: this.Type = FlagType.Drowing; break;
            }
        }

        #region "[rgn] Public Properties   "
        public FlagType Type; public uint ID;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return this.Type.ToString();
        }
    }
}
