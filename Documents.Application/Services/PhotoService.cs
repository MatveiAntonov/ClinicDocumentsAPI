using Documents.Domain.DTOs;
using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Interfaces.Contexts;
using Documents.Domain.Interfaces.Repositories;
using Documents.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Documents.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _documentRepository;
        private readonly IDocumentsDbContext _documentsDbContext;

        public PhotoService(IPhotoRepository repository, IDocumentsDbContext documentsDbContext)
        {
            _documentRepository = repository;
            _documentsDbContext = documentsDbContext;

        }
        public Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken)
        {
            return _documentRepository.ListAsync(cancellationToken);
        }

        public async Task<Blob?> DownloadAsync(int id, CancellationToken cancellationToken)
        {
            var doc = await _documentsDbContext.Photos.FindAsync(id);

            if (doc is not null)
            {
                return await _documentRepository.DownloadAsync(doc.Name, cancellationToken);
            }
            return null;
        }

        public async Task<BlobResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var doc = await _documentsDbContext.Photos.FindAsync(id);

            if (doc is not null)
            {
                return await _documentRepository.DeleteAsync(doc.Name, cancellationToken);
            }
            return null;
        }

        public async Task<BlobResponse> UploadAsync(PhotoDto file, CancellationToken cancellationToken)
        {
            return await _documentRepository.UploadAsync(file, cancellationToken);
        }
    }
}
