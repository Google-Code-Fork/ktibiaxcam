using System;
using System.Collections.Generic;
using Tibia.Connection;
using Tibia.Memory;
using Tibia.Features.Structures;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Messages {
    public class System {
        /// <summary>
        /// Initializes a new instance of the <see cref="System"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public System(ConnectionProvider connection) {
            this.Connection = connection;
        }

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
        #endregion

        #region "[rgn] Channel Methods   "
        /// <summary>
        /// Open the specified Channel.
        /// </summary>
        /// <param name="channel"></param>
        public void OpenChannel(InternalChannels channel) {

            #region "[rgn] Packet Structure Analyze "
            //---------------------------------------
            //.  .  ID ID .  SZ .  H  e  l  p  .  .  
            //---------------------------------------
            //0B 00 AC 0A 00 06 00 4B 65 79 58 00 00 
            //---------------------------------------
            //00 01 02 03 04 05 06 07 08 09 10 11 12 
            #endregion

            PacketBuilder Builder = new PacketBuilder(0xAC, Connection);
            Builder.Append(GetChannelInfo(channel).Key, 2);
            Builder.Append(GetChannelInfo(channel).Value, true, true);

            Builder.SetPacketSource(PacketSource.Server);
            Connection.Send(Builder.GetPacket());
        }
        /// <summary>
        /// Send Message to defined Internal Channel.
        /// </summary>
        public void SendToChannel(InternalChannels channel, CustomMessageType color, string charname, uint level, string message) {

            #region "[rgn] Packet Structure Analyze "
            //--------------------------------------------------------------------------------------------------------------
            //SZ    ID ?? ?? ?? ?? SZ    [                   NAME                ] LV    CR CH    SZ    [        MSG       ]
            //--------------------------------------------------------------------------------------------------------------
            //23 00 AA 00 00 00 00 0E 00 4F 6D 65 67 61 20 4C 61 6D 62 61 20 4C 61 04 00 0C 0A 00 07 00 68 65 6C 6C 6F 00 00
            //.  .  .  .  S  .  .  .  .  N  i  g  h  t  .  C  r  e  a  t  u  r  e  .  .  .  .  .  .  .  H  e  l  l  o  .  . 
            //--------------------------------------------------------------------------------------------------------------
            //00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36
            //--------------------------------------------------------------------------------------------------------------
            #endregion

            PacketBuilder Builder = new PacketBuilder(0xAA, Connection);
            Builder.Append(0x00);
            Builder.Append(0x00);
            Builder.Append(0x00);
            Builder.Append(0x00);
            Builder.Append(charname, true);
            Builder.Append(level, 2);
            Builder.Append(color.GetHashCode());
            Builder.Append(GetChannelInfo(channel).Key, 2);
            Builder.Append(message, true, true);

            Builder.SetPacketSource(PacketSource.Server);
            Connection.Send(Builder.GetPacket());
        }
        /// <summary>
        /// Send Message to defined Internal Channel.
        /// </summary>
        public void SendToChannel(InternalChannels channel, CustomMessageType color, string message) {
            SendToChannel(channel, color, "KTibiaX®", 0x0, message);
        }
        /// <summary>
        /// Get Channel Information.
        /// </summary>
        /// <returns>KeyValuePair<ChannelID, ChannelName></returns>
        public KeyValuePair<uint, string> GetChannelInfo(InternalChannels channel) {
            switch (channel) {

                case InternalChannels.Command:
                    return new KeyValuePair<uint, string>(0x1A, "Command®");

                case InternalChannels.TradeWatcher:
                    return new KeyValuePair<uint, string>(0x1B, "MyTrade®");
            }
            return new KeyValuePair<uint, string>(0, "");
        }
        #endregion

        /// <summary>
        /// Send a System Message to Client.
        /// </summary>
        public byte[] Send(string message, SystemMsgColor color) {

            #region "[rgn] Packet Structure Analyze "
            //---------------------------
            //07 00 B4 12 03 00 69 61 65 
            //---------------------------
            //SZ    ID CR SZ    i  a  e  
            //---------------------------
            #endregion

            PacketBuilder Builder = new PacketBuilder(0xB4, Connection);
            Builder.Append(color.GetHashCode());
            Builder.Append(message, true, true);
            Builder.SetPacketSource(PacketSource.Server);
            Connection.Send(Builder.GetPacket());
            return Builder.GetPacket().Data;
        }

        /// <summary>
        /// Send a Animated Message to Client.
        /// </summary>
        public byte[] SendAnimated(AnimatedMsgColor color, Location sqm, string message) {

            #region "[rgn] Packet Structure Analyze "
            #endregion

            PacketBuilder Builder = new PacketBuilder(0x84, Connection);
            Builder.Append(sqm.X, 2);
            Builder.Append(sqm.Y, 2);
            Builder.Append(sqm.Z, 1);
            Builder.Append(color.GetHashCode(), 1);
            Builder.Append(message, true);
            Builder.SetPacketSource(PacketSource.Server);
            Connection.Send(Builder.GetPacket());
            return Builder.GetPacket().Data;
        }
    }
}
