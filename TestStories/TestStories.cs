namespace API_Rsystem;
using API_Rsystem.BAL;
using API_Rsystem.Interface;
using API_Rsystem.Model;
using NUnit.Framework;
using API_Rsystem.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

public class Tests
{
    
    Istories _stories;
    StoriesController _StoriesController;
    IMemoryCache _cacheStories;
    MemoryCacheOptions _MemoryCacheOptions;
    IOptions<MemoryCacheOptions> _optionsAccessor;
    newStories newStorie;
    [SetUp]
    public void Setup()
    {
        _MemoryCacheOptions = new MemoryCacheOptions();
        _optionsAccessor = new MemoryCacheOptions();
        _cacheStories = new MemoryCache(_optionsAccessor);
        _stories = new BAL_Stories(_cacheStories);
        _StoriesController = new StoriesController(_stories);
        newStorie = new newStories();

    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<Istories>();
        services.AddScoped<StoriesController>();
    }

    //[Test]
    [TestCase]
    public void InsertAndFetchRecords()
    {


        var FetchAllRecords = _StoriesController.FetchAllRecordsTitleAndURL();
        Assert.IsNotNull(FetchAllRecords);

    }

    //[Test]
    [TestCase]
    public void SaveRecordsInCache()
    {   
        newStorie.title = "this is new stores";
        newStorie.url = "http://localhost:4200/ViewStories";
        bool flag = _stories.InsertTitleLinkStories(newStorie);
        Assert.IsTrue(flag);
        FetchInsertedRecords(newStorie);
    }
    [TestCase]
    public void FetchInsertedRecordsCache()
    {
        List<newStories> lstCachesStories = _stories.FetchAllRecords();
        Assert.IsNull(lstCachesStories);
        Assert.GreaterOrEqual(1, lstCachesStories.Count());
        Assert.IsNotNull(lstCachesStories);

    }

    private void FetchInsertedRecords(newStories newStorie)
    {
        List<newStories> lstCachesStories = _stories.FetchAllRecords();
        Assert.GreaterOrEqual(1, lstCachesStories.Count());
        if (lstCachesStories != null)
        {
            foreach (var item in lstCachesStories)
            {
                Assert.AreSame(newStorie.url, item.url);
            }
        }

    }
}