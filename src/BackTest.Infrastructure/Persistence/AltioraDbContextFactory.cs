using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Infrastructure.Persistence
{
    public class AltioraDbContextFactory: IDesignTimeDbContextFactory<AltioraDbContext>
    {
        public AltioraDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AltioraDbContext>();
            var connectionString = configuration.GetConnectionString("ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);

            return new AltioraDbContext(optionsBuilder.Options);
        }
    }
}
