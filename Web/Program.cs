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

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();


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

app.Run();
