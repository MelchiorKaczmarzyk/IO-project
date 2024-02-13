using IOProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IOProjectDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IOProjectDbContextConnection' not found.");
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IOProjectDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<IHelpProjectRepos, HelpProjectRepos>();

builder.Services.AddDefaultIdentity<SystemUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IOProjectDbContext>();

/*//Change default login requirements. (Doesn't want to work, i gave up)
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings. Default values in comments.
    options.Password.RequireDigit = false; //true
    options.Password.RequireLowercase = true; //true
    options.Password.RequireNonAlphanumeric = false; //true
    options.Password.RequireUppercase = false; //true
    options.Password.RequiredLength = 3; //6
    options.Password.RequiredUniqueChars = 1; //1

    // Username settings.
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
});*/

var app = builder.Build();


app.UseSession();
app.UseStaticFiles();
app.UseAuthentication();
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.UseRouting();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Organisation", "Beneficiary", "Benefactor" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role)); 
        }

    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<SystemUser>>();
    string email = "admin@admin.com";
    string password = "Password1!";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new SystemUser();
        user.Email = email;
        user.UserName = email;
        await userManager.CreateAsync(user,password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
    app.Run();
