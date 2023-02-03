using Documents.Domain.Entities;
using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Interfaces.Contexts;
using Documents.Domain.Interfaces.Repositories;
using Documents.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentsDbContext _documentsDbContext;

        public DocumentService(IDocumentRepository repository, IDocumentsDbContext documentsDbContext)
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
            var doc = await _documentsDbContext.Documents.FindAsync(id);

            if (doc is not null)
            {
                return await _documentRepository.DownloadAsync(doc.Name, cancellationToken);
            }
            return null;
        }

        public async Task<BlobResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var doc = await _documentsDbContext.Documents.FindAsync(id);

            if (doc is not null)
            {
                return await _documentRepository.DeleteAsync(doc.Name, cancellationToken);
            }
            return null;
        }

        public async Task<BlobResponse> UploadAsync(IFormFile file, int resultId, CancellationToken cancellationToken)
        {
            return await _documentRepository.UploadAsync(file, resultId, cancellationToken);
        }
    }
}
