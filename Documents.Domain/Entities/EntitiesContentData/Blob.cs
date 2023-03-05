namespace Documents.Domain.Entities.EntitiesContentData
{
    public class Blob
    {
        public int Id { get; set; }
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}
