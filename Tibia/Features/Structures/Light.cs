using System.ComponentModel;
using Keyrox.Shared.Objects;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Creature Light Structure.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Light {
        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        public Light() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        /// <param name="intensity">The intensity.</param>
        /// <param name="color">The color.</param>
        public Light(uint intensity, uint color) {
            this.Intensity = intensity;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the intensity.
        /// </summary>
        /// <value>The intensity.</value>
        public uint Intensity { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public uint Color { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return " I {" + Intensity + "} C {" + Color + "}";
        }
    }
}
