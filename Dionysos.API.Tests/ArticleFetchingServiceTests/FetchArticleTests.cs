using System.Collections.Generic;
using System.Linq;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Services.ArticleServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dionysos.API.Tests.ArticleFetchingServiceTests;

public class FetchArticleTests
{
    #region TestData

    private const string Article1Ean = "1";

    private static readonly Article Article1 = new() {Ean = Article1Ean, Description = "someDesc1", Name = "someName1", VendorId = 1,};
    private static readonly Article Article2 = new() {Ean = "2", Description = "someDesc2", Name = "someName2", VendorId = 2,};
    
    private readonly ArticleDto _expectedArticleDto1 = new() { Description = "someDesc1", Ean = Article1Ean, Name = "someName1", VendorId = 1 };
    private readonly ArticleDto _emptyArticleDto = new();

    #endregion
    
    [Fact]
    public void ArticleNotInDb_EmptyArticle()
    {
        var expected = _emptyArticleDto;
        var dbContextMock = GetDbMock();
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticle("I do not exist");

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ArticleInDb_CorrectArticleDto()
    {
        var expected = _expectedArticleDto1;
        var dbContextMock = GetDbMock();
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticle(Article1Ean);

        actual.Should().BeEquivalentTo(expected);
    }

    #region DbSetMock

    private Mock<IMainDbContext> GetDbMock()
    {
        var articles = new List<Article>{Article1, Article2};
        var data = articles.AsQueryable();

        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(mockSet.Object);
        return dbContextMock;
    }

    #endregion

}