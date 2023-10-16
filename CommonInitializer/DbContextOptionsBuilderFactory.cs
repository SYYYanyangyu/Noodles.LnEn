using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CommonInitializer
{
    public static class DbContextOptionsBuilderFactory
    {
        private static readonly WebApplicationBuilder build;
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {         
            string connStr = build.Configuration.GetSection("ConnectionString").Value;
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=YouzackVNextDB;User ID=sa;Password=dLLikhQWy5TBz1uM;");
            optionsBuilder.UseSqlServer(connStr);
            return optionsBuilder;
        }
    }
}
