using System.ComponentModel.DataAnnotations;

namespace Dionysos.Database.Database;

public class Article
{
    [Key] public string Ean { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public int VendorId { get; set; }
    public virtual Vendor Vendor { get; set; }

    public virtual List<InventoryItem> InventoryItems { get; set; }
}
