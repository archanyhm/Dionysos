using System.Linq;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Services.ArticleServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dionysos.API.Tests.ArticleSavingServiceTests;

public class SaveArticleTests
{
    [Fact]
    public void NewData_DbContextSaveMethodGetsCalled()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupMock(dbContextMock);

        var classUnderTest = new ArticleSavingService(dbContextMock.Object);
        classUnderTest.SaveArticle(_articleDto1);

        dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public void NewData_NewArticleAdded()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupMock(dbContextMock);

        var mainDbContext = dbContextMock.Object;
        var classUnderTest = new ArticleSavingService(mainDbContext);
        classUnderTest.SaveArticle(_articleDto1);

        dbContextMock.Verify(x => x.Articles.Add(It.IsAny<Article>()), Times.Once);
    }

    [Fact]
    public void AlreadyExsistingPk_NoNewArticle()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupMock(dbContextMock, Article1);

        var mainDbContext = dbContextMock.Object;
        var classUnderTest = new ArticleSavingService(mainDbContext);
        classUnderTest.SaveArticle(_articleDto1);

        dbContextMock.Verify(x => x.Articles.Add(It.IsAny<Article>()), Times.Never);
    }

    #region TestData

    private static readonly Article Article1 = new()
    { Ean = "1", Description = "someDesc", Name = "someName", VendorId = 1 };

    private readonly ArticleDto _articleDto1 = new()
    { Ean = "1", Description = "someDesc", Name = "someName", VendorId = 1 };

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
