using Documents.Domain.Interfaces.Contexts;
using Documents.Domain.Interfaces.Repositories;
using Documents.Persistence.Contexts;
using Documents.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Documents.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConnectDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DocumentsDbContext>(options => options
            .UseSqlServer(connectionString));

            services.AddScoped<IDocumentsDbContext, DocumentsDbContext>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            return services;
        }
    }
}
