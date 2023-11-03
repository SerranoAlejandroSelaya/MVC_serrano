using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MVC.Controllers.tasks.repository;
using MVC.Controllers.tasks.service;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var mongoConnectionString = configuration.GetConnectionString("MongoConnection");

var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase("DatabaseName");
builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

// Registrar los servicios
builder.Services.AddScoped<ITaskRep, TaskRepImpl>();
builder.Services.AddScoped<CardSev>();

var app = builder.Build();

// Configurar el middleware y las rutas
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar la ruta predeterminada
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Card}/{action=Index}/{id?}");

app.Run();