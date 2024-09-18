using System.Security.Claims;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Inicializar Firebase
FirebaseConfig.Initialize();

// Configurar Google Cloud Storage
string credentialPath =
    @"C:\Users\Fermin\Desktop\inmoviliriaSegundoCuatri\Inmobiliaria2Cuarti\Config\inmobilirianet-bda045475369.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Autenticación - cookies
builder
    .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Error/401";
    });

// Autorización para manejar permisos
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "Administrador",
        policy => policy.RequireClaim(ClaimTypes.Role, "Administrador")
    );
    options.AddPolicy("Empleado", policy => policy.RequireClaim(ClaimTypes.Role, "Empleado"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/500");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

// Manejar solicitudes no encontradas
app.Use(
    async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Request.Path = "/Home/Restringido";
            await next();
        }
    }
);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(name: "authenticated", pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization();

app.Run();
