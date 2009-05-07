using Tibia.Connection;
using Tibia.Features.Model;
using Tibia.Connection.Providers;

namespace Tibia.Features.Providers {
    /// <summary>
    /// Message Actions Provider.
    /// </summary>
    public class MessageProvider {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProvider"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public MessageProvider(ConnectionProvider connection) {
            Connection = connection;
            Server = new Actions.Messages.Server(connection);
            System = new Actions.Messages.System(connection);
            Screen = new Actions.Messages.Screen(connection);
        }

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public Actions.Messages.Server Server { get; set; }
        public Actions.Messages.System System { get; set; }
        public Actions.Messages.Screen Screen { get; set; }
        #endregion

        /// <summary>
        /// Return the Connection Source State.
        /// </summary>
        public override string ToString() {
            return Connection.State.ToString();
        }
    }
}
