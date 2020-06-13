namespace Authentication.Helpers
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;


    namespace WebApi.Helpers
    {
        public class DataContext : DbContext
        {
            protected readonly IConfiguration Configuration;

            public DataContext(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                // connect to sql server database
                options.UseSqlServer(Configuration.GetConnectionString("AuthDatabase"));
            }

            public DbSet<User> Users { get; set; }
        }
    }
}