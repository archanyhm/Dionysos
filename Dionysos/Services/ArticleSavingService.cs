using Dionysos.Database;
using Dionysos.Models;

namespace Dionysos.Services;

public class ArticleSavingService
{
    private IMainDbContext _dbContext;
    public ArticleSavingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }
    public void SaveArticle(ArticleDto articleDto)
    {
        var articleAlreadyKnown = _dbContext.Articles.Any(x => x.Ean == articleDto.Ean);
        if (!articleAlreadyKnown)
        {
            var newArticle = new Article
            {
                Ean = articleDto.Ean,
                Description = articleDto.Description,
                Name = articleDto.Name,
                Vendor = articleDto.Vendor
            };
            _dbContext.Articles.Add(newArticle);
        }
        
        var inventoryItem = new InventoryItem
        {
            Ean = articleDto.Ean,
            BestBefore = articleDto.BestBefore,
        };
        _dbContext.InventoryItems.Add(inventoryItem);
        
        
        _dbContext.SaveChanges();
    }
}