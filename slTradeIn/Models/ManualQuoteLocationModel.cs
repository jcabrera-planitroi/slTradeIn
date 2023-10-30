namespace slTradeIn.Models
{
    public class ManualQuoteLocationModel
    {
        public int iManualQuoteLocID { get; set; }
        public int iManualQuoteID { get; set; }
        public int iLogisticID { get; set; }
        public string vLocationLabel { get; set; }
        public bool bCheck { get; set; }
    }
}