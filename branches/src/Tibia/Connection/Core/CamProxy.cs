using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Tibia.Connection.Model;
using Tibia.Memory;
using Tibia.Connection.Events;
using Tibia.Connection.Providers;
using Keyrox.Shared.Controls;
using Tibia.Connection.Common;
using Keyrox.Shared.Objects;

namespace Tibia.Connection.Core {
    public class CamProxy : ConnectionProvider {
        /// <summary>
        /// Object Constructor.
        /// </summary>
        public CamProxy(IPAddress loginServer, int port, TibiaMemoryProvider manager, int ProcessID, bool isOTServer)
            : base(loginServer, port, manager, ProcessID, isOTServer) {
        }

        #region " Public Obj Properties "
        public Socket SocketLocal { get; set; }
        public NetworkStream StreamLocal { get; set; }
        public TcpListener TcpLocal { get; set; }
        #endregion

        /// <summary>
        /// Send Data to Connection.
        /// </summary>
        /// <param name="data">Data Information.</param>
        public override void Send(Packet data) {
            try {
                data.SetConnectionProvider(this);
                StreamLocal.Write(data.EncryptedData, 0, data.EncryptedData.Length);
            }
            catch (SocketException) { System.Diagnostics.Debugger.Break(); return; }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Writes the local server.
        /// </summary>
        public void WriteLocalServer() {
            var localIP = IPLocal.ToString();
            var pointer = Memory.Addresses.Client.LoginServerStart;

            localIP += (char)0;
            for (int i = 0; i < Memory.Addresses.Client.LoginServersMax; i++) {
                Memory.Writer.String(pointer, localIP);
                Memory.Writer.Uint(pointer + Memory.Addresses.Client.PortDistance, LocalPort.ToUInt32());
                pointer += Memory.Addresses.Client.LoginServerStep;
            }
        }

        #region " Connection Events     "
        protected override void Connection_OnBeforeConnect(object sender, EventArgs e) {
            try {
                WriteLocalServer();
            }
            catch (Exception ex) { SetException(ex); throw new Exception(ex.Message, ex.InnerException); }
        }
        protected override void Connection_OnAfterDisconnect(object sender, EventArgs e) {
            Initialize();
        }
        #endregion

        /// <summary>
        /// Connect Proxy on Server.
        /// </summary>
        protected override void Connection_OnConnect(object sender, EventArgs e) {
            //Intialize the Packet data Store.
            var Data = new byte[1024]; int len = 0;
                        
            //Start the TCP Listener Local.
            if (TcpLocal == null) {
                TcpLocal = new TcpListener(IPAddress.Any, LocalPort);
                TcpLocal.Start();
            }
            Output.Add("Configuration has complete, waiting for client response.");

            //Start the Connection Socket.
            SocketLocal = TcpLocal.AcceptSocket();

            //If there isn't a Remote Connection. (Reconect)
            if (!SocketLocal.Connected) { Connection_OnConnect(this, EventArgs.Empty); }

            //Initialize the Streams Local Listener.
            StreamLocal = new NetworkStream(SocketLocal);

            //Read Login Packet From Client and Send to Server.
            len = StreamLocal.Read(Data, 0, Data.Length);

            //Store the login packet in the output log.
            Output.AddSeparator(); Output.Add("Login packet intercepted.");
        }

        /// <summary>
        /// Dispose all Connection Objects.
        /// </summary>
        protected override void Connection_OnDisconnect(object sender, EventArgs e) {
            if (SocketLocal != null) { SocketLocal.Disconnect(false); SocketLocal.Close(); SocketLocal = null; }
            if (StreamLocal != null) { StreamLocal.Close(); StreamLocal.Dispose(); StreamLocal = null; }
            if (TcpLocal != null) { TcpLocal.Stop(); TcpLocal = null; }
        }

        /// <summary>
        /// Return the Connection Type.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return "Proxy Connection!";
        }

    }
}
