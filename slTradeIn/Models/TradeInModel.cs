namespace slTradeIn.Models
{
    public class TradeInModel
    {
        public int iCartDetailID { get; set; }
        public int iCategory { get; set; }
        public string Category { get; set; }
        public int iManufacturer { get; set; }
        public string Manufacturer { get; set; }
        public string FilterWord { get; set; }
        public int iModelID { get; set; }
        public string Model { get; set; }

        public bool? bIsAccesible { get; set; }
        public int? iProcessor { get; set; }
        public int? iProcessorGen { get; set; }
        public int? iMemory { get; set; }
        public int? iHDD { get; set; }
        public string vGrade { get; set; }
        public int iQuantity { get; set; }
        public decimal mPrice { get; set; }
        public List<ModelModel> ModelDetail { get; set; }
    }
}