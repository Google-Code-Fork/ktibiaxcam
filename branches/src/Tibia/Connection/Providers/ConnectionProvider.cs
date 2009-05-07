using System;
using System.IO;
using System.Net;
using Tibia.Memory;
using Tibia.Connection.Core;
using Tibia.Connection.Events;
using Tibia.Connection.Model;
using Tibia.Connection.Providers.Contracts;
using Keyrox.Shared.Extensions;

namespace Tibia.Connection.Providers {
    public abstract class ConnectionProvider : IConnection, IDisposable {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionProvider"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="processID">The process ID.</param>
        public ConnectionProvider(TibiaMemoryProvider manager, int processID) {
            Memory = manager;
            ProcessID = processID;
            Initialize();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionProvider"/> class.
        /// </summary>
        /// <param name="loginServer">The login server.</param>
        /// <param name="basePort">The base port.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="processID">The process ID.</param>
        public ConnectionProvider(IPAddress loginServer, int basePort, TibiaMemoryProvider manager, int processID, bool isOtServer)
            : this(manager, processID) {
            IPLoginServer = loginServer;
            LoginServerPort = basePort;
            IsOtServer = isOtServer;
        }

        #region "[rgn] Private Abstract Events "
        protected virtual void Connection_OnAfterDisconnect(object sender, EventArgs e) { }
        protected virtual void Connection_OnBeforeDisconnect(object sender, EventArgs e) { }
        protected abstract void Connection_OnDisconnect(object sender, EventArgs e);
        protected abstract void Connection_OnConnect(object sender, EventArgs e);
        protected virtual void Connection_OnAfterConnect(object sender, EventArgs e) { }
        protected virtual void Connection_OnBeforeConnect(object sender, EventArgs e) { }
        #endregion

        #region "[rgn] Pipe Initialization     "
        /// <summary>
        /// Initializes the pipe.
        /// </summary>
        public void InitializePipe() {
            if (Pipe != null) return;

            Pipe = new Pipe(this, string.Concat("KTibiaX", ClientProcessID.ToString()));
            Pipe.OnConnected += OnPipeConnect;

            var hookDllFileName = Path.Combine(Environment.CurrentDirectory, "KTibiaXHooK.dll");
            if (!InjectDLL(hookDllFileName)) throw new Exception(string.Concat(hookDllFileName, " not Found!"));
        }

        /// <summary>
        /// Injects the DLL.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool InjectDLL(string filename) {

            if (!File.Exists(filename)) return false;
            IntPtr remoteAddress = Keyrox.Shared.WindowsAPI.VirtualAllocEx(Memory.HandleProcess, IntPtr.Zero, (uint)filename.Length, Keyrox.Shared.WindowsAPI.MEM_COMMIT | Keyrox.Shared.WindowsAPI.MEM_RESERVE, Keyrox.Shared.WindowsAPI.PAGE_READWRITE);
            Memory.Writer.String(remoteAddress, filename); ;

            IntPtr thread = Keyrox.Shared.WindowsAPI.CreateRemoteThread(Memory.HandleProcess, IntPtr.Zero, 0, Keyrox.Shared.WindowsAPI.GetProcAddress(Keyrox.Shared.WindowsAPI.GetModuleHandle("Kernel32"), "LoadLibraryA"), remoteAddress, 0, IntPtr.Zero);
            Keyrox.Shared.WindowsAPI.VirtualFreeEx(Memory.HandleProcess, remoteAddress, (uint)filename.Length, Keyrox.Shared.WindowsAPI.MEM_RELEASE);
            return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
        }

        /// <summary>
        /// Called when [pipe connect].
        /// </summary>
        public void OnPipeConnect() {
            PipeConnect(this, EventArgs.Empty);
        }
        #endregion

        /// <summary>
        /// Initialize All Events and Initial Values of Object.
        /// </summary>
        protected void Initialize() {
            State = ConnectionState.Disconnected;
            PacketsToSave = PacketSource.Unknow;
            Xtea = new CryptoManager(this);
            IPLocal = new IPAddress(new byte[] { 127, 0, 0, 1 });
            PipeIsReady = new System.Threading.AutoResetEvent(false);

            OnBeforeConnect += Connection_OnBeforeConnect;
            OnConnect += Connection_OnConnect;
            OnAfterConnect += Connection_OnAfterConnect;
            OnBeforeDisconnect += Connection_OnBeforeDisconnect;
            OnDisconnect += Connection_OnDisconnect;
            OnAfterDisconnect += Connection_OnAfterDisconnect;
        }

