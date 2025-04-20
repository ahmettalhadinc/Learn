using Learn.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repository.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
             builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x=>x.Status).HasDefaultValue(true);
            builder.HasQueryFilter(x => x.Status);
          //  builder.Property(x=>x.Name).HasMaxLength(50).IsRequired();
    

        }
    }
}
