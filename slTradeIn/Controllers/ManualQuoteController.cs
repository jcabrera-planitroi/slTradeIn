using Microsoft.AspNetCore.Mvc;
using slTradeIn.Data;
using slTradeIn.DataAccess;
using slTradeIn.Help;
using slTradeIn.Models;

namespace slTradeIn.Controllers
{
    public class ManualQuoteController : Controller
    {
        private readonly Detail_TTU_userManualQuote_Data _detail_TTU_UserManualQuote_Data;
        private readonly Detail_TTU_userCartDetail_Data _detail_TTU_UserCartDetail_Data;
        private readonly Detail_TTU_userManualQuoteDetail_Data _detail_TTU_UserManualQuoteDetail_Data;
        private readonly Detail_TTU_userManualQuoteLocation_Data _detail_TTU_UserManualQuoteLocation_Data;


        public ManualQuoteController(Detail_TTU_userManualQuote_Data detail_TTU_UserManualQuote_Data, Detail_TTU_userCartDetail_Data detail_TTU_UserCartDetail_Data, Detail_TTU_userManualQuoteDetail_Data detail_TTU_UserManualQuoteDetail_Data, Detail_TTU_userManualQuoteLocation_Data detail_TTU_UserManualQuoteLocation_Data)
        {
            _detail_TTU_UserManualQuote_Data = detail_TTU_UserManualQuote_Data;
            _detail_TTU_UserCartDetail_Data = detail_TTU_UserCartDetail_Data;
            _detail_TTU_UserManualQuoteDetail_Data = detail_TTU_UserManualQuoteDetail_Data;
            _detail_TTU_UserManualQuoteLocation_Data = detail_TTU_UserManualQuoteLocation_Data;
        }

        // GET: ManualQuote
        public ActionResult Index()
        {
            return View(_detail_TTU_UserManualQuote_Data.GetList(Convert.ToInt32(SessionTradeIn.iUserID)));
        }

        public ActionResult SelectManualQuote(int iManualQuoteID = 0)
        {
            SessionTradeIn.iManualQuoteID = iManualQuoteID.ToString();
            return RedirectToAction("Edit");
        }
        [HttpGet]
        public int QuoteCount()
        {
            int cartID = Int32.Parse(SessionTradeIn.iCartID);

            var count = _detail_TTU_UserCartDetail_Data.GetListCartDetail(cartID).Count;

            return count;
        }
        public ActionResult Edit()
        {
            var Q = _detail_TTU_UserManualQuote_Data.GetInfo(Convert.ToInt32(SessionTradeIn.iManualQuoteID));
            var Model = new ManualQuoteModel();
            if (Q == null)
            {
                Model = new ManualQuoteModel
                {
                    iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                    bSerializedReport = Convert.ToBoolean(SessionTradeIn.bSerializedReport),
                    vShippingType = Convert.ToInt32(SessionTradeIn.iShippingType),
                    ListDetail = new List<Detail_TTU_userManualQuoteDetail>(_detail_TTU_UserManualQuoteDetail_Data.GetList(0)),
                    ListLocation = new List<ManualQuoteLocationModel>(_detail_TTU_UserManualQuoteLocation_Data.GetList(0, Convert.ToInt32(SessionTradeIn.iUserID)))
                };
            }
            else
            {
                Model = new ManualQuoteModel
                {
                    iUserID = Q.iUserID,
                    bSerializedReport = Q.bSerializedReport,
                    iBoxStatusID = Q.iBoxStatusID,
                    iManualQuoteID = Q.iManualQuoteID,
                    dSignDate = Q.dSignDate,
                    vShippingType = Q.vShippingType,
                    ListDetail = new List<Detail_TTU_userManualQuoteDetail>(_detail_TTU_UserManualQuoteDetail_Data.GetList(Q.iManualQuoteID)),
                    ListLocation = new List<ManualQuoteLocationModel>(_detail_TTU_UserManualQuoteLocation_Data.GetList(Q.iManualQuoteID, Convert.ToInt32(SessionTradeIn.iUserID)))
                };
            }

            return View(Model);
        }
        [HttpPost]
        public ActionResult Edit(ManualQuoteModel model)
        {

            var Q = _detail_TTU_UserManualQuote_Data.GetInfo(model.iManualQuoteID);
            if (Q == null)
            {
                model.iUserID = Convert.ToInt32(SessionTradeIn.iUserID);
                model.bSerializedReport = Convert.ToBoolean(SessionTradeIn.bSerializedReport);
                model.vShippingType = Convert.ToInt32(SessionTradeIn.iShippingType);


                model.iManualQuoteID = _detail_TTU_UserManualQuote_Data.SaveManualQuote(new Detail_TTU_userManualQuote
                {
                    iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                    bSerializedReport = model.bSerializedReport,
                    vShippingType = model.vShippingType
                });

            }

            _detail_TTU_UserManualQuoteDetail_Data.SaveDetail(new Detail_TTU_userManualQuoteDetail
            {
                iManualQuoteID = model.iManualQuoteID,
                iQuantity = model.Quantity,
                vHDD = model.HDD,
                vManufacturer = model.Manufacturer,
                vModelNumber = model.ModelNumber,
                iManualQuoteDetID = model.iManualQuoteDetID,
                vProcessor = model.Processor,
                vRam = model.Ram
            });
            model.ListDetail = _detail_TTU_UserManualQuoteDetail_Data.GetList(model.iManualQuoteID);
            model.ListLocation = new List<ManualQuoteLocationModel>(_detail_TTU_UserManualQuoteLocation_Data.GetList(model.iManualQuoteID, Convert.ToInt32(SessionTradeIn.iUserID)));
            foreach (var item in model.ListLocation)
            {
                item.iManualQuoteID = model.iManualQuoteID;
                if (Request.Form.ContainsKey("chk_" + item.iLogisticID.ToString()))
                    item.bCheck = true;
                else
                    item.bCheck = false;
            }
            _detail_TTU_UserManualQuoteLocation_Data.SaveLocation(model.ListLocation, model.iManualQuoteID);
            SessionTradeIn.iManualQuoteID = model.iManualQuoteID.ToString();

            return View(model);
        }

        public JsonResult SendManualQuote(int ID = 0)
        {
            var Data = _detail_TTU_UserManualQuoteDetail_Data.GetInfo(ID);



            return Json(Data);
        }

        public JsonResult GetInfoManualQuote(int ID = 0)
        {
            var Data = _detail_TTU_UserManualQuoteDetail_Data.GetInfo(ID);

            return Json(Data);
        }

        public JsonResult DeleteLocation(int iManualQuoteDetID = 0)
        {
            var Data = _detail_TTU_UserManualQuoteDetail_Data.DeleteDetail(iManualQuoteDetID);
            return Json(Data);
        }
    }
}