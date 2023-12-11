using API_Rsystem.Model;

namespace API_Rsystem.Interface
{
    public interface Istories
    {
        bool IsRecords();
        List<newStories> FetchAllRecords();
        
         Task<List<string>> FetchStoriesID();

         Task<newStories> FetchTitleLinkBystoriesID(string item, bool flag = false);

        bool InsertAllRecords();
    }
}
