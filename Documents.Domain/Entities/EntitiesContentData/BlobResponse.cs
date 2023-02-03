using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Domain.Entities.EntitiesContentData
{
    public class BlobResponse
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public Blob? Blob { get; set; } = new();
    }
}
