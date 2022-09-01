using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.ArticleServices;

public class ArticleSavingService
{
    private readonly IMainDbContext _dbContext;
    public ArticleSavingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
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
            VendorId = articleDto.VendorId
        };
    }

    public void UpdateArticle(ArticleDto articleToChange)
    {
        _dbContext.Articles.Update(CreateDBArticle(articleToChange));
        _dbContext.SaveChanges();
    }
}