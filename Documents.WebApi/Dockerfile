#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Documents.WebApi/Documents.WebApi.csproj", "Documents.WebApi/"]
COPY ["Documents.Persistence/Documents.Persistence.csproj", "Documents.Persistence/"]
COPY ["Documents.Application/Documents.Application.csproj", "Documents.Application/"]
COPY ["Documents.Domain/Documents.Domain.csproj", "Documents.Domain/"]
RUN dotnet restore "Documents.WebApi/Documents.WebApi.csproj"
COPY . .
WORKDIR "/src/Documents.WebApi"
RUN dotnet build "Documents.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Documents.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:9009
ENTRYPOINT ["dotnet", "Documents.WebApi.dll"]