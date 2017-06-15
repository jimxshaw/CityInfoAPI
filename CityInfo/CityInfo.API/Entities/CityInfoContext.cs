using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    // There's no need for all entities that map to tables in a database
    // to be in the same context. Create additional contexts accordingly.
    // The context must be registered in Startup so that it's available for
    // dependency injection.
    public class CityInfoContext : DbContext
    {
        // Define db sets for our entities. These are used to query and 
        // save instances of its entity type. LINQ queries against the
        // db set will translate into queries against the database.
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
                                    : base(options)
        {
            // The Database object is defined on DbContext.
            // If the db exists, nothing happens. If the db doesn't exist, it 
            // will be created wherever this context is injected (typically 
            // by constructor injection is a controller).
            Database.EnsureCreated();
        }
    }
}
