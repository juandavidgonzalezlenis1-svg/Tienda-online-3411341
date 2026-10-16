using Microsoft.EntityFrameworkCore;
using TiendaOnline.API.Data;
var builder = WebApplication.CreateBuilder(args);
// Sólo forzar la URL de escucha en desarrollo local. En entornos de producción (Railway, Azure, etc.)
// el host/proxy asignará la URL/puerto adecuados y forzar localhost puede provocar errores de binding.
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("https://localhost:7164");
}
else
{
    // En plataformas como Railway se nos asigna un puerto en la variable de entorno PORT.
    // Configuramos Kestrel para escuchar en cualquier IP en ese puerto (HTTP) para evitar errores de bind a localhost.
    var portEnv = Environment.GetEnvironmentVariable("PORT");
    if (!string.IsNullOrEmpty(portEnv) && int.TryParse(portEnv, out var port))
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(port);
        });
    }
}
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TiendaOnlineDbContext>(options =>
options.UseSqlServer(builder.Configuration
.GetConnectionString("TiendaOnlineConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirWasm", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});
var app = builder.Build();
// Habilitar Swagger siempre (útil para desarrollo local y pruebas de redirección)
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors("PermitirWasm");
app.UseAuthorization();
app.MapControllers();
app.Run();