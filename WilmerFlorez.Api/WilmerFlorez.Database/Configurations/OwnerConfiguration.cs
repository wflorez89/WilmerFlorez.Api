using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WilmerFlorez.Entities;

namespace WilmerFlorez.Database.Configurations
{
    public class OwnerConfiguration
    {
        public OwnerConfiguration(EntityTypeBuilder<Owner> entityBuilder)
        {
            entityBuilder.HasKey(c => c.IdOwner);
        }
    }
}
