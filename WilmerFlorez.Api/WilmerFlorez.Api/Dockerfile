#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WilmerFlorez.Api/WilmerFlorez.Api.csproj", "WilmerFlorez.Api/"]
COPY ["WilmerFlorez.Logic/WilmerFlorez.Logic.csproj", "WilmerFlorez.Logic/"]
COPY ["WilmerFlorez.Persistence/WilmerFlorez.Persistence.csproj", "WilmerFlorez.Persistence/"]
COPY ["WilmerFlorez.Database/WilmerFlorez.Database.csproj", "WilmerFlorez.Database/"]
COPY ["WilmerFlorez.Entities/WilmerFlorez.Entities.csproj", "WilmerFlorez.Entities/"]
COPY ["WilmerFlorez.Common/WilmerFlorez.Common.csproj", "WilmerFlorez.Common/"]
COPY ["WilmerFlorez.Models/WilmerFlorez.Models.csproj", "WilmerFlorez.Models/"]
RUN dotnet restore "WilmerFlorez.Api/WilmerFlorez.Api.csproj"
COPY . .
WORKDIR "/src/WilmerFlorez.Api"
RUN dotnet build "WilmerFlorez.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WilmerFlorez.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WilmerFlorez.Api.dll"]
