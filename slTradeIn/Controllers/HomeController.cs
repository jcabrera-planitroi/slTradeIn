using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using slTradeIn.Data;
using slTradeIn.DataAccess;
using slTradeIn.Help;
using slTradeIn.Models;
using slTradeIn.Shared;

namespace slTradeIn.Controllers;

public class HomeController : CustomController
{
    private readonly Detail_ModelMaster_Data _detail_ModelMaster_Data;
    private readonly Detail_TTU_user_Data _detail_TTU_User_Data;
    private readonly Detail_TTU_userCart_Data _detail_TTU_userCart_Data;
    private readonly Detail_TTU_userCartDetail_Data _detail_TTU_userCartDetail_Data;
    private readonly Detail_TTU_userLocation_Data _detail_TTU_UserLocation_Data;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Ref_Cat_Data _ref_Cat_Data;
    private readonly Ref_main_Data _ref_main_Data;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly LocationList locationList;
    private readonly TradingMail tradingMail = new();

    public HomeController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
        Detail_TTU_user_Data detail_TTU_User_Data, Detail_TTU_userCart_Data detail_TTU_userCart_Data,
        Ref_Cat_Data ref_Cat_Data, Ref_main_Data ref_main_Data,
        Detail_TTU_userCartDetail_Data detail_TTU_userCartDetail_Data,
        Detail_TTU_userLocation_Data detail_TTU_UserLocation_Data, Detail_ModelMaster_Data detail_ModelMaster_Data)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;

        locationList = new LocationList(_httpContextAccessor);
        _detail_TTU_User_Data = detail_TTU_User_Data;
        _detail_TTU_userCart_Data = detail_TTU_userCart_Data;
        _ref_Cat_Data = ref_Cat_Data;
        _ref_main_Data = ref_main_Data;
        _detail_TTU_userCartDetail_Data = detail_TTU_userCartDetail_Data;
        _detail_TTU_UserLocation_Data = detail_TTU_UserLocation_Data;
        _detail_ModelMaster_Data = detail_ModelMaster_Data;
    }

    public ActionResult AppleQuestionnaire()
    {
        var model = new TradeInModel
        {
            iCategory = Convert.ToInt32(SessionTradeIn.iCategoryID),
            iManufacturer = Convert.ToInt32(SessionTradeIn.iManufacturerID),
            iModelID = Convert.ToInt32(SessionTradeIn.iModelNumberID)
        };

        var dx = _detail_ModelMaster_Data.GetModel(model.iModelID);
        if (dx != null)
        {
            model.Category = dx.Category;
            model.Manufacturer = dx.Manufacturer;
            model.Model = dx.Model;
        }

        return View(model);
    }

    public ActionResult ChromebookQuestionnaire()
    {
        var model = new TradeInModel
        {
            iCategory = Convert.ToInt32(SessionTradeIn.iCategoryID),
            iManufacturer = Convert.ToInt32(SessionTradeIn.iManufacturerID),
            iModelID = Convert.ToInt32(SessionTradeIn.iModelNumberID)
        };

        var dx = _detail_ModelMaster_Data.GetModel(model.iModelID);
        if (dx != null)
        {
            model.Category = dx.Category;
            model.Manufacturer = dx.Manufacturer;
            model.Model = dx.Model;
        }

        return View(model);
    }

    public ActionResult TradeInQuestionnaire()
    {
        var model = new TradeInModel
        {
            iCategory = Convert.ToInt32(SessionTradeIn.iCategoryID),
            iManufacturer = Convert.ToInt32(SessionTradeIn.iManufacturerID),
            iModelID = Convert.ToInt32(SessionTradeIn.iModelNumberID)
        };

        var dx = _detail_ModelMaster_Data.GetModel(model.iModelID);
        if (dx != null)
        {
            model.Category = dx.Category;
            model.Manufacturer = dx.Manufacturer;
            model.Model = dx.Model;
        }

        ViewBag.iManufacturerID = Convert.ToInt32(SessionTradeIn.iManufacturerID);
        return View(model);
    }

    [HttpPost]
    public ActionResult TradeInQuestionnaire(TradeInModel model)
    {
        Detail_TTU_userCart userCart;

        if (SessionTradeIn.iCartID == null || SessionTradeIn.iCartID == "0")
        {
            userCart = new Detail_TTU_userCart
            {
                iCartID = 0,
                bSerializedReport = Convert.ToBoolean(SessionTradeIn.bSerializedReport),
                iLogisticID = Convert.ToInt32(SessionTradeIn.iLogisticID),
                iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                vShippingType = Convert.ToInt32(SessionTradeIn.iShippingType),
                dDateCreated = DateTime.Now
            };
            _detail_TTU_userCart_Data.SaveCart(userCart);
            SessionTradeIn.iCartID = userCart.iCartID.ToString();
        }


        _detail_TTU_userCartDetail_Data.SaveCartDetail(new Detail_TTU_userCartDetail
        {
            iCartDetailID = model.iCartDetailID,
            iCartID = Convert.ToInt32(SessionTradeIn.iCartID),
            iCategory = model.iCategory,
            iManufacturer = model.iManufacturer,
            iModelID = model.iModelID,
            bIsAccessible = model.bIsAccesible ?? false,
            iProcessorType = model.iProcessor ?? 0,
            iProcessorGen = model.iProcessorGen ?? 0,
            iMemory = model.iMemory ?? 0,
            iHDD = model.iHDD ?? 0,
            vGrade = model.vGrade,
            iQuantity = model.iQuantity,
            //mPrice = Math.Round(model.mPrice * 80, 2),
            mPrice = 80,
            bDOD = true,
            //mTotal = Math.Round(model.mPrice * 80 * model.iQuantity, 2)
            mTotal = 80 * model.iQuantity
        });

        return RedirectToAction("TradeInCategory");
    }

    #region Box

    public ActionResult Box(int iCartID = 0)
    {
        var cartId = Convert.ToInt32(iCartID) != 0 ? Convert.ToInt32(iCartID) : Convert.ToInt32(SessionTradeIn.iCartID);
        var cart = _detail_TTU_userCart_Data.GetCart(cartId);

        if (cart.iBoxStatusID == Constants.QUOTE_SIGNED) return BoxReview(cart.iCartID);

        var details = _detail_TTU_userCartDetail_Data.GetListCartDetail(cartId);
        var referencesIds = details.SelectMany(x => new List<int>
        {
            x.iManufacturer, x.iModelID, x.iProcessorType, x.iProcessorGen, x.iMemory, x.iHDD
        });
        var references = _ref_main_Data.GetMany(referencesIds.ToList());
        var categoriesIds = details.Select(x => x.iCategory);
        var categories = _ref_Cat_Data.GetMany(categoriesIds.ToList());
        var modelsIds = details.Select(x => x.iModelID);
        var models = _detail_ModelMaster_Data.GetMany(modelsIds.ToList());

        _detail_TTU_userCart_Data.UpdateTotal(cartId);

        ViewBag.Cart = cart;
        ViewBag.Shipping = new BeforeTradeInModel
        {
            NeedSerializedAssetReport = cart.bSerializedReport,
            TypeOfShipping = cart.vShippingType
        };
        ViewBag.bHasSignature = !string.IsNullOrEmpty(cart.vImageName);

        return View(details.Select(x =>
            BoxFlowItem.FromCart(references, categories, models, x, cart.dDateCreated ?? DateTime.Now)
        ).ToList());
    }

    #endregion

    #region BoxReview

    public ActionResult BoxReview(int iCartID = 0, string view = "BoxReview")
    {
        var user = _detail_TTU_User_Data.GetProfile(SessionTradeIn.vUserEmail);
        var cartId = Convert.ToInt32(iCartID) != 0 ? Convert.ToInt32(iCartID) : Convert.ToInt32(SessionTradeIn.iCartID);


        var details = _detail_TTU_userCartDetail_Data.GetListCartDetail(cartId);
        var referencesIds = details.SelectMany(x => new List<int>
        {
            x.iManufacturer, x.iModelID, x.iProcessorType, x.iProcessorGen, x.iMemory, x.iHDD
        });
        var references = _ref_main_Data.GetMany(referencesIds.ToList());
        var categoriesIds = details.Select(x => x.iCategory);
        var categories = _ref_Cat_Data.GetMany(categoriesIds.ToList());
        var modelsIds = details.Select(x => x.iModelID);
        var models = _detail_ModelMaster_Data.GetMany(modelsIds.ToList());

        _detail_TTU_userCart_Data.UpdateTotal(cartId);
        var cart = _detail_TTU_userCart_Data.GetCart(cartId);
        ViewBag.Cart = cart;
        ViewBag.Shipping = new BeforeTradeInModel
        {
            NeedSerializedAssetReport = cart.bSerializedReport,
            TypeOfShipping = cart.vShippingType
        };
        ViewBag.CartStatus = getQuoteStatus(cart.iBoxStatusID.Value);
        ViewBag.institutionName = user.CompanyName;
        ViewBag.fullName = user.FirstName + " " + user.LastName;
        ViewBag.userTitle = user.Title;
        ViewBag.bHasSignature = !string.IsNullOrEmpty(cart.vImageName);
        ViewBag.quoteSignature = ViewBag.bHasSignature ? Path.Combine($"{user.iUserID}_Customer", cart.vImageName) : "";

        return View(view,
            details.Select(x => BoxFlowItem.FromCart(references, categories, models, x, cart.dDateCreated.Value))
                .ToList());
    }

    #endregion

    private string getQuoteStatus(int iBoxStatusID = Constants.QUOTE_CREATED)
    {
        return _ref_main_Data.GetListReference(Constants.IBOX_STATUS_ID)
            .FirstOrDefault(x => x.iRefMainID == iBoxStatusID).vDescription;
    }

    private List<BoxFlowItem> getListBoxFlowItems()
    {
        var cartId = Convert.ToInt32(SessionTradeIn.iCartID);
        var details = _detail_TTU_userCartDetail_Data.GetListCartDetail(cartId);
        var referencesIds = details.SelectMany(x => new List<int>
        {
            x.iManufacturer, x.iModelID, x.iProcessorType, x.iProcessorGen, x.iMemory, x.iHDD
        });
        var cart = _detail_TTU_userCart_Data.GetCart(cartId);
        var references = _ref_main_Data.GetMany(referencesIds.ToList());
        var categoriesIds = details.Select(x => x.iCategory);
        var categories = _ref_Cat_Data.GetMany(categoriesIds.ToList());
        var modelsIds = details.Select(x => x.iModelID);
        var models = _detail_ModelMaster_Data.GetMany(modelsIds.ToList());

        _detail_TTU_userCart_Data.UpdateTotal(cartId);

        return details.Select(x => BoxFlowItem.FromCart(references, categories, models, x, cart.dDateCreated.Value))
            .ToList();
    }

    [HttpPost]
    public ActionResult UpdateQuoteExpiredPost(int iCartID = 0)
    {
        var cartId = Convert.ToInt32(iCartID) != 0 ? Convert.ToInt32(iCartID) : Convert.ToInt32(SessionTradeIn.iCartID);
        _detail_TTU_userCart_Data.UpdateExpiredQuote(cartId);
        return BoxReview(cartId);
    }

    [HttpPost]
    public ActionResult BoxReviewPost()
    {
        var user = _detail_TTU_User_Data.GetProfile(SessionTradeIn.vUserEmail);

        var boxFlowItems = getListBoxFlowItems();
        var cartId = Convert.ToInt32(SessionTradeIn.iCartID);
        var cart = _detail_TTU_userCart_Data.GetCart(cartId);
        ViewBag.Cart = cart;
        _detail_TTU_userCart_Data.UpdateQuoteSignatureDate(cart.iCartID);
        cart = _detail_TTU_userCart_Data.GetCart(cartId);

        ViewBag.Shipping = new BeforeTradeInModel
        {
            NeedSerializedAssetReport = cart.bSerializedReport,
            TypeOfShipping = cart.vShippingType
        };
        // TODO SEND EMAIL

        var selectedTextSerializedReport = new[]
        {
            "No, I don't want a Serialized Asset Report",
            "Yes, I want a Serialized Asset Report"
        }[cart.bSerializedReport ? 1 : 0];

        var selectedTextShippingType = new[]
        {
            "We pick up your pre-packaged devices.",
            "We provide all the materials for your team to pack your devices and we pick up",
            "PlanITROI does all the work. We will pick, pack and ship for you"
        }[cart.vShippingType - 1];
        var itemList = string.Empty;
        var itemrowSR =
            new StreamReader(_webHostEnvironment.ContentRootPath + "/Content/Email/OrderCompleted1/itemrow.html");
        var itemRowTemplate = itemrowSR.ReadToEnd();
        var totalValue = 0m;
        var i = 1;
        foreach (var item in boxFlowItems)
        {
            totalValue += item.total;
            itemList += itemRowTemplate
                .Replace("{{OEM}}", item.manufacturer)
                .Replace("{{Model}}", item.model)
                .Replace("{{Configuration}}", $"{item.processorGen}, {item.processorType}, {item.memory}, {item.hdd}")
                .Replace("{{Qty}}", item.quantity.ToString())
                .Replace("{{GRADE}}", item.grade)
                .Replace("{{TRADINGPRICE}}", string.Format("{0:C}", item.price))
                .Replace("{{bgColorRow}}", i % 2 == 0 ? "#d9ead3" : "#ffffff");
            i++;
        }

        i++;

        itemList += itemRowTemplate
            .Replace("{{OEM}}", " ")
            .Replace("{{Model}}", " ")
            .Replace("{{Configuration}}", " ")
            .Replace("{{Qty}}", " ")
            .Replace("{{GRADE}}", "<strong>**TOTAL</strong>")
            .Replace("{{TRADINGPRICE}}", string.Format("{0:C}", totalValue))
            .Replace("{{bgColorRow}}", i % 2 == 0 ? "#d9ead3" : "#ffffff");
        ;


        // Load template
        using (var sr = new StreamReader(_webHostEnvironment.ContentRootPath +
                                         "/Content/Email/OrderCompleted1/index.html"))
        {
            var content = sr.ReadToEnd();
            var domainName = new Uri(HttpContext.Request.Path.ToUriComponent()).GetLeftPart(UriPartial.Authority);
            var imageArray = System.IO.File.ReadAllBytes(_webHostEnvironment.ContentRootPath +
                                                         $"/Signatures/{cart.iUserID}_Customer/{cart.vImageName}");
            var base64ImageRepresentation = Convert.ToBase64String(imageArray);
            content = content
                .Replace("{{QuoteOrder}}", cart.iQuoteNumber.ToString())
                .Replace("{{InstitutionName}}", user.CompanyName)
                .Replace("{{QuoteCreated}}", cart.dDateCreated.Value.ToLongDateString())
                .Replace("{{QuoteExpiration}}", cart.dDateCreated.Value.AddDays(30).ToLongDateString())
                .Replace("{{ClientCompany}}", user.CompanyName)
                .Replace("{{Name}}", user.FirstName + " " + user.LastName)
                .Replace("{{Title}}", user.Title)
                .Replace("{{Date}}", cart.bDateSigned.Value.ToString("MM/dd/yyyy"))
                .Replace("{{Serialized}}", selectedTextSerializedReport)
                .Replace("{{SARValue}}", string.Format("{0:C}", cart.mTotalSerializedReport))
                .Replace("{{ShippingDetail}}", selectedTextShippingType)
                .Replace("{{SValue}}", string.Format("{0:C}", cart.mTotalShipping))
                .Replace("{{DoDValue}}", string.Format("{0:C}", cart.mTotalDOD))
                .Replace("{{ItemList}}", itemList)
                .Replace("{{UrlSignature}}", $"data:image/png;base64,{base64ImageRepresentation}");

            tradingMail.SendQuoteReview(SessionTradeIn.vUserEmail, content);
        }

        return RedirectToAction("QuoteResult", "Home");
    }

    #region QuoteResult

    public ActionResult QuoteResult()
    {
        ViewBag.iCartID = SessionTradeIn.iCartID;
        return View();
    }

    #endregion

    [Route("{id}")]
    [HttpPost]
    public ActionResult SaveBox(int id, Detail_TTU_userCartDetail detail_TTU_userCartDetail)
    {
        _detail_TTU_userCartDetail_Data.Update(id, entity =>
        {
            entity.iQuantity = detail_TTU_userCartDetail.iQuantity;
            entity.mTotal = detail_TTU_userCartDetail.mPrice * detail_TTU_userCartDetail.iQuantity;
            entity.bDOD = Request.Form.ContainsKey("bDOD");
        });

        return RedirectToAction("Box", "Home");
    }

    [Route("{id}")]
    [HttpPost]
    public ActionResult DeleteBox(int id)
    {
        _detail_TTU_userCartDetail_Data.Delete(id);
        return RedirectToAction("Box", "Home");
    }

    [Authorize]
    public ActionResult UserProfile()
    {
        var user = _detail_TTU_User_Data.GetProfile(SessionTradeIn.vUserEmail);

        if (user == null) return View("Login");

        ViewBag.ListPartner = _ref_main_Data.GetList("iTTUTechPartner");
        return View(user);
    }

    [HttpPost]
    public ActionResult SaveProfile(RegisterUserModel model)
    {
        _detail_TTU_User_Data.SaveProfile(model);
        return RedirectToAction("UserProfile", "Home");
    }

    [HttpPost]
    public ActionResult UpdateSignature(SignatureModel model)
    {
        if (model.file == null) return BoxReviewPost();

        if (HttpContext.Request.Form.Files[0].Length > 0)
        {
            var rootPath = $@"\Signatures\{model.iUserID}_Customer";
            createFolder(rootPath);

            var oldName = Path.Combine(rootPath, model.vImageName ?? "");
            if (System.IO.File.Exists(oldName)) System.IO.File.Delete(oldName);

            var name = generateImageName(model.iCartID.ToString());
            var fullName = Path.Combine(_webHostEnvironment.ContentRootPath + rootPath, name);
            using (var fileStream = System.IO.File.Create(fullName))
            {
                Request.Form.Files[0].CopyToAsync(fileStream);
            }

            model.vImageName = name;
        }

        _detail_TTU_userCart_Data.UpdateQuoteSignature(model);

        return RedirectToAction("BoxReview", "Home");
    }

    private void createFolder(string fullPath)
    {
        var exists = Directory.Exists(_webHostEnvironment.ContentRootPath + fullPath);

        if (!exists) Directory.CreateDirectory(_webHostEnvironment.ContentRootPath + fullPath);
    }

    private string generateImageName(string name)
    {
        return $"{name}_{Guid.NewGuid()}.png";
    }

    public ActionResult Verify(string redirect)
    {
        SessionTradeIn.vVerificationCode = new Random().Next(100000, 999999).ToString();
        Trace.WriteLine(SessionTradeIn.vVerificationCode);
        tradingMail.SendCode(SessionTradeIn.vUserEmail, SessionTradeIn.vVerificationCode, "Verification code");

        ViewBag.Message = TempData["Message"];
        ViewBag.Redirect = redirect;
        return View();
    }

    [HttpPost]
    public ActionResult VerifyCode()
    {
        if (Request.Form["Code"].ToString() == SessionTradeIn.vVerificationCode)
            return RedirectToAction(Request.Form["Redirect"].ToString());

        TempData["Message"] = "Error";

        return RedirectToAction(
            "Verify",
            new { redirect = Request.Form["Redirect"].ToString() }
        );
    }

    public ActionResult ResetPassword()
    {
        ViewBag.Message = TempData["Message"];
        return View();
    }

    [HttpPost]
    public ActionResult SavePassword()
    {
        if (Request.Form["Pass1"].ToString() != Request.Form["Pass2"].ToString())
        {
            TempData["Message"] = "Error";
            return RedirectToAction("ResetPassword");
        }

        var user = _detail_TTU_User_Data.GetProfile(SessionTradeIn.vUserEmail);
        user.Password = Request.Form["Pass1"].ToString();
        _detail_TTU_User_Data.SavePassword(user);
        SessionTradeIn.iUserID = user.iUserID.ToString();
        return RedirectToAction("TradeInLocation");
    }

    public ActionResult ProvideEmail()
    {
        return View();
    }

    [HttpPost]
    public ActionResult SaveEmail()
    {
        SessionTradeIn.vUserEmail = Request.Form["Email"].ToString();
        return RedirectToAction("Verify", new { redirect = "ResetPassword" });
    }

    #region Login

    public ActionResult Logout()
    {
        SessionTradeIn.iUserID = string.Empty;
        SessionTradeIn.vUserEmail = string.Empty;
        SessionTradeIn.bIsTechPartner = string.Empty;
        SessionTradeIn.bIsAdmin = string.Empty;

        SessionTradeIn.iCartID = string.Empty;
        SessionTradeIn.bSerializedReport = string.Empty;
        SessionTradeIn.iShippingType = string.Empty;
        SessionTradeIn.iLogisticID = string.Empty;
        SessionTradeIn.vUserName = string.Empty;
        return RedirectToAction("Login");
    }

    public ActionResult Login()
    {
        var model = new RegisterUserModel();
        return View(model);
    }

    [HttpPost]
    public ActionResult Login(RegisterUserModel model)
    {
        var User = _detail_TTU_User_Data.GetInfo(model.Email, model.Password);
        if (User == null)
        {
            ViewBag.Message = "Login credentials incorrect";
            return View(model);
        }

        SessionTradeIn.iUserID = User.iUserID.ToString();
        SessionTradeIn.vUserEmail = User.Email;
        SessionTradeIn.bIsTechPartner = (User.iTTUTechPartner == 0 ? false : true).ToString();
        SessionTradeIn.bIsAdmin = User.bIsAdmin.ToString();
        SessionTradeIn.vUserName = User.FirstName;
        SessionTradeIn.iCartID = "0";
        var Cart = _detail_TTU_userCart_Data.GetInfo(0, User.iUserID, 0);
        if (Cart != null)
        {
            SessionTradeIn.iCartID = Cart.iCartID.ToString();
            SessionTradeIn.bSerializedReport = Cart.bSerializedReport.ToString();
            SessionTradeIn.iShippingType = Cart.vShippingType.ToString();
            SessionTradeIn.iLogisticID = Cart.iLogisticID.ToString();

            if (SessionTradeIn.iCartID != null && SessionTradeIn.iCartID != "0")
            {
                var cartDetail = _detail_TTU_userCartDetail_Data.GetListCartDetail(int.Parse(SessionTradeIn.iCartID))
                    .Count;
            }
        }

        var HasOnlyOneCart = false;
        var HasDetailInCart = false;
        var iCartID = 0;
        if (Convert.ToBoolean(SessionTradeIn.bIsTechPartner))
            return RedirectToAction("GenerateLinkCampaing", "Partner");

        _detail_TTU_userCart_Data.UserHasOnlyOneCart(User.iUserID, ref HasOnlyOneCart, ref HasDetailInCart,
            ref iCartID);
        if (HasOnlyOneCart)
        {
            if (HasDetailInCart)
                return RedirectToAction("Box");
            return RedirectToAction("BeforeTradeIn");
        }

        return RedirectToAction("BoxList");
    }

    #endregion

    #region Register user

    public ActionResult Index()
    {
        if (SessionTradeIn.iUserID != null) return RedirectToAction("UserProfile");

        ViewBag.ListPartner = _ref_main_Data.GetList("iTTUTechPartner");
        var model = new RegisterUserModel
        {
            LocationDetail = new List<Detail_TTU_userLocation>()
        };

        return View(model);
    }

    [HttpPost]
    public ActionResult Index(RegisterUserModel model)
    {
        model.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

        if (_detail_TTU_User_Data.CheckIfUserExists(model.Email))
        {
            ViewBag.ListPartner = _ref_main_Data.GetList("iTTUTechPartner");
            ViewBag.Message = "Email already registered";
            return View(model);
        }

        if (_detail_TTU_User_Data.CreateUser(model))
        {
            SessionTradeIn.iUserID = model.iUserID.ToString();
            SessionTradeIn.vUserEmail = model.Email;
            SessionTradeIn.vUserName = model.FirstName;
            var Cart = _detail_TTU_userCart_Data.GetInfo(0, model.iUserID, 0);
            if (Cart != null)
            {
                SessionTradeIn.iCartID = Cart.iCartID.ToString();
                SessionTradeIn.bSerializedReport = Cart.bSerializedReport.ToString();
                SessionTradeIn.iShippingType = Cart.vShippingType.ToString();
                SessionTradeIn.iLogisticID = Cart.iLogisticID.ToString();
                SessionTradeIn.bIsTechPartner = (model.iTTUTechPartner == 0 ? false : true).ToString();
                SessionTradeIn.bIsAdmin = false.ToString();
            }

            return RedirectToAction("Verify", new { redirect = "TradeInLocation" });
        }

        ViewBag.ListPartner = _ref_main_Data.GetList("iTTUTechPartner");
        return View(model);
    }

    #endregion

    #region Select Location

    public ActionResult TradeInLocation()
    {
        var model = new RegisterUserModel();
        model.LocationDetail = _detail_TTU_UserLocation_Data.GetLocations(Convert.ToInt32(SessionTradeIn.iUserID));
        locationList.set_list(model.LocationDetail, 1);
        return View(model);
    }

    [HttpPost]
    public ActionResult TradeInLocation(RegisterUserModel model)
    {
        _detail_TTU_UserLocation_Data.SaveLocation(new Detail_TTU_userLocation
        {
            iLogisticID = model.iLogisticID,
            iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
            vLocationLabel = model.LocationLabel,
            vLocationEmail = model.LocationEmail,
            vLocationContactPerson = model.ContactPerson,
            vLocationStreetAddress = model.StreetAddress,
            vSuiteAppt = model.SuiteApt,
            vCity = model.City,
            vState = model.State,
            vPostalCode = model.PostalCode,
            vIPCreated = HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
        });

        model.LocationDetail = _detail_TTU_UserLocation_Data.GetLocations(Convert.ToInt32(SessionTradeIn.iUserID));
        locationList.set_list(model.LocationDetail, 1);

        return View(model);
    }

    public ActionResult SelectLocation(int Location = 0)
    {
        var model = new Detail_TTU_userCart
        {
            iCartID = 0,
            bSerializedReport = Convert.ToBoolean(SessionTradeIn.bSerializedReport),
            iLogisticID = Location,
            iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
            vShippingType = Convert.ToInt32(SessionTradeIn.iShippingType)
        };
        //dc.SaveCart(model);
        SessionTradeIn.iLogisticID = Location.ToString();
        SessionTradeIn.iCartID = model.iCartID.ToString();
        return RedirectToAction("BeforeTradeIn");
    }

    #endregion

    #region Before TradeIn

    public ActionResult BeforeTradeIn()
    {
        var Cart = _detail_TTU_userCart_Data.GetInfo(Convert.ToInt32(SessionTradeIn.iCartID),
            Convert.ToInt32(SessionTradeIn.iUserID), Convert.ToInt32(SessionTradeIn.iLogisticID));

        var model = new BeforeTradeInModel
        {
            TypeOfShipping = 1,
            NeedSerializedAssetReport = true
        };

        return View(model);
    }

    [HttpPost]
    public ActionResult BeforeTradeIn(BeforeTradeInModel model, bool save = false)
    {
        if (save)
            _detail_TTU_userCart_Data.SaveCart(new Detail_TTU_userCart
            {
                iCartID = Convert.ToInt32(SessionTradeIn.iCartID),
                iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                bSerializedReport = model.NeedSerializedAssetReport,
                vShippingType = model.TypeOfShipping,
                iLogisticID = Convert.ToInt32(SessionTradeIn.iLogisticID)
            });

        SessionTradeIn.bSerializedReport = model.NeedSerializedAssetReport.ToString();
        SessionTradeIn.iShippingType = model.TypeOfShipping.ToString();

        return RedirectToAction("TradeInCategory");
    }

    #endregion

    #region Select Category

    public ActionResult TradeInCategory()
    {
        var model = new TradeInModel();

        return View(model);
    }

    [HttpPost]
    public ActionResult TradeInCategory(TradeInModel model)
    {
        return View(model);
    }

    public ActionResult SelectCategory(string Category = "")
    {
        var model = new TradeInModel();
        switch (Category)
        {
            case "MacBook":
                SessionTradeIn.iCategoryID = "97";
                SessionTradeIn.iManufacturerID = "315";
                break;
            case "iMac":
                SessionTradeIn.iCategoryID = "206";
                SessionTradeIn.iManufacturerID = "315";
                break;
            case "Mac":
                SessionTradeIn.iCategoryID = "34";
                SessionTradeIn.iManufacturerID = "315";
                break;
            case "iPad":
                SessionTradeIn.iCategoryID = "205";
                SessionTradeIn.iManufacturerID = "315";
                break;

            case "Notebooks":
                SessionTradeIn.iCategoryID = "97";
                break;
            case "AllInOne":
                SessionTradeIn.iCategoryID = "206";
                break;
            case "PC":
                SessionTradeIn.iCategoryID = "34";
                break;
            case "Chromebooks":
                SessionTradeIn.iCategoryID = "225";
                break;
        }

        if (Category == "MacBook"
            || Category == "iMac"
            || Category == "Mac"
            || Category == "iPad"
            || Category == "Chromebooks")
            return RedirectToAction("TradeInModel");

        return RedirectToAction("TradeInManufacturer");
    }

    #endregion

    #region Select Manufacturer

    public ActionResult TradeInManufacturer()
    {
        var model = new TradeInModel { iCategory = Convert.ToInt32(SessionTradeIn.iCategoryID) };
        return View(model);
    }

    [HttpPost]
    public ActionResult TradeInManufacturer(TradeInModel model)
    {
        return View(model);
    }

    public ActionResult SelectManufacturer(int Manufacturer = 0)
    {
        SessionTradeIn.iManufacturerID = Manufacturer.ToString();
        return RedirectToAction("TradeInModel");
    }

    #endregion

    #region Select Model

    public ActionResult TradeInModel()
    {
        var model = new TradeInModel
        {
            iCategory = Convert.ToInt32(SessionTradeIn.iCategoryID),
            iManufacturer = Convert.ToInt32(SessionTradeIn.iManufacturerID)
        };

        return View(model);
    }

    [HttpPost]
    public ActionResult TradeInModel(TradeInModel model)
    {
        model.ModelDetail =
            _detail_ModelMaster_Data.GetListModel(model.iCategory, model.iManufacturer, model.FilterWord);
        ViewBag.iManufacturerID = Convert.ToInt32(SessionTradeIn.iManufacturerID);
        return View(model);
    }

    public ActionResult SelectModel(int Model = 0)
    {
        SessionTradeIn.iModelNumberID = Model.ToString();
        if (SessionTradeIn.iCategoryID == "225")
            return RedirectToAction("ChromebookQuestionnaire");
        if (SessionTradeIn.iManufacturerID == "315")
            return RedirectToAction("AppleQuestionnaire");
        return RedirectToAction("TradeInQuestionnaire");
    }

    #endregion

    #region Box List

    public ActionResult BoxList()
    {
        var model = _detail_TTU_userCart_Data.GetList(Convert.ToInt32(SessionTradeIn.iUserID));

        var allDetails = _detail_TTU_userCartDetail_Data
            .FromCarts(model.Select(x => x.iCartID).ToList());

        model.ForEach(m => m.AssetsAmount = allDetails
            .Where(cd => cd.iCartID == m.iCartID)
            .Sum(x => x.iQuantity)
        );
        return View(model);
    }

    public ActionResult SelectBox(int iCartID = 0)
    {
        var Cart = _detail_TTU_userCart_Data.GetCart(iCartID);
        if (Cart != null)
        {
            SessionTradeIn.iCartID = Cart.iCartID.ToString();
            SessionTradeIn.bSerializedReport = Cart.bSerializedReport.ToString();
            SessionTradeIn.iShippingType = Cart.vShippingType.ToString();
            SessionTradeIn.iLogisticID = Cart.iLogisticID.ToString();
        }

        return RedirectToAction("Box");
    }

    #endregion

    //public override bool Equals(object? obj)
    //{
    //    return obj is HomeController controller &&
    //           EqualityComparer<Ref_Cat_Data>.Default.Equals(ref_Cat_Data, controller.ref_Cat_Data);
    //}

    //public override int GetHashCode()
    //{
    //    return HashCode.Combine(ref_Cat_Data);
    //}
}