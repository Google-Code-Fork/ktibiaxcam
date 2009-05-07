using Tibia.Connection;
using Tibia.Features.Actions.Items;
using Tibia.Features.Actions.Player;
using Tibia.Connection.Providers;
using Tibia.Features.Actions;

namespace Tibia.Features.Providers {
    /// <summary>
    /// Player Actions Provider
    /// </summary>
    public class ActionProvider {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionProvider"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ActionProvider(ConnectionProvider connection) {
            Connection = connection;
        }

        #region "[rgn] Public Properties "
        /// <summary>
        /// Gets or sets the connection source.
        /// </summary>
        /// <value>The connection source.</value>
        public ConnectionProvider Connection { get; private set; }

        /// <summary>
        /// Gets the hot key.
        /// </summary>
        /// <value>The hot key.</value>
        public Hotkey HotKey { get { return new Hotkey(Connection); } }

        /// <summary>
        /// Gets the stack actions.
        /// </summary>
        /// <value>The stack.</value>
        public Stack Stack { get { return new Stack(Connection); } }

        /// <summary>
        /// Gets the use actions.
        /// </summary>
        /// <value>The use.</value>
        public Use Use { get { return new Use(Connection); } }

        /// <summary>
        /// Gets the fish actions.
        /// </summary>
        /// <value>The fish.</value>
        public Fish Fish { get { return new Fish(Connection); } }

        /// <summary>
        /// Gets the message actions.
        /// </summary>
        /// <value>The message.</value>
        public MessageProvider Message { get { return new MessageProvider(Connection); } }

        /// <summary>
        /// Gets the attack.
        /// </summary>
        /// <value>The attack.</value>
        public Attack Attack { get { return new Attack(Connection); } }

        /// <summary>
        /// Gets the position actions.
        /// </summary>
        /// <value>The position.</value>
        public Position Position { get { return new Position(Connection); } }
        #endregion

        /// <summary>
        /// Return the Connection Source State.
        /// </summary>
        public override string ToString() {
            return Connection.State.ToString();
        }
    }
}
