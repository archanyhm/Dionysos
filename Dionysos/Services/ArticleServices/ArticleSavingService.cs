using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.ArticleServices;

public class ArticleSavingService
{
    private MainDbContext _dbContext;
    public ArticleSavingService()
    {
        _dbContext = new MainDbContext();
    }
    public void SaveArticle(ArticleDto articleToAdd)
    {
        var articleAlreadyKnown = _dbContext.Articles.Any(x => x.Ean == articleToAdd.Ean);
        if (!articleAlreadyKnown)
        {
            var newArticle = CreateDBArticle(articleToAdd);
            _dbContext.Articles.Add(newArticle);
        }
        
        _dbContext.SaveChanges();
    }

    private static Article CreateDBArticle(ArticleDto articleDto)
    {
        return new Article
        {
            Ean = articleDto.Ean,
            Description = articleDto.Description,
            Name = articleDto.Name,
            Vendor = articleDto.Vendor
        };
    }

    public void UpdateArticle(ArticleDto articleToChange)
    {
        _dbContext.Articles.Update(CreateDBArticle(articleToChange));
        _dbContext.SaveChanges();
    }
}