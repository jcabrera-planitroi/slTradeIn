using slTradeIn.Data;


namespace slTradeIn.Models
{
    public class CreateCampaignModel
    {
        public List<Detail_TTU_emailTemplate> Templates { get; set; }
        public List<EmailGroupModel> EmailGroups { get; set; }
        public List<EmailGroupModel> EmailGroupsSelected { get; set; }
        public string vSubject { get; set; }
        //TODO: Not necessary anymore
        //[AllowHtml]
        public string vHTMLBody { get; set; }
        public int iEmailTemplateID { get; set; }
        public Detail_TTU_emailTemplate selectedTemplate { get; set; }
        public int iUserID { get; set; }
        public string vName { get; set; }
    }
}