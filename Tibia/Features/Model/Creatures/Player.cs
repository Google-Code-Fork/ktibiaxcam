using System;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Contracts;
using Tibia.Features.Model.Items;
using Tibia.Features.Model.List;
using Tibia.Features.Structures;

namespace Tibia.Features.Model {
	public class Player : Creature, IPlayer {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="connection">The connection source.</param>
		public Player(ConnectionProvider connection) : base(connection, 0) {
            var battlelist = BattleList.Get(connection, new Range());
            base.Initialize(battlelist.FindAddress(Id));
		}
        
		#region "[rgn] Public Properties "
		/// <summary>
		/// Verify if Player is Connected.
		/// </summary>
		public bool IsConnected {
			get { uint Value; Memory.Reader.Uint(Memory.Addresses.Player.InGame, 4, out Value); if (Value == 8) { return true; } else { return false; } }
            set { uint Value = value == false ? (uint)0 : (uint)8; Memory.Writer.Uint(Memory.Addresses.Player.InGame, Value, 1); }
		}
		/// <summary>
		/// Get the Static Player Id.
		/// </summary>
		public override uint Id {
			get { uint Value; Memory.Reader.Uint(Memory.Addresses.Player.ID, 4, out Value); return Value; }
		}
		/// <summary>
		/// Exp Ammount.
		/// </summary>
		public uint Exp {
			get { uint Value; Memory.Reader.Uint(Memory.Addresses.Player.Exp, 4, out Value); return Value; }
			set { Memory.Writer.Uint(Memory.Addresses.Player.Exp, value, 4); }
		}
		/// <summary>
		/// Capacity Ammount.
		/// </summary>
		public uint Cap {
			get { uint Value; Memory.Reader.Uint(Memory.Addresses.Player.Cap, 2, out Value); return Value; }
			set { Memory.Writer.Uint(Memory.Addresses.Player.Cap, value, 2); }
		}
		/// <summary>
		/// Available Flags.
		/// </summary>
		public Flags Flags {
			get { uint Value; Memory.Reader.Uint(Memory.Addresses.Player.Flags, 1, out Value); return new Flags(Value); }
			set { if (Flags.GetHashCode() != 99) { Memory.Writer.Uint(Memory.Addresses.Player.Flags, value.ID, 1); } }
		}
		/// <summary>
		/// Soul Points.
		/// </summary>
		public PointsMax Soul {
			get {
				uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.Soul, 1, out Pts);
				return new PointsMax(Pts, 0);
			}
			set {
				Memory.Writer.Uint(Memory.Addresses.Player.Soul, value.Quantity, 1);
			}
		}
        /// <summary>
        /// Soul Points.
        /// </summary>
        public PointsMax Stamina {
            get {
                uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.Stamina, 4, out Pts);
                return new PointsMax(Pts, 0);
            }
            set {
                Memory.Writer.Uint(Memory.Addresses.Player.Stamina, value.Quantity, 4);
            }
        }
		/// <summary>
		/// Mana Points.
		/// </summary>
		public PointsMax Mana {
			get {
				uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.Mana, 2, out Pts);
				uint Max; Memory.Reader.Uint(Memory.Addresses.Player.Mana_Max, 2, out Max);
				return new PointsMax(Pts, Max);
			}
			set {
				Memory.Writer.Uint(Memory.Addresses.Player.Mana, value.Quantity, 2);
				Memory.Writer.Uint(Memory.Addresses.Player.Mana_Max, value.Max, 2);
			}
		}
		/// <summary>
		/// Hit Points.
		/// </summary>
		public PointsMax Hp {
			get {
				uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.HP, 2, out Pts);
				uint Max; Memory.Reader.Uint(Memory.Addresses.Player.HP_Max, 2, out Max);
				return new PointsMax(Pts, Max);
			}
			set {
				Memory.Writer.Uint(Memory.Addresses.Player.HP, value.Quantity, 2);
				Memory.Writer.Uint(Memory.Addresses.Player.HP_Max, value.Max, 2);
			}
		}
		/// <summary>
		/// Player Level.
		/// </summary>
		public PointsLeft Level {
			get {
				uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.Level, 2, out Pts);
				uint Lft; Memory.Reader.Uint(Memory.Addresses.Player.Level_Perc, 2, out Lft);
				return new PointsLeft(Pts, Lft);
			}
			set {
				Memory.Writer.Uint(Memory.Addresses.Player.Level, value.Value, 2);
				Memory.Writer.Uint(Memory.Addresses.Player.Level_Perc, value.Left, 2);
			}
		}
		/// <summary>
		/// Player Magic Level.
		/// </summary>
		public PointsLeft MagicLevel {
			get {
				uint Pts; Memory.Reader.Uint(Memory.Addresses.Player.MLevel, 2, out Pts);
				uint Lft; Memory.Reader.Uint(Memory.Addresses.Player.MLevel_Perc, 2, out Lft);
				return new PointsLeft(Pts, Lft);
			}
			set {
				Memory.Writer.Uint(Memory.Addresses.Player.MLevel, value.Value, 2);
				Memory.Writer.Uint(Memory.Addresses.Player.MLevel_Perc, value.Left, 2);
			}
		}
		#endregion

        #region "[rgn] Public Actions    "
        /// <summary>
        /// Drink the Defined Fluid.
        /// </summary>
        public void Drink(Slot SlotSource, bool DropAfterDrink) {
            if (SlotSource.Id == InventoryID.Container) {
                Actions.Use.OnPlayer(SlotSource, Location, DropAfterDrink);
            }
        }
        /// <summary>
        /// Drink the Defined Fluid as HotKey.
        /// </summary>
        public void Drink(uint FluidID) {
            Actions.HotKey.DrinkFluid(FluidID);
        }
        /// <summary>
        /// Stop all Player Actions.
        /// </summary>
        public void Stop() {
            Actions.Position.Stop();
        }
        /// <summary>
        /// Logout the Player.
        /// </summary>
        public void Logout() {
            Actions.Position.Logout();
        }
        /// <summary>
        /// Move the Player to Defined Location.
        /// </summary>
        public void Go(Location To) {
            Actions.Position.Go(To);
        }
        /// <summary>
        /// Turn the Player to Defined Direction.
        /// </summary>
        public void Turn(uint Direction) {
            Actions.Position.Turn(Direction);
        }
        /// <summary>
        /// Walk one SQM to the Defined Direction.
        /// </summary>
        public void Walk(uint Direction) {
            Actions.Position.Walk(Direction);
        }
        /// <summary>
        /// Change the Current Attack Mode.
        /// </summary>
        public void SetAttackMode(AttackMode Mode) {
            Actions.Attack.SetMode(Mode);
        }
        /// <summary>
        /// Send a Message on Default Channel.
        /// </summary>
        public void SayOnDefault(string Message) {
            Actions.Message.Server.Default(Message);
        }
        /// <summary>
        /// Send a Message on Default Channel.
        /// </summary>
        public void YellsOnDefault(string Message) {
            Actions.Message.Server.Default(Message, MessageType.Yell);
        }
        /// <summary>
        /// Send a Message on Default Channel.
        /// </summary>
        public void WhisperOnDefault(string Message) {
            Actions.Message.Server.Default(Message, MessageType.Whisper);
        }
        /// <summary>
        /// Send a Message to defined Player.
        /// </summary>
        public void SendPM(string PlayerName, string Message) {
            Actions.Message.Server.Private(PlayerName, Message);
        }
        /// <summary>
        /// Send a Message on Trade Channel.
        /// </summary>
        public void SayOnTrade(string Message) {
            Actions.Message.Server.Trade(Message);
        }
        /// <summary>
        /// Opens the backpack inventory container.
        /// </summary>
        public void OpenBodyContainer() {
            var slot = this.Inventory.Backpack;
            if (slot.Item != null && slot.Item.Id > 0) {
                slot.Item.Use(false);
            }
        }
        #endregion

        /// <summary>
        /// Get or Set the Creature Target.
        /// </summary>
        public Creature Target {
            get {
                uint tID = Memory.Reader.Uint(Memory.Addresses.Player.Target_ID);
                if (tID > 0) {
                    var BL = new BattleList(Connection, new Range());
                    return new Creature(Connection, BL.FindAddress(tID));
                }
                return null;
            }
            set { Memory.Writer.Uint(Memory.Addresses.Player.Target_ID, value.Id, 4); }
        }

        /// <summary>
        /// Player Skill Points.
        /// </summary>
        public Skills SkillPoints {
            get { return new Skills(Memory); }
        }

        /// <summary>
        /// Player Body Slots.
        /// </summary>
        public Inventory Inventory {
            get { return new Inventory(Connection); }
        }

        /// <summary>
        /// Opened Container Collection.
        /// </summary>
        public ContainerCollection Containers {
            get { return new ContainerCollection(Connection); }
        }

        /// <summary>
        /// Return the Necessary Exp to Get the Next Level.
        /// </summary>
        /// <returns></returns>
        public uint ExpToNextLevel() {
            try {
                uint nLevel = Level.Value + 1;
                uint NeedeD = Convert.ToUInt32(((50 * nLevel * nLevel * nLevel) / 3 - 100 * nLevel * nLevel + (850 * nLevel) / 3 - 200));
                NeedeD = NeedeD - Exp;
                return NeedeD;
            }
            catch { return 0; }
        }

		/// <summary>
		/// Return the Name of the Player.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return Name;
		}
	}
}
