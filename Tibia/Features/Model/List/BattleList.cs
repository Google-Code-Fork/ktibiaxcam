using System.Linq;
using Tibia.Features.Structures;
using Tibia.Connection;
using Tibia.Memory;
using Tibia.Connection.Providers;
using Tibia.Features.Common;

namespace Tibia.Features.Model.List {
    public class BattleList {
        /// <summary>
        /// Initializes a new instance of the <see cref="BattleList"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="range">The range.</param>
        public BattleList(ConnectionProvider connection, Range range) {
            if (range.Value > 0) { this.Range = range; } else { this.Range = new Range(RangeType.Screen, 2); }
            this.Connection = connection;
        }

        /// <summary>
        /// Gets or sets the creatures.
        /// </summary>
        /// <value>The creatures.</value>
        public CreatureCollection Creatures { get; set; }

        #region "[rgn] Internal Properties "
        /// <summary>
        /// Process Memory Manager.
        /// </summary>
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
        /// <summary>
        /// Connection Manager.
        /// </summary>
        public ConnectionProvider Connection { get; private set; }
        /// <summary>
        /// Current Owner Player.
        /// </summary>
        public Player MyPlayer { get { return new Player(Connection); } }
        /// <summary>
        /// ID of Current Owner Player.
        /// </summary>
        public uint MyPlayerID { get { return Memory.Reader.Uint(Memory.Addresses.Player.ID); } }
        /// <summary>
        /// Range to Fill Battle List.
        /// </summary>
        public Range Range { get; set; }
        /// <summary>
        /// Get the Total of Creatures in Battle List.
        /// </summary>
        public int Count { get { return GetAll().Count; } }
        #endregion

