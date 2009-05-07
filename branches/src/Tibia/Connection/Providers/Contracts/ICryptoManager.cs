namespace Tibia.Connection.Providers.Contracts {

    interface ICryptoManager {
        
        ConnectionProvider ConnectionSource { get; set; }
        uint KeyAddress { get; set; }

        byte[] Cryptograph(byte[] Data);
        byte[] DeCryptograph(byte[] Data);

        byte[] Cryptograph(byte[] Data, byte[] key);
        byte[] DeCryptograph(byte[] Data, byte[] key);
    }
}
