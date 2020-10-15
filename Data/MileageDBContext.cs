using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MileageDBContext : DbContext
    {
        public MileageDBContext(DbContextOptions<MileageDBContext> options)
            : base(options)
        {

        }
        public DbSet<MILEAGE> Mileage { get; set; }

    }
}
