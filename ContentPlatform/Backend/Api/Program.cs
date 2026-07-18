using Api.Data;
using Api.Modules.Catalog.Repositories;
using Api.Modules.Catalog.Services;
using Api.Modules.Identity.Repositories;
using Api.Modules.Identity.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Database connection
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la conexión DefaultConnection."
    );

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

//CORS para conectar el front
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


//Content
builder.Services.AddScoped<ContentRepository>();
builder.Services.AddScoped<ContentService>();
builder.Services.AddScoped<ContentImporter>();

//Identity
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SessionRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthorizationService>();

var app = builder.Build();


//Importar content del csv
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Contents.Any())
    {
        var importer = scope.ServiceProvider
            .GetRequiredService<ContentImporter>();

        var source = Path.Combine(
            app.Environment.ContentRootPath,
            "Data",
            "Import",
            "MovieLens",
            "movies.csv");

        importer.ImportContent(source);
    }
}

app.UseCors("Frontend");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
