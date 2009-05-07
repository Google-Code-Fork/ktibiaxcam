using System.Collections.Generic;
using Tibia.Connection;
using Tibia.Features.Model;
using Tibia.Features.Model.Items;
using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Connection.Providers;

namespace Tibia.Features.Actions.Items {
    public class Fish {
        /// <summary>
        /// Default Object Constructor.
        /// </summary>
        /// <param name="connection">Connection Source.</param>
        public Fish(ConnectionProvider connection) {
            Memory = connection.Memory;
            Connection = connection;
        }

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public TibiaMemoryProvider Memory { get; set; }
        #endregion

        /// <summary>
        /// Just to analyse the Get Tiles With Fish Operation.
        /// </summary>
        //public void OnSQMWithFish() {
        //    if (!Map.IsBusy) {
        //        var FishingRod = new Item();
        //        var Worms = new Item();
        //        var nPlayer = new Model.Player(Connection);

        //        var Warn = new Messages.System(Connection);
        //        var UseActions = new Use(Connection);

        //        var Tiles = new List<MapReader.TileObject>();
        //        if (nPlayer.Containers.SearchItem(Worms.Id) == null) {
        //            //TODO: Stop Actions?
        //            Warn.Send("Warning: Auto Fisher couldn't find any worms.", SystemMsgColor.Red);
        //            return;
        //        }


        //        if (nPlayer.Containers.SearchItem(FishingRod.Id) == null) {
        //            Warn.Send("Warning: Auto Fisher couldn't find Fishing Rod.",
        //                      SystemMsgColor.Red);
        //            return;
        //        }
        //        int nDist;
        //        var XXX = 1;
        //        do {
        //            var YYY = 1;
        //            do {
        //                var TileObjects = Map.GetTileObjects(XXX, YYY,
        //                                                                        Map.WorldZToClientZ(
        //                                                                            int.Parse(
        //                                                                                nPlayer.Location.Z.ToString())));
        //                if (TileObjects.Length == 1) {
        //                    int TileID = TileObjects[0].GetObjectID;
        //                    if ((TileID >= 0x11f5) & (TileID <= 0x11fa)) {
        //                        Tiles.Add(TileObjects[0]);
        //                    }
        //                }
        //                YYY++;
        //                nDist = 11;
        //            } while (YYY <= nDist);
        //            XXX++;
        //            nDist = 15;
        //        } while (XXX <= nDist);
        //        if (Tiles.Count > 0) {
        //            //Randomize the Tile Collection.
        //            //Proxy.SendPacketToServer(PacketUtils.UseFishingRodOnLocation(FishingRodItemData, Tile.GetMapLocation, (uint)Tile.GetObjectID));

        //            var Tile = Tiles[0];
        //            var FishingRodSlot = nPlayer.Containers.SearchItem(FishingRod.Id);
        //            var SQMWithFish = new Location(uint.Parse(Tile.GetMapLocation.X.ToString()),
        //                                           uint.Parse(Tile.GetMapLocation.Y.ToString()),
        //                                           uint.Parse(Tile.GetMapLocation.Z.ToString()));
        //            UseActions.OnGround(FishingRodSlot, SQMWithFish, uint.Parse(Tile.GetObjectID.ToString()),
        //                                uint.Parse(Tile.GetStackPosition.ToString()));

        //            //Send the Packet With according byte the above parameters.
        //        }
        //    }
        //}
    }
}