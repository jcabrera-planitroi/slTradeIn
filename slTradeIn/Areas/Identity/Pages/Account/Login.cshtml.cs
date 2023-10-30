// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using slTradeIn.DataAccess;
using slTradeIn.Help;

namespace slTradeIn.Areas.Identity.Pages.Account;

[IgnoreAntiforgeryToken]
public class LoginModel : PageModel
{
    private readonly Detail_TTU_user_Data _detail_TTU_User_Data;
    private readonly Detail_TTU_userCart_Data _detail_TTU_userCart_Data;
    private readonly Detail_TTU_userCartDetail_Data _detail_TTU_userCartDetail_Data;
    private readonly ILogger<LoginModel> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger,
        Detail_TTU_user_Data detailTtuUserData, Detail_TTU_userCart_Data detailTtuUserCartData,
        Detail_TTU_userCartDetail_Data detailTtuUserCartDetailData)
    {
        _signInManager = signInManager;
        _logger = logger;
        _detail_TTU_User_Data = detailTtuUserData;
        _detail_TTU_userCart_Data = detailTtuUserCartData;
        _detail_TTU_userCartDetail_Data = detailTtuUserCartDetailData;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string returnUrl = null)
    {
        if (_signInManager.IsSignedIn(User)) return RedirectToAction("BoxList", "Home");

        if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        ReturnUrl = returnUrl;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                var User = _detail_TTU_User_Data.GetInfo(Input.Email);

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
                        var cartDetail = _detail_TTU_userCartDetail_Data
                            .GetListCartDetail(int.Parse(SessionTradeIn.iCartID))
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
                        return RedirectToAction("Box", "Home");
                    return RedirectToAction("BeforeTradeIn", "Home");
                }

                return RedirectToAction("BoxList", "Home");
                // return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}