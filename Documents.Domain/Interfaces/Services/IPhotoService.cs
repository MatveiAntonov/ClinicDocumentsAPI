using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.AspNetCore.Http;

namespace Documents.Domain.Interfaces.Services
{
    public interface IPhotoService
    {
        Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken);
        Task<Blob?> DownloadAsync(int id, CancellationToken cancellationToken);
        Task<BlobResponse> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<BlobResponse> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
