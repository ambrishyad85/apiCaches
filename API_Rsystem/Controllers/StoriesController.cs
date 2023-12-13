using API_Rsystem.BAL;
using API_Rsystem.Interface;
using API_Rsystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Rsystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
       
        private readonly Istories stories;
        

        public StoriesController(Istories storie)
        {
            stories = storie;
        }
        /// <summary>
        /// insert the all value in cache and fetch the all value from caches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FetchAllRecordsTitleAndURL()
        {

            if (stories.IsRecords())
                stories.InsertAllRecords();
            List<newStories> FetchAllRecords = stories.FetchAllRecords();
            return Ok(FetchAllRecords);
        }

      
    }
}
