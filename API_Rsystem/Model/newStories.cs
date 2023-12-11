using System.ComponentModel.DataAnnotations;

namespace API_Rsystem.Model
{
    public class newStories
    {
       
        public string by { get; set; }
        public Int64 descendants { get; set; }       
        public Int64 id { get; set; }
        public Int64 score { get; set; }
        public Int64 time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }


        //[Required(ErrorMessage = "Please select New/Old stories")]
        //public bool IsNew { get; set; }
    }
}
