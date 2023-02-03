using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.EntityFrameworkCore;

namespace Documents.Domain.Interfaces.Contexts
{
    public interface IDocumentsDbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
