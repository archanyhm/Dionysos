using System.Collections.Generic;
using System.Linq;
using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.BL.Dionysos.BL.Services.ArticleServices;
using Dionysos.Database.Database;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dionysos.API.Tests.ArticleFetchingServiceTests;

public class FetchArticlesTests
{
    [Fact]
    public void NoArticlesOrItems_NoArticleDto()
    {
        var expected = new List<ArticleDto>();
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles());

        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OneItem_OneArticleDto()
    {
        var expected = new List<ArticleDto> { _expectedArticleDto1 };

        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1));

        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void TwoArticles_TwoArticleDtos()
    {
        var expected = new List<ArticleDto> { _expectedArticleDto1, _expectedArticleDto2 };
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1, Article2));

        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }

    #region DbSetMock

    private DbSet<Article> GetTestArticles(params Article[] articles)
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

    #region TestData

    private static readonly Article Article1 = new()
        { Ean = "1", Description = "someDesc1", Name = "someName1", VendorId = 1 };

    private static readonly Article Article2 = new()
        { Ean = "2", Description = "someDesc2", Name = "someName2", VendorId = 2 };

    private readonly ArticleDto _expectedArticleDto1 =
        new() { Description = "someDesc1", Ean = "1", Name = "someName1", VendorId = 1 };

    private readonly ArticleDto _expectedArticleDto2 =
        new() { Description = "someDesc2", Ean = "2", Name = "someName2", VendorId = 2 };

    #endregion
}
