using System;
using Tibia.Connection.Model;
using Tibia.Connection;
using System.Collections.Generic;

namespace Tibia.Connection.Providers {
	public class PacketBuilder {
        /// <summary>
        /// Initialize a New Packet Data Builder.
        /// </summary>
        public PacketBuilder() { }
        
        /// <summary>
		/// Add the Packet Header and the defined ID on Packet.
		/// </summary>
		public PacketBuilder(uint packetID, ConnectionProvider connection) {
			var nData = BitConverter.GetBytes(packetID);
			Connection = connection;
			RemoveLastEmptyBytes(ref nData);

			if (AddHeader(ref nData)) {
				Packet = PacketFactory.BindPacket(nData, connection);
			}
			else { throw new Exception("Is not possible Add the Packet Header!", new FormatException()); }
		}

		#region " Private Obj Variables "
		#endregion

		#region " Public Obj Properties "
        protected Packet Packet { get; set; }
        public ConnectionProvider Connection { get; set; }
		#endregion

		#region " Internal Obj Methods  "
		public void Append(byte[] value) {
			var nData = new byte[Packet.Data.Length + value.Length];
			Array.Copy(Packet.Data, 0, nData, 0, Packet.Data.Length);
			Array.Copy(value, 0, nData, Packet.Data.Length, value.Length);

			FixHeader(ref nData);
			Packet = PacketFactory.BindPacket(nData, Connection);
		}
		public void Append(byte value) {
			var nvalue = new[] { value };
			Append(nvalue);
		}
		public void Append(int value) {
			if (value > 0) {
				var nvalue = BitConverter.GetBytes(value);
				RemoveLastEmptyBytes(ref nvalue);
				Append(nvalue);
			}
			else { Append(new byte[] { 0x0 }); }
		}
		public void Append(int value, int size) {
			if (value > 0) {
				var nvalue = BitConverter.GetBytes(value);
				Resize(ref nvalue, size);
				Append(nvalue);
			}
			var nbyte = new byte[size];
			for (var i = 0; i < size; i++) {
				nbyte[i] = 0x00;
			}
			Append(nbyte);
		}
		public void Append(uint value) {
			if (value > 0) {
				var nvalue = BitConverter.GetBytes(value);
				RemoveLastEmptyBytes(ref nvalue);
				Append(nvalue);
			}
			else { Append(new byte[] { 0x0 }); }
		}
		public void Append(uint value, int size) {
			if (value > 0) {
				var nvalue = BitConverter.GetBytes(value);
				Resize(ref nvalue, size);
				Append(nvalue);
			}
			else {
				var nbyte = new byte[size];
				for(var i = 0;i < size; i++) {
					nbyte[i] = 0x00;
				}
				Append(nbyte);
			}
		}
		public void Append(bool value) {
			var nvalue = BitConverter.GetBytes(value);
			Append(nvalue);
		}
		public void Append(string value) {
			var nvalue = new byte[value.Length];
			for (var i = 0; i < value.Length; i++) { nvalue[i] = Convert.ToByte(value[i]); }
			Append(nvalue);
		}
		public void Append(string value, bool needHeader) {
			var nvalue = new byte[value.Length];
			for (var i = 0; i < value.Length; i++) { nvalue[i] = Convert.ToByte(value[i]); }
			if (needHeader) { AddHeader(ref nvalue); }
			Append(nvalue);
		}
		public void Append(string value, bool needHeader, bool zeroEnd) {
		    var nvalue = !zeroEnd ? new byte[value.Length] : new byte[value.Length + 1];
		    for (var i = 0; i < value.Length; i++) { nvalue[i] = Convert.ToByte(value[i]); }
			if (zeroEnd) { nvalue[value.Length] = 0x0; }

			if (needHeader) { AddHeader(ref nvalue); }
			Append(nvalue);
		}

