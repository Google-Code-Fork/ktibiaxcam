using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Structure of Point Values.
    /// </summary>
    [Serializable]
    public struct PointsMax {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointsMax"/> struct.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="max">The max.</param>
        public PointsMax(uint quantity, uint max) {
            this.Quantity = quantity;
            this.Max = max;
        }

        #region "[rgn] Public Properties   "
        [XmlElement]
        public uint Quantity;
        [XmlElement]
        public uint Max;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return this.Quantity.ToString();
        }
    }
}
