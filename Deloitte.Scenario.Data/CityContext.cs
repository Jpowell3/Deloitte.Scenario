using Deloitte.Scenario.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Data
{
    public class CityContext : DbContext, ICityContext
    {
        public CityContext(DbContextOptions<CityContext> options)
            : base(options)
        {
        }

        public DbSet<CityEntity> Cities { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}

