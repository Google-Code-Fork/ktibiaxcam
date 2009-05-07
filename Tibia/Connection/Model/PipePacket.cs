using System;
using Tibia.Connection.Providers;

namespace Tibia.Connection.Model {
    public class PipePacket : Packet {

        /// <summary>
        /// Initializes a new instance of the <see cref="PipePacket"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public PipePacket(ConnectionProvider connection)
            : base(connection) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipePacket"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="data">The data.</param>
        public PipePacket(ConnectionProvider connection, byte[] data)
            : this(connection) {
            this.ParseData(data);
        }

        #region "[rgn] Internal Variables "
        protected PipePacketType pipetype;
        protected short specifiedLength;
        protected int index;
        #endregion

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Get/Set the type of the packet (specified in the third byte of the data).
        /// </summary>
        /// <value></value>
        public PipePacketType PipeType {
            get {
                return pipetype;
            }
            set {
                pipetype = value;
                if (data != null && data.Length > 3) {
                    data[2] = (byte)pipetype;
                }
            }
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public short Length {
            get {
                return specifiedLength;
            }
            set {
                specifiedLength = value;
                if (data != null && data.Length > 3) {
                    Array.Copy(BitConverter.GetBytes(value), data, 2);
                }
            }
        }

        /// <summary>
        /// Parses the data.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns></returns>
        public bool ParseData(byte[] packet) {
            data = packet;
            if (data.Length > 3) {
                pipetype = (PipePacketType)data[2];
                specifiedLength = BitConverter.ToInt16(Data, 0);
                return true;
            }
            else { return false; }
        }
        
        /// <summary>
        /// Gets a value indicating whether this instance is pipe packet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is pipe packet; otherwise, <c>false</c>.
        /// </value>
        public override bool IsPipePacket {
            get { return true; }
        }
    }
}