        #region "[rgn] Public Properties   "
        /// <summary>
        /// The Exacly Location of Current Search Range.
        /// </summary>
        public Location RangeLocation {
            get {
                var nlocal = new Location();
                uint x, y, z;

                switch (Range.Type) {
                    case RangeType.Screen:
                        x = (uint.Parse(Range.Value.ToString()) * 7);
                        y = (uint.Parse(Range.Value.ToString()) * 7);
                        z = (uint.Parse(Range.Value.ToString()) - 1);
                        nlocal = new Location(x, y, z); break;

                    case RangeType.SQM:
                        x = (uint.Parse(Range.Value.ToString()));
                        y = (uint.Parse(Range.Value.ToString()));
                        z = (uint.Parse(Range.Value.ToString()));
                        nlocal = new Location(x, y, z); break;
                }
                return nlocal;
            }
        }
        /// <summary>
        /// Get the Target creature.
        /// </summary>
        public Creature Target {
            get {
                uint nID; Memory.Reader.Uint(Memory.Addresses.BattleList.Target_BList_ID, 4, out nID);
                if (nID > 0) { return new Creature(Connection, FindAddress(SearchType.Id, nID)); }
                return null;
            }
            set { Memory.Writer.Uint(Memory.Addresses.BattleList.Target_BList_ID, value.Id, 4); }
        }
        /// <summary>
        /// Target Type
        /// </summary>
        public CreatureType TargetType {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.BattleList.Target_BList_Type, 1, out Value); return (CreatureType)Value; }
        }
        /// <summary>
        /// Get the creature on Red Square (attacking).
        /// </summary>
        public Creature RedSquare {
            get {
                uint nID; Memory.Reader.Uint(Memory.Addresses.BattleList.RedSQuare, 4, out nID);
                if (nID > 0) { return new Creature(Connection, FindAddress(SearchType.Id, nID)); }
                return null;
            }
            set {
                if (value != null) { Memory.Writer.Uint(Memory.Addresses.BattleList.RedSQuare, value.Id, 4); }
                else { Memory.Writer.Uint(Memory.Addresses.BattleList.RedSQuare, 0x0000, 4); }
            }
        }
        /// <summary>
        /// Get the creature on Green Square (follow).
        /// </summary>
        public Creature GreenSQuare {
            get {
                uint nID; Memory.Reader.Uint(Memory.Addresses.BattleList.GreenSQuare, 4, out nID);
                if (nID > 0) { return new Creature(Connection, FindAddress(SearchType.Id, nID)); }
                return null;
            }
            set {
                if (value != null) { Memory.Writer.Uint(Memory.Addresses.BattleList.GreenSQuare, value.Id, 4); }
                else { Memory.Writer.Uint(Memory.Addresses.BattleList.GreenSQuare, 0x0000, 4); }
            }
        }
        /// <summary>
        /// Get the creature on White Square (mouse over on battle list).
        /// </summary>
        public Creature WhiteSQuare {
            get {
                uint nID; Memory.Reader.Uint(Memory.Addresses.BattleList.WhiteSQuare, 4, out nID);
                if (nID > 0) { return new Creature(Connection, FindAddress(SearchType.Id, nID)); }
                return null;
            }
            set {
                if (value != null) { Memory.Writer.Uint(Memory.Addresses.BattleList.WhiteSQuare, value.Id, 4); }
                else { Memory.Writer.Uint(Memory.Addresses.BattleList.WhiteSQuare, 0x0000, 4); }
            }
        }
        #endregion

        #region "[rgn] Find Creatures      "
        /// <summary>
        /// Get all on BattleList.
        /// </summary>
        public CreatureCollection GetAll() {
            Reload();
            return Creatures;
        }
        /// <summary>
        /// Get all the Players in BattleList.
        /// </summary>
        public CreatureCollection GetPlayers() {
            Reload();
            var list = from creature in Creatures
                       where creature.Type == CreatureType.Player
                       select creature;
            return new CreatureCollection(list.ToList());
        }
        /// <summary>
        /// Get all the Creatures in BattleList.
        /// </summary>
        public CreatureCollection GetCreatures() {
            Reload();
            var list = from creature in Creatures
                       where creature.Type == CreatureType.NPC
                       select creature;
            return new CreatureCollection(list.ToList());
        }
        /// <summary>
        /// Get all white and RedSkulls on BattleList.
        /// </summary>
        public CreatureCollection GetPKs() {
            Reload();
            var list = from creature in Creatures
                       where creature.Skull == Skull.White || creature.Skull == Skull.Red
                       select creature;
            return new CreatureCollection(list.ToList());
        }
        /// <summary>
        /// Get all GM's in BattleList.
        /// </summary>
        public CreatureCollection GetGMs() {
            Reload();
            var list = from creature in Creatures
                       where creature.Name.StartsWith("GM ")
                       select creature;
            return new CreatureCollection(list.ToList());
        }

        /// <summary>
        /// Finds the creature.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Creature FindCreature(string name) {
            var addr = FindAddress(name);
            return addr > 0 ? new Creature(Connection, addr) : null;
        }

        /// <summary>
        /// Finds the creature.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Creature FindCreature(uint id) {
            var addr = FindAddress(id);
            return addr > 0 ? new Creature(Connection, addr) : null;
        }
        #endregion

        #region "[rgn] Initialize List     "
        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload() {
            if (Range.Value == 0) { Range = new Range(RangeType.Screen, 1); }
            uint blBegin = Memory.Addresses.BattleList.Start;
            uint blEnd = Memory.Addresses.BattleList.Ended;
            uint blDist = Memory.Addresses.BattleList.DistC;

            uint maxZ = MyPlayer.Location.Z + RangeLocation.Z;
            uint minZ = MyPlayer.Location.Z - RangeLocation.Z;
            uint maxY = MyPlayer.Location.Y + RangeLocation.Y;
            uint minY = MyPlayer.Location.Y - RangeLocation.Y;
            uint maxX = MyPlayer.Location.X + RangeLocation.X;
            uint minX = MyPlayer.Location.X - RangeLocation.X;
            Creatures = new CreatureCollection();

            for (uint i = blBegin; i < blEnd; i += blDist) {
                if (Memory.Reader.Uint(i) > 0x0) {
                    Creature nCreature = new Creature(Connection, i);
                    bool okZ, okY, okX;
                    if (nCreature.Location.Z >= minZ && nCreature.Location.Z <= maxZ) { okZ = true; } else { okZ = false; }
                    if (nCreature.Location.Y >= minY && nCreature.Location.Y <= maxY) { okY = true; } else { okY = false; }
                    if (nCreature.Location.X >= minX && nCreature.Location.X <= maxX) { okX = true; } else { okX = false; }
                    if (okZ && okY && okX) { Creatures.Add(nCreature); }
                }
                else { return; }
            }
        }

        /// <summary>
        /// Reloads the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        public void Reload(Range range) {
            if (range.Value > 0) { this.Range = range; } else { this.Range = new Range(RangeType.Screen, 2); }
            Reload();
        }

        /// <summary>
        /// Finds the address.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        private uint FindAddress(SearchType Type, object Value) {
            uint blBegin = Memory.Addresses.BattleList.Start;
            uint blEnd = Memory.Addresses.BattleList.Ended;
            uint blDist = Memory.Addresses.BattleList.DistC;

            for (uint i = blBegin; i < blEnd; i += blDist) {
                if (Memory.Reader.Uint(i) > 0x0) {
                    Creature nCreature = new Creature(Connection, i);
                    switch (Type) {
                        case SearchType.Id:
                            uint gID = uint.Parse(Value.ToString());
                            if (nCreature.Id == gID) { return i; } break;

                        case SearchType.Name:
                            if (nCreature.Name == Value.ToString()) { return i; } break;
                    }
                }
                else { break; }
            }
            return 0;
        }

        /// <summary>
        /// Finds the address.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public uint FindAddress(uint id) {
            return FindAddress(SearchType.Id, id);
        }

        /// <summary>
        /// Finds the address.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public uint FindAddress(string name) {
            return FindAddress(SearchType.Name, name);
        }
        #endregion

        /// <summary>
        /// Reveals the invisible creatures.
        /// </summary>
        /// <param name="outfit">The outfit.</param>
        public void RevealInvisibleCreatures(OutfitKind outfit) {
            Reload();
            var invisibleCreatures = from creature in Creatures 
                                     where !creature.IsVisible || 
                                     creature.Outfit == OutfitKind.Invisible
                                     select creature;
            foreach (var creature in invisibleCreatures) {
                creature.Outfit = outfit;
                creature.IsVisible = true;
            }
        }

        /// <summary>
        /// Gets a new BattleList with the defined parameters.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static BattleList Get(ConnectionProvider connection, Range range) {
            return new BattleList(connection, range);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return string.Format("Count: {0}", Count);
        }

    }
}
