using Microsoft.AspNetCore.Mvc;
using slTradeIn.Data;
using slTradeIn.DataAccess;

namespace slTradeIn.Controllers
{
    public class MailController : Controller
    {
        private readonly Detail_TTU_userEmail_Data _detail_TTU_UserEmail_Data;

        public MailController(Detail_TTU_userEmail_Data detail_TTU_UserEmail_Data)
        {
            _detail_TTU_UserEmail_Data = detail_TTU_UserEmail_Data;
        }

        public ActionResult Index(int page = 0)
        {
            ViewBag.Page = page;
            ViewBag.Count = _detail_TTU_UserEmail_Data.Count() / 10;
            return View(_detail_TTU_UserEmail_Data.List(page));
        }

        [Route("{id}")]
        [HttpPost]
        public ActionResult Edit(int id, Detail_TTU_userEmail detail_TTU_userEmail)
        {
            _detail_TTU_UserEmail_Data.Update(id, detail_TTU_userEmail);
            return RedirectToAction("Index", "Mail");
        }

        [Route("{id}")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _detail_TTU_UserEmail_Data.Delete(id);
            return RedirectToAction("Index", "Mail");
        }

        // POST: Excel
        //[HttpPost]
        //public ActionResult Excel()
        //{
        //    using (var reader = ExcelReaderFactory.CreateReader(Request.Files[0].InputStream))
        //    {
        //        var list = new List<Detail_TTU_userEmail>();
        //        reader.Read();

        //        while (reader.Read())
        //        {
        //            list.Add(new Detail_TTU_userEmail
        //            {
        //                vEmail = reader.GetString(0),
        //                vName = reader.GetString(1),
        //                vEmailGroup = reader.GetString(2),
        //                dCreatedDate = DateTime.Now,
        //                dUpdatedDate = DateTime.Now,
        //            });
        //        }

        //        new Detail_TTU_userEmail_Data().Save(list);
        //    }

        //    return RedirectToAction("Index", "Mail");
        //}
    }
}
