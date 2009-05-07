using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Tibia.Memory.Address.Contracts;

namespace Tibia.Memory.Address {
    /// <summary>
    /// Memory Addresses to Client.
    /// </summary>
    public class Client : IClient {
                
        public uint XTeaKey { get; set; }
        public uint RSAKey { get; set; }
        public uint DatPointer { get; set; }

        public uint FrameRateBegin { get; set; }
        public uint FrameRateCrOffset { get { return 0x0; } }
        public uint FrameRateLmOffset { get { return 0x000058; } }

        public uint MultiClient { get; set; }
        public uint MultiClientValue { get; set; }

        public uint SafeMode { get; set; }
        public uint FollowMode { get { return SafeMode + 4; } }
        public uint AttackMode { get { return FollowMode + 4; } }

        public uint PrintName { get; set; }
        public uint PrintFPS { get; set; }
        public uint howFPS { get; set; }
        public uint PrintTextFunc { get; set; }
        public uint NopFPS { get; set; }

        public uint LoginSelectedChar { get; set; }
        public uint LoginCharList { get; set; }

        public uint LoginServerStart { get; set; }
        public uint LoginServerStep { get { return 112; } }
        public uint PortDistance { get { return 100; } }
        public uint LoginServersMax { get { return 10; } }
        public uint LoginServerEnd { get { return LoginServerStart + (LoginServerStep * LoginServersMax); } }
    }
}
