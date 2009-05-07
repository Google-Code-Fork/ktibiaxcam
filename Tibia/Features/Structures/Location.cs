using System.ComponentModel;
using Keyrox.Shared.Objects;
using System;
using Tibia.Features.Events;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Creature Location.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Location {
        public Location() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="direction">The direction.</param>
        public Location(uint x, uint y, uint z, uint direction) {
            X = x;
            Y = y;
            Z = z;
            Direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Location(uint x, uint y, uint z) {
            X = x;
            Y = y;
            Z = z;
            Direction = 0x0;
        }

        #region "[rgn] Public Properties "
        private uint x, y, z, d;
        public uint X {
            get { return x; }
            set { x = value; if (LocationChanged != null) LocationChanged(this, new LocationEventArgs(this)); }
        }
        public uint Y {
            get { return y; }
            set { y = value; if (LocationChanged != null) LocationChanged(this, new LocationEventArgs(this)); }
        }
        public uint Z {
            get { return z; }
            set { z = value; if (LocationChanged != null) LocationChanged(this, new LocationEventArgs(this)); }
        }
        public uint Direction {
            get { return d; }
            set { d = value; if (LocationChanged != null) LocationChanged(this, new LocationEventArgs(this)); }
        }
        public event EventHandler<LocationEventArgs> LocationChanged;
        #endregion
         
        /// <summary>
        /// Distances the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static int Distance(Location from, Location to) {
            var ix = (from.X - to.X).ToPositive();
            var iy = (from.Y - to.Y).ToPositive();
            return ((ix + iy) / 2).ToInt32();
        }

        /// <summary>
        /// Distances the specified to.
        /// </summary>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public int Distance(Location to) {
            return Distance(this, to);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj) {
            if (obj != null && obj.GetType() == typeof(Location)) {
                if (((Location)obj).X == X &&
                    ((Location)obj).Y == Y &&
                    ((Location)obj).Z == Z) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty() {
            return X == 0 && Y == 0 && Z == 0;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return " X:" + X + " Y:" + Y + " Z:" + Z + " D:" + Direction;
        }
    }
}
