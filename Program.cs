using FPRWeb.Interface.Login;
using FPRWeb.Service.Login;
using Microsoft.Data.SqlClient;

AppContext.SetSwitch("Microsoft.Data.SqlClient.DisablePerformanceCounters", true);
AppContext.SetSwitch("SqlClient.DisableRetrying", true);
AppContext.SetSwitch("Microsoft.Data.SqlClient.EnableRetryLogic", false);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Asegúrate de que LoginService y SqlConnection estén registrados correctamente
builder.Services.AddScoped<ILoginService, LoginService>();

// Configurar SqlConnection con un ciclo de vida adecuado
builder.Services.AddScoped<SqlConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("FPRConnection");
    return new SqlConnection(connectionString);
});
builder.Services.AddSession(options =>
{
    // Configura las opciones de la sesión según tus necesidades
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
var app = builder.Build();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Luego tus rutas específicas
app.MapControllerRoute(
    name: "externo",
    pattern: "Externo/{controller=Equipo}/{action=Equipo}/{id?}");

app.MapControllerRoute(
    name: "interno",
    pattern: "Interno/{controller=Jugadores}/{action=Jugadores}/{id?}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Login");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
