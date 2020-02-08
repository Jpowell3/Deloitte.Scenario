using Deloitte.Scenario.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Data
{
    public interface ICityContext
    {
        DbSet<CityEntity> Cities { get; set; }

        Task<Int32> SaveChangesAsync();
    }
}