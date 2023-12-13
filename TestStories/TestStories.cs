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
    [SetUp]
    public void Setup()
    {
        _MemoryCacheOptions = new MemoryCacheOptions();
        _optionsAccessor = new MemoryCacheOptions();
        _cacheStories = new MemoryCache(_optionsAccessor);
        _stories = new BAL_Stories(_cacheStories);
        _StoriesController = new StoriesController(_stories);

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
}