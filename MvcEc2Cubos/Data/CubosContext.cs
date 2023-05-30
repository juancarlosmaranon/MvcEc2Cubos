using Microsoft.EntityFrameworkCore;
using MvcEc2Cubos.Models;

namespace MvcEc2Cubos.Data
{
    public class CubosContext: DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options)
            : base(options) { }
        public DbSet<Cubo> Cubo { get; set; }
    }
}
