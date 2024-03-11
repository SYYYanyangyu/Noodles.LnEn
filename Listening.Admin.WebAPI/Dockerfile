FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Listening.Admin.WebAPI/Listening.Admin.WebAPI.csproj", "Listening.Admin.WebAPI/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["Noodles.ASPNETCore/Noodles.ASPNETCore.csproj", "Noodles.ASPNETCore/"]
COPY ["Noodles.Common/Noodles.Common.csproj", "Noodles.Common/"]
COPY ["Noodles.EventBus/Noodles.EventBus.csproj", "Noodles.EventBus/"]
COPY ["Noodles.Infrastructure/Noodles.Infrastructure.csproj", "Noodles.Infrastructure/"]
COPY ["Noodles.DomainCommons/Noodles.DomainCommons.csproj", "Noodles.DomainCommons/"]
COPY ["Noodles.Jwt/Noodles.Jwt.csproj", "Noodles.Jwt/"]
COPY ["Listening.Domain/Listening.Domain.csproj", "Listening.Domain/"]
COPY ["Listening.Infrastructure/Listening.Infrastructure.csproj", "Listening.Infrastructure/"]
COPY ["FileService.Domain/FileService.Domain.csproj", "FileService.Domain/"]
RUN dotnet restore "Listening.Admin.WebAPI/Listening.Admin.WebAPI.csproj"
COPY . .
WORKDIR "/src/Listening.Admin.WebAPI"
RUN dotnet build "Listening.Admin.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Listening.Admin.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Listening.Admin.WebAPI.dll"]
