using Documents.Application.Services;
using Documents.Domain.Interfaces.Services;
using Documents.Persistence;
using Documents.WebApi.Extensions;
using Profiles.WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConnectDatabase(builder.Configuration.GetConnectionString("DocumentsConnection"));

services.AddScoped<IDocumentService, DocumentService>();
services.AddScoped<IPhotoService, PhotoService>();

services.AddAutoMapper(typeof(MappingProfile));

services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{   
    endpoints.MapControllers();
});

app.ConfigureExceptionHandler();

app.Run();
