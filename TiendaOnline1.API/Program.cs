using Microsoft.EntityFrameworkCore;
using TiendaOnline.API.Data;
var builder = WebApplication.CreateBuilder(args);
// Forzar que la app escuche en la URL HTTPS especificada (ajusta el puerto si es necesario)
builder.WebHost.UseUrls("https://localhost:7164");
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