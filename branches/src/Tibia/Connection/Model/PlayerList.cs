using System;
namespace Tibia.Connection.Model {
    /// <summary>
    /// Structure of Character Login List.
    /// </summary>
    public class PlayerList {

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerList"/> class.
        /// </summary>
        public PlayerList() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerList"/> class.
        /// </summary>
        /// <param name="charName">Name of the char.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="serverIP">The server IP.</param>
        /// <param name="serverPort">The server port.</param>
        public PlayerList(string charName, string serverName, byte[] serverIP, int serverPort) {
            CharName = charName;
            ServerName = serverName;
            ServerIP = serverIP;
            ServerPort = serverPort;
        }

        #region "[rgn] Public Properties "
        public string CharName { get; set; }
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public byte[] ServerIP { get; set; }
        #endregion

        /// <summary>
        /// IPs to string.
        /// </summary>
        /// <returns></returns>
        public string IPToString() {
            return String.Format("{0}.{1}.{2}.{3}", ServerIP[0], ServerIP[1], ServerIP[2], ServerIP[3]);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return ServerName;
        }
    }
}
