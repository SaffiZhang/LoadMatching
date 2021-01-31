#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["LoadLink.LoadMatching.Api/LoadLink.LoadMatching.Api.csproj", "LoadLink.LoadMatching.Api/"]
RUN dotnet restore "LoadLink.LoadMatching.Api/LoadLink.LoadMatching.Api.csproj"
COPY . .
WORKDIR "/src/LoadLink.LoadMatching.Api"
RUN dotnet build "LoadLink.LoadMatching.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoadLink.LoadMatching.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoadLink.LoadMatching.Api.dll"]