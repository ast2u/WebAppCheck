using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppCheck.Areas.Identity.Data;
using WebAppCheck.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri("https://keyvaultcarlo.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
//var connectionString = builder.Configuration.GetConnectionString("Basecarlodata") ?? throw new InvalidOperationException("Connection string 'WebappDB' not found.");

var LocalconnectionString = builder.Configuration.GetConnectionString("WebDbContextConnection") ?? throw new InvalidOperationException("Connection string 'WebappDB' not found.");

//builder.Services.AddDbContext<WebDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<WebDbContext>(options => options.UseSqlServer(LocalconnectionString).EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddDefaultIdentity<WebAppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<WebDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
