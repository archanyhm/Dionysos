namespace Dionysos.Models;

public class ArticleDto
{
    public string Ean { get; set; }
    public DateTime? BestBefore { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }
    public string Vendor { get; set; }
    public string Description { get; set; }
}