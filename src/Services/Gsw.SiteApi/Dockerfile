FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["src/Services/Gsw.SiteApi/Gsw.SiteApi.csproj", "src/Services/Gsw.SiteApi/"]
COPY ["src/Infrastructure/Auth/Auth.csproj", "src/Infrastructure/Auth/"]
COPY ["src/Domain/GSW.Domain/GSW.Domain.csproj", "src/Domain/GSW.Domain/"]
COPY ["src/Infrastructure/CQRS/CQRS.csproj", "src/Infrastructure/CQRS/"]
COPY ["src/Infrastructure/DDD/DDD.csproj", "src/Infrastructure/DDD/"]
RUN dotnet restore "src/Services/Gsw.SiteApi/Gsw.SiteApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Gsw.SiteApi"
RUN dotnet build "Gsw.SiteApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Gsw.SiteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gsw.SiteApi.dll"]