using Documents.Domain.DTOs.Photos;
using Documents.Domain.Interfaces.Repositories;
using Events;
using MassTransit;

namespace Documents.Application.Consumer.Events.Photos
{
	public class PhotoAddedConsumer : IConsumer<PhotoAdded>
	{
		private readonly IPhotoRepository _photoRepository;

		public PhotoAddedConsumer(IPhotoRepository photoRepository)
		{
			_photoRepository = photoRepository;
		}

		public async Task Consume(ConsumeContext<PhotoAdded> context)
		{
			if (context.Message is not null)
			{
				var photoDto = new PhotoDto
				{
					PhotoData = context.Message.PhotoData,
					PhotoName = context.Message.PhotoName
				};
				var result = await _photoRepository.UploadAsync(photoDto, default(CancellationToken));
				await context.RespondAsync(new
				{
					Id = result.Blob.Id,
					PhotoUrl = result.Blob.Uri,
					PhotoName = result.Blob.Name
				});
			}

		}
	}
}
