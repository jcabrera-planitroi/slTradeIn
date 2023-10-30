namespace slTradeIn.Models
{
    public class EmailCampaignModel
    {
        public int iEmailCampaignID { get; set; }
        public int iUserID { get; set; }
        public int iEmailTemplateID { get; set; }
        public string vSubject { get; set; }
        public string vHTMLBody { get; set; }
        public DateTime dDateCreated { get; set; }
        public string emailGroup { get; set; }
        public string vName { get; set; }
    }
}