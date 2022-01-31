using WilmerFlorez.Entities;
using Microsoft.EntityFrameworkCore;
using WilmerFlorez.Database.Configurations;

namespace WilmerFlorez.Database
{
    public class ContextDb : DbContext
    {
        public ContextDb(
            DbContextOptions options
        )
            : base(options)
        {
        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Wf");
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new PropertyTraceConfiguration(modelBuilder.Entity<PropertyTrace>());
            new OwnerConfiguration(modelBuilder.Entity<Owner>());
            new PropertyConfiguration(modelBuilder.Entity<Property>());
            new PropertyImageConfiguration(modelBuilder.Entity<PropertyImage>());
            
        }
    }
}
