using System;
using System.Text;
using Tibia.Connection;

namespace Tibia.Connection.Providers {
    /// <summary>
    /// Class for building and parsing packets.
    /// </summary>
    public class PipePacketBuilder {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PipePacketBuilder(ConnectionProvider connection) {
            Connection = connection;
            data = new byte[MaxLength];
        }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        public PipePacketBuilder(ConnectionProvider connection, byte[] packet) : this(connection, packet, 0, packet.Length) { }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start"></param>
        public PipePacketBuilder(ConnectionProvider connection, byte[] packet, int start) : this(connection, packet, start, packet.Length - start) { }

        /// <summary>
        /// Start parsing the given packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public PipePacketBuilder(ConnectionProvider connection, byte[] packet, int start, int length)
            : this(connection) {
            Array.Copy(packet, start, data, 0, length);
        }

        /// <summary>
        /// Start building a packet of the desired type.
        /// </summary>
        /// <param name="type"></param>
        public PipePacketBuilder(ConnectionProvider connection, PipePacketType type)
            : this(connection) {
            Type = type;
            index++;
        }

        #region "[rgn] Private Variables "
        public const int MaxLength = 15360;
        private byte[] data;
        private PipePacketType type;
        private int index = 0;
        private ConnectionProvider Connection;
        #endregion

        #region "[rgn] Public Properties "
        /// <summary>
        /// Get/Set the unencrypted bytes associated with this PipePacketBuilder object.
        /// </summary>
        public byte[] Data {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Get/Set the type of the packet (specified in the third byte of the data).
        /// </summary>
        public PipePacketType Type {
            get { return type; }
            set {
                type = value;
                if (data != null && data.Length > 3) {
                    data[0] = (byte)type;
                }
            }
        }

        /// <summary>
        /// Get/Set the current index in this packet. Set is the same as Seek(int).
        /// </summary>
        public int Index {
            get { return index; }
            set { index = value; }
        }
        #endregion

        #region "[rgn] Add Methods       "
        /// <summary>
        /// Add a byte at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int AddByte(byte b) {
            return AddBytes(new byte[] { b });
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b) {
            return AddBytes(b, b.Length);
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b, int length) {
            return AddBytes(b, 0, length);
        }

        /// <summary>
        /// Add an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int AddBytes(byte[] b, int sourceIndex, int length) {
            Array.Copy(b, sourceIndex, data, index, length);
            index += length;
            return length;
        }

        /// <summary>
        /// Add an "integer" (aka. ushort) at the current index and advance.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int AddInt(int i) {
            return AddBytes(BitConverter.GetBytes((ushort)i));
        }
        /// <summary>
        /// Add a "short" (aka. 2 byte int) at the current index and advance
        /// </summary>
        /// <param name="i">Value between -127 and 127</param>
        /// <returns></returns>
        public int AddShort(uint i) {
            return AddBytes(BitConverter.GetBytes((uint)i));
        }

        /// <summary>
        /// Add a "long" (aka. 4 byte int) at the current index and advance.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public int AddLong(int l) {
            return AddBytes(BitConverter.GetBytes(l));
        }

        /// <summary>
        /// Add the string length and the a string at the current index and advance.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int AddString(string s) {
            AddInt(s.Length);
            return AddBytes(Encoding.ASCII.GetBytes(s));
        }
        #endregion

        #region "[rgn] Get Methods       "
        /// <summary>
        /// Get the byte at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public byte GetByte() {
            return data[index++];
        }

        /// <summary>
        /// Get an array of bytes at the current index and advance.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBytes(int length) {
            byte[] b = new byte[length];
            Array.Copy(data, index, b, 0, length);
            index += length;
            return b;
        }

        /// <summary>
        /// Get an "int" (aka. 2 byte unsigned short) at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public ushort GetInt() {
            ushort i = BitConverter.ToUInt16(data, index);
            index += 2;
            return i;
        }

        public uint GetShort() {
            var i = BitConverter.ToUInt32(data, index);
            index += 2;
            return i;
        }

        /// <summary>
        /// Get a "long" (aka. 4 byte integer) at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public int GetLong() {
            int l = BitConverter.ToInt32(data, index);
            index += 4;
            return l;
        }

        /// <summary>
        /// Get the string length and then the string at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public string GetString() {
            int length = GetInt();
            string s = Encoding.ASCII.GetString(data, index, length);
            index += length;
            return s;
        }

        /// <summary>
        /// Get the string  only at the current index and advance.
        /// </summary>
        /// <returns></returns>
        public string GetString(int length) {
            string s = Encoding.ASCII.GetString(data, index, length);
            index += length;
            return s;
        }

        /// <summary>
        /// Get the completed packet with the two byte length header attached.
        /// </summary>
        /// <returns></returns>
        public byte[] GetPacket() {
            byte[] b = new byte[index + 2];
            Array.Copy(BitConverter.GetBytes((short)index), b, 2);
            Array.Copy(data, 0, b, 2, index);
            return b;
        }
        #endregion

        #region "[rgn] Peek Methods      "
        /// <summary>
        /// Get the byte at the current index.
        /// </summary>
        /// <returns></returns>
        public byte PeekByte() {
            return data[index++];
        }

        /// <summary>
        /// Get an array of bytes at the current index.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] PeekBytes(int length) {
            byte[] b = new byte[length];
            Array.Copy(data, index, b, 0, length);
            return b;
        }

        /// <summary>
        /// Get an "int" (aka. 2 byte unsigned short) at the current index.
        /// </summary>
        /// <returns></returns>
        public ushort PeekInt() {
            ushort i = BitConverter.ToUInt16(data, index);
            return i;
        }

        /// <summary>
        /// Get a "long" (aka. 4 byte integer) at the current index.
        /// </summary>
        /// <returns></returns>
        public int PeekLong() {
            int l = BitConverter.ToInt32(data, index);
            return l;
        }

        /// <summary>
        /// Get a string at the current index.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string PeekString(int length) {
            string s = Encoding.ASCII.GetString(data, index, length);
            return s;
        }

        #endregion

        /// <summary>
        /// Move the index to the specified value. Same as setting Index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Seek(int index) {
            this.index = index;
            return index;
        }

        /// <summary>
        /// Skip the index ahead the specified amount of bytes.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int Skip(int length) {
            index += length;
            return index;
        }
    }
}