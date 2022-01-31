using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WilmerFlorez.Entities;

namespace WilmerFlorez.Database.Configurations
{
    public class PropertyImageConfiguration
    {
        public PropertyImageConfiguration(EntityTypeBuilder<PropertyImage> entityBuilder)
        {
            entityBuilder.HasKey(c => c.IdPropertyImage);
            entityBuilder.HasOne(c => c.Property).WithMany(c => c.PropertyImages).HasForeignKey(c => c.IdProperty);
        }
    }
}
