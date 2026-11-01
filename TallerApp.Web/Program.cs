using TallerApp.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

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

// 2. Configurar autenticación por cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options => {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccesoDenegado";
    });

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();