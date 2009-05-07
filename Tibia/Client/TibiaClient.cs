using System;
using System.Diagnostics;
using System.Net;
using Keyrox.Shared;
using Tibia.Client.Contracts;
using Tibia.Connection.Core;
using Tibia.Connection.Providers;
using Tibia.Features;
using Tibia.Features.Providers;
using Tibia.Memory;

namespace Tibia.Client {
    /// <summary>
    /// Base Class for all Tibia Client Objects.
    /// </summary>
    public class TibiaClient : IClient {

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="windowProcess">The window process.</param>
        public TibiaClient(Process windowProcess) {
            process = windowProcess;
            mainWindowHandle = process.MainWindowHandle;
            Connect();
        }

        #region "[rgn] Private Variables "
        protected TibiaMemoryProvider memoryProvider;
        private ConnectionProvider connection;
        protected Process process;
        protected IntPtr hprocess;
        protected IntPtr mainWindowHandle;
        #endregion

        #region "[rgn] Public Properties "
        /// <summary>
        /// Window Process.
        /// </summary>
        public Process Process {
            get { return process; }
        }
        /// <summary>
        /// Handle Process Pointer.
        /// </summary>
        public IntPtr hProcess {
            get {
                if (hprocess == IntPtr.Zero) {
                    OpenProcess();
                    return hprocess;
                }
                else { return hprocess; }
            }
        }
        /// <summary>
        /// Window Handle.
        /// </summary>
        public IntPtr Handle {
            get { return process.Handle; }
        }
        /// <summary>
        /// Main Window Handle.
        /// </summary>
        public IntPtr MainWindowHandle {
            get {
                if (process != null && !process.HasExited && mainWindowHandle == (IntPtr)0) {
                    process.Refresh();
                    mainWindowHandle = process.MainWindowHandle;
                }
                return mainWindowHandle;
            }
            set { mainWindowHandle = value; }
        }
        /// <summary>
        /// Main Window Tile.
        /// </summary>
        public string Tile {
            get { return process.MainWindowTitle; }
        }
        /// <summary>
        /// Main Char is Connected.
        /// </summary>
        public bool IsConnected {
            get {
                if (Memory != null && !Process.HasExited) {
                    uint nOnline; Memory.Reader.Uint(Memory.Addresses.Player.InGame, 1, out nOnline);
                    if (nOnline == 8) { return true; } else { return false; }
                }
                return false;
            }
        }
        /// <summary>
        /// Get Read Process Memory Manager.
        /// </summary>
        public TibiaMemoryProvider Memory {
            get {
                if (memoryProvider == null) {
                    memoryProvider = new TibiaMemoryProvider(Process);
                }
                return memoryProvider;
            }
        }
        /// <summary>
        /// Get All Client Features.
        /// </summary>
        public FeatureProvider Features {
            get {
                return new FeatureProvider(Connection);
            }
        }
        /// <summary>
        /// Return All Client Actions.
        /// </summary>
        public ActionProvider Actions {
            get {
                return new ActionProvider(Connection);
            }
        }
        /// <summary>
        /// Client Connection.
        /// </summary>
        public ConnectionProvider Connection {
            get { return connection; }
            set { connection = value; }
        }

        /// <summary>
        /// Occurs when [connection called].
        /// </summary>
        public event EventHandler<EventArgs> ConnectionCalled;
        #endregion

        #region "[rgn] Client Methods    "
        /// <summary>
        /// Open the Process and update the Handle Process.
        /// </summary>
        public void OpenProcess() {
            if (!process.HasExited) {
                var access = WindowsAPI.ProcessAccessRights.PROCESS_VM_READ | WindowsAPI.ProcessAccessRights.PROCESS_VM_WRITE | WindowsAPI.ProcessAccessRights.PROCESS_VM_OPERATION;
                hprocess = WindowsAPI.OpenProcess((uint)access, 0, (uint)process.Id);
                if (hprocess.ToInt32() == 0) { throw new Exception("OpenProcess Failed!"); }
            }
            else { process = null; hprocess = IntPtr.Zero; }
        }

        /// <summary>
        /// Define the Active Window.
        /// </summary>
        public void SetActiveWindow() {
            IntPtr hWnd = process.MainWindowHandle;
            WindowsAPI.SetActiveWindow(hWnd);
            WindowsAPI.SwitchToThisWindow(hWnd, true);
        }

        /// <summary>
        /// Change the Window Title Text of the Client.
        /// </summary>
        /// <param name="text">New Window Title.</param>
        public void SetWindowText(string text) {
            WindowsAPI.SetWindowText(MainWindowHandle, text);
        }

        /// <summary>
        /// Close the Opened Handle of Process from Client.
        /// </summary>
        public void CloseHandle() {
            int returnVal = WindowsAPI.CloseHandle(hprocess);
            if (returnVal == 0) { throw new Exception("CloseHandle Failed!"); }
        }
        #endregion

        /// <summary>
        /// Connect the Currently Client with Proxy.
        /// </summary>
        /// <param name="loginServer">The login server.</param>
        /// <param name="port">The port.</param>
        /// <param name="isOTServer">if set to <c>true</c> [is OT server].</param>
        public void Connect(string loginServer, int port, bool isOTServer) {
            if (Connection != null) { Connection.Disconnect(); Connection.Dispose(); }
            if (ConnectionCalled != null) ConnectionCalled(this, EventArgs.Empty);

            if (!IsConnected) {
                IPHostEntry Host = Dns.GetHostEntry(loginServer);
                if (Host.AddressList.Length > 0) {
                    Connection = new CamProxy(Host.AddressList[0], port, Memory, process.Id, isOTServer);
                    Connection.Connect();
                }
                else { throw new Exception(String.Format("The Login Server: '{0}' is currently OFFLine!", loginServer)); }
            }
        }

        /// <summary>
        /// Connects this instance with a Local (packetless) connection.
        /// </summary>
        public void Connect() {
            if (Connection != null) { Connection.Disconnect(); Connection.Dispose(); }
            Connection = new Local(Memory, Process.Id);
        }

        /// <summary>
        /// Return the Main Window Title of the Client.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return process.MainWindowTitle;
        }
    }
}
