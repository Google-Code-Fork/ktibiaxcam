using System;
using Tibia.Connection.Model;
using Keyrox.Shared.Extensions;

namespace Tibia.Connection.Common {
    public static class Extensions {

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static string ReadString(this Packet packet, int index) {
            if ((index + 3) < packet.Data.Length) {
                var length = new byte[] { packet.Data[index], packet.Data[index + 1] }.GetTheLong();
                var res = new byte[length];
                Array.Copy(packet.Data, index + 2, res, 0, length);
                return res.GetString(false);
            }
            return string.Empty;
        }

        /// <summary>
        /// Reads the long.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static uint ReadLong(this Packet packet, int index) {
            if ((index + 4) < packet.Data.Length) {
                return new byte[] { packet.Data[index], packet.Data[index + 1], packet.Data[index + 2], packet.Data[index + 3] }.GetTheLong();
            }
            return 0x0;
        }

        /// <summary>
        /// Reads the short.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static uint ReadShort(this Packet packet, int index) {
            if ((index + 2) < packet.Data.Length) {
                return new byte[] { packet.Data[index], packet.Data[index + 1] }.GetTheLong();
            }
            return 0x0;
        }

        /// <summary>
        /// Reads the int.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static int ReadInt(this Packet packet, int index) {
            if (index < packet.Data.Length) {
                return Convert.ToInt32(packet.Data[index]);
            }
            return 0x0;
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] ReadBytes(this Packet packet, int index, int length) {
            if ((index + length) < packet.Data.Length) {
                var res = new byte[length];
                Array.Copy(packet.Data, index, res, 0, length);
                return res;
            }
            return null;
        }
    }
}
