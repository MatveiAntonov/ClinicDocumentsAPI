using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure;
using Documents.Domain.Entities.EntitiesContentData;
using Documents.Domain.Entities.EntitiesLocationData;
using Documents.Domain.Interfaces.Repositories;
using Documents.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Documents.Domain.DTOs.Photos;

namespace Documents.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DocumentsDbContext _documentsDbContext;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public PhotoRepository(IConfiguration configuration, DocumentsDbContext documentsDbContext)
        {
            _storageConnectionString = configuration.GetConnectionString("AzureBlobStorage");
            _storageContainerName = configuration["BlobPhotosContainerName"];
            _documentsDbContext = documentsDbContext;
        }
        public async Task<IEnumerable<Blob>> ListAsync(CancellationToken cancellationToken)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            List<Blob> files = new List<Blob>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new Blob
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }

        public async Task<BlobResponse> UploadAsync(PhotoDto blob, CancellationToken cancellationToken)
        {
            BlobResponse response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                //BlobClient client = container.GetBlobClient(blob.PhotoName);
                //using MemoryStream data = new MemoryStream(blob.PhotoData);

                //await using (data)
                //{
                //    await client.UploadAsync(data);
                //}

                response.Status = $"File {blob.PhotoName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = "Test Uri";
                response.Blob.Name = blob.PhotoName;

                var photo = new Photo
                {
                    Url = "Client Url",
                    Name = blob.PhotoName
                };

                _documentsDbContext.Photos.Add(photo);
                _documentsDbContext.SaveChanges();

                response.Blob.Id = photo.Id;
            }
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File with name {blob.PhotoName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            return response;
        }

        public async Task<Blob?> DownloadAsync(string blobFilename, CancellationToken cancellationToken)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobClient file = client.GetBlobClient(blobFilename);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFilename;
                string contentType = content.Value.Details.ContentType;

                return new Blob { Content = blobContent, Name = name, ContentType = contentType };
            }

            return null;
        }

        public async Task<BlobResponse> DeleteAsync(string blobFilename, CancellationToken cancellationToken)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobClient file = client.GetBlobClient(blobFilename);

            try
            {
                await file.DeleteAsync();

                var photo = await _documentsDbContext.Photos.FirstOrDefaultAsync(x => x.Name == blobFilename);

                if (photo is not null)
                {
                    _documentsDbContext.Photos.Remove(photo);
                    _documentsDbContext.SaveChanges();
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return new BlobResponse { Error = true, Status = $"File with name {blobFilename} not found." };
            }

            return new BlobResponse { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };
        }
    }
}
