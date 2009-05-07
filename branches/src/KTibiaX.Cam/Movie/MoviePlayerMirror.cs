using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Features.Model;
using Tibia.Features.Structures;

namespace KTibiaX.Cam.Movie {
    [Serializable]
    public class MoviePlayerMirror {

        public MoviePlayerMirror() { }

        public MoviePlayerMirror(Player player) {
            Level = player.Level;
            MLevel = player.MagicLevel;
            HP = player.Hp;
            MP = player.Mana;
            SP = player.Soul;
            Stamina = player.Stamina;

            X = player.Location.X;
            Y = player.Location.Y;
            Z = player.Location.Z;

            Head = player.Inventory.Head.Item.Id;
            Armor = player.Inventory.Armor.Item.Id;
            Weapon = player.Inventory.Right.Item.Id;
            Shield = player.Inventory.Left.Item.Id;
            Foots = player.Inventory.Feet.Item.Id;
            Ammo = player.Inventory.Ammo.Item.Id;
            Ring = player.Inventory.Ring.Item.Id;
            Ammulet = player.Inventory.Neck.Item.Id;
            Legs = player.Inventory.Legs.Item.Id;
        }

        public PointsLeft Level { get; set; }
        public PointsLeft MLevel { get; set; }
        public PointsMax HP { get; set; }
        public PointsMax MP { get; set; }
        public PointsMax SP { get; set; }
        public PointsMax Stamina { get; set; }

        public uint X { get; set; }
        public uint Y { get; set; }
        public uint Z { get; set; }

        public uint Head { get; set; }
        public uint Armor { get; set; }
        public uint Weapon { get; set; }
        public uint Shield { get; set; }
        public uint Foots { get; set; }
        public uint Ammo { get; set; }
        public uint Ring { get; set; }
        public uint Ammulet { get; set; }
        public uint Legs { get; set; }
    }

    public static class MoviePlayerMirrorExtesions {

        /// <summary>
        /// Updates the player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="mirror">The mirror.</param>
        public static void UpdatePlayer(this Player player, MoviePlayerMirror mirror) {
            player.Level = mirror.Level;
            player.MagicLevel = mirror.MLevel;
            player.Hp = mirror.HP;
            player.Mana = mirror.MP;
            player.Soul = mirror.SP;
            player.Stamina = mirror.Stamina;

            player.Location = new Location(mirror.X, mirror.Y, mirror.Z);

            player.Inventory.Head.Item.Id = mirror.Head;
            player.Inventory.Armor.Item.Id = mirror.Armor;
            player.Inventory.Right.Item.Id = mirror.Weapon;
            player.Inventory.Left.Item.Id = mirror.Shield;
            player.Inventory.Feet.Item.Id = mirror.Foots;
            player.Inventory.Ammo.Item.Id = mirror.Ammo;
            player.Inventory.Ring.Item.Id = mirror.Ring;
            player.Inventory.Neck.Item.Id = mirror.Ammulet;
            player.Inventory.Legs.Item.Id = mirror.Legs;
        }

    }
}
