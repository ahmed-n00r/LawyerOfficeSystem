using AuthorizeLibrary.Data;
using DBModels.IdentityModel;
using jsonCultuerLocalizerLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DBLow") ?? throw new InvalidOperationException("Connection string 'DBLow' not found.");

ApplicationDbContext.DBConnctionString = connectionString;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IStringLocalizerFactory, jsonStringLocalizerFactory>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(option =>
    {
        option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(jsonStringLocalizerFactory));
    });

var supportedLanguage = new[] { "en", "ar" }; //this array of language

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = supportedLanguage.Select(c => new CultureInfo(c)).ToList();

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedLanguage[0])
    .AddSupportedCultures(supportedLanguage)
    .AddSupportedUICultures(supportedLanguage);

app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
