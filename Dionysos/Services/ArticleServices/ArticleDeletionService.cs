using Dionysos.CustomExceptions;
using Dionysos.Database;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Services.ArticleServices;

public class ArticleDeletionService
{
    private readonly IMainDbContext _dbContext;

    public ArticleDeletionService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }

    public void DeleteArticle(string ean)
    {
        try
        {
           var article = _dbContext.Articles
                .SingleOrDefault(x => x.Ean == ean);

            if (article is null) throw new ObjectDoesNotExistException();
            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();
        }
        catch (ArgumentNullException e)
        {
            throw new MultipleEntriesFoundException();
        }
        catch (DbUpdateException e)
        {
            throw new DatabaseException(
                "Beim Aktualisieren der Datenbank trat ein Fehler auf: ",
                e);
        }
    }
}
