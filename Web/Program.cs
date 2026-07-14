using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using CitasApp.Data;
using CitasApp.Application.Interfaces;
using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
});

// ── Carpeta de datos para los Adapters ───────────────────────────────────────
var dataFolder = Path.Combine(builder.Environment.WebRootPath ?? Path.Combine(builder.Environment.ContentRootPath, "wwwroot"), "data");
Directory.CreateDirectory(dataFolder);

// Rutas para CSV y SQLite (siempre activas — solo son textos, no prenden nada)
var csvPacientes = Path.Combine(dataFolder, "pacientes.csv");
var csvMedicos   = Path.Combine(dataFolder, "medicos.csv");
var csvCitas     = Path.Combine(dataFolder, "citas.csv");

// Ruta para SQLite (un solo archivo .db para las 3 tablas)
var sqlitePath   = Path.Combine(dataFolder, "citasapp.db");

// ── Adapters de salida (Infrastructure) ──────────────────────────────────────
// Descomenta el bloque que quieras y comenta los otros dos.
// ¡Las interfaces (Ports) no cambian!

// ▶ Bloque A — JSON (como estaba antes)


builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();
builder.Services.AddScoped<IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env  = sp.GetRequiredService<IWebHostEnvironment>();
    var repo = RepositoryFactory.CrearPacienteRepository(
                    builder.Environment.EnvironmentName, env);
    return new LoggingPacienteRepository(repo);
});


// ▶ Bloque B — CSV  ← activo ahora
 /*
builder.Services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));
builder.Services.AddSingleton<IMedicoRepository>  (_ => new CsvMedicoRepository(csvMedicos));
builder.Services.AddSingleton<ICitaRepository>    (_ => new CsvCitaRepository(csvCitas));
*/

// ▶ Bloque C — SQLite
/*
builder.Services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
builder.Services.AddSingleton<IMedicoRepository>  (_ => new SqliteMedicoRepository(sqlitePath));
builder.Services.AddSingleton<ICitaRepository>    (_ => new SqliteCitaRepository(sqlitePath));
*/

// Núcleo de negocio (Application Services)
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var db          = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    db.Database.Migrate();

    foreach (var rol in new[] { "Admin", "Medico", "Paciente" })
        if (!await roleManager.RoleExistsAsync(rol))
            await roleManager.CreateAsync(new IdentityRole(rol));

    await SeedUsuario(userManager, "jorgepedrozo@gmail.com", "Admin@123", "Admin");
    await SeedUsuario(userManager, "josuepoot@gmail.com",    "Paciente@123", "Paciente");
    await SeedUsuario(userManager, "carlos.reyes@citasapp.com",    "Medico@123", "Medico");
    await SeedUsuario(userManager, "patricia.vega@citasapp.com",   "Medico@123", "Medico");
    await SeedUsuario(userManager, "roberto.sanchez@citasapp.com", "Medico@123", "Medico");
}

await app.RunAsync();

static async Task SeedUsuario(UserManager<IdentityUser> um, string email, string password, string rol)
{
    if (await um.FindByEmailAsync(email) != null) return;
    var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
    var result = await um.CreateAsync(user, password);
    if (result.Succeeded) await um.AddToRoleAsync(user, rol);
}
