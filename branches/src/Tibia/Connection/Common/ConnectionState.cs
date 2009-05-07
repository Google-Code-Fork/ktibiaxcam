namespace Tibia.Connection {
    /// <summary>
    /// Tibia Client Connection State.
    /// </summary>
    public enum ConnectionState { 
        
        Connecting = 0, 
        
        Connected = 1, 
        
        Disconnecting = 2, 
        
        Disconnected = 3 }
}
