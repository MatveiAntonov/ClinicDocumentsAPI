namespace Events
{
    public class PhotoAddedResponse
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
    }
}
