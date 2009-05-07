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
    public class Proxy : ConnectionProvider {
        /// <summary>
        /// Object Constructor.
        /// </summary>
        public Proxy(IPAddress loginServer, int port, TibiaMemoryProvider manager, int ProcessID, bool isOTServer)
            : base(loginServer, port, manager, ProcessID, isOTServer) {
            PacketChanged += PacketAnalyze;
            TMGameListener = new System.Threading.Timer(GameListener, null, System.Threading.Timeout.Infinite, 100);
        }

        #region " Public Obj Properties "
        public Socket SocketLocal { get; set; }

        public Socket SocketGame { get; set; }

        public NetworkStream StreamLocal { get; set; }

        public NetworkStream StreamGame { get; set; }

        public NetworkStream StreamRemote { get; set; }

        public TcpClient TcpGame { get; set; }

        public TcpClient TcpRemote { get; set; }

        public TcpListener TcpLocal { get; set; }

        public List<PlayerList> PlayerList { get; set; }

        public System.Threading.Timer TMClientListener { get; set; }

        public System.Threading.Timer TMGameListener { get; set; }

        public bool GameListening { get; set; }

        public bool ClientListening { get; set; }

        protected delegate void PacketChangedDelegate(byte[] data, PacketSource source);

        PacketChangedDelegate PacketChangedCall { get { return FireChangedPacket; } }
        #endregion

        /// <summary>
        /// Send Data to Connection.
        /// </summary>
        /// <param name="data">Data Information.</param>
        public override void Send(Packet data) {
            try {
                //Set the Connection Source.
                data.SetConnectionProvider(this);

                switch (data.PacketSource) {
                    //Send a Packet to Server Connection.
                    case PacketSource.Client:
                        StreamGame.Write(data.EncryptedData, 0, data.EncryptedData.Length);
                        FireChangedPacket(data.EncryptedData, PacketSource.Client);
                        if (data.OutType == OutgoingPacketType.Logout) {
                            SwitchCharacter(null, true);
                        }
                        break;

                    //Send a Packet to Client Connection.
                    case PacketSource.Server:
                        StreamLocal.Write(data.EncryptedData, 0, data.EncryptedData.Length);
                        FireChangedPacket(data.EncryptedData, PacketSource.Server);
                        break;

                    //Send a Packet to Server Connection.
                    case PacketSource.Unknow:
                        StreamGame.Write(data.EncryptedData, 0, data.EncryptedData.Length);
                        FireChangedPacket(data.EncryptedData, PacketSource.Client);
                        if (data.OutType == OutgoingPacketType.Logout) {
                            SwitchCharacter(null, true);
                        }
                        break;
                }
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
        protected override void Connection_OnAfterConnect(object sender, EventArgs e) {
            if (TMGameListener == null) {
                TMGameListener = new System.Threading.Timer(GameListener, null, 0, 100);
            }
            else { TMGameListener.Change(0, 100); }
            Output.Add("Proxy connection was complete successfully.");
        }
        protected override void Connection_OnBeforeDisconnect(object sender, EventArgs e) {
            if (TMClientListener != null) {
                TMClientListener.Dispose(); TMClientListener = null;
            }
            if (TMGameListener != null) {
                TMGameListener.Dispose(); TMGameListener = null;
            }
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

            //Verify the Connection caller.
            var reconnection = sender.GetType() == typeof(CharListIgnored);
            if (reconnection) { goto ConnectionAlreadyStarted; }
            Output.Add("Detected connection attemp... forwarding to 127.0.0.1");

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

            //Initialize the Remote Login Server Connection.
            TcpRemote = new TcpClient(IPLoginServer.ToString(), LoginServerPort);

            //Initialize the Streams Local Listener.
            StreamLocal = new NetworkStream(SocketLocal);
            StreamRemote = TcpRemote.GetStream();

            //Read Login Packet From Client and Send to Server.
            len = StreamLocal.Read(Data, 0, Data.Length);
            StreamRemote.Write(Data, 0, len);

            //Store the login packet in the output log.
            Output.AddSeparator(); Output.Add("Login packet intercepted.");
            Output.Add(Data.Redim(len).GetString(true)); Output.AddSeparator();

            #region " Connection Already Started Label "
        ConnectionAlreadyStarted:
            #endregion

            //Read Response of The Server "If Login = OK".
            len = StreamRemote.Read(Data, 0, Data.Length);
            byte[] charList = Xtea.DeCryptograph(Data.Redim(len));

            //Check the type of the returned packet.
            RaiseSwichCharacter(charList, reconnection);
        }

        /// <summary>
        /// Dispose all Connection Objects.
        /// </summary>
        protected override void Connection_OnDisconnect(object sender, EventArgs e) {
            if (SocketGame != null) { SocketGame.Disconnect(false); SocketGame.Close(); SocketGame = null; }
            if (StreamGame != null) { StreamGame.Close(); StreamGame.Dispose(); StreamGame = null; }
            if (TcpGame != null) { TcpGame.Close(); TcpGame = null; }

            if (SocketLocal != null) { SocketLocal.Disconnect(false); SocketLocal.Close(); SocketLocal = null; }
            if (StreamLocal != null) { StreamLocal.Close(); StreamLocal.Dispose(); StreamLocal = null; }
            if (TcpLocal != null) { TcpLocal.Stop(); TcpLocal = null; }
        }

        #region " Player Login Methods  "
        /// <summary>
        /// Parse Player List and Connect a New Character.
        /// </summary>
        /// <param name="dataPlayerList">Player List.</param>
        /// <param name="raiseAfterConnect">Define if AfterConnect will be Raised.</param>
        protected void SwitchCharacter(byte[] dataPlayerList, bool raiseAfterConnect) {
            //Get Player List Information.
            var data = ParsePlayerList(dataPlayerList);

            //Login successfully, write parsed player list.
            if (data != null) {
                Output.Add("Waiting for character choose...");
                var encData = Xtea.Cryptograph(data);
                StreamLocal.Write(encData, 0, encData.Length);
            }

            //Login failed, write the result and reconnect the proxy.
            else {
                if (dataPlayerList != null) {
                    Output.Add("Player login has failed.");
                    StreamLocal.Write(dataPlayerList, 0, dataPlayerList.Length);
                }                
                Connection_OnBeforeDisconnect(this, null);
                SocketGame.Disconnect(true);
                //WriteLocalServer();
                //Connection_OnConnect(this, EventArgs.Empty);
                //return;
            }

            //Open Game Server Connection.
            SocketGame = TcpLocal.AcceptSocket();
            StreamLocal = new NetworkStream(SocketGame); data = new byte[1024];
            var len = StreamLocal.Read(data, 0, data.Length);

            //Output.AddSeparator();
            //Output.Add("Character selection packet intercepted!");
            //Output.Add(data.Redim(len).GetString(true));
            //Output.AddSeparator();

            //Verify if is CharList was Ignored.
            if (CharListWasIgnored(data.Redim(len))) { return; }

            //Get The Selected Char on List.
            uint CharIndex;
            Memory.Reader.Uint(Memory.Addresses.Client.LoginSelectedChar, 1, out CharIndex);

            //Update Connection Information.
            IPServer = new IPAddress(PlayerList[Convert.ToInt32(CharIndex)].ServerIP);
            ServerPort = PlayerList[Convert.ToInt32(CharIndex)].ServerPort;
            ServerName = PlayerList[Convert.ToInt32(CharIndex)].ServerName;

            //Update Log information.
            Output.Add("Forwarding connection to: ", ServerName, " (", IPServer.ToString(), ":", ServerPort.ToString(), ") - ", PlayerList[Convert.ToInt32(CharIndex)].CharName);

            //Connect on Selected Char Server.
            TcpGame = new TcpClient(PlayerList[Convert.ToInt32(CharIndex)].IPToString(), ServerPort);
            StreamGame = TcpGame.GetStream();
            StreamGame.Write(data, 0, len);

            //Start Listener, if already connected.
            if (raiseAfterConnect) { Connection_OnAfterConnect(this, null); }
        }

        /// <summary>
        /// Fill Player List and Change the Server IP Address to Local.
        /// </summary>
        /// <param name="data">Player List Server Packet.</param>
        protected byte[] ParsePlayerList(byte[] data) {
            if (data == null) { return null; }

            var localIP = IPLocal.GetAddressBytes().Redim(4);
            var localPort = LocalPort.ToUInt32().GetBytes().Redim(2);
            var packet = new Packet() { Data = data, ConnectionSource = this };
            PlayerList = new List<PlayerList>();

            var index = 2;
            switch (packet.ReadInt(2)) {
                case 0x0A: Output.Add("Error Received: ", packet.ReadString(3)); return null;
                case 0x0B: Output.Add("For your information: ", packet.ReadString(3)); return null;
                case 0x14: Output.Add("MOTD: ", packet.ReadString(3)); index += (3 + packet.ReadString(3).Length); break;
                case 0x1E: Output.Add("Patching Message: ", packet.ReadString(3)); return null;
                case 0x20: Output.Add("New Version: ", packet.ReadString(3)); return null;
            }

            if (packet.ReadInt(index) == 0x64) {
                var charCount = packet.ReadInt(index + 1);

                var addler = index;
                var result = packet.Data;
                for (var i = 0; i < charCount; i++) {

                    addler += 2;
                    var charName = packet.ReadString(addler);

                    addler += charName.Length + 2;
                    var serverName = packet.ReadString(addler);

                    addler += serverName.Length + 2;
                    var serverIP = packet.ReadBytes(addler, 4);
                    result = result.Replace(localIP, addler);

                    addler += 4;
                    var serverPort = packet.ReadShort(addler);
                    result = result.Replace(localPort, addler);

                    PlayerList.Add(new PlayerList(charName, serverName, serverIP, serverPort.ToInt32()));
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Verify if its a Acc Packet and reestart connection.
        /// </summary>
        /// <returns></returns>
        protected bool CharListWasIgnored(byte[] data) {
            //TODO: Melhorar a verificação do pacote de login.
            if (data[00] == 0x89) return false;

            //Initialize the Remote Login Server Connection.
            TcpRemote = new TcpClient(IPLoginServer.ToString(), LoginServerPort);
            StreamRemote = TcpRemote.GetStream();

            //Send Acc Packet and Reestart Proxy Connection.
            StreamRemote.Write(data, 0, data.Length);
            WriteLocalServer();
            Connection_OnConnect(new CharListIgnored(), EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Raises the Swich Character Method.
        /// </summary>
        public void RaiseSwichCharacter(byte[] data, bool reconnection) {
            if (reconnection) { SwitchCharacter(data, true); }
            else { SwitchCharacter(data, false); }
        }
        #endregion

        /// <summary>
        /// Game Connection Packet List Manager.
        /// </summary>
        protected void GameListener(object args) {
            if (State == ConnectionState.Connected && StreamGame != null && StreamLocal != null) {
                try {
                    if (StreamGame != null && StreamGame.CanRead && StreamGame.DataAvailable) {
                        //Byte to Store Server Packets.
                        var data = new Byte[4096];

                        //Get Game Server Packets.
                        var length = StreamGame.Read(data, 0, data.Length);

                        if (length > 0) {
                            //Send Packet to Client if Needed.
                            if (StreamLocal.CanWrite) { StreamLocal.Write(data, 0, length); }

                            //Stack the Intercepted Packet.
                            PacketChangedCall.BeginInvoke(data.Redim(length), PacketSource.Server, null, PacketChangedCall);

                            //Initialize the Client Listener, if Needed.
                            if (TMClientListener != null) { return; }
                            TMClientListener = new System.Threading.Timer(ClientListener, null, 0, 100);
                        }
                    }
                }
                catch (IOException) { Disconnect(); }
            }
        }

        /// <summary>
        /// Client Connection Packet List Manager.
        /// </summary>
        protected void ClientListener(object args) {
            try {
                if (StreamLocal != null && StreamLocal.CanRead && StreamLocal.DataAvailable) {
                    //Byte to Store Client Packets.
                    var data = new Byte[4096];

                    //Get Client Local Packets.
                    var length = StreamLocal.Read(data, 0, data.Length);

                    if (length > 0) {
                        //Send Packet to Server if Needed.
                        if (StreamGame.CanWrite) { StreamGame.Write(data, 0, length); }

                        //Stack the Intercepted Packet.
                        PacketChangedCall.BeginInvoke(data.Redim(length), PacketSource.Client, null, PacketChangedCall);
                    }
                }
            }
            catch (IOException) { Disconnect(); }
        }

        /// <summary>
        /// Analyze the Incoming Packets.
        /// </summary>
        protected void PacketAnalyze(object sender, PacketEventArgs e) {
            var listener = new Callback(delegate() {
                if (e.Source == PacketSource.Client && e.Packet.OutType == OutgoingPacketType.Logout) {
                    SwitchCharacter(null, true);
                }
            });
            listener.BeginInvoke(null, listener);
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
