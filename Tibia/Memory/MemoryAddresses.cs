using System;
using Tibia.Memory.Address;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory {
    public class MemoryAddresses {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressProvider"/> class.
        /// </summary>
        public MemoryAddresses(AddressDTO dto) {
            Parse(dto);
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; private set; }

        /// <summary>
        /// Gets the num version.
        /// </summary>
        /// <value>The num version.</value>
        public decimal NumVersion {
            get {
                if (Version != string.Empty) {
                    return Convert.ToDecimal(Version.Replace(".", ","));
                }
                else { return 0; }
            }
        }

        #region "[rgn] Memory Address "
        public IBattleList BattleList { get; set; }
        public IClient Client { get; set; }
        public IContainers Containers { get; set; }
        public ICreature Creature { get; set; }
        public IDatItem DatItem { get; set; }
        public IInventory Inventory { get; set; }
        public IMap Map { get; set; }
        public IPlayer Player { get; set; }
        public ISkills Skills { get; set; }
        public ISpyLevel SpyLevel { get; set; }
        public IVipList VipList { get; set; }
        #endregion

        /// <summary>
        /// Parses the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        private void Parse(AddressDTO dto) {
            this.Version = dto.ClientVersion;

            this.Containers = new Containers() {
                StarT = dto.ContainerBegin
            };
            this.Inventory = new Inventory() {
                Head = dto.HeadInventory
            };
            this.Map = new Map() {
                Pointer = dto.MapPointer,
                FullLight = dto.FullLight,
                FullLightNop = dto.FullLightNop
            };
            this.Player = new Player() {
                PlayerZ = dto.PlayerZ,
                Exp = dto.Exp,
                InGame = dto.InGame,
                Target_ID = dto.RedSquare
            };
            this.VipList = new VipList() {
                VipBegin = dto.VipBegin
            };
            this.SpyLevel = new SpyLevel() {
                LevelSpy1 = dto.LevelSpy1,
                LevelSpy2 = dto.LevelSpy2,
                LevelSpy3 = dto.LevelSpy3,
                LevelSpyPtr = dto.LevelSpyPtr,
                NameSpy1 = dto.NameSpy1,
                NameSpy2 = dto.NameSpy2,
            };
            this.Client = new Address.Client() {
                XTeaKey = dto.XTeaKey,
                RSAKey = dto.RSAKey,
                DatPointer = dto.DatPointer,
                FrameRateBegin = dto.FrameRateBegin,
                MultiClient = dto.MapPointer,
                SafeMode = dto.SafeMode,
                LoginCharList = dto.LoginCharList,
                LoginServerStart = dto.LoginServerStart,
                LoginSelectedChar = dto.LoginSelectedChar,
                PrintName = dto.PrintName,
                PrintFPS = dto.PrintFPS,
                howFPS = dto.howFPS,
                NopFPS = dto.NopFPS,
                PrintTextFunc = dto.PrintTextFunc,
            };
            this.BattleList = new BattleList(Player.Exp) {
                RedSQuare = dto.RedSquare
            };
            this.Creature = new Creature();
            this.DatItem = new DatItem();
            this.Skills = new Skills(Player.Exp);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return Version;
        }

    }
}
