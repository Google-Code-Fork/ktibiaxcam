using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Keyrox.Shared.Converters;

namespace Tibia.Memory {
    [Serializable]
    public class AddressDTO {

        [Browsable(false), XmlAttribute]
        public string ClientVersion { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("XTea Key"), Description("Client Criptograph XTea Key.")]
        public uint XTeaKey { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("RSA Key"), Description("Client Criptograph RSA Key.")]
        public uint RSAKey { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Dat Pointer"), Description("Pointer to TibiaDat Address.")]
        public uint DatPointer { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Frame Rate Begin"), Description("Pointer to Framerate Begin.")]
        public uint FrameRateBegin { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Multi Client"), Description("Address to enable MultiClient Feature.")]
        public uint MultiClient { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Safe Mode"), Description("Safe mode battle Address.")]
        public uint SafeMode { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Login Selected Char"), Description("Login Selected Char Address.")]
        public uint LoginSelectedChar { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Login Char List"), Description("Selected Char List Address.")]
        public uint LoginCharList { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Client"), DisplayName("Login Server Start"), Description("First Login Server Address.")]
        public uint LoginServerStart { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("In Game"), Description("Address to Player InGame value.")]
        public uint InGame { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Experience"), Description("Total of Player Experience Points.")]
        public uint Exp { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Location Z"), Description("The current floor of the player location.")]
        public uint PlayerZ { get; set; }
        
        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Red Square"), Description("The creature id in RedSquare (attack) address.")]
        public uint RedSquare { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Container Begin"), Description("Address of first opened container.")]
        public uint ContainerBegin { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Head Inventory"), Description("Address of head inventory.")]
        public uint HeadInventory { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Player"), DisplayName("Vip List Begin"), Description("Address of first player on VipList.")]
        public uint VipBegin { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("Map Pointer"), Description("Address to Map Pointer.")]
        public uint MapPointer { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("NameSpy1"), Description("Address to NameSpy1.")]
        public uint NameSpy1 { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("NameSpy2"), Description("Address to NameSpy2.")]
        public uint NameSpy2 { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("LevelSpy1"), Description("Address to LevelSpy1.")]
        public uint LevelSpy1 { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("LevelSpy2"), Description("Address to LevelSpy2.")]
        public uint LevelSpy2 { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("LevelSpy3"), Description("Address to LevelSpy3.")]
        public uint LevelSpy3 { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("LevelSpyPtr"), Description("Address to LevelSpyPtr.")]
        public uint LevelSpyPtr { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("Full Light"), Description("Full Light Hook address.")]
        public uint FullLight { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Graphics"), DisplayName("Full Light Nop"), Description("Full Light Nop address.")]
        public uint FullLightNop { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Print"), DisplayName("Print Name"), Description("Address to Print Name function.")]
        public uint PrintName { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Print"), DisplayName("Print FPS"), Description("Address to Print FPS function.")]
        public uint PrintFPS { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Print"), DisplayName("How FPS"), Description("Address to How FPS function.")]
        public uint howFPS { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Print"), DisplayName("Print Text"), Description("Address to Print Text function.")]
        public uint PrintTextFunc { get; set; }

        [XmlElement, TypeConverter(typeof(HexConverter))]
        [Category("Print"), DisplayName("Nop FPS"), Description("Value of Nop to FPS functions.")]
        public uint NopFPS { get; set; }

    }
}
