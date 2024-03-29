#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebAPI/WebAPI.csproj", "src/WebAPI/"]
COPY ["src/WebAPI.Commands/WebAPI.Commands.csproj", "src/WebAPI.Commands/"]
COPY ["src/WebAPI.Services/WebAPI.Services.csproj", "src/WebAPI.Services/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/DataAccess.EF/DataAccess.EF.csproj", "src/DataAccess.EF/"]
COPY ["src/MediatR.Core/MediatR.Core.csproj", "src/MediatR.Core/"]
COPY ["src/WebAPI.Queries/WebAPI.Queries.csproj", "src/WebAPI.Queries/"]
RUN dotnet restore "src/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
