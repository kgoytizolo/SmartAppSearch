namespace SmartAppService.Models
{
    public class SearchInputParams
    {
        public string SearchPhase { get ; set; }    //Required
        public int Limit { get; set; }              //Default = 25 results per search
        public string[] Markets { get; set; }       //Optional
    }
}