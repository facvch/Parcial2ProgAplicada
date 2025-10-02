using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Sql
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Infrastructure.Repositories.Sql
    {
        public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
        {
            public StoreDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

                //
                optionsBuilder.UseSqlServer(
                       "Server=(localdb)\\MSSQLLocalDB;Database=ParcialDB;Trusted_Connection=true;TrustServerCertificate=true");

                return new StoreDbContext(optionsBuilder.Options);
            }
        }
    }

}
