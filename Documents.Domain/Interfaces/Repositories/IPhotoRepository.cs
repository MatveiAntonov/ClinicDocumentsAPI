using Documents.Domain.DTOs;
using Documents.Domain.Entities.EntitiesContentData;

namespace Documents.Domain.Interfaces.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken);
        Task<Blob?> DownloadAsync(string blobFileName, CancellationToken cancellationToken);
        Task<BlobResponse> DeleteAsync(string blobFileName, CancellationToken cancellationToken);
        Task<BlobResponse> UploadAsync(PhotoDto file, CancellationToken cancellationToken);
    }
}
