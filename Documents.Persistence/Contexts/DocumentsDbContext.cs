using Documents.Domain.Entities.EntitiesLocationData;
using Documents.Domain.Interfaces.Contexts;
using Documents.Persistence.EntityTypeConfigurations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace Documents.Persistence.Contexts
{
    public class DocumentsDbContext : DbContext, IDocumentsDbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DocumentsConfiguration());
            builder.ApplyConfiguration(new PhotosConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