		public int GetTotalSize() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.Data.Length;
			}
			return 0;
		}
		public int GetTrueSize() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 1) {
				return Packet.Data.Length - 2;
			}
			return 0;
		}

		public byte[] GetData() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.Data;
			}
			return new byte[1];
		}
		public byte[] GetCryptoData() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.EncryptedData;
			}
			return new byte[1];
		}
		public Packet GetPacket() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet;
			}
			return null;
		}

		public string ToHexString() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.Data.GetString(true);
			}
			return "";
		}
		public string ToDecimalString() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.Data.GetUintString(Packet.Data.Length);
			}
			return "";
		}
		public void SetPacketSource(PacketSource source) {
			if (Packet == null) { Packet = PacketFactory.BindPacket(new byte[1], Connection); }
			Packet.PacketSource = source;
		}
		#endregion

		#region " Static Byte Functions "
		/// <summary>
		/// Return the bytes of defined value;
		/// </summary>
		public static byte[] GetBytes(uint value, int size) {
			byte[] nvalue = BitConverter.GetBytes(value);
			Resize(ref nvalue, size);
			return nvalue;
		}

		/// <summary>
		/// Return the bytes of defined value;
		/// </summary>
		public static byte[] GetBytes(int value, int size) {
			byte[] nvalue = BitConverter.GetBytes(value);
			Resize(ref nvalue, size);
			return nvalue;
		}

		/// <summary>
		/// Get the Long value of defined array of bytes.
		/// </summary>
		public static uint GetUint(byte[] value) {
			byte[] nvalue = value;
			if (value.Length < 4 || value.Length > 4) {
				nvalue = new byte[4];
				Array.Copy(value, nvalue, 4);
			}
			return BitConverter.ToUInt32(nvalue, 0);
		}

		/// <summary>
		/// Update the Packet Header of Defined Packet.
		/// </summary>
		public static bool FixHeader(ref byte[] data) {
			if (data != null && data.Length > 2) {
				byte[] DataLength = BitConverter.GetBytes(data.Length - 2);
				data[0] = DataLength[0];
				data[1] = DataLength[1];
				return true;
			}
			return false;
		}

		/// <summary>
		/// Add a Header to defined Packet.
		/// </summary>
		public static bool AddHeader(ref byte[] data) {
			if (data != null && data.Length > 0) {
				var nData = new byte[data.Length + 2];
				Array.Copy(data, 0, nData, 2, data.Length);

				byte[] DataLength = BitConverter.GetBytes(data.Length);
				nData[0] = DataLength[0];
				nData[1] = DataLength[1];
				data = nData; return true;
			}
			data = new byte[2]; return false;

		}

		/// <summary>
		/// Resize the defined Packet.
		/// </summary>
		public static bool Resize(ref byte[] data, int length) {
			if (data != null && data.Length > 0) {
				var nData = new byte[length];
				Array.Copy(data, 0, nData, 0, length);
				data = nData;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Remove the last empty bytes of defined Packet.
		/// </summary>
		public static bool RemoveLastEmptyBytes(ref byte[] data) {
			if (data != null && data.Length > 0) {
				for (int i = data.Length - 1; i > -1; i--) {
					if (data[i] != 0) {
						var  nData = new byte[i + 1];
						Array.Copy(data, 0, nData, 0, i + 1);
						data = nData; return true;
					}
				}
			}
			return false;
		}
		#endregion

        /// <summary>
        /// Gets the packets.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static List<Packet> GetPackets(byte[] data, ConnectionProvider connection, PacketSource source) {
            var packets = new List<Packet>();
            if (data.Length > (int)(new[] { data[0], data[1] }.GetTheLong() + 2)) {

                //Is a muti-packet, get all packets.
                var position = 0; while (position < data.Length) {

                    //Get the Size and increment position.
                    var dataSize = (int)new[] { data[0 + position], data[1 + position] }.GetTheLong();

                    if (dataSize > 0 && dataSize < (data.Length + position)) {
                        //Get the Data of this Multiple Packet.
                        var fireData = new byte[dataSize + 2];
                        Array.Copy(data, position, fireData, 0, fireData.Length);

                        //Store the new packet with the partial byte array.
                        var innerPacket = PacketFactory.BindPacket(fireData, DateTime.Now, connection, source);
                        packets.Add(innerPacket);

                        //Increment Position to Get the next Packet.
                        position += fireData.Length;
                    }
                    else { position = data.Length; }
                }
            }
            else {
                //Fire the Packet Changed Event.
                var innerPacket = PacketFactory.BindPacket(data, DateTime.Now, connection, source);
                packets.Add(innerPacket);
            }
            return packets;
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static List<byte[]> GetPacketData(byte[] data) {
            var packets = new List<byte[]>();
            if (data.Length > (int)(new[] { data[0], data[1] }.GetTheLong() + 2)) {

                //Is a muti-packet, get all packets.
                var position = 0; while (position < data.Length) {

                    //Get the Size and increment position.
                    var dataSize = (int)new[] { data[0 + position], data[1 + position] }.GetTheLong();

                    if (dataSize > 0 && dataSize < (data.Length + position)) {
                        //Get the Data of this Multiple Packet.
                        var fireData = new byte[dataSize + 2];
                        Array.Copy(data, position, fireData, 0, fireData.Length);
                        packets.Add(fireData);

                        //Increment Position to Get the next Packet.
                        position += fireData.Length;
                    }
                    else { position = data.Length; }
                }
            }
            else if (data.Length < (int)(new[] { data[0], data[1] }.GetTheLong() + 2)) {
                return packets;
            }
            else {
                //Store the original byte array.
                packets.Add(data);
            }
            return packets;
        }

		/// <summary>
		/// Return the String Representation.
		/// </summary>
		public override string ToString() {
			if (Packet != null && Packet.Data != null && Packet.Data.Length > 0) {
				return Packet.Data.GetString(false);
			}
            return base.ToString();
		}
	}
}
