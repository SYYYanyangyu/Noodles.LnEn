FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MediaEncoder.WebAPI/MediaEncoder.WebAPI.csproj", "MediaEncoder.WebAPI/"]
COPY ["FileService.SDK.NETCore/FileService.SDK.NETCore.csproj", "FileService.SDK.NETCore/"]
COPY ["Noodles.Common/Noodles.Common.csproj", "Noodles.Common/"]
COPY ["Noodles.Jwt/Noodles.Jwt.csproj", "Noodles.Jwt/"]
COPY ["MediaEncoder.Domain/MediaEncoder.Domain.csproj", "MediaEncoder.Domain/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["Noodles.ASPNETCore/Noodles.ASPNETCore.csproj", "Noodles.ASPNETCore/"]
COPY ["Noodles.EventBus/Noodles.EventBus.csproj", "Noodles.EventBus/"]
COPY ["Noodles.Infrastructure/Noodles.Infrastructure.csproj", "Noodles.Infrastructure/"]
COPY ["Noodles.DomainCommons/Noodles.DomainCommons.csproj", "Noodles.DomainCommons/"]
COPY ["FileService.Infrastructure/FileService.Infrastructure.csproj", "FileService.Infrastructure/"]
COPY ["FileService.Domain/FileService.Domain.csproj", "FileService.Domain/"]
COPY ["MediaEncoder.Infrastructure/MediaEncoder.Infrastructure.csproj", "MediaEncoder.Infrastructure/"]
RUN dotnet restore "MediaEncoder.WebAPI/MediaEncoder.WebAPI.csproj"
COPY . .
WORKDIR "/src/MediaEncoder.WebAPI"
RUN dotnet build "MediaEncoder.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaEncoder.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaEncoder.WebAPI.dll"]
