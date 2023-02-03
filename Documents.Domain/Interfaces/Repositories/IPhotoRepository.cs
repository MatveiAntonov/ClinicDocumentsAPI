using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.AspNetCore.Http;

namespace Documents.Domain.Interfaces.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken);
        Task<Blob?> DownloadAsync(string blobFileName, CancellationToken cancellationToken);
        Task<BlobResponse> DeleteAsync(string blobFileName, CancellationToken cancellationToken);
        Task<BlobResponse> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
