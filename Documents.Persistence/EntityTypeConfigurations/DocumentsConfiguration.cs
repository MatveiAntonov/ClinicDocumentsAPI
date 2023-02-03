using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Persistence.EntityTypeConfigurations
{
    public class DocumentsConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(document => document.Id);
            builder.HasIndex(document => document.Id).IsUnique();
            builder.Property(document => document.Name).HasMaxLength(250)
                .IsRequired();
            builder.Property(document => document.Url).IsRequired();
        }
    }
}
