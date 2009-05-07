using System;
using System.Collections.Generic;

namespace Tibia.Features.Actions.Messages {
	public class Helper {
		/// <summary>
		/// return a KeyValuePair<Username, Message>.
		/// </summary>
		/// <param name="packet">Arrived Trade Mesage Packet.</param>
		public static KeyValuePair<string, string> GetArivedMsgInfo(byte[] packet) {
            try {
                #region " Packet Structure Analyze "
                //------------------------------------------------------------------------------------
                //SZ    ID             SZ    NM                         SZ    MS                      
                //------------------------------------------------------------------------------------
                //1A 00 AA 00 00 00 00 09 00 5A 65 20 47 6F 69 61 62 41 32 00 05 05 00 03 00 61 65 77 
                //.  .  .  .  .  .  .  .  .  Z  e  .  G  o  i  a  b  A  2  .  .  .  .  .  .  a  e  w  
                //00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 
                #endregion

                int nameSize = Convert.ToInt32(new[] { packet[7], packet[8] }.GetTheLong());
                int msgSize = Convert.ToInt32(new[] { packet[10 + nameSize + 4], packet[10 + nameSize + 5] }.GetTheLong());

                byte[] nameBytes = new byte[nameSize];
                byte[] msgBytes = new byte[msgSize];

                Array.Copy(packet, 9, nameBytes, 0, nameSize);
                Array.Copy(packet, 10 + nameSize + 6, msgBytes, 0, msgSize);

                string userName = nameBytes.GetString(false);
                string message = msgBytes.GetString(false);
                return new KeyValuePair<string, string>(userName.Replace(".", " "), message.Replace(".", " ").Replace("  ", ""));
            }
            catch (ArgumentException) { return new KeyValuePair<string, string>(); }
		}
	}
}
