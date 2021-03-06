using Duende.IdentityServer.Services;
using FoodShoop.Services.Identity;
using FoodShoop.Services.Identity.DBContext;
using FoodShoop.Services.Identity.Initializer;
using FoodShoop.Services.Identity.Models;
using FoodShoop.Services.Identity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.IdentityResources).
AddInMemoryApiScopes(SD.ApiScopes).
AddInMemoryClients(SD.Clients).
AddAspNetIdentity<ApplicationUser>().AddDeveloperSigningCredential();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    IdentityModelEventSource.ShowPII = true;
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
