using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.DataAccess;
using slTradeIn.Help;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PlanITVisionContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<PlanITVisionContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();

builder.Services.AddScoped<Detail_TTU_emailTemplate_Data>();
builder.Services.AddScoped<Detail_TTU_user_Data>();
builder.Services.AddScoped<Detail_TTU_userCart_Data>();
builder.Services.AddScoped<Detail_TTU_userLocation_Data>();
builder.Services.AddScoped<Ref_main_Data>();
builder.Services.AddScoped<Ref_Cat_Data>();
builder.Services.AddScoped<Detail_TTU_userManualQuote_Data>();
builder.Services.AddScoped<Detail_TTU_userManualQuoteDetail_Data>();
builder.Services.AddScoped<Detail_TTU_userManualQuoteLocation_Data>();
builder.Services.AddScoped<Detail_TTU_userEmailCampaign_Data>();
builder.Services.AddScoped<Detail_TTU_userCartDetail_Data>();
builder.Services.AddScoped<Detail_TTU_userEmail_Data>();
builder.Services.AddScoped<Detail_ModelMaster_Data>();

// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//     options.CheckConsentNeeded = context => true;
//     options.MinimumSameSitePolicy = SameSiteMode.None;
// });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
});

builder.Services.AddHttpContextAccessor();
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
// app.Use(async (context, next) =>
// {
//     var sessionValueProvider = new SessionTradeIn.WebSessionValueProvider(context.Session);
//     SessionTradeIn.Initialize(sessionValueProvider);
//     await next();
// });

var sessionValueProvider = new SessionTradeIn.WebSessionValueProvider(app.Services.GetService<IHttpContextAccessor>() ??
                                                                      throw new Exception(
                                                                          "HTTP CONTEXT ACCESSOR NOT INITIALIZED"));
SessionTradeIn.Initialize(sessionValueProvider);

app.MapRazorPages();
app.MapControllerRoute(
    "default",
    "{controller=Landing}/{action=Index}/{id?}");

app.Run();