using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using slTradeIn.DataAccess;

namespace slTradeIn.Controllers
{
    public class PartnerController : Controller
    {
        private readonly Detail_TTU_emailTemplate_Data detail_TTU_EmailTemplate_Data;
        private readonly Detail_TTU_userEmail_Data _detail_TTU_UserEmail_Data;
        private readonly Detail_TTU_userEmailCampaign_Data _detail_TTU_UserEmailCampaign_Data;

        public PartnerController(Detail_TTU_emailTemplate_Data detail_TTU_EmailTemplate_Data, Detail_TTU_userEmail_Data detail_TTU_UserEmail_Data, Detail_TTU_userEmailCampaign_Data detail_TTU_UserEmailCampaign_Data)
        {
            this.detail_TTU_EmailTemplate_Data = detail_TTU_EmailTemplate_Data;
            _detail_TTU_UserEmail_Data = detail_TTU_UserEmail_Data;
            _detail_TTU_UserEmailCampaign_Data = detail_TTU_UserEmailCampaign_Data;
        }

        // GET: Partner
        public ActionResult GenerateLinkCampaing()
        {
            ViewBag.EmailsRegistered = _detail_TTU_UserEmail_Data.Count();
            return View(_detail_TTU_UserEmailCampaign_Data.GetAll());
        }
        public ActionResult CampaignModal(int id)
        {
            return PartialView(_detail_TTU_UserEmailCampaign_Data.GetEmailGroups(id));
        }

        public ActionResult CreateEmailCampaign()
        {

            return View(_detail_TTU_UserEmailCampaign_Data.GetCreateView());
        }
        public ActionResult DuplicateEmailCampaign(int id)
        {
            return View("CreateEmailCampaign", _detail_TTU_UserEmailCampaign_Data.GetCreateViewWithID(id));
        }
        public string GetTemplateByID(int id)
        {
            return JsonConvert.SerializeObject(detail_TTU_EmailTemplate_Data.GetWithId(id));
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult CreateEmailCampaign(FormCollection collection)
        //{
        //    if (ModelState.IsValid)
        //    {            
        //        CreateCampaignModel cet = new CreateCampaignModel();
        //        cet.iUserID = Convert.ToInt32(slTradeIn.Help.SessionTradeIn.iUserID);
        //        if(collection["iEmailTemplateID"] == null)
        //        {
        //            cet.iEmailTemplateID = Convert.ToInt32(collection["Templates"].ToString());
        //        }     
        //        else
        //        {
        //            cet.iEmailTemplateID = Convert.ToInt32(collection["iEmailTemplateID"].ToString());
        //        }
        //        cet.vSubject = collection["vSubject"].ToString();
        //        cet.vHTMLBody = collection["vHTMLBody"].ToString();
        //        cet.vName = collection["vName"].ToString();
        //        cet.EmailGroups = new List<EmailGroupModel>();
        //        foreach (var item in collection["SelectedEmailGroups"].Split(','))
        //        {
        //            cet.EmailGroups.Add(new EmailGroupModel { emailGroup = item.ToString() }); 
        //        }


        //        Detail_TTU_userEmailCampaign_Data ec = new Detail_TTU_userEmailCampaign_Data();
        //        ec.SaveEmailCampaign(cet);

        //        return RedirectToAction("GenerateLinkCampaing");
        //    }
        //    return View();
        //}
    }
}
