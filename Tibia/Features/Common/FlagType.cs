using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Flag Types.
    /// </summary>
    public enum FlagType { 
        
        Poison = 1, 
        
        Burn = 2, 
        
        Eletric = 4, 
        
        Drunk = 8, 
        
        MShield = 16, 
        
        Slowed = 32, 
        
        Haste = 64, 
        
        Battle = 128, 
        
        Drowing = 256, 
        
        None = 0, 
        
        Merged = 99 }
}
