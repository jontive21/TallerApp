using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TallerApp.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // Necesario para la API

// Registrar los repositorios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioCliente, RepositorioCliente>();
builder.Services.AddScoped<IRepositorioVehiculo, RepositorioVehiculo>();
builder.Services.AddScoped<IRepositorioMecanico, RepositorioMecanico>();
builder.Services.AddScoped<IRepositorioRepuesto, RepositorioRepuesto>();
builder.Services.AddScoped<IRepositorioOrdenTrabajo, RepositorioOrdenTrabajo>();
builder.Services.AddScoped<IRepositorioComprobantePago, RepositorioComprobantePago>();

// 1. Configurar Autorización (Políticas de Roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SoloAdmin", policy => policy.RequireRole("Admin"));
});

// 2. Configurar Autenticación: Cookies (Web por defecto) + JWT (API)
builder.Services.AddAuthentication(options =>
{
    // ¡IMPORTANTE! El esquema por defecto es Cookies para que la web funcione normal
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccesoDenegado";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "TallerAppIssuer",
        ValidAudience = "TallerAppAudience",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("TuClaveSecretaMuyLargaYSeguraDeAlMenos32Caracteres123!")
        )
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ¡El orden es importante!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();