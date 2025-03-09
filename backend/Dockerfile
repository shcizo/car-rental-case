FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CarRental.Api/CarRental.Api.csproj", "src/CarRental.Api/"]
COPY ["src/CarRental.Infrastructure/CarRental.Infrastructure.csproj", "src/CarRental.Infrastructure/"]
COPY ["src/CarRental.Infrastructure.Sql/CarRental.Infrastructure.Sql.csproj", "src/CarRental.Infrastructure.Sql/"]
COPY ["src/CarRental.Core/CarRental.Core.csproj", "src/CarRental.Core/"]
COPY ["src/CarRental.SharedKernel/CarRental.SharedKernel.csproj", "src/CarRental.SharedKernel/"]
COPY ["src/CarRental.UseCase/CarRental.UseCase.csproj", "src/CarRental.UseCase/"]
RUN dotnet restore "src/CarRental.Api/CarRental.Api.csproj"
COPY . .
WORKDIR "/src/src/CarRental.Api"
RUN dotnet build "CarRental.Api.csproj" -restore:false -c $BUILD_CONFIGURATION -o /app/build 

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarRental.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.Api.dll"]
