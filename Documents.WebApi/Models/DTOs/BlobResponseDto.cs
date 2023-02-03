using Documents.Domain.Entities.EntitiesContentData;

namespace Documents.WebApi.Models.DTOs
{
    public class BlobResponseDto
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobDto? Blob { get; set; } = new();
    }
}
