using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;
using Microsoft.EntityFrameworkCore;

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
        if (DoesArticleExist(articleToAdd)) throw new ObjectAlreadyExistsException();

        try
        {
            _dbContext.Articles.Add(articleToAdd.ToDbArticle());
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }
    }

    public void UpdateArticle(ArticleDto articleToChange)
    {
        if (!DoesArticleExist(articleToChange)) throw new ObjectDoesNotExistException();

        try
        {
            _dbContext.Articles.Update(articleToChange.ToDbArticle());
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }

    }

    private static void ThrowDatabaseException(DbUpdateException e)
    {
        throw new DatabaseException(
            "Beim Aktualisieren der Datenbank trat ein Fehler auf: ",
            e);
    }
    
    private bool DoesArticleExist(ArticleDto articleToAdd)
    {
        return _dbContext.Articles.Any(x => x.Ean == articleToAdd.Ean);
    }
}
