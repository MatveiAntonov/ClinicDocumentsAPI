using Documents.Application.Consumer.Events.Photos;
using Documents.Application.Consumer.Events.Results;
using Documents.Application.Services;
using Documents.Domain.Interfaces.Services;
using Documents.Persistence;
using Documents.WebApi.Extensions;
using MassTransit;
using Profiles.WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConnectDatabase(builder.Configuration.GetConnectionString("DocumentsConnection"));

services.AddScoped<IDocumentService, DocumentService>();
services.AddScoped<IPhotoService, PhotoService>();

services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<PhotoAddedConsumer>();
	x.AddConsumer<PhotoDeletedConsumer>();

	x.AddConsumer<ResultCreatedConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("rabbit-mq", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});
		cfg.ConfigureEndpoints(context);
	});
});

services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{   
    endpoints.MapControllers();
});

app.ConfigureExceptionHandler();

app.Run();
