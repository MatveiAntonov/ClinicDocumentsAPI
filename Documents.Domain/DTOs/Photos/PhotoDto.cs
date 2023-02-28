using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Domain.DTOs.Photos
{
	public class PhotoDto
	{
		public string PhotoName { get; set; } = String.Empty;
		public byte[] PhotoData { get; set; }
	}
}
