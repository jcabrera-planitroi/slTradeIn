using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using slTradeIn.Areas.Identity.Middleware;
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
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<PlanITVisionContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var sessionValueProvider = new SessionTradeIn.WebSessionValueProvider(app.Services.GetService<IHttpContextAccessor>() ??
                                                                      throw new Exception(
                                                                          "HTTP CONTEXT ACCESSOR NOT INITIALIZED"));
SessionTradeIn.Initialize(sessionValueProvider);

app.UseMiddleware<FixAntiForgeryIssueMiddleware>("/account/login");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseHttpLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Landing}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();