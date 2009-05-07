using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Range to Fill Battle List.
    /// </summary>
    public struct Range {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public Range(RangeType type, int value) {
            this.Type = type;
            this.Value = value;
        }

        #region "[rgn] Public Properties   "
        public RangeType Type;
        public int Value;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return this.Type.ToString() + " : " + this.Value.ToString();
        }
    }
}
