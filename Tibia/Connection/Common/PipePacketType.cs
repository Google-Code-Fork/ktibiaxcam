namespace Tibia.Connection {
    public enum PipePacketType : byte {

        Unknow = 0x00,

        SetConstant = 0x01,

        DisplayText = 0x02,

        RemoveText = 0x03,

        RemoveAllText = 0x04,

        InjectDisplayText = 0x05,

        DisplayCreatureText = 0x06,

        RemoveCreatureText = 0x07,

        UpdateCreatureText = 0x08
    }
}
