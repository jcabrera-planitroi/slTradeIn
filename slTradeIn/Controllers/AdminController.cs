using Microsoft.AspNetCore.Mvc;
using slTradeIn.DataAccess;
using slTradeIn.Models;

namespace slTradeIn.Controllers
{
    public class AdminController : Controller
    {
        private readonly Detail_TTU_emailTemplate_Data detail_TTU_EmailTemplate_Data;

        public AdminController(Detail_TTU_emailTemplate_Data detail_TTU_EmailTemplate_Data)
        {
            this.detail_TTU_EmailTemplate_Data = detail_TTU_EmailTemplate_Data;
        }

        // GET: Admin
        public ActionResult EmailTemplate()
        {
            return View(detail_TTU_EmailTemplate_Data.GetAll());
        }

        // GET: Admin/Create
        public ActionResult CreateEmailTemplate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmailTemplate(EmailTemplateModel collection)
        {
            if (ModelState.IsValid)
            {
                EmailTemplateModel etm = new EmailTemplateModel();
                etm.vTemplateName = collection.vTemplateName;
                etm.vSubject = collection.vSubject;
                etm.vHTMLBody = collection.vHTMLBody;
                etm.bActive = true;

                detail_TTU_EmailTemplate_Data.SaveEmailTemplate(etm);

                return RedirectToAction("EmailTemplate");
            }
            return View();
        }


        public ActionResult EditEmailTemplate(int id)
        {
            return View(detail_TTU_EmailTemplate_Data.GetWithId(id));
        }

        [HttpPost]
        public ActionResult EditEmailTemplate(EmailTemplateModel collection)
        {
            if (ModelState.IsValid)
            {
                EmailTemplateModel etm = new EmailTemplateModel();
                etm.iEmailTemplateID = collection.iEmailTemplateID;
                etm.vTemplateName = collection.vTemplateName;
                etm.vSubject = collection.vSubject;
                etm.vHTMLBody = collection.vHTMLBody;
                etm.bActive = true;
                etm.dUpdatedDate = DateTime.Now;

                detail_TTU_EmailTemplate_Data.UpdateEmailTemplate(etm);

                return RedirectToAction("EmailTemplate");
            }
            return View();
        }

        public ActionResult DeleteEmailTemplate(int id)
        {
            detail_TTU_EmailTemplate_Data.DeleteEmailTemplate(id);

            return RedirectToAction("EmailTemplate");
        }
    }
}
