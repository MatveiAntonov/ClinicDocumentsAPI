using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Documents.Persistence.EntityTypeConfigurations
{
    public class PhotosConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(photo => photo.Id);
            builder.HasIndex(photo => photo.Id).IsUnique();
			builder.Property(photo => photo.Name).HasMaxLength(250)
                .IsRequired();
            builder.Property(photo => photo.Url).IsRequired();
        }
    }
}
