namespace slTradeIn.Models
{
    public class BoxListModel
    {
        public int iCartID { get; set; }
        public string vLocationLabel { get; set; }
        public string vLocationContactPerson { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string QuoteStatus { get; set; }
        public bool IsSigned { get; set; }
        public string TextDateCreated => string.Format("{0:MM/dd/yyyy}", DateCreated);
        public int AssetsAmount { get; set; }
    }
}