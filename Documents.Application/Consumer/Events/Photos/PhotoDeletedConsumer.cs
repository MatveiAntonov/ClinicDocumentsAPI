using Documents.Domain.Interfaces.Repositories;
using Events;
using MassTransit;

namespace Documents.Application.Consumer.Events.Photos
{
	public class PhotoDeletedConsumer : IConsumer<PhotoDeleted>
	{
		private readonly IPhotoRepository _photoRepository;

		public PhotoDeletedConsumer(IPhotoRepository photoRepository)
		{
			_photoRepository = photoRepository;
		}

		public async Task Consume(ConsumeContext<PhotoDeleted> context)
		{
			if (context.Message is not null)
			{
				var result = await _photoRepository.DeleteAsync(context.Message.PhotoName, default(CancellationToken));
			}

		}
	}
}
