using BonifatiusERP.Admin.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
.AddSessionStateTempDataProvider();
builder.Services.AddMvc()
            .AddSessionStateTempDataProvider()
    //.AddJsonOptions(a => a.JsonSerializerOptions.)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)//ml
                .AddDataAnnotationsLocalization();
builder.Services.AddSession();
builder.Services.AddSingleton<UserSession>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
