using DockerMVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookDbContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
var app = builder.Build();
InitilizeDatabase(app);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void InitilizeDatabase(WebApplication app)
{
    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var database = serviceScope.ServiceProvider.GetService<BookDbContext>()?.Database;
        database?.Migrate();
    }
}


