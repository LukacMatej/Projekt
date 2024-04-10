using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test.Data;
using UW.AspNetCore.Authentication;
using UW.Shibboleth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = ShibbolethDefaults.AuthenticationScheme;
}).AddUWShibboleth(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        var attributes = new ShibbolethAttributeValueCollection()
        {
            new ShibbolethAttributeValue("uid", "bbadger"),
            new ShibbolethAttributeValue("givenName", "Bucky"),
            new ShibbolethAttributeValue("sn", "Badger"),
            new ShibbolethAttributeValue("mail", "bucky.badger@wisc.edu"),
            new ShibbolethAttributeValue("wiscEduPVI", "UW999A999"),
            new ShibbolethAttributeValue("isMemberOf", "uw:domain:dept.wisc.edu:administrativestaff;uw:domain:dept.wisc.edu:it:sysadmin")
        };
        options.Events = new ShibbolethEvents
        {
            OnSelectingProcessor = ctx =>
            {
                ctx.Processor = new ShibbolethDevelopmentProcessor(attributes);
                return Task.CompletedTask;
            }
        };
    }

});*/

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] {"Admin", "Lecturer", "Student" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "lecturer@lecturer.com";
    string password = "Aa1234.";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Lecturer");
    }
    email = "student@student.com";
    password = "Aa1234.";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Student");
    }
}
app.Run();
