using Microsoft.AspNetCore.Identity;             // Biblioteca para autenticaci�n y manejo de usuarios
using Microsoft.EntityFrameworkCore;             // Biblioteca para el acceso a bases de datos con Entity Framework Core
using ProyectoParqueo.Models;                    // Namespace donde est� tu clase ERPDbContext y modelos personalizados

var builder = WebApplication.CreateBuilder(args); // Se crea el objeto builder para configurar la aplicaci�n

// Agrega los controladores con vistas (estructura MVC)
builder.Services.AddControllersWithViews();

// Obtiene la cadena de conexi�n desde el archivo appsettings.json
// Si no se encuentra, lanza una excepci�n
var connectionString = builder.Configuration.GetConnectionString("ConexionLocalBD")
    ?? throw new InvalidOperationException("Connection string 'ConexionDesar' not found.");


//var connectionString = builder.Configuration.GetConnectionString("SomeeConexion") ??
//throw new InvalidOperationException("Connection string 'ConexionDesar' not found.");

// Tambi�n podr�as usar otra conexi�n (como a Somee), pero est� comentada

// Registra el DbContext con SQL Server como proveedor de base de datos
// ServiceLifetime.Transient permite crear una nueva instancia por cada solicitud
builder.Services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

// Configura el sistema de autenticaci�n Identity
// No requiere confirmaci�n por correo al iniciar sesi�n
// Se almacenan los datos de usuarios en el contexto ERPDbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ERPDbContext>();

// Construye la aplicaci�n web con toda la configuraci�n previa
var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
// Si no est� en entorno de desarrollo, usa p�gina de error personalizada
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // P�gina de error global
    app.UseHsts();                           // Aplica HTTP Strict Transport Security por 30 d�as
}

app.UseHttpsRedirection();  // Redirige HTTP a HTTPS
app.UseStaticFiles();       // Habilita archivos est�ticos como CSS, JS, im�genes

app.UseRouting();           // Habilita el sistema de ruteo (URLs -> controladores/acciones)

app.UseAuthentication();    // Habilita el middleware de autenticaci�n (login, cookies)
app.UseAuthorization();     // Habilita el middleware de autorizaci�n (verifica permisos del usuario)

// Ruta para controladores dentro de "�reas" (estructura modular)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Ruta por defecto: HomeController -> Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapea las Razor Pages (necesario para Identity)
app.MapRazorPages();

// Ejecuta la aplicaci�n
app.Run();
