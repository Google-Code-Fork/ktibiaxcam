using Tibia.Connection.Providers;
using Tibia.Features.Model;
using Tibia.Features.Model.List;
using Tibia.Features.Structures;

namespace Tibia.Features {
    /// <summary>
    /// Feature Actions Provider.
    /// </summary>
    public class FeatureProvider {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureProvider"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public FeatureProvider(ConnectionProvider connection) {
            Connection = connection;
        }

        #region "[rgn] Public Properties "
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public ConnectionProvider Connection { get; private set; }

        /// <summary>
        /// The Main Player of Current Connection.
        /// </summary>
        public Player Player { get { return new Player(Connection); } }

        /// <summary>
        /// Get the Vip List Manager.
        /// </summary>
        public VipList VipList { get { return new VipList(Connection); } }

        /// <summary>
        /// Get the Battle List Manager to Players and Creatures.
        /// </summary>
        public BattleList BattleList { get { return new BattleList(Connection, new Range(RangeType.Screen, 1)); } }
        #endregion

        /// <summary>
        /// Return the Connection Source State.
        /// </summary>
        public override string ToString() {
            return Connection.State.ToString();
        }
    }
}
