using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WilmerFlorez.Entities;

namespace WilmerFlorez.Database.Configurations
{
    public class PropertyConfiguration
    {
        public PropertyConfiguration(EntityTypeBuilder<Property> entityBuilder)
        {
            entityBuilder.HasKey(c => c.IdProperty);
            entityBuilder.HasOne(c => c.Owner).WithMany(c => c.Properties).HasForeignKey(c => c.IdOwner);
        }
    }
}
