using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TiendaOnline.API.Data
{
    // Factory used by EF Core tools at design time to create the DbContext
    public class TiendaOnlineDbContextFactory : IDesignTimeDbContextFactory<TiendaOnlineDbContext>
    {
        public TiendaOnlineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TiendaOnlineDbContext>();

            // Cadena de conexión usada solo en tiempo de diseño. Asegúrate que coincide con appsettings.json
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TiendaOnlineDb;Integrated Security=True;TrustServerCertificate=True;");

            return new TiendaOnlineDbContext(optionsBuilder.Options);
        }
    }
}
