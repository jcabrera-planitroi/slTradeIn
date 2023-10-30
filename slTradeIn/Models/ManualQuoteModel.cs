using slTradeIn.Data;

namespace slTradeIn.Models
{
    public class ManualQuoteModel
    {
        public int iManualQuoteID { get; set; }
        public int iUserID { get; set; }
        public int vShippingType { get; set; }
        public bool bSerializedReport { get; set; }
        public System.DateTime dDateCreated { get; set; }
        public int iBoxStatusID { get; set; }
        public string QuoteStatus { get; set; }
        public Nullable<System.DateTime> dSignDate { get; set; }
        public List<ManualQuoteLocationModel> ListLocation { get; set; }
        public List<Detail_TTU_userManualQuoteDetail> ListDetail { get; set; }

        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public string HDD { get; set; }
        public string Ram { get; set; }
        public string Processor { get; set; }
        public int Quantity { get; set; }
        public int iManualQuoteDetID { get; set; }
    }
}