using System;
using System.IO.Pipes;
using Tibia.Connection.Model;
using Tibia.Connection.Providers;
using Tibia.Connection.Model.Contracts;

namespace Tibia.Connection.Core {
    public class Pipe : IPipe {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pipe"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="name">The name.</param>
        public Pipe(ConnectionProvider connection, string name) {
            Connection = connection;
            Name = name;
            Buffer = new byte[1024];
            pipe = new NamedPipeServerStream(name, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipe.BeginWaitForConnection(new AsyncCallback(BeginWaitForConnection), null);
        }

        #region "[rgn] Object Properties "
        public ConnectionProvider Connection { get; set; }
        private NamedPipeServerStream pipe { get; set; }
        private byte[] Buffer { get; set; }
        public string Name { get; set; }
        public bool Connected { get { return pipe != null ? pipe.IsConnected : false; } }
        #endregion

        #region "[rgn] Object Events     "
        public delegate void PipeNotification();
        public delegate void PipeListener(Packet packet);
        public PipeNotification OnConnected;
        public PipeListener OnReceive;
        public PipeListener OnSend;
        #endregion
        
        /// <summary>
        /// Begins the wait for connection.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void BeginWaitForConnection(IAsyncResult ar) {
            pipe.EndWaitForConnection(ar);
            if (pipe.IsConnected) {
                if (OnConnected != null)
                    OnConnected.BeginInvoke(null, null);
                pipe.BeginRead(Buffer, 0, Buffer.Length, new AsyncCallback(BeginRead), null);
            }
        }

        /// <summary>
        /// Begins the read.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void BeginRead(IAsyncResult ar) {
            pipe.EndRead(ar);
            if (OnReceive != null)
                OnReceive.BeginInvoke(new Packet(Connection, Buffer), null, null);
            pipe.BeginRead(Buffer, 0, Buffer.Length, new AsyncCallback(BeginRead), null);
        }

        /// <summary>
        /// Sends packet to the destination.
        /// </summary>
        public void Send(Packet packet) {
            if (OnSend != null)
                OnSend.BeginInvoke(packet, null, null);
            pipe.Write(packet.Data, 0, packet.Data.Length);
        }
    }
}
