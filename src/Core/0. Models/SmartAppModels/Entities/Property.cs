using  SmartAppModels.Common;

namespace SmartAppModels.Entities
{
    public class Property : CustomerAssets
    {
        public int PropertyID { get; set; }
        public string FormerName { get; set; }        
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}