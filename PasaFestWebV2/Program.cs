using Microsoft.EntityFrameworkCore;
using PasaFestWebV2.Models; // Asegurate que este namespace coincida con tu carpeta Models

var builder = WebApplication.CreateBuilder(args);

// 👉 Agregar soporte para controladores con vistas
builder.Services.AddControllersWithViews();

// 👉 Registrar el DbContext usando la cadena de conexión del appsettings.json
builder.Services.AddDbContext<PasaFestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 👉 Middleware de manejo de errores y seguridad
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 👉 Middleware base
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 👉 Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 👉 Ejecutar la app
app.Run();
