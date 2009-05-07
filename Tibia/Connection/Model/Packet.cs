using System;
using Tibia.Connection.Model.Contracts;
using Tibia.Connection.Providers;

namespace Tibia.Connection.Model {
    public class Packet : IPacket {

        /// <summary>
        /// Default Object Constructor.
        /// </summary>
        public Packet() {
            InterceptedDate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Packet(ConnectionProvider connection) {
            ConnectionSource = connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="data">The data.</param>
        public Packet(ConnectionProvider connection, byte[] data) {
            ConnectionSource = connection;
            Data = data;
        }

        #region " Private Obj Variables "
        protected byte[] data, encryptedData;
        private ConnectionProvider connectionSource;
        private Exception cryptoException;
        #endregion

        #region " Public Obj Properties "
        /// <summary>
        /// Packet ID.
        /// </summary>
        public uint Id {
            get {
                if (Data != null && Data.Length > 2) {
                    return Convert.ToUInt32(Data[2]);
                }
                return 0x0;
            }
            set {
                if (Data == null && Data.Length > 2) { Data[2] = Convert.ToByte(value); }
            }
        }
        /// <summary>
        /// Packet Header Size.
        /// </summary>
        public int TrueSize {
            get {
                if (Data != null && Data.Length > 1) {
                    return Convert.ToInt32(new[] { Data[0], Data[1] }.GetTheLong());
                }
                return -1;
            }
        }
        /// <summary>
        /// Packet Length.
        /// </summary>
        public int DataSize {
            get {
                if (Data != null && Data.Length > 1) {
                    return Data.Length;
                }
                return -1;
            }
        }
        /// <summary>
        /// Verify if Packet Header Matches with Packet True Size.
        /// </summary>
        public bool IsValid {
            get {
                if (TrueSize == (DataSize - 2)) {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is pipe packet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is pipe packet; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsPipePacket { get { return false; } }
        /// <summary>
        /// Verify if Packet is a Character List Packet.
        /// </summary>
        public bool CharacterList {
            get {
                if (Data != null && Data.Length > 4) {
                    if (Data[2] == 0x14 || Data[2] == 0x64) {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// Packet Decrypted Data.
        /// </summary>
        public byte[] Data {
            get {
                if (data == null && encryptedData != null && connectionSource != null) {
                    var innerPackets = PacketBuilder.GetPacketData(encryptedData);
                    if (innerPackets.Count > 0) {
                        try {
                            data = connectionSource.Xtea.DeCryptograph(innerPackets[0]);
                        }
                        catch (FormatException) { System.Diagnostics.Debugger.Break(); }
                    }
                }
                return data;
            }
            set { data = value; }
        }
        /// <summary>
        /// Packet Enctypted Data.
        /// </summary>
        public byte[] EncryptedData {
            get {
                if (encryptedData == null && data != null && connectionSource != null) {
                    encryptedData = connectionSource.Xtea.Cryptograph(data);
                }
                return encryptedData;
            }
            set { encryptedData = value; }
        }
        /// <summary>
        /// Connection Source of this Packet.
        /// </summary>
        public ConnectionProvider ConnectionSource {
            get { return connectionSource; }
            set { connectionSource = value; }
        }
        /// <summary>
        /// Packet Data Source.
        /// </summary>
        public PacketSource PacketSource { get; set; }
        /// <summary>
        /// Gets the type of the in.
        /// </summary>
        /// <value>The type of the in.</value>
        public IncomingPacketType InType {
            get {
                if (Data != null && Data.Length >= 3) {
                    return (IncomingPacketType)Data[2];
                }
                return IncomingPacketType.Unknow;
            }
        }
        /// <summary>
        /// Gets the type of the out.
        /// </summary>
        /// <value>The type of the out.</value>
        public OutgoingPacketType OutType {
            get {
                if (Data != null && Data.Length >= 3) {
                    return (OutgoingPacketType)Data[2];
                }
                return OutgoingPacketType.Unknow;
            }
        }
        /// <summary>
        /// Time of Packet Interception.
        /// </summary>
        public DateTime InterceptedDate { get; set; }
        /// <summary>
        /// Exeception Generated on Cryptograph Operation.
        /// </summary>
        public Exception CryptoException {
            get { return cryptoException; }
        }
        #endregion

        /// <summary>
        /// Returns the Hexadecimal Representation of Dectypted Data.
        /// </summary>
        public string ToHexString() {
            if (IsValid) {
                return Data.GetString(true);
            }
            return "";
        }

        /// <summary>
        /// Returns the Hexadecimal Representation of Dectypted Data.
        /// </summary>
        public string ToDecString() {
            if (IsValid) {
                return Data.GetUintString(Data.Length);
            }
            return "";
        }

        /// <summary>
        /// Returns the Hexadecimal Representation of Enctypted Data.
        /// </summary>
        public string ToCryptHexString() {
            if (IsValid) {
                return EncryptedData.GetString(true);
            }
            return "";
        }

        /// <summary>
        /// Set an Exception from Cryptograph 
        /// </summary>
        internal void SetCriptoException(Exception ex) {
            cryptoException = ex;
        }

        /// <summary>
        /// Set a Connection Source.
        /// </summary>
        public void SetConnectionProvider(ConnectionProvider connection) {
            connectionSource = connection;
        }

        /// <summary>
        /// Returns the String Representation of the Packet.
        /// </summary>
        public override string ToString() {
            return string.Format("Length: {0}", TrueSize);
        }
    }
}
