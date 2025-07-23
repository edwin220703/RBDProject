using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using RBDProject.Components;
using RBDProject.Components.Helpers;
using RBDProject.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//SERVICIO DE CADENA DE CONEXION
builder.Services.AddDbContext<BdrbdContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//SERVICIO DE CONFIGURACION
builder.Services.Configure<Configuracion>(
    builder.Configuration.GetSection("Configuracion")
);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient();

//SERVIDOR LOCAL HOST
builder.Services.AddHttpClient("Servidor", client =>
{
    client.BaseAddress = new Uri("https://localhost:44359/");
});

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddControllers();

builder.Services.AddServerSideBlazor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
