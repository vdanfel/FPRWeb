using FPRWeb.Areas.Externo.Interface;
using FPRWeb.Areas.Externo.Service;
using FPRWeb.Interface.Login;
using FPRWeb.Service.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;

AppContext.SetSwitch("Microsoft.Data.SqlClient.DisablePerformanceCounters", true);
AppContext.SetSwitch("SqlClient.DisableRetrying", true);
AppContext.SetSwitch("Microsoft.Data.SqlClient.EnableRetryLogic", false);

var builder = WebApplication.CreateBuilder(args);

// **Agregar autenticación con cookies**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login"; // Redirige al login si no está autenticado
        options.AccessDeniedPath = "/Login/AccesoDenegado"; // Página de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Expiración de la cookie
    });

builder.Services.AddAuthorization();

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

// Registrar LoginService y SqlConnection
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEquipoService,EquipoService>();
builder.Services.AddScoped<SqlConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("FPRConnection");
    return new SqlConnection(connectionString);
});

var app = builder.Build();

// Configurar Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Login");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// **Agregar autenticación y autorización**
app.UseAuthentication();
app.UseAuthorization();

// Configurar rutas de áreas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "externo",
    pattern: "Externo/{controller=Equipo}/{action=Equipo}/{id?}");

app.MapControllerRoute(
    name: "interno",
    pattern: "Interno/{controller=Jugadores}/{action=Jugadores}/{id?}");

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
