﻿using AutoMapper;
using Documents.Domain.Interfaces.Services;
using Documents.WebApi.Models.DTOs;
using Documents.WebApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Documents.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        public DocumentController(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlobDto>>> GetAllDocuments()
        {
            var blobs = await _documentService.ListAsync(default(CancellationToken));
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
        public async Task<ActionResult<BlobDto>> GetDocument(int id)
        {
            var blob = await _documentService.DownloadAsync(id, default(CancellationToken));
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
        public async Task<ActionResult<BlobResponseDto>> CreateDocument([FromForm] FileViewModel model)
        {
            if (model.File == null)
                return BadRequest();

            var blobResponse = await _documentService.UploadAsync(model.File, model.ResultId, default(CancellationToken));

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
        public async Task<ActionResult<BlobResponseDto>> DeleteDocument(int id)
        {
            var blobResponse = await _documentService.DeleteAsync(id, default(CancellationToken));
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
