using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Tibia.Client;
using Keyrox.Shared.Controls;
using Tibia.Connection.Providers;
using Keyrox.Shared.Objects;
using Keyrox.Shared.Files;
using Tibia.Connection.Model;

namespace KTibiaX.Cam.Movie {
    [Serializable]
    public class MovieChapter {

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieChapter"/> class.
        /// </summary>
        public MovieChapter() {
        }

        #region "[rgn] Properties "
        [XmlElement]
        public DateTime Time { get; set; }

        [XmlElement]
        public MoviePlayerMirror PlayerMirror { get; set; }

        [XmlElement]
        public byte[] Map { get; set; }

        [XmlElement]
        public byte[] BattleList { get; set; }

        [XmlArray]
        public List<ChapterPacket> Packets { get; set; }
        #endregion

        #region "[rgn] Helpers    "
        public bool LevelSpyOn(int floor, TibiaClient Client) {
            uint playerZ, tempPtr;

            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy1, Client.Memory.Addresses.SpyLevel.Nops);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy2, Client.Memory.Addresses.SpyLevel.Nops);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy3, Client.Memory.Addresses.SpyLevel.Nops);

            tempPtr = Client.Memory.Reader.Uint(Client.Memory.Addresses.SpyLevel.LevelSpyPtr);
            tempPtr += Client.Memory.Addresses.SpyLevel.LevelSpyAdd1;
            tempPtr = Client.Memory.Reader.Uint(tempPtr);
            tempPtr += Client.Memory.Addresses.SpyLevel.LevelSpyAdd2;

            playerZ = Client.Memory.Reader.Uint(Client.Memory.Addresses.Player.PlayerZ);

            if (playerZ <= 7) {
                if (playerZ - floor >= 0 && playerZ - floor <= 7) {
                    playerZ = 7 - playerZ;
                    Client.Memory.Writer.Uint(tempPtr, playerZ + (uint)floor);
                    return true;
                }
            }
            else {
                if (floor >= -2 && floor <= 2 && playerZ - floor < 16) {
                    Client.Memory.Writer.Uint(tempPtr, 2 + (uint)floor);
                    return true;
                }
            }

            return false;
        }
        public void LevelSpyOff(TibiaClient Client) {
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy1, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy2, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
            Client.Memory.Writer.Bytes(Client.Memory.Addresses.SpyLevel.LevelSpy3, Client.Memory.Addresses.SpyLevel.LevelSpyDefault);
        }
        #endregion

        /// <summary>
        /// Inits the chapter.
        /// </summary>
        /// <param name="client">The client.</param>
        public void KeepChapter(TibiaClient client) {

            this.Time = DateTime.Now;
            this.PlayerMirror = new MoviePlayerMirror(client.Features.Player);

            var map = new byte[40320];
            var pointer = client.Memory.Reader.Uint(client.Memory.Addresses.Map.Pointer);
            client.Memory.Reader.Byte(pointer, 40320, out map);
            this.Map = map;

            var blist = new byte[client.Memory.Addresses.BattleList.Ended - client.Memory.Addresses.BattleList.Start];
            client.Memory.Reader.Byte(client.Memory.Addresses.BattleList.Start, (uint)blist.Length, out blist);
            this.BattleList = blist;
        }

        /// <summary>
        /// Starts the chapter.
        /// </summary>
        /// <param name="client">The client.</param>
        public void StartChapter(TibiaClient client) {
            //var pointer = client.Memory.Reader.Uint(client.Memory.Addresses.Map.Pointer);
            //client.Memory.Writer.Bytes(pointer, this.Map);

            /*
             78832c senha
             78834c pass 
             */
            
            client.Memory.Writer.String(0x78832C, "123456", true);
            client.Memory.Writer.String(0x78834C, "ktibiax", true);
            client.Memory.Writer.Uint(client.Memory.Addresses.Player.InGame, 8, 1);

            client.Features.Player.UpdatePlayer(this.PlayerMirror);
            client.Memory.Writer.Bytes(client.Memory.Addresses.BattleList.Start, this.BattleList);
            
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { 
                LevelSpyOn((int)PlayerMirror.Z, client);

                var pack = new System.IO.FileInfo("C:\\firstpack.txt").Read().GetBytes(true);
                client.Connection.Send(new Packet(client.Connection, pack));

            }), 2000);
        }

        /// <summary>
        /// Adds the packet.
        /// </summary>
        /// <param name="data">The data.</param>
        public void AddPacket(byte[] data) {
            if (Packets == null) { Packets = new List<ChapterPacket>(); }
            Packets.Add(new ChapterPacket() { Time = DateTime.Now, Data = data });
        }

    }
}
