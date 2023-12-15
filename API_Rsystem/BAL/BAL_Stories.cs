using API_Rsystem.Interface;
using API_Rsystem.Model;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Reflection;

namespace API_Rsystem.BAL
{
    public class BAL_Stories: Istories
    {
        private IMemoryCache cacheStories ;
        
        public BAL_Stories(IMemoryCache memoryCache)
        {
            cacheStories = memoryCache;
            
        }
        public bool IsRecords()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(cacheStories) as ICollection;
            
            return collection!=null && collection.Count==0? true:false;
        }
        /// <summary>
        /// fetch all records from cache
        /// </summary>
        /// <returns></returns>
        public List<newStories> FetchAllRecords()
        {
            newStories objNewStorie = null;

            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(cacheStories) as ICollection;
            List<newStories> items = new List<newStories>();
            if (collection != null && collection.Count > 0)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    objNewStorie = (newStories)cacheStories.Get(val);
                    
                    if (objNewStorie != null)
                    items.Add(objNewStorie);
                }

            return items;
        }

        public bool InsertAllRecords()
        {
            Task<List<string>>_FetchStoriesID= FetchStoriesID();
            if (_FetchStoriesID.Result != null)
            {
                Parallel.ForEach(_FetchStoriesID.Result, item =>
                {
                    Task<newStories> _FetchTitleLinkBystoriesID = FetchTitleLinkBystoriesID(item);
                });

            }
            return true;
        }

        /// <summary>
        /// insert the title and link in chache if key is not avilable
        /// </summary>
        /// <param name="newStorie"></param>
        public bool InsertTitleLinkStories(newStories newStorie)
        {

            try
            {
                if (newStorie != null)
                {
                     var abc = cacheStories.Get(newStorie.id);
                        if (abc == null)
                        { cacheStories.Set(newStorie.id, newStorie); }
                   
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;

        }

        /// <summary>
        /// fetch stories
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> FetchStoriesID()
        {
            try
            {
                List<string> newStoriesInfo = new List<string>();
                string Baseurl = "https://hacker-news.firebaseio.com/";
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage ObjResponse = client.GetAsync("v0/newstories.json?print=pretty").Result;

                    if (ObjResponse.StatusCode.ToString().ToLower() == "ok")
                    {
                        var ObjResponse1 = ObjResponse.Content.ReadAsStringAsync().Result;
                        newStoriesInfo = JsonConvert.DeserializeObject<List<string>>(ObjResponse1);
                      
                    }

                    return newStoriesInfo;

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// fetch the title and link
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<newStories> FetchTitleLinkBystoriesID(string item)
        {
            try
            {
                newStories _newStoriesTitleLink = new newStories();

                //List<newStories> newStoriesInfo = new List<newStories>();
                string Baseurl = "https://hacker-news.firebaseio.com/";
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage ObjResponse = client.GetAsync("v0/item/" + item + ".json?print=pretty").Result;

                    if (ObjResponse.StatusCode.ToString().ToLower() == "ok")
                    {
                        var ObjResponse1 = ObjResponse.Content.ReadAsStringAsync().Result;
                        _newStoriesTitleLink = JsonConvert.DeserializeObject<newStories>(ObjResponse1);

                        // stored the title and link
                        if(_newStoriesTitleLink!=null)
                        InsertTitleLinkStories(_newStoriesTitleLink);
                    }

                    return _newStoriesTitleLink;

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
