using System.ComponentModel;
using Keyrox.Shared.Objects;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Attack Mode.
    /// </summary>
    public struct AttackMode {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttackMode"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="follow">The follow.</param>
        public AttackMode(AttackType type, ChaseType follow) {
            this.Type = type;
            this.Follow = follow;
            AttackUnmarkedPlayers = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackMode"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="follow">The follow.</param>
        /// <param name="attackUnmarkedPlayers">if set to <c>true</c> [attack unmarked players].</param>
        public AttackMode(AttackType type, ChaseType follow, bool attackUnmarkedPlayers) {
            this.Type = type;
            this.Follow = follow;
            this.AttackUnmarkedPlayers = attackUnmarkedPlayers;
        }

        #region "[rgn] Public Properties   "
        public AttackType Type;
        public ChaseType Follow;
        public bool AttackUnmarkedPlayers;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return Type + " - " + Follow;
        }
    }
}
