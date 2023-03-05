namespace Documents.Domain.DTOs
{
    public class PhotoDto
    {
        public string PhotoName { get; set; } = string.Empty;
        public byte[] PhotoData { get; set; }
    }
}
