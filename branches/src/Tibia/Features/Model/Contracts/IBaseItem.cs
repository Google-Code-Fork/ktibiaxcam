using Tibia.Features.Model.Items;

namespace Tibia.Features.Model.Contracts {
    public interface IBaseItem {
        string Name { get; set; }
        uint Id { get; set; }
        uint Count { get; set; }
        uint Weigth { get; set; }
        uint Volume { get; set; }
        
        Slot Slot { get; set; }
        DatItem DatItem { get; }
        DatReader DatReader { get; }
    }
}
