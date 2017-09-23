using Microsoft.EntityFrameworkCore;
using SondageApi.Model;

namespace SondageApi.Model
{
    public class SondageContext : DbContext
    {
        public SondageContext(DbContextOptions<SondageContext> options)
            : base(options)
        {
        }

        public DbSet<SimpleSondageDAO> SimpleSondageDAOs { get; set; }

    }
}