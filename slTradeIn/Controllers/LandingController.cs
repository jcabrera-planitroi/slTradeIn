using Microsoft.AspNetCore.Mvc;

namespace slTradeIn.Controllers;

public class LandingController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult EnvironmentalAdvocacy()
    {
        return View();
    }

    public ActionResult DataDestruction()
    {
        return View();
    }
}