using Documents.Domain.DTOs;
using Documents.Domain.Interfaces.Repositories;
using Events;
using MassTransit;

namespace Documents.Application.Consumer.Events.Results
{
	public class ResultCreatedConsumer : IConsumer<ResultCreated>
	{
		private readonly IDocumentRepository _documentRepository;

		public ResultCreatedConsumer(IDocumentRepository documentRepository)
		{
			_documentRepository = documentRepository;
		}

		public async Task Consume(ConsumeContext<ResultCreated> context)
		{
			if (context.Message is not null)
			{
				var documentDto = new ResultDto
				{
					Id = context.Message.Id,
					Document = context.Message.Document
				};
				await _documentRepository.UploadAsync(documentDto, default(CancellationToken));
			}

		}
	}
}
