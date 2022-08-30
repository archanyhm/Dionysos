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

namespace Dionysos.API.Tests.ArticleSavingServiceTests;

public class SaveArticleTests
{
    #region TestData

    private static readonly DateTime SomeBestBefore = new(2023, 10, 21);
    
    private static readonly Article Article1 = new() {Ean = "1", Description = "someDesc1", Name = "someName1", Vendor = "someVendor1",};
    private static readonly Article Article2 = new() {Ean = "2", Description = "someDesc2", Name = "someName2", Vendor = "someVendor2",};
    
    private readonly InventoryItem _item1Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 0};
    private readonly InventoryItem _item2Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 1};
    private readonly InventoryItem _item3Article1 = new() {Article = Article1, Ean = Article1.Ean, Id = 1, BestBefore = SomeBestBefore};
    private readonly InventoryItem _item1Article2 = new() {Article = Article2, Ean = Article2.Ean, Id = 2};
    
    private readonly ArticleDto _articleDtoWithArt1WithItem1 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 1, Vendor = "someVendor1" , BestBefore = null};
    private readonly ArticleDto _expectedArticleDtoArticle1WithItem3 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 1, Vendor = "someVendor1", BestBefore = SomeBestBefore};
    private readonly ArticleDto _expectedArticleDtoArticle2WithItem1 = new() { Description = "someDesc2", Ean = "2", Name = "someName2", Quantity = 1, Vendor = "someVendor2" };
    private readonly ArticleDto _expectedArticleDtoArticle1WithItem1And2 = new() { Description = "someDesc1", Ean = "1", Name = "someName1", Quantity = 2, Vendor = "someVendor1" };
    private List<InventoryItem> _itemsAsList;
    private List<Article> _articlesAsList;

    #endregion

    
    [Fact]
    public void SaveArticleTestsGetsCalledWithNewData_DbContextSaveMethodGetsCalled()
    {
        var dbContextMock = new Mock<IMainDbContext>();
        SetupDefaultMock(dbContextMock);

        var classUnderTest = new ArticleSavingService(dbContextMock.Object);
        classUnderTest.SaveArticle(_articleDtoWithArt1WithItem1);
        
        dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
    }



    [Fact]
    public void ArticleDtoWithNewArticleAndItem_ArticleAndItemCreated()
    {
        var expectedArticles = SetupArticlesMock(Article1);
        var expectedItems = SetupInventoryItemsMock(_item1Article1);
        
        var dbContextMock = new Mock<IMainDbContext>();
        SetupDefaultMock(dbContextMock);

        var mainDbContext = dbContextMock.Object;
        var classUnderTest = new ArticleSavingService(mainDbContext);
        classUnderTest.SaveArticle(_articleDtoWithArt1WithItem1);
        
        mainDbContext.Articles.Should().BeEquivalentTo(expectedArticles);
        mainDbContext.InventoryItems.Should().BeEquivalentTo(expectedItems);
    }
    
    [Fact]
    public void ArticleDtoWithOldArticleAndNewItem_NewEntryForItemNoNewEntryForArticle()
    {
        
    }
    
    [Fact]
    public void MultipleArticleDtosWithSameItem_NewEntryForEveryItem()
    {
        
    }
    
    #region DbSetMock

    private void SetupDefaultMock(Mock<IMainDbContext> dbContextMock)
    {
        dbContextMock.Setup(x => x.Articles).Returns(SetupArticlesMock());
        dbContextMock.Setup(x => x.InventoryItems).Returns(SetupInventoryItemsMock());
    }
    
    private DbSet<InventoryItem> SetupInventoryItemsMock(params InventoryItem[] items)
    {
        _itemsAsList = items.ToList();
        var data = items.AsQueryable();
        
        var itemDbSetMock = new Mock<DbSet<InventoryItem>>();
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Provider).Returns(data.Provider);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Expression).Returns(data.Expression);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        itemDbSetMock.Setup(d => d.Add(It.IsAny<InventoryItem>())).Callback<InventoryItem>((s) => _itemsAsList.Add(s));

        return itemDbSetMock.Object;
    }

    private DbSet<Article> SetupArticlesMock(params Article[] articles)
    {
        _articlesAsList = articles.ToList();
        var data = articles.AsQueryable();

        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IQueryable<Article>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockSet.Setup(d => d.Add(It.IsAny<Article>())).Callback<Article>((s) => _articlesAsList.Add(s));
        
        return mockSet.Object;
    }

    #endregion

}