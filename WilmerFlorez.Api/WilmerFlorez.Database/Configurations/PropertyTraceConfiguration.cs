using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WilmerFlorez.Entities;

namespace WilmerFlorez.Database.Configurations
{
    public class PropertyTraceConfiguration
    {
        public PropertyTraceConfiguration(EntityTypeBuilder<PropertyTrace> entityBuilder)
        {
            entityBuilder.HasKey(c => c.IdPropertyTrace);
            entityBuilder.HasOne(c => c.Property).WithMany(c => c.PropertyTraces).HasForeignKey(c => c.IdProperty);
        }
    }
}
