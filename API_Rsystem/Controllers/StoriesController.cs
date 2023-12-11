﻿using API_Rsystem.BAL;
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

        [HttpGet]
        public IActionResult FetchAllRecordsTitleAndURL()
        {
            
            if (stories.IsRecords())
                InsertAllRecords();
            List<newStories> FetchAllRecords = stories.FetchAllRecords();
            return Ok(FetchAllRecords);
        }

        /// <summary>
        /// insert the all records for title and link by storyid
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        public IActionResult InsertAllRecords()
        {
            bool flag = stories.InsertAllRecords();
            return Ok(flag);
        }




    }
}
