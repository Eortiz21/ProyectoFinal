using Microsoft.AspNetCore.Identity;             // Biblioteca para autenticación y manejo de usuarios
using Microsoft.EntityFrameworkCore;             // Biblioteca para el acceso a bases de datos con Entity Framework Core
using ProyectoParqueo.Models;                    // Namespace donde está tu clase ERPDbContext y modelos personalizados

var builder = WebApplication.CreateBuilder(args); // Se crea el objeto builder para configurar la aplicación

// Agrega los controladores con vistas (estructura MVC)
builder.Services.AddControllersWithViews();

// Obtiene la cadena de conexión desde el archivo appsettings.json
// Si no se encuentra, lanza una excepción
var connectionString = builder.Configuration.GetConnectionString("ConexionLocalBD")
    ?? throw new InvalidOperationException("Connection string 'ConexionDesar' not found.");


//var connectionString = builder.Configuration.GetConnectionString("SomeeConexion") ??
//throw new InvalidOperationException("Connection string 'ConexionDesar' not found.");

// También podrías usar otra conexión (como a Somee), pero está comentada

// Registra el DbContext con SQL Server como proveedor de base de datos
// ServiceLifetime.Transient permite crear una nueva instancia por cada solicitud
builder.Services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

// Configura el sistema de autenticación Identity
// No requiere confirmación por correo al iniciar sesión
// Se almacenan los datos de usuarios en el contexto ERPDbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ERPDbContext>();

// Construye la aplicación web con toda la configuración previa
var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
// Si no está en entorno de desarrollo, usa página de error personalizada
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Página de error global
    app.UseHsts();                           // Aplica HTTP Strict Transport Security por 30 días
}

app.UseHttpsRedirection();  // Redirige HTTP a HTTPS
app.UseStaticFiles();       // Habilita archivos estáticos como CSS, JS, imágenes

app.UseRouting();           // Habilita el sistema de ruteo (URLs -> controladores/acciones)

app.UseAuthentication();    // Habilita el middleware de autenticación (login, cookies)
app.UseAuthorization();     // Habilita el middleware de autorización (verifica permisos del usuario)

// Ruta para controladores dentro de "áreas" (estructura modular)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Ruta por defecto: HomeController -> Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapea las Razor Pages (necesario para Identity)
app.MapRazorPages();

// Ejecuta la aplicación
app.Run();
