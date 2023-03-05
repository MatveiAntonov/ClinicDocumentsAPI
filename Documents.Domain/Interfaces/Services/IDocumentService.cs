using Documents.Domain.DTOs;
using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Entities.EntitiesLocationData;
using Microsoft.AspNetCore.Http;

namespace Documents.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken);
        Task<Blob?> DownloadAsync(int id, CancellationToken cancellationToken);
        Task<BlobResponse> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<BlobResponse> UploadAsync(ResultDto file, CancellationToken cancellationToken);
    }
}
