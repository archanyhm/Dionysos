namespace Dionysos.Database;

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public virtual List<Article> Articles { get; set; }
}
