using AutoMapper;
using Documents.Application.Services;
using Documents.Domain.Interfaces.Services;
using Documents.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Documents.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public PhotoController(IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlobDto>>> GetAllPhotos()
        {
            var blobs = await _photoService.ListAsync(default(CancellationToken));
            if (blobs is not null)
            {
                var blobsDto = new List<BlobDto>();
                foreach (var blob in blobs)
                {
                    blobsDto.Add(_mapper.Map<BlobDto>(blob));
                }
                return Ok(blobsDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlobDto>> GetPhoto(int id)
        {
            var blob = await _photoService.DownloadAsync(id, default(CancellationToken));
            if (blob is not null)
            {
                var blobDto = _mapper.Map<BlobDto>(blob);
                return File(blobDto.Content, blobDto.ContentType, blobDto.Name);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<BlobResponseDto>> CreatePhoto([FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest();

            var blobResponse = await _photoService.UploadAsync(file, default(CancellationToken));

            if (blobResponse.Error == false)
            {
                var blobResponseDto = _mapper.Map<BlobResponseDto>(blobResponse);
                return Created($"/document", blobResponseDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BlobResponseDto>> DeletePhoto(int id)
        {
            var blobResponse = await _photoService.DeleteAsync(id, default(CancellationToken));
            if (blobResponse.Error == false)
            {
                return Ok(blobResponse);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
