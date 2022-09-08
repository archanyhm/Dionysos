using System.Linq;
using Dionysos.Database.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dionysos.API.Tests.ArticleDeletionService;

public class DeleteArticleTests
{
    [Fact]
    public void SaveChangesGetsCalled()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupMock(dbContextMock, Article1);

        var classUnderTest = new BL.Dionysos.BL.Services.ArticleServices.ArticleDeletionService(dbContextMock.Object);
        classUnderTest.DeleteArticle(Ean);

        dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public void NewData_DbContextSaveMethodGetsCalled()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupMock(dbContextMock, Article1);

        var classUnderTest = new BL.Dionysos.BL.Services.ArticleServices.ArticleDeletionService(dbContextMock.Object);
        classUnderTest.DeleteArticle(Ean);

        dbContextMock.Verify(x => x.Articles.Remove(It.IsAny<Article>()), Times.Once);
    }

    #region TestData

    private const string Ean = "1";

    private static readonly Article Article1 = new()
        { Ean = Ean, Description = "someDesc", Name = "someName", VendorId = 1 };

    #endregion

    #region DbSetMock

    private void SetupMock(Mock<IMainDbContext> dbContextMock, params Article[] articles)
    {
        dbContextMock.Setup(x => x.Articles).Returns(SetupArticlesMock(articles));
    }

    private DbSet<Article> SetupArticlesMock(params Article[] articles)
    {
        var data = articles.AsQueryable();

        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        return mockSet.Object;
    }

    #endregion
}
