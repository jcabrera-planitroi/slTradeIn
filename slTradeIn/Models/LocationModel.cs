namespace slTradeIn.Models
{
    public class LocationModel
    {
        public int iUserID { get; set; }
        public int iLocationID { get; set; }
        public string LocationLabel { get; set; }
        public string StreetAddress { get; set; }
        public string SuiteApt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string LocationEmail { get; set; }
        public bool IsDefault { get; set; }
    }
}