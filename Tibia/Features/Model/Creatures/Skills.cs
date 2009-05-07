using Tibia.Features.Structures;
using Tibia.Memory;

namespace Tibia.Features.Model {
    public class Skills {
        /// <summary>
        /// Initializes a new instance of the <see cref="Skills"/> class.
        /// </summary>
        /// <param name="memory">The memory.</param>
        public Skills(TibiaMemoryProvider memory) {
            Memory = memory;
        }

        /// <summary>
        /// Gets or sets the memory.
        /// </summary>
        /// <value>The memory.</value>
        public TibiaMemoryProvider Memory { get; set; }

        /// <summary>
        /// Gets or sets the fist fighting skill.
        /// </summary>
        /// <value>The fist.</value>
        public PointsLeft Fist {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Fist, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Fist_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Fist, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Fist_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the club fighting skill.
        /// </summary>
        /// <value>The club.</value>
        public PointsLeft Club {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Club, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Club_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Club, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Club_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the sword fighting skill.
        /// </summary>
        /// <value>The sword.</value>
        public PointsLeft Sword {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Sword, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Sword_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Sword, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Sword_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the axe fighting skill.
        /// </summary>
        /// <value>The axe.</value>
        public PointsLeft Axe {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Axe, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Axe_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Axe, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Axe_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the distance fighting skill.
        /// </summary>
        /// <value>The distance.</value>
        public PointsLeft Distance {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Distance, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Distance_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Distance, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Distance_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the shielding skill.
        /// </summary>
        /// <value>The shielding.</value>
        public PointsLeft Shielding {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Shielding, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Shielding_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Shielding, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Shielding_Perc, value.Left, 1);
            }
        }
        /// <summary>
        /// Gets or sets the fishing skill.
        /// </summary>
        /// <value>The fishing.</value>
        public PointsLeft Fishing {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Skills.Fishing, 1, out Pts);
                uint Lft; Memory.Reader.Uint(Memory.Addresses.Skills.Fishing_Perc, 1, out Lft);
                return new PointsLeft(Pts, Lft);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Skills.Fishing, value.Value, 1);
                Memory.Writer.Uint(Memory.Addresses.Skills.Fishing_Perc, value.Left, 1);
            }
        }

    }
}
