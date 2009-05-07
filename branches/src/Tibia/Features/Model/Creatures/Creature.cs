using System;
using System.Linq;
using System.ComponentModel;
using Tibia.Connection.Providers;
using Tibia.Features.Actions.Messages;
using Tibia.Features.Model.Contracts;
using Tibia.Features.Model.Items;
using Tibia.Features.Structures;
using Tibia.Memory;
using Tibia.Features.Common;
using Keyrox.Shared.Objects;

namespace Tibia.Features.Model {
    public class Creature : ICreature {
        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="address">The address.</param>
        public Creature(ConnectionProvider connection, uint address) {
            Connection = connection;
            Initialize(address);
        }

        /// <summary>
        /// Initializes the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void Initialize(uint address) {
            Address = address;
            Memory.Addresses.Creature.Pointer = Address;
        }

        #region "[rgn] Public Properties "
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        [Browsable(false)]
        public ConnectionProvider Connection { get; set; }
        private TibiaMemoryProvider memory;
        /// <summary>
        /// Gets the memory.
        /// </summary>
        /// <value>The memory.</value>
        [Browsable(false)]
        public TibiaMemoryProvider Memory {
            get {
                if (memory == null) {
                    memory = Connection.Memory;
                }
                if (Address > 0) memory.Addresses.Creature.Pointer = Address;
                return memory;
            }
        }
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
        [Category("Data")]
        public uint Address { get; private set; }
        /// <summary>
        /// Get the actions provider.
        /// </summary>
        /// <value>The actions.</value>
        [Browsable(false)]
        public Tibia.Features.Providers.ActionProvider Actions {
            get { return new Tibia.Features.Providers.ActionProvider(Connection); }
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Category("Data")]
        public string Name {
            get { return Memory.Reader.String(Memory.Addresses.Creature.Name); }
            set { Memory.Writer.String(Memory.Addresses.Creature.Name, value, true); }
        }
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        [Category("Data")]
        public virtual uint Id {
            get { uint Value; Memory.Reader.Uint(Address, 4, out Value); return Value; }
        }
        /// <summary>
        /// Gets or sets the hp bar.
        /// </summary>
        /// <value>The hp bar.</value>
        [Category("Points")]
        public uint HpBar {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.HPBar, 1, out Value); return Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.HPBar, value, 1); }
        }
        /// <summary>
        /// Gets or sets the walk speed.
        /// </summary>
        /// <value>The walk speed.</value>
        [Category("Points")]
        public uint WalkSpeed {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.WalkSpeed, 2, out Value); return Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.WalkSpeed, value, 2); }
        }
        /// <summary>
        /// Gets or sets the outfit.
        /// </summary>
        /// <value>The outfit.</value>
        [Category("Appearance")]
        public OutfitKind Outfit {
            get { uint res; Memory.Reader.Uint(Memory.Addresses.Creature.OutFit, 2, out res); return (OutfitKind)res; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.OutFit, value.GetHashCode().ToUInt32(), 2); }

        }
        /// <summary>
        /// Gets or sets the addon.
        /// </summary>
        /// <value>The addon.</value>
        [Category("Appearance")]
        public uint Addon {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.AddOn, 1, out Value); return Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.AddOn, value, 1); }

        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is walking.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is walking; otherwise, <c>false</c>.
        /// </value>
        [Category("Appearance")]
        public bool IsWalking {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.IsWalking, 1, out Value); return Convert.ToBoolean(Value); }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.IsWalking, Convert.ToUInt32(value), 1); }

        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        [Category("Appearance")]
        public bool IsVisible {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.IsVisible, 1, out Value); return Convert.ToBoolean(Value); }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.IsVisible, Convert.ToUInt32(value), 1); }
        }
        /// <summary>
        /// Gets or sets the skull.
        /// </summary>
        /// <value>The skull.</value>
        [Category("Appearance")]
        public Skull Skull {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.Skull, 1, out Value); return (Skull)Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Skull, Convert.ToUInt32(value.GetHashCode()), 1); }
        }
        /// <summary>
        /// Gets or sets the party.
        /// </summary>
        /// <value>The party.</value>
        [Category("Appearance")]
        public PartyRank Party {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.Party, 1, out Value); return (PartyRank)Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Party, Convert.ToUInt32(value.GetHashCode()), 2); }
        }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Category("Data"), ReadOnly(true)]
        public CreatureType Type {
            get { uint Value; Memory.Reader.Uint(Memory.Addresses.Creature.Type, 1, out Value); return (CreatureType)Value; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Type, Convert.ToUInt32(value.GetHashCode()), 1); }
        }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [Category("Location")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Location Location {
            get {
                var loc = new Location(LocationX, LocationY, LocationZ, Direction);
                loc.LocationChanged += Creature_LocationChanged;
                return loc;
            }
            set {
                LocationX = value.X;
                LocationY = value.Y;
                LocationZ = value.Z;
                Direction = value.Direction;
            }
        }
        /// <summary>
        /// Gets or sets the location X.
        /// </summary>
        /// <value>The location X.</value>
        [Category("Location")]
        public uint LocationX {
            get { uint res; Memory.Reader.Uint(Memory.Addresses.Creature.X, 2, out res); return res; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.X, value, 2); }
        }
        /// <summary>
        /// Gets or sets the location Y.
        /// </summary>
        /// <value>The location Y.</value>
        [Category("Location")]
        public uint LocationY {
            get { uint res; Memory.Reader.Uint(Memory.Addresses.Creature.Y, 2, out res); return res; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Y, value, 2); }
        }
        /// <summary>
        /// Gets or sets the location Z.
        /// </summary>
        /// <value>The location Z.</value>
        [Category("Location")]
        public uint LocationZ {
            get { uint res; Memory.Reader.Uint(Memory.Addresses.Creature.Z, 1, out res); return res; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Z, value, 1); }
        }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        [Category("Location")]
        public uint Direction {
            get { uint res; Memory.Reader.Uint(Memory.Addresses.Creature.Direction, 1, out res); return res; }
            set { Memory.Writer.Uint(Memory.Addresses.Creature.Direction, value, 1); }
        }

        /// <summary>
        /// Gets or sets the light.
        /// </summary>
        /// <value>The light.</value>
        [Category("Points")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Light Light {
            get {
                uint Intensity; Memory.Reader.Uint(Memory.Addresses.Creature.Light, 1, out Intensity);
                uint Color; Memory.Reader.Uint(Memory.Addresses.Creature.LightColor, 1, out Color);
                return new Light(Intensity, Color);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Creature.Light, value.Intensity, 1);
                Memory.Writer.Uint(Memory.Addresses.Creature.LightColor, value.Color, 1);
            }
        }
        #endregion

        #region "[rgn] Public Actions    "
        /// <summary>
        /// Follow this Creature.
        /// </summary>
        public void Follow() {
            Actions.Position.Follow(Id);
        }
        /// <summary>
        /// Shoot a Rune against this Creature.
        /// </summary>
        public void Shoot(Slot RuneSlot) {
            Actions.Attack.Shoot(RuneSlot, this);
        }
        /// <summary>
        /// Shoot a Rune against this Creature as Hotkey.
        /// </summary>
        public void Shoot(uint RuneID) {
            Actions.HotKey.Shoot(Id, RuneID);
        }
        /// <summary>
        /// Attack this Creature.
        /// </summary>
        public void Attack() {
            Actions.Attack.Creature(this);
        }
        /// <summary>
        /// Send a Private Message to this Creature.
        /// </summary>
        public void SendMessage(string Message) {
            Actions.Message.Server.Private(Name, Message);
        }
        #endregion

        /// <summary>
        /// Get the distance in SQM's from Defined Player.
        /// </summary>
        /// <param name="Player">The player.</param>
        /// <returns></returns>
        public Distance Distance(Player Player) {
            Distance Value = new Distance();

            if (Location.X > Player.Location.X) { Value.X = Location.X - Player.Location.X; }
            else { Value.X = Player.Location.X - Location.X; }

            if (Location.Y > Player.Location.Y) { Value.Y = Location.Y - Player.Location.Y; }
            else { Value.Y = Player.Location.Y - Location.Y; }

            Value.XY = Value.X + Value.Y; return Value;
        }

        /// <summary>
        /// Handles the LocationChanged event of the Creature control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KTibiaX.Analyzer.Events.LocationEventArgs"/> instance containing the event data.</param>
        private void Creature_LocationChanged(object sender, Tibia.Features.Events.LocationEventArgs e) {
            this.Location = e.Location;
        }

        /// <summary>
        /// Returns the Creature Name that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return Name;
        }
    }
}
