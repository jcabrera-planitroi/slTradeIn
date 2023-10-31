using Microsoft.AspNetCore.Mvc;
using slTradeIn.Data;
using slTradeIn.DataAccess;
using slTradeIn.Help;
using slTradeIn.Models;

namespace slTradeIn.Controllers;

public class LocationController : Controller
{
    private readonly Detail_TTU_userCart_Data _detail_TTU_UserCart_Data;
    private readonly Detail_TTU_userLocation_Data _detail_TTU_UserLocation_Data;

    public LocationController(Detail_TTU_userLocation_Data detail_TTU_UserLocation_Data,
        Detail_TTU_userCart_Data detail_TTU_UserCart_Data)
    {
        _detail_TTU_UserLocation_Data = detail_TTU_UserLocation_Data;
        _detail_TTU_UserCart_Data = detail_TTU_UserCart_Data;
    }

    public ActionResult Index()
    {
        var model = new RegisterUserModel();
        model.LocationDetail = _detail_TTU_UserLocation_Data.GetLocations(Convert.ToInt32(SessionTradeIn.iUserID));
        return View(model);
    }

    // [Route("{id}")]
    // public ActionResult Show(int id)
    // {
    //     return View(_detail_TTU_UserLocation_Data.GetLocation(id));
    // }

    [HttpPost]
    public ActionResult Save(Detail_TTU_userLocation location)
    {
        _detail_TTU_UserLocation_Data.SaveLocation(location);
        return RedirectToAction("Show", "Location", new { id = location.iLogisticID });
    }

    public JsonResult GetInfoLocation(int iLogisticID = 0)
    {
        var Data = _detail_TTU_UserLocation_Data.GetLocation(iLogisticID);

        return Json(
            new
            {
                location = Data,
                quotes = _detail_TTU_UserCart_Data
                    .GetList(int.Parse(SessionTradeIn.iUserID))
                    .Where(x => x.vLocationLabel == Data.vLocationLabel)
            }
        );
    }

    public JsonResult DeleteLocation(int iLogisticID = 0)
    {
        if (_detail_TTU_UserLocation_Data.GetLocations(Convert.ToInt32(SessionTradeIn.iUserID)).Count <= 1)
            return Json(new { error = "There must be at least one location" });

        var Data = _detail_TTU_UserLocation_Data.DeleteLocation(iLogisticID);
        return Json(Data);
    }
}