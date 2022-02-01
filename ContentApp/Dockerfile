#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContentApp/ContentApp.csproj", "ContentApp/"]
COPY ["ContentApp.Infrastructure/ContentApp.Infrastructure.csproj", "ContentApp.Infrastructure/"]
COPY ["ContentApp.Domain/ContentApp.Domain.csproj", "ContentApp.Domain/"]
COPY ["MyApp.Contracts/MyApp.Contracts.csproj", "MyApp.Contracts/"]
RUN dotnet restore "ContentApp/ContentApp.csproj"
COPY . .
WORKDIR "/src/ContentApp"
RUN dotnet build "ContentApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContentApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentApp.dll"]
