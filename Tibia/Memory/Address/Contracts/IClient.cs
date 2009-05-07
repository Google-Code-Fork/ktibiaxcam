namespace Tibia.Memory.Address.Contracts {

	public interface IClient {
		uint XTeaKey { get; }
		uint RSAKey { get; }
        uint DatPointer { get; }

		uint FrameRateBegin { get; }
		uint FrameRateCrOffset { get; }
		uint FrameRateLmOffset { get; }

		uint MultiClient { get; }
		uint MultiClientValue { get; }

		uint FollowMode { get; }
		uint AttackMode { get; }
		uint SafeMode { get; }

        uint PrintName { get; }
        uint PrintFPS { get; }
        uint howFPS { get; }
        uint PrintTextFunc { get; }
        uint NopFPS { get; }
                
        uint LoginCharList { get; }
		uint LoginSelectedChar { get; }

        uint LoginServerStart { get; }
        uint LoginServerStep { get; }
        uint PortDistance { get; }
        uint LoginServersMax { get; }
        uint LoginServerEnd { get; }
	}
}
