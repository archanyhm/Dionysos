using Dionysos.Database;
using Dionysos.Models;

namespace Dionysos.Services;

public class ArticleFetchingService
{
    private readonly IMainDbContext _dbContext;
    
    public ArticleFetchingService(IMainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ArticleDto> FetchArticles()
    {
        var inventoryItemDtos = new List<ArticleDto>();

        var articles = _dbContext.Articles.ToList();
        var inventoryItems = _dbContext.InventoryItems.ToList();
        foreach (var article in articles)
        {
            var articleItemsBestBeforeGroups = inventoryItems
                .Where(item => item.Ean == article.Ean)
                .GroupBy(item => item.BestBefore);

            foreach (var articleItemsBestBeforeGroup in articleItemsBestBeforeGroups)
            {
                var newDto = CreateArticleDto(article, articleItemsBestBeforeGroup);
                inventoryItemDtos.Add(newDto);
            }
        }
        
        return inventoryItemDtos;
    }

    private static ArticleDto CreateArticleDto(Article article, IGrouping<DateTime?, InventoryItem> articleItemsBestBeforeGroup)
    {
        var newDto = new ArticleDto
        {
            Ean = article.Ean,
            Description = article.Description,
            Vendor = article.Vendor,
            Name = article.Name,
            BestBefore = articleItemsBestBeforeGroup.Key,
            Quantity = articleItemsBestBeforeGroup.Count()
        };
        return newDto;
    }
}