        /// <summary>
        /// Raise Connect Events.
        /// </summary>
        public void Connect() {
            State = ConnectionState.Connecting;
            OnBeforeConnect(this, EventArgs.Empty);
            if (LastException == null) { OnConnect(this, EventArgs.Empty); }
            if (LastException == null) { State = ConnectionState.Connected; }
            else { Disconnect(); }
            OnAfterConnect(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise Disconnect Events.
        /// </summary>
        public void Disconnect() {
            State = ConnectionState.Disconnecting;
            OnBeforeDisconnect(this, EventArgs.Empty);
            OnDisconnect(this, EventArgs.Empty);
            State = ConnectionState.Disconnected;
            OnAfterDisconnect(this, EventArgs.Empty);
        }


        #region "[rgn] Public Properties "
        /// <summary>
        /// Gets the X tea key.
        /// </summary>
        /// <value>The X tea key.</value>
        public byte[] XTeaKey {
            get {
                if (Memory != null) { byte[] res; Memory.Reader.Byte(Memory.Addresses.Client.XTeaKey, 16, out res); return res; }
                return null;
            }
        }

        /// <summary>
        /// Get the Connection State.
        /// </summary>
        public ConnectionState State { get; private set; }

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

        protected int ilocalPort;
        /// <summary>
        /// Connection Base Port.
        /// </summary>
        public int LocalPort { get { if (ilocalPort == 0) { ilocalPort = Network.GetNextAvailablePort(7172); } return ilocalPort; } }

        /// <summary>
        /// Gets or sets the base port.
        /// </summary>
        /// <value>The base port.</value>
        public int ServerPort { get; set; }

        /// <summary>
        /// Gets or sets the login server port.
        /// </summary>
        /// <value>The login server port.</value>
        public int LoginServerPort { get; set; }
        
        /// <summary>
        /// Get Client Memory Reader.
        /// </summary>
        public TibiaMemoryProvider Memory { get; protected set; }

        /// <summary>
        /// Gets or sets the pipe.
        /// </summary>
        /// <value>The pipe.</value>
        public Pipe Pipe { get; set; }

        /// <summary>
        /// Xtea Cryptograph Manager.
        /// </summary>
        public CryptoManager Xtea { get; set; }

        /// <summary>
        /// RSA Cryptograph Manager.
        /// </summary>
        public CryptoManager RSA { get; set; }

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
        public Exception LastException { get; protected set; }

        /// <summary>
        /// Client Process ID.
        /// </summary>
        public int ProcessID { get; set; }

        /// <summary>
        /// Gets or sets the process handle.
        /// </summary>
        /// <value>The process handle.</value>
        public IntPtr ProcessHandle { get; set; }

        /// <summary>
        /// Gets a value indicating if a dialog is opened.
        /// </summary>
        public System.Threading.AutoResetEvent PipeIsReady { get; set; }

        /// <summary>
        /// Gets or sets the client process ID.
        /// </summary>
        /// <value>The client process ID.</value>
        public int ClientProcessID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [not raise after connect].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [not raise after connect]; otherwise, <c>false</c>.
        /// </value>
        public bool NotRaiseAfterConnect { get; set; }
        #endregion

        #region "[rgn] Public Obj Events "
        /// <summary>
        /// Occurs when [pipe connect].
        /// </summary>
        public event EventHandler PipeConnect;
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
        /// Sends the specified packet.
        /// </summary>
        /// <param name="data">The packet.</param>
        public abstract void Send(Packet data);

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void SetException(Exception ex) {
            LastException = ex;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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

        /// <summary>
        /// Fires the changed packet.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="source">The source.</param>
        internal void FireChangedPacket(byte[] data, PacketSource source) {
            if (PacketChanged != null) {
                var packets = PacketBuilder.GetPackets(data, this, source);
                packets.ForEach(pk => PacketChanged(this, new PacketEventArgs(pk, source)));
            }
        }
    }
}
