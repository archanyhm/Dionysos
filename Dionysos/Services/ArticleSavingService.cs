using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services;

public class ArticleSavingService
{
    private MainDbContext _dbContext;
    public ArticleSavingService()
    {
        _dbContext = new MainDbContext();
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
        
        _dbContext.SaveChanges();
    }
}