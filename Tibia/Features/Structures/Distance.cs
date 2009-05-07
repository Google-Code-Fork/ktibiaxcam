using System.ComponentModel;
using Keyrox.Shared.Objects;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Player SQM's Distance Structure.
    /// </summary>
    public struct Distance {
        /// <summary>
        /// Initializes a new instance of the <see cref="Distance"/> struct.
        /// </summary>
        /// <param name="X">The X.</param>
        /// <param name="Y">The Y.</param>
        /// <param name="XY">The XY.</param>
        public Distance(uint X, uint Y, uint XY) {
            this.X = X;
            this.Y = Y;
            this.XY = XY;
        }

        #region "[rgn] Public Properties   "
        public uint X, Y, XY;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return XY.ToString();
        }
    }
}
