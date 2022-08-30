using System;
using System.Collections.Generic;
using System.Linq;
using Dionysos.Database;
using Dionysos.Models;
using Dionysos.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dionysos.API.Tests.ArticleFetchingServiceTests;

public class FetchArticlesTests
{
    #region TestData

    private static readonly DateTime SomeBestBefore = new(2023, 10, 21);
    
    private static readonly Article Article1 = new() {Ean = "1", Description = "someDesc1", Name = "someName1", Vendor = "someVendor1",};
    private static readonly Article Article2 = new() {Ean = "2", Description = "someDesc2", Name = "someName2", Vendor = "someVendor2",};
    
    private readonly InventoryItem _item1Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 0};
    private readonly InventoryItem _item2Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 1};
    private readonly InventoryItem _item3Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 1, BestBefore = SomeBestBefore};
    private readonly InventoryItem _item1Article2 = new() {Article = Article2, Ean = Article2.Ean, Id = 2};
    
    private readonly ArticleDto _expectedArticleDtoArticle1WithItem1 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 1, Vendor = "someVendor1" };
    private readonly ArticleDto _expectedArticleDtoArticle1WithItem3 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 1, Vendor = "someVendor1", BestBefore = SomeBestBefore};
    private readonly ArticleDto _expectedArticleDtoArticle2WithItem1 = new() { Description = "someDesc2", Ean = "2", Name = "someName2", Quantity = 1, Vendor = "someVendor2" };
    private readonly ArticleDto _expectedArticleDtoArticle1WithItem1And2 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 2, Vendor = "someVendor1" };
    #endregion

    [Fact]
    public void NoArticlesOrItems_NoArticleDto()
    {
        var expected = new List<ArticleDto>();
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles());
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems());
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OneItem_OneArticleDto()
    {
        var expected = new List<ArticleDto>{_expectedArticleDtoArticle1WithItem1};
        
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1));
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems(_item1Article1));
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void TwoArticles_TwoArticleDtos()
    {
        var expected = new List<ArticleDto>{_expectedArticleDtoArticle1WithItem1, _expectedArticleDtoArticle2WithItem1};
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1, Article2));
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems(_item1Article1, _item1Article2));
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void MultipleItemsOneArticleSameBestBefore_OneArticleDto()
    {
        var expected = new List<ArticleDto>{_expectedArticleDtoArticle1WithItem1And2};
        
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1));
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems(_item1Article1, _item2Article1));
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void MultipleItemsOneArticleDifferentBestBefore_TwoArticleDtos()
    {
        var expected = new List<ArticleDto>{_expectedArticleDtoArticle1WithItem1, _expectedArticleDtoArticle1WithItem3};
        
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1));
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems(_item1Article1, _item3Article1));
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void ArticleWithoutItem_NoArticleDto()
    {
        var expected = new List<ArticleDto>();
        var dbContextMock = new Mock<IMainDbContext>();
        dbContextMock.Setup(x => x.Articles).Returns(GetTestArticles(Article1));
        dbContextMock.Setup(x => x.InventoryItems).Returns(GetTestInventoryItems());
        
        var classUnderTest = new ArticleFetchingService(dbContextMock.Object);
        var actual = classUnderTest.FetchArticles();

        actual.Should().BeEquivalentTo(expected);
    }
    #region DbSetMock

    private DbSet<InventoryItem> GetTestInventoryItems(params InventoryItem[] items)
    {
        var data = items.AsQueryable();
        
        var itemDbSetMock = new Mock<DbSet<InventoryItem>>();
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Provider).Returns(data.Provider);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Expression).Returns(data.Expression);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        return itemDbSetMock.Object;
    }

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
}