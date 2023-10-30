namespace slTradeIn.Models
{
    public class ModelModel
    {
        public int iModelID { get; set; }
        public string Category { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int iMfg { get; internal set; }
    }
}