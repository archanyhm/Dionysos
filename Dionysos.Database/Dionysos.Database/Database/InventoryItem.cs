using System.ComponentModel.DataAnnotations.Schema;

namespace Dionysos.Dionysos.Database.Database;

public class InventoryItem
{
    public int Id { get; set; }
    public DateTime? BestBefore { get; set; }

    [ForeignKey("Article")] public string Ean { get; set; }

    public virtual Article Article { get; set; }
}
