using Microsoft.AspNetCore.Http;

namespace Events
{
	public class PhotoAdded
	{
		public string PhotoName { get; set; } = String.Empty;
		public byte[] PhotoData { get; set; }
	}
}
