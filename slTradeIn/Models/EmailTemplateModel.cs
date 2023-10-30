using System.ComponentModel.DataAnnotations;

namespace slTradeIn.Models
{
    public class EmailTemplateModel
    {
        public int iEmailTemplateID { get; set; }
        [Required(ErrorMessage = "Please enter template name")]
        public string vTemplateName { get; set; }
        [Required(ErrorMessage = "Please enter subject")]
        public string vSubject { get; set; }
        //TODO: Not necessary anymore
        //[AllowHtml]
        [Required(ErrorMessage = "Please enter body(HTML)")]
        public string vHTMLBody { get; set; }
        [Required(ErrorMessage = "Please enter state")]
        public bool bActive { get; set; }
        public DateTime dCreatedDate { get; set; }
        public DateTime dUpdatedDate { get; set; }
        public DateTime dInactiveDate { get; set; }
    }
}