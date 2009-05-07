using System;
using System.Net;
using Tibia.Connection.Common;
using Tibia.Memory;
using Tibia.Connection.CriptoProviders;
using Tibia.Connection.Packets;

namespace Tibia.Connection {
    public abstract class ConnectionProvider : IConnection, IDisposable {

        #region " Object Constructors   "
        public ConnectionProvider(MemoryProvider manager, int processID) {
            memory = manager;
            Initialize();
        }
        public ConnectionProvider(IPAddress loginServer, MemoryProvider manager, int processID, bool isOTServer)
            : this(manager, processID) {
            isOtServer = isOTServer;
            iPLoginServer = loginServer;
        }
        public ConnectionProvider(IPAddress loginServer, int basePort, MemoryProvider manager, int processID)
            : this(manager, processID) {
            iPLoginServer = loginServer;
            this.basePort = basePort;
        }
        #endregion

        /// <summary>
        /// Initialize All Events and Initial Values of Object.
        /// </summary>
        protected void Initialize() {
            state = ConnectionState.Disconnected;
            packetsToSave = PacketSource.Unknow;
            Xtea = new NativeCryptoManager(CryptoType.Xtea, this);
            RSA = new NativeCryptoManager(CryptoType.RSA, this);
            IPLocal = new IPAddress(new byte[] { 127, 0, 0, 1 });

            OnBeforeConnect += Connection_OnBeforeConnect;
            OnConnect += Connection_OnConnect;
            OnAfterConnect += Connection_OnAfterConnect;
            OnBeforeDisconnect += Connection_OnBeforeDisconnect;
            OnDisconnect += Connection_OnDisconnect;
            OnAfterDisconnect += Connection_OnAfterDisconnect;
        }

        #region " Object Abstract Event "
        protected virtual void Connection_OnAfterDisconnect(object sender, EventArgs e) { }
        protected virtual void Connection_OnBeforeDisconnect(object sender, EventArgs e) { }
        protected abstract void Connection_OnDisconnect(object sender, EventArgs e);
        protected abstract void Connection_OnConnect(object sender, EventArgs e);
        protected virtual void Connection_OnAfterConnect(object sender, EventArgs e) { }
        protected virtual void Connection_OnBeforeConnect(object sender, EventArgs e) { }
        #endregion

        /// <summary>
        /// Raise Connect Events.
        /// </summary>
        public void Connect() {
            lock (this) {
                //Update the ConnectionState.
                state = ConnectionState.Connecting;

                //Raise OnBeforeConect();
                OnBeforeConnect(this, new EventArgs());

                //Raise OnConnect();
                if (LastException == null) { OnConnect(this, new EventArgs()); }

                //Update the ConnectionState.
                if (LastException == null) { state = ConnectionState.Connected; }

                //If Got Errors Disconnect.
                else { Disconnect(); }

                //Raise OnAfterConnect();
                OnAfterConnect(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raise Disconnect Events.
        /// </summary>
        public void Disconnect() {
            lock (this) {
                //Update the Connection State.
                state = ConnectionState.Disconnecting;

                //Raise OnBefore();
                OnBeforeDisconnect(this, new EventArgs());

                //Raise On();
                OnDisconnect(this, new EventArgs());

                //Update the Connection State.
                state = ConnectionState.Disconnected;

                //Raise OnAfter();
                OnAfterDisconnect(this, new EventArgs());
            }
        }

        #region " Private Obj Variables "
        protected string clientPath;
        protected ConnectionState state;
        protected PacketSource packetsToSave;
        protected IPAddress iPServer, iPLocal, iPLoginServer;
        protected int basePort;
        protected string serverName;
        protected bool isOtServer;
        protected int processID;
        protected MemoryProvider memory;
        protected Exception lastException;
        #endregion

        #region " Public Obj Properties "
        /// <summary>
        /// Get the Connection State.
        /// </summary>
        public ConnectionState State { get { return state; } }
        /// <summary>
        /// Define Which Packets Need Save.
        /// </summary>
        public PacketSource PacketsToSave { get; set; }
        /// <summary>
        /// Connection Server IP.
        /// </summary>
        public IPAddress IPServer { get; set; }
        /// <summary>
        /// Connection Local IP.
        /// </summary>
        public IPAddress IPLocal { get; set; }
        /// <summary>
        /// Connection Login Server IP.
        /// </summary>
        public IPAddress IPLoginServer { get; set; }
        /// <summary>
        /// Connection Base Port.
        /// </summary>
        public int BasePort { get; set; }
        /// <summary>
        /// Get Client Memory Reader.
        /// </summary>
        public MemoryProvider Memory { get { return memory; } }
        /// <summary>
        /// Xtea Cryptograph Manager.
        /// </summary>
        public ICryptoManager Xtea { get; set; }
        /// <summary>
        /// RSA Cryptograph Manager.
        /// </summary>
        public ICryptoManager RSA { get; set; }
        /// <summary>
        /// Connection Server Name.
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// Connect OT Server Flag.
        /// </summary>
        public bool IsOtServer { get; set; }
        /// <summary>
        /// Last Exception Ocurred.
        /// </summary>
        public Exception LastException {
            get { return lastException; }
        }
        /// <summary>
        /// Client Process ID.
        /// </summary>
        public int ProcessID { get; set; }
        /// <summary>
        /// Gets or sets the client path.
        /// </summary>
        /// <value>The client path.</value>
        public string ClientPath { get; set; }
        /// <summary>
        /// Gets or sets the type of the connection.
        /// </summary>
        /// <value>The type of the connection.</value>
        public ConnectionType ConnectionType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [not raise after connect].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [not raise after connect]; otherwise, <c>false</c>.
        /// </value>
        public bool NotRaiseAfterConnect { get; set; }
        /// <summary>
        /// Connection Events.
        /// </summary>
        public event EventHandler<EventArgs> OnBeforeConnect, OnConnect, OnAfterConnect;
        /// <summary>
        /// Disconnection Events.
        /// </summary>
        public event EventHandler<EventArgs> OnBeforeDisconnect, OnDisconnect, OnAfterDisconnect;
        /// <summary>
        /// Fired when a new Packet is found.
        /// </summary>
        public event EventHandler<PacketEventArgs> PacketChanged;
        #endregion

        /// <summary>
        /// Fire a changed packet event.
        /// </summary>
        internal void FireChangedPacket(byte[] data, PacketSource source) {
            lock (this) {
                if (PacketChanged != null) {

                    var packets = PacketBuilder.GetPackets(data, this, source);
                    foreach (var packet in packets) {
                        PacketChanged(this, new PacketEventArgs(packet, source));
                    }

                }
            }
        }

        /// <summary>
        /// Implementation of Connection Send Packet Data.
        /// </summary>
        public abstract void Send(Packet data);

        /// <summary>
        /// Set The Last Exception Ocurred.
        /// </summary>
        public void SetException(Exception ex) {
            lastException = ex;
        }

        /// <summary>
        /// Dispose the Current Object.
        /// </summary>
        public void Dispose() {

            if (State == ConnectionState.Connected) { Disconnect(); }
            Xtea = null;
            RSA = null;
            IPLocal = null;

            OnBeforeConnect = null;
            OnConnect = null;
            OnAfterConnect = null;
            OnBeforeDisconnect = null;
            OnDisconnect = null;
            OnAfterDisconnect = null;
        }
    }
}
