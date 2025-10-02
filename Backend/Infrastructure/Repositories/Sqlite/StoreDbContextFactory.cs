using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Sqlite
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Infrastructure.Repositories.Sqlite
    {
        public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
        {
            public StoreDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

                optionsBuilder.UseSqlite("Data Source=ParcialDB.db");

                return new StoreDbContext(optionsBuilder.Options);
            }
        }
    }

}